using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SPApi.Controls.UI
{
    public partial class SLabel : Label
    {
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

        #region RoundCorner Properties

        //Round Radius
        private int _roundCornerRadius = 1;
        [Browsable(true), Category("Extended-RoundConner")]
        [DefaultValue(1)]
        public int RoundCornerRadius
        {
            get { return _roundCornerRadius; }
            set
            {
                _roundCornerRadius = Math.Abs(value);
                Invalidate();
            }
        }

        #endregion

        #region Shadow Properties

        //그림자 크기
        private int _shadowOffSet = 1;
        [Browsable(true), Category("Extended-Shadow")]
        [DefaultValue(1)]
        public int ShadowOffSet
        {
            get { return _shadowOffSet; }
            set
            {
                _shadowOffSet = Math.Abs(value);
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
        public Image Picture
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

        public SLabel()
        {
            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            
            InitializeComponent();
        }

        #endregion

        #region Events


        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            //Control Size가 1x1픽셀 보다 크면
            if (Width > 1 && Height > 1)
            {
                GraphicsPath graphPath, graphPathShadow;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                int tmpRoundCornerRadius = Math.Min(Math.Min(_roundCornerRadius, Width), Height);
                int tmpShadowOffSet = Math.Min(Math.Min(_shadowOffSet, Width - 2), Height - 2);

                Rectangle rect = new Rectangle(0, 0, Width - tmpShadowOffSet, Height - tmpShadowOffSet);
                Rectangle rectShadow = new Rectangle(tmpShadowOffSet, tmpShadowOffSet, Width - tmpShadowOffSet, Height - tmpShadowOffSet);


                //RoundCorner, Shadow
                graphPath = SGraphics.GetRoundPath(rect, tmpRoundCornerRadius);
                graphPathShadow = SGraphics.GetRoundPath(rectShadow, tmpRoundCornerRadius);

                if (tmpRoundCornerRadius > 0)
                {
                    using (PathGradientBrush gBrush = new PathGradientBrush(graphPathShadow))
                    {
                        gBrush.WrapMode = WrapMode.Clamp;
                        ColorBlend colorBlend = new ColorBlend(3);
                        colorBlend.Colors = new Color[]{Color.Transparent,
                                                        Color.FromArgb(180, Color.DimGray),
                                                        Color.FromArgb(180, Color.DimGray)};

                        colorBlend.Positions = new float[] { 0f, .1f, 1f };

                        gBrush.InterpolationColors = colorBlend;
                        e.Graphics.FillPath(gBrush, graphPathShadow);
                    }
                }

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
                else
                {
                    SolidBrush sbrush = new SolidBrush(BackColor);

                    e.Graphics.FillPath(sbrush, graphPath);

                    sbrush.Dispose();
                }

                //Border
                if (_borderVisible)
                    e.Graphics.DrawPath(new Pen(Color.FromArgb(180, _borderColor), _borderWidth), graphPath);
                else
                    DrawBorder(e.Graphics);

                //Image
                if (_imageEnabled)
                    if (_image != null)
                        e.Graphics.DrawImageUnscaled(_image, _imageLocation);

                graphPathShadow.Dispose();
                graphPath.Dispose();

            }
        }

        private void DrawBorder(Graphics g)
        {
            switch (_borderStyle)
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

        #endregion
    }
}
