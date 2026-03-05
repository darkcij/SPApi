using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Security.Permissions;

namespace SPApi.Controls.UI
{
    [Flags]
    public enum Corners
    {
        None = 0,
        TopLeft = 1,
        TopRight = 2,
        BottomLeft = 4,
        BottomRight = 8,
        All = TopLeft | TopRight | BottomLeft | BottomRight
    }


    public enum CustomButtonState
    {
        Normal = 1,
        Hot,
        Pressed,
        Disabled,
        Focused
    }

    public partial class SButton : Control, IButtonControl
    {
        public SButton(): base()
        {
            SetStyle(ControlStyles.Selectable | 
                     ControlStyles.StandardClick | 
                     ControlStyles.ResizeRedraw | 
                     ControlStyles.AllPaintingInWmPaint | 
                     ControlStyles.DoubleBuffer | 
                     ControlStyles.UserPaint | 
                     ControlStyles.SupportsTransparentBackColor
                     , true);
        }

        #region Private Instance Variables

        private DialogResult m_DialogResult;
        private bool m_IsDefault;

        private int m_CornerRadius = 8;
        private Corners m_RoundCorners;
        private CustomButtonState m_ButtonState = CustomButtonState.Normal;

        private ContentAlignment m_ImageAlign = ContentAlignment.MiddleCenter;
        private ContentAlignment m_TextAlign = ContentAlignment.MiddleCenter;
        private ImageList m_ImageList;
        private int m_ImageIndex = -1;

        private bool keyPressed;
        private Rectangle contentRect;

        #endregion

        #region IButtonControl Implementation

        [Category("Behavior"), DefaultValue(typeof(DialogResult), "None")]
        [Description("The dialog result produced in a modal form by clicking the button.")]
        public DialogResult DialogResult
        {
            get { return m_DialogResult; }
            set
            {
                if (Enum.IsDefined(typeof(DialogResult), value))
                    m_DialogResult = value;
            }
        }


        public void NotifyDefault(bool value)
        {
            if (m_IsDefault != value)
                m_IsDefault = value;
            Invalidate();
        }


        public void PerformClick()
        {
            if (CanSelect)
                base.OnClick(EventArgs.Empty);
        }


        #endregion

        #region Properties

        #region Border Properties

        //Border Color
        private Color _borderColor = Color.Black;
        [Browsable(true), Category("Border")]
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
        [Browsable(true), Category("Border")]
        [DefaultValue("CustomBorderStyle.None")]
        public CustomBorderStyle BorderStyle
        {
            get { return _borderStyle; }
            set
            {
                _borderStyle = value;
                Invalidate();
            }
        }

        #endregion

        //ButtonState
        [Browsable(false)]
        public CustomButtonState ButtonState
        {
            get { return m_ButtonState; }
        }


        //CornerRadius
        [Category("Appearance")]
        [DefaultValue(8)]
        [Description("Defines the radius of the controls RoundedCorners.")]
        public int CornerRadius
        {
            get { return m_CornerRadius; }
            set
            {
                if (m_CornerRadius == value)
                    return;
                m_CornerRadius = value;
                Invalidate();
            }
        }


        //DefaultSize
        protected override Size DefaultSize
        {
            get { return new Size(75, 23); }
        }


        //IsDefault
        [Browsable(false)]
        public bool IsDefault
        {
            get { return m_IsDefault; }
        }


        //ImageList
        [Category("Appearance"), DefaultValue(typeof(ImageList), null)]
        [Description("The image list to get the image to display in the face of the control.")]
        public ImageList ImageList
        {
            get { return m_ImageList; }
            set
            {
                m_ImageList = value;
                Invalidate();
            }
        }


        //ImageIndex
        [Category("Appearance"), DefaultValue(-1)]
        [Description("The index of the image in the image list to display in the face of the control.")]
        [TypeConverter(typeof(ImageIndexConverter))]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design", typeof(System.Drawing.Design.UITypeEditor))]
        public int ImageIndex
        {
            get { return m_ImageIndex; }
            set
            {
                m_ImageIndex = value;
                Invalidate();
            }
        }


