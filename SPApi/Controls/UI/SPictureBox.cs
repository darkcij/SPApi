using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SPApi.Commons;

namespace SPApi.Controls.UI
{
    public partial class SPictureBox : PictureBox
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

        #region Constructor

        public SPictureBox()
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

            Pen p = null;
            SolidBrush sb = null;

            System.Diagnostics.Debug.WriteLine(_hoverEnable.ToString() + " / " + _hoverCheck.ToString());

            try
            {
                if (_hoverEnable && _hoverCheck)
                {
                    using (sb = new SolidBrush(_hoverColor))
                        using (p = new Pen(sb, _hoverWidth))
                            pe.Graphics.DrawRectangle(p, pe.ClipRectangle.X,
                                                         pe.ClipRectangle.Y,
                                                         pe.ClipRectangle.Width - 1,
                                                         pe.ClipRectangle.Height - 1);
                }
            }                                    
            catch { }                            
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            //Border
            if(_borderHoverStyle != CustomBorderStyle.None && _hoverCheck)
                DrawBorder(e.Graphics, _borderHoverStyle);
            else
                DrawBorder(e.Graphics, _borderStyle);

            
        }

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

        #endregion

        #region Methods

        /// <summary>
        /// 이미지의 특정 색을 변경
        /// </summary>
        /// <param name="toReplace">타겟 색</param>
        /// <param name="Replacement">변경 색</param>
        public void ChangeImageColor(Color toReplace, Color Replacement)
        {
            if(Image != null)
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
