using SPApi.Commons;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SPApi.Controls.UI
{
    public partial class SPanel : Panel
    {
        #region Mouse Properties

        private bool _hoverCheck = false;

        //Hover Color
        private Color _hoverColor = Color.DarkOrange;
        [Browsable(true), Category("Extended-Mouse")]
        [DefaultValue("Color.DarkOrange")]
        public Color HoverColor
        {
            get { return _hoverColor; }
            set { _hoverColor = value; }
        }

        //Hover Border Width
        private int _hoverWidth = 5;
        [Browsable(true), Category("Extended-Mouse")]
        [DefaultValue(5)]
        public int HoverWidth
        {
            get { return _hoverWidth; }
            set { _hoverWidth = value; }
        }

        //Hover Enable
        private bool _hoverEnable = false;
        [Browsable(true), Category("Extended-Mouse")]
        [DefaultValue("false")]
        public bool HoverEnable
        {
            get { return _hoverEnable; }
            set { _hoverEnable = value; }
        }

        #endregion

        #region Border Properties

        //Border Visible
        private bool _borderVisible = false;
        [Browsable(true), Category("Extended-Border")]
        [DefaultValue(false)]
        public bool BorderVisible
        {
            get { return _borderVisible; }
            set
            {
                _borderVisible = value;
                Invalidate();
            }
        }

        //Border Color
        private Color _borderColor = Color.Black;
        [Browsable(true), Category("Extended-Border")]
        [DefaultValue("Color.Black")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        //Border Size
        private int _borderWidth = 1;
        [Browsable(true), Category("Extended-Border")]
        [DefaultValue(1)]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                _borderWidth = value;
                Invalidate();
            }
        }

        //Border Style
        private CustomBorderStyle _borderHoverStyle = CustomBorderStyle.None;
        [Browsable(true), Category("Extended-Border")]
        [DefaultValue("CustomBorderStyle.None")]
        public CustomBorderStyle ExBorderHoverStyle
        {
            get { return _borderHoverStyle; }
            set { _borderHoverStyle = value; }
        }

        //Border Style
        private CustomBorderStyle _borderStyle = CustomBorderStyle.None;
        [Browsable(true), Category("Extended-Border")]
        [DefaultValue("CustomBorderStyle.None")]
        public CustomBorderStyle ExBorderStyle
        {
            get { return _borderStyle; }
            set
            {
                _borderStyle = value;
                Invalidate();
            }
        }

        #endregion

        #region Gradient Properties

        //Gradient Enabled
        private bool _gradientEnabled = false;
        [Browsable(true), Category("Extended-Gradient")]
        [DefaultValue(false)]
        public bool GradientEnabled
        {
            get { return _gradientEnabled; }
            set
            {
                _gradientEnabled = value;
                Invalidate();
            }
        }

        //Gradient 방향
        private LinearGradientMode _gradientMode = LinearGradientMode.BackwardDiagonal;
        [Browsable(true), Category("Extended-Gradient")]
        [DefaultValue("LinearGradientMode.BackwardDiagonal")]
        public LinearGradientMode GradientMode
        {
            get { return _gradientMode; }
            set
            {
                _gradientMode = value;
                Invalidate();
            }
        }

        //Gradient StartColor
        private Color _gradientStartColor = Color.White;
        [Browsable(true), Category("Extended-Gradient")]
        [DefaultValue("Color.White")]
        public Color GradientStartColor
        {
            get { return _gradientStartColor; }
            set
            {
                _gradientStartColor = value;
                Invalidate();
            }
        }

        //Gradient EndColor
        private Color _gradientEndColor = Color.Gray;
        [Browsable(true), Category("Extended-Gradient")]
        [DefaultValue("Color.Gray")]
        public Color GradientEndColor
        {
            get { return _gradientEndColor; }
            set
            {
                _gradientEndColor = value;
                Invalidate();
            }
        }

        #endregion

        #region Image Properties

        //이미지 사용
        private bool _imageEnabled = false;
        [Browsable(true), Category("Extended-Image")]
        public bool ImageEnabled
        {
            get { return _imageEnabled; }
            set
            {
                _imageEnabled = value;
                Invalidate();
            }
        }

        //이미지 지정
        private Image _image;
        [Browsable(true), Category("Extended-Image")]
        public Image Image
        {
            get { return _image; }
            set
            {
                _image = value;
                Invalidate();
            }
        }

        //이미지 위치
        private Point _imageLocation = new Point(0, 0);
        [Browsable(true), Category("Extended-Image")]
        [DefaultValue("0,0")]
        public Point ImageLocation
        {
            get { return _imageLocation; }
            set
            {
                _imageLocation = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructor

        public SPanel()
        {
            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();
        }

        #endregion

        #region Events


        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);

            _hoverCheck = true;

            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            _hoverCheck = false;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            //Image
            if (_imageEnabled)
                if (_image != null)
                    pe.Graphics.DrawImage(_image, _imageLocation);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            
            GraphicsPath graphPath;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
                
            //RoundCorner, Shadow
            graphPath = SGraphics.GetRoundPath(rect, 0);

            //Gradient
            if (_gradientEnabled)
            {
                LinearGradientBrush brush = new LinearGradientBrush(rect,
                                                                    _gradientStartColor,
                                                                    _gradientEndColor,
                                                                    _gradientMode);
                e.Graphics.FillPath(brush, graphPath);

                brush.Dispose();
            }
            
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            
            if (_hoverEnable && _hoverCheck)
            {
                using (SolidBrush sb = new SolidBrush(_hoverColor))
                using (Pen p = new Pen(sb, _hoverWidth))
                    e.Graphics.DrawRectangle(p, e.ClipRectangle.X,
                                                e.ClipRectangle.Y,
                                                e.ClipRectangle.Width,
                                                e.ClipRectangle.Height);
            }

            //Border
            if (_borderHoverStyle != CustomBorderStyle.None && _hoverCheck)
                DrawBorder(e.Graphics, _borderHoverStyle);
            else if(!_borderVisible)
                DrawBorder(e.Graphics, _borderStyle);
            else
                e.Graphics.DrawPath(new Pen(Color.FromArgb(180, _borderColor), _borderWidth), graphPath);

            graphPath.Dispose();
        }

        #endregion

        #region Methods

        private void DrawBorder(Graphics g, CustomBorderStyle b)
        {
            switch (b)
            {
                case CustomBorderStyle.Dashed: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, ClientRectangle, _borderColor, ButtonBorderStyle.Dashed);
                    break;
                case CustomBorderStyle.Dotted: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, ClientRectangle, _borderColor, ButtonBorderStyle.Dotted);
                    break;
                case CustomBorderStyle.Inset: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, ClientRectangle, _borderColor, ButtonBorderStyle.Inset);
                    break;
                case CustomBorderStyle.Outset: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, ClientRectangle, _borderColor, ButtonBorderStyle.Outset);
                    break;
                case CustomBorderStyle.Solid: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, ClientRectangle, _borderColor, ButtonBorderStyle.Solid);
                    break;

                case CustomBorderStyle.Adjust: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.Adjust);
                    break;
                case CustomBorderStyle.Bump: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.Bump);
                    break;
                case CustomBorderStyle.Etched: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.Etched);
                    break;
                case CustomBorderStyle.Flat: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.Flat);
                    break;
                case CustomBorderStyle.Raised: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.Raised);
                    break;
                case CustomBorderStyle.RaisedInner: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.RaisedInner);
                    break;
                case CustomBorderStyle.RaisedOuter: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.RaisedOuter);
                    break;
                case CustomBorderStyle.Sunken: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.Sunken);
                    break;
                case CustomBorderStyle.SunkenInner: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.SunkenInner);
                    break;
                case CustomBorderStyle.SunkenOuter: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.SunkenOuter);
                    break;
                case CustomBorderStyle.None:
                default:
                    break;
            }
        }

        /// <summary>
        /// 이미지의 특정 색을 변경
        /// </summary>
        /// <param name="toReplace">타겟 색</param>
        /// <param name="Replacement">변경 색</param>
        public void ChangeImageColor(Color toReplace, Color Replacement)
        {
            if (Image != null)
                Image = Common.ReplaceColor2((Bitmap)Image, toReplace, Replacement);
        }

        /// <summary>
        /// 백그라운드 이미지의 특정 색을 변경
        /// </summary>
        /// <param name="toReplace">타겟 색</param>
        /// <param name="Replacement">변경 색</param>
        public void ChangeBackgroundImageColor(Color toReplace, Color Replacement)
        {
            if (BackgroundImage != null)
                BackgroundImage = Common.ReplaceColor2((Bitmap)BackgroundImage, toReplace, Replacement);
        }

        #endregion
    }
}