        //ImageAlign
        [Category("Appearance"), DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        [Description("The alignment of the image that will be displayed in the face of the control.")]
        public ContentAlignment ImageAlign
        {
            get { return m_ImageAlign; }
            set
            {
                if (!Enum.IsDefined(typeof(ContentAlignment), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
                if (m_ImageAlign == value)
                    return;
                m_ImageAlign = value;
                Invalidate();
            }
        }


        //RoundCorners
        [Category("Appearance")]
        [DefaultValue(typeof(Corners), "None")]
        [Description("Gets/sets the corners of the control to round.")]
        [Editor(typeof(RoundCornersEditor), typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Corners RoundCorners
        {
            get { return m_RoundCorners; }
            set
            {
                if (m_RoundCorners == value)
                    return;
                m_RoundCorners = value;
                Invalidate();
            }
        }


        //TextAlign
        [Category("Appearance"), DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        [Description("The alignment of the text that will be displayed in the face of the control.")]
        public ContentAlignment TextAlign
        {
            get { return m_TextAlign; }
            set
            {
                if (!Enum.IsDefined(typeof(ContentAlignment), value))
                    throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
                if (m_TextAlign == value)
                    return;
                m_TextAlign = value;
                Invalidate();
            }
        }

        [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public override string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
            }
        }


        #endregion

        #region Overriden Methods

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space)
            {
                keyPressed = true;
                m_ButtonState = CustomButtonState.Pressed;
            }
            OnStateChange(EventArgs.Empty);
        }


        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Space)
            {
                if (ButtonState == CustomButtonState.Pressed)
                    PerformClick();
                keyPressed = false;
                m_ButtonState = CustomButtonState.Focused;
            }
            OnStateChange(EventArgs.Empty);
        }


        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!keyPressed)
                m_ButtonState = CustomButtonState.Hot;
            OnStateChange(EventArgs.Empty);
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!keyPressed)
                if (IsDefault)
                    m_ButtonState = CustomButtonState.Focused;
                else
                    m_ButtonState = CustomButtonState.Normal;
            OnStateChange(EventArgs.Empty);
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Focus();
                m_ButtonState = CustomButtonState.Pressed;
            }
            OnStateChange(EventArgs.Empty);
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_ButtonState = CustomButtonState.Focused;
            OnStateChange(EventArgs.Empty);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (new Rectangle(Point.Empty, Size).Contains(e.X, e.Y) && e.Button == MouseButtons.Left)
                m_ButtonState = CustomButtonState.Pressed;
            else
            {
                if (keyPressed)
                    return;
                m_ButtonState = CustomButtonState.Hot;
            }
            OnStateChange(EventArgs.Empty);
        }


        //protected override void OnGotFocus(EventArgs e)
        //{
        //    base.OnGotFocus(e);
        //    m_ButtonState = CustomButtonState.Focused;
        //    NotifyDefault(true);
        //}


        //protected override void OnLostFocus(EventArgs e)
        //{
        //    base.OnLostFocus(e);
        //    if (FindForm().Focused)
        //        NotifyDefault(false);
        //    m_ButtonState = CustomButtonState.Normal;
        //}


        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            if (Enabled)
                m_ButtonState = CustomButtonState.Normal;
            else
                m_ButtonState = CustomButtonState.Disabled;
            OnStateChange(EventArgs.Empty);
        }


        protected override void OnClick(EventArgs e)
        {
            //Click gets fired before MouseUp which is handy
            if (ButtonState == CustomButtonState.Pressed)
            {
                Focus();
                PerformClick();
            }
        }


        protected override void OnDoubleClick(EventArgs e)
        {
            if (ButtonState == CustomButtonState.Pressed)
            {
                Focus();
                PerformClick();
            }
        }


        protected override bool ProcessMnemonic(char charCode)
        {
            if (IsMnemonic(charCode, Text))
            {
                base.OnClick(EventArgs.Empty);
                return true;
            }
            return base.ProcessMnemonic(charCode);
        }


        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }


        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //Simulate Transparency
            GraphicsContainer g = pevent.Graphics.BeginContainer();

            Rectangle translateRect = Bounds;

            pevent.Graphics.TranslateTransform(-Left, -Top);

            PaintEventArgs pe = new PaintEventArgs(pevent.Graphics, translateRect);

            InvokePaintBackground(Parent, pe);

            InvokePaint(Parent, pe);

            pevent.Graphics.ResetTransform();
            pevent.Graphics.EndContainer(g);

            pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Color shadeColor, fillColor;
            Color darkColor = DarkenColor(BackColor, 35);
            Color lightColor = LightenColor(BackColor, 35);

            if (ButtonState == CustomButtonState.Hot)
            {
                fillColor = lightColor;
                shadeColor = darkColor;
            }
            else if (ButtonState == CustomButtonState.Pressed)
            {
                fillColor = BackColor;
                shadeColor = BackColor;
            }
            else
            {
                fillColor = BackColor;
                shadeColor = darkColor;
            }

            Rectangle r = ClientRectangle;
            GraphicsPath path = RoundRectangle(r, CornerRadius, RoundCorners);

            using (LinearGradientBrush paintBrush = new LinearGradientBrush(r, fillColor, shadeColor, LinearGradientMode.Vertical))
            {
                //We want a sharp change in the colors so define a Blend for the brush
                Blend b = new Blend();
                b.Positions = new float[] { 0, 0.45F, 0.55F, 1 };
                b.Factors = new float[] { 0, 0, 1, 1 };
                paintBrush.Blend = b;

                //Draw the Button Background
                pevent.Graphics.FillPath(paintBrush, path);
                paintBrush.Dispose();
            }

            //...and border
            DrawBorder(pe.Graphics, r);

            //Get the Rectangle to be used for Content
            bool inBounds = false;
            //We could use some Math to get this from the radius but I'm 
            //not great at Math so for the example this hack will suffice.
            while (!inBounds && r.Width >= 1 && r.Height >= 1)
            {
                inBounds = path.IsVisible(r.Left, r.Top) &&
                           path.IsVisible(r.Right, r.Top) &&
                           path.IsVisible(r.Left, r.Bottom) &&
                           path.IsVisible(r.Right, r.Bottom);
                r.Inflate(-1, -1);

            }

            contentRect = r;

        }


        protected override void OnPaint(PaintEventArgs e)
        {
            DrawImage(e.Graphics);
            DrawText(e.Graphics);
            DrawFocus(e.Graphics);
            base.OnPaint(e);
        }


        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            Invalidate();
        }


        protected override void OnParentBackgroundImageChanged(EventArgs e)
        {
            base.OnParentBackgroundImageChanged(e);
            Invalidate();
        }


        #endregion

        #region Internal Draw Methods

        private void DrawImage(Graphics g)
        {
            if (ImageList == null || ImageIndex == -1)
                return;
            if (ImageIndex < 0 || ImageIndex >= ImageList.Images.Count)
                return;

            Image _Image = ImageList.Images[ImageIndex];

            Point pt = Point.Empty;

            switch (ImageAlign)
            {
                case ContentAlignment.TopLeft:
                    pt.X = contentRect.Left;
                    pt.Y = contentRect.Top;
                    break;

                case ContentAlignment.TopCenter:
                    pt.X = (Width - _Image.Width) / 2;
                    pt.Y = contentRect.Top;
                    break;

                case ContentAlignment.TopRight:
                    pt.X = contentRect.Right - _Image.Width;
                    pt.Y = contentRect.Top;
                    break;

                case ContentAlignment.MiddleLeft:
                    pt.X = contentRect.Left;
                    pt.Y = (Height - _Image.Height) / 2;
                    break;

                case ContentAlignment.MiddleCenter:
                    pt.X = (Width - _Image.Width) / 2;
                    pt.Y = (Height - _Image.Height) / 2;
                    break;

                case ContentAlignment.MiddleRight:
                    pt.X = contentRect.Right - _Image.Width;
                    pt.Y = (Height - _Image.Height) / 2;
                    break;

                case ContentAlignment.BottomLeft:
                    pt.X = contentRect.Left;
                    pt.Y = contentRect.Bottom - _Image.Height;
                    break;

                case ContentAlignment.BottomCenter:
                    pt.X = (Width - _Image.Width) / 2;
                    pt.Y = contentRect.Bottom - _Image.Height;
                    break;

                case ContentAlignment.BottomRight:
                    pt.X = contentRect.Right - _Image.Width;
                    pt.Y = contentRect.Bottom - _Image.Height;
                    break;
            }

            if (ButtonState == CustomButtonState.Pressed)
                pt.Offset(1, 1);

            if (Enabled)
                ImageList.Draw(g, pt, ImageIndex);
            else
                ControlPaint.DrawImageDisabled(g, _Image, pt.X, pt.Y, BackColor);

        }


        private void DrawText(Graphics g)
        {
            SolidBrush TextBrush = new SolidBrush(ForeColor);

            RectangleF R = (RectangleF)contentRect;

            if (!Enabled)
                TextBrush.Color = SystemColors.GrayText;

            StringFormat sf = new StringFormat(StringFormatFlags.NoWrap | StringFormatFlags.NoClip);

            if (ShowKeyboardCues)
                sf.HotkeyPrefix = HotkeyPrefix.Show;
            else
                sf.HotkeyPrefix = HotkeyPrefix.Hide;

            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.TopCenter:
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.TopRight:
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Near;
                    break;

                case ContentAlignment.MiddleLeft:
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.MiddleCenter:
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.MiddleRight:
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Center;
                    break;

                case ContentAlignment.BottomLeft:
                    sf.Alignment = StringAlignment.Near;
                    sf.LineAlignment = StringAlignment.Far;
                    break;

                case ContentAlignment.BottomCenter:
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Far;
                    break;

                case ContentAlignment.BottomRight:
                    sf.Alignment = StringAlignment.Far;
                    sf.LineAlignment = StringAlignment.Far;
                    break;
            }

            if (ButtonState == CustomButtonState.Pressed)
                R.Offset(1, 1);

            if (Enabled)
                g.DrawString(Text, Font, TextBrush, R, sf);
            else
                ControlPaint.DrawStringDisabled(g, Text, Font, BackColor, R, sf);

        }

        private void DrawBorder(Graphics g, Rectangle r)
        {
            switch (_borderStyle)
            {
                case CustomBorderStyle.Dashed: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, r, _borderColor, ButtonBorderStyle.Dashed);
                    break;
                case CustomBorderStyle.Dotted: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, r, _borderColor, ButtonBorderStyle.Dotted);
                    break;
                case CustomBorderStyle.Inset: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, r, _borderColor, ButtonBorderStyle.Inset);
                    break;
                case CustomBorderStyle.Outset: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, r, _borderColor, ButtonBorderStyle.Outset);
                    break;
                case CustomBorderStyle.Solid: //from ButtonBorderStyle Enumeration
                    ControlPaint.DrawBorder(g, r, _borderColor, ButtonBorderStyle.Solid);
                    break;

                case CustomBorderStyle.Adjust: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, r, Border3DStyle.Adjust);
                    break;
                case CustomBorderStyle.Bump: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, r, Border3DStyle.Bump);
                    break;
                case CustomBorderStyle.Etched: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, r, Border3DStyle.Etched);
                    break;
                case CustomBorderStyle.Flat: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, r, Border3DStyle.Flat);
                    break;
                case CustomBorderStyle.Raised: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, r, Border3DStyle.Raised);
                    break;
                case CustomBorderStyle.RaisedInner: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, r, Border3DStyle.RaisedInner);
                    break;
                case CustomBorderStyle.RaisedOuter: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, r, Border3DStyle.RaisedOuter);
                    break;
                case CustomBorderStyle.Sunken: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, r, Border3DStyle.Sunken);
                    break;
                case CustomBorderStyle.SunkenInner: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, r, Border3DStyle.SunkenInner);
                    break;
                case CustomBorderStyle.SunkenOuter: //from Border3DStyle Enumeration
                    ControlPaint.DrawBorder3D(g, r, Border3DStyle.SunkenOuter);
                    break;
                case CustomBorderStyle.None:
                default:
                    break;
            }
        }


        private void DrawFocus(Graphics g)
        {
            Rectangle r = contentRect;
            r.Inflate(1, 1);
            if (Focused && ShowFocusCues && TabStop)
                ControlPaint.DrawFocusRectangle(g, r, ForeColor, BackColor);
        }


        #endregion

        #region Helper Methods

        private GraphicsPath RoundRectangle(Rectangle r, int radius, Corners corners)
        {
            //Make sure the Path fits inside the rectangle
            r.Width -= 1;
            r.Height -= 1;

            //Scale the radius if it's too large to fit.
            if (radius > (r.Width))
                radius = r.Width;
            if (radius > (r.Height))
                radius = r.Height;

            GraphicsPath path = new GraphicsPath();

            if (radius <= 0)
                path.AddRectangle(r);
            else
                if ((corners & Corners.TopLeft) == Corners.TopLeft)
                path.AddArc(r.Left, r.Top, radius, radius, 180, 90);
            else
                path.AddLine(r.Left, r.Top, r.Left, r.Top);

            if ((corners & Corners.TopRight) == Corners.TopRight)
                path.AddArc(r.Right - radius, r.Top, radius, radius, 270, 90);
            else
                path.AddLine(r.Right, r.Top, r.Right, r.Top);

            if ((corners & Corners.BottomRight) == Corners.BottomRight)
                path.AddArc(r.Right - radius, r.Bottom - radius, radius, radius, 0, 90);
            else
                path.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);

            if ((corners & Corners.BottomLeft) == Corners.BottomLeft)
                path.AddArc(r.Left, r.Bottom - radius, radius, radius, 90, 90);
            else
                path.AddLine(r.Left, r.Bottom, r.Left, r.Bottom);

            path.CloseFigure();

            return path;
        }


        //The ControlPaint Class has methods to Lighten and Darken Colors, but they return a Solid Color.
        //The Following 2 methods return a modified color with original Alpha.
        private Color DarkenColor(Color colorIn, int percent)
        {
            //This method returns Black if you Darken by 100%

            if (percent < 0 || percent > 100)
                throw new ArgumentOutOfRangeException("percent");

            int a, r, g, b;

            a = colorIn.A;
            r = colorIn.R - (int)((colorIn.R / 200f) * percent);
            g = colorIn.G - (int)((colorIn.G / 200f) * percent);
            b = colorIn.B - (int)((colorIn.B / 200f) * percent);

            return Color.FromArgb(a, r, g, b);
        }


        private Color LightenColor(Color colorIn, int percent)
        {
            //This method returns White if you lighten by 100%

            if (percent < 0 || percent > 100)
                throw new ArgumentOutOfRangeException("percent");

            int a, r, g, b;

            a = colorIn.A;
            r = colorIn.R + (int)(((255f - colorIn.R) / 100f) * percent);
            g = colorIn.G + (int)(((255f - colorIn.G) / 100f) * percent);
            b = colorIn.B + (int)(((255f - colorIn.B) / 100f) * percent);

            return Color.FromArgb(a, r, g, b);
        }


        #endregion

        private CustomButtonState currentState;
        private void OnStateChange(EventArgs e)
        {
            //Repaint the button only if the state has actually changed
            if (ButtonState == currentState)
                return;
            currentState = ButtonState;
            Invalidate();
        }


    }

    #region Custom TypeEditor for RoundCorners property

    [PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public class RoundCornersEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, Object value)
        {
            if (value.GetType() != typeof(Corners) || provider == null)
                return value;

            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                CheckedListBox lb = new CheckedListBox();
                lb.BorderStyle = BorderStyle.None;
                lb.CheckOnClick = true;

                lb.Items.Add("TopLeft", (((SButton)context.Instance).RoundCorners & Corners.TopLeft) == Corners.TopLeft);
                lb.Items.Add("TopRight", (((SButton)context.Instance).RoundCorners & Corners.TopRight) == Corners.TopRight);
                lb.Items.Add("BottomLeft", (((SButton)context.Instance).RoundCorners & Corners.BottomLeft) == Corners.BottomLeft);
                lb.Items.Add("BottomRight", (((SButton)context.Instance).RoundCorners & Corners.BottomRight) == Corners.BottomRight);
                lb.Items.Add("All", (((SButton)context.Instance).RoundCorners & Corners.All) == Corners.All);

                edSvc.DropDownControl(lb);
                Corners cornerFlags = Corners.None;
                foreach (object o in lb.CheckedItems)
                {
                    cornerFlags = cornerFlags | (Corners)Enum.Parse(typeof(Corners), o.ToString());
                }
                lb.Dispose();
                edSvc.CloseDropDown();
                return cornerFlags;
            }
            return value;
        }

    }

    #endregion
}
