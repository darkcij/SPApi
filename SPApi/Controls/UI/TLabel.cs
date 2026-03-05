using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPApi.Commons;
using System.Runtime.InteropServices;

namespace SPApi.Controls.UI
{
    public partial class TLabel : Control
    {
        #region TEXT

        //Text Align
        private ContentAlignment _textAlign = ContentAlignment.TopLeft;
        [Browsable(true), Category("Extended-Text")]
        [DefaultValue("ContentAlignment.TopLeft")]
        public ContentAlignment TextAlign
        {
            get { return _textAlign; }
            set
            {
                _textAlign = value;
                PropertyChanged(true);
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                PropertyChanged(true);
            }
        }

        #endregion

        #region Border Properties

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
                PropertyChanged(true);
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
                PropertyChanged(true);
            }
        }

        //Border Style
        private CustomBorderStyle _borderStyle = CustomBorderStyle.None;
        [Browsable(true), Category("Extended-Border")]
        [DefaultValue("CustomBorderStyle.None")]
        public CustomBorderStyle BorderStyleEx
        {
            get { return _borderStyle; }
            set
            {
                _borderStyle = value;
                PropertyChanged(true);
            }
        }

        #endregion

        #region Image Properties

        //이미지 지정
        private Image _image;
        [Browsable(true), Category("Extended-Image")]
        public Image Picture
        {
            get { return _image; }
            set
            {
                _image = value;
                PropertyChanged(true);
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
                PropertyChanged(true);
            }
        }

        #endregion

        #region Disable Properties

        [Browsable(false)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        [Browsable(false)]
        public override ImageLayout BackgroundImageLayout
        {
            get
            {
                return base.BackgroundImageLayout;
            }
            set
            {
                base.BackgroundImageLayout = value;
            }
        }

        [Browsable(false)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        [Browsable(false)]
        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
            }
        }

        [Browsable(false)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
            }
        }

        #endregion

        #region Constructor

        public TLabel()
        {

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, false);

            InitializeComponent();

            PropertyChanged(false);
        }

        #endregion

        #region Events
        
        private void PropertyChanged(bool recreateHandle)
        {
            if (recreateHandle)
            {
                this.RecreateHandle();
            }
            Invalidate();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20;
                return cp;
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            PropertyChanged(true);
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            PropertyChanged(true);
        }
        
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;
            Rectangle clientRect = ClientRectangle;

            g.CompositingMode = CompositingMode.SourceOver;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            
            DrawBorder(ref g, clientRect);
            DrawControlText(ref g, clientRect);
            DrawControlImage(ref g, clientRect);
        }
        
        /// <summary>
        /// Call this from OnPaint() to Draw Control's Text
        /// </summary>
        /// <param name="g">Graphics Object Used to Paint</param>
        /// <remarks></remarks>
        protected void DrawControlText(ref Graphics g, Rectangle clientRect)
        {
            Rectangle textRect = GetTextRectangle(this.Text, this.Font, ref g, clientRect, this.TextAlign);

            using (SolidBrush fb = new SolidBrush(this.ForeColor))
            {
                g.DrawString(this.Text, this.Font, fb, textRect);
            }
        }

        /// <summary>
        /// Call this from OnPaint() to Draw Control's Image
        /// </summary>
        /// <param name="g">Graphics Object Used to Paint</param>
        /// <param name="_ClientRect">The Rectangle that represents the client area of the control.</param>
        /// <remarks></remarks>
        protected void DrawControlImage(ref Graphics g, Rectangle clientRect)
        {
            if (_image != null)
            {
                g.DrawImageUnscaled(_image, _imageLocation);
            }
        }

        /// <summary>
        /// Get(s) the Text Rectangle to draw String, Call this from OnPaint() to Draw Control's Text
        /// </summary>
        /// <param name="_Text">Gets or sets the Text of the control, Represents Text as a series of Unicode characters.</param>
        /// <param name="_Font">System.Drawing.Font, Defines a particular format for text, including font face, size, and style attributes. </param>
        /// <param name="g">Graphics Object Used to Paint</param>
        /// <param name="_ClientRect">The Rectangle that represents the client area of the control.</param>
        /// <param name="_TextAlign">Specifies alignment of text on the control.</param>
        /// <returns></returns>
        /// <remarks>Public Enum System.Drawing.ContentAlignment As Integer</remarks>
        protected Rectangle GetTextRectangle(string text, Font font, ref Graphics g, Rectangle clientRect, ContentAlignment textAlign)
        {
            Rectangle rtn = Rectangle.Empty;
            SizeF textSize = g.MeasureString(text, font);
            int textLeft;
            textSize.Width += 4;

            switch (textAlign)
            {
                case ContentAlignment.TopLeft:
                    return clientRect;
                case ContentAlignment.TopCenter:
                    textLeft = (clientRect.Width / 2) - (textSize.Width.ToInt() / 2);
                    rtn = new Rectangle((clientRect.Left + textLeft).ToInt(), clientRect.Top, textSize.Width.ToInt(), textSize.Height.ToInt());
                    break;
                case ContentAlignment.TopRight:
                    textLeft = Math.Truncate(clientRect.Width - textSize.Width).ToInt();
                    rtn = new Rectangle((clientRect.Left + textLeft), clientRect.Top, textSize.Width.ToInt(), textSize.Height.ToInt());
                    break;
                case ContentAlignment.MiddleLeft:
                    rtn = new Rectangle(clientRect.Left, (clientRect.Height / 2).ToInt() - (textSize.Height / 2).ToInt(),
                                    textSize.Width.ToInt(), textSize.Height.ToInt());
                    break;
                case ContentAlignment.MiddleCenter:
                    textLeft = (clientRect.Width / 2) - (textSize.Width.ToInt() / 2);
                    rtn = new Rectangle((clientRect.Left + textLeft), (clientRect.Height / 2).ToInt() - (textSize.Height / 2).ToInt(),
                                    textSize.Width.ToInt(), textSize.Height.ToInt());
                    break;
                case ContentAlignment.MiddleRight:
                    textLeft = (clientRect.Width - textSize.Width).ToInt();
                    rtn = new Rectangle((clientRect.Left + textLeft), (clientRect.Height / 2).ToInt() - (textSize.Height / 2).ToInt(),
                                    textSize.Width.ToInt(), textSize.Height.ToInt());
                    break;
                case ContentAlignment.BottomLeft:
                    rtn = new Rectangle(clientRect.Left, (clientRect.Bottom - textSize.Height).ToInt(),
                                    textSize.Width.ToInt(), textSize.Height.ToInt());
                    break;
                case ContentAlignment.BottomCenter:
                    textLeft = (clientRect.Width / 2) - (textSize.Width / 2).ToInt();
                    rtn = new Rectangle((clientRect.Left + textLeft), (clientRect.Bottom - textSize.Height).ToInt(),
                                    textSize.Width.ToInt(), textSize.Height.ToInt());
                    break;
                case ContentAlignment.BottomRight:
                    textLeft = (clientRect.Width - textSize.Width).ToInt();
                    rtn = new Rectangle((clientRect.Left + textLeft), (clientRect.Bottom - textSize.Height).ToInt(),
                                    textSize.Width.ToInt(), textSize.Height.ToInt());
                    break;
            }

            return rtn;
        }

        private void DrawBorder(ref Graphics g, Rectangle clientRect)
        {
            switch (_borderStyle)
            {
                case CustomBorderStyle.Dashed: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, clientRect, _borderColor, ButtonBorderStyle.Dashed);
                    break;
                case CustomBorderStyle.Dotted: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, clientRect, _borderColor, ButtonBorderStyle.Dotted);
                    break;
                case CustomBorderStyle.Inset: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, clientRect, _borderColor, ButtonBorderStyle.Inset);
                    break;
                case CustomBorderStyle.Outset: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, clientRect, _borderColor, ButtonBorderStyle.Outset);
                    break;
                case CustomBorderStyle.Solid: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, clientRect, _borderColor, ButtonBorderStyle.Solid);
                    break;
                case CustomBorderStyle.Adjust: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, clientRect, Border3DStyle.Adjust);
                    break;
                case CustomBorderStyle.Bump: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, clientRect, Border3DStyle.Bump);
                    break;
                case CustomBorderStyle.Etched: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, clientRect, Border3DStyle.Etched);
                    break;
                case CustomBorderStyle.Flat: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, clientRect, Border3DStyle.Flat);
                    break;
                case CustomBorderStyle.Raised: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, clientRect, Border3DStyle.Raised);
                    break;
                case CustomBorderStyle.RaisedInner: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, clientRect, Border3DStyle.RaisedInner);
                    break;
                case CustomBorderStyle.RaisedOuter: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, clientRect, Border3DStyle.RaisedOuter);
                    break;
                case CustomBorderStyle.Sunken: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, clientRect, Border3DStyle.Sunken);
                    break;
                case CustomBorderStyle.SunkenInner: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, clientRect, Border3DStyle.SunkenInner);
                    break;
                case CustomBorderStyle.SunkenOuter: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, clientRect, Border3DStyle.SunkenOuter);
                    break;
                case CustomBorderStyle.None:
                default:
                    //DO NOTHING
                    break;

            }
        }

        #endregion
    }
}
