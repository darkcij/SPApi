using System;
using System.Windows.Forms;

namespace SPApi.Controls.UI
{
    public partial class TenKey : UserControl
    {
        public delegate void DelKeyClick(string key);
        public event DelKeyClick OnKeyClick;

        public TenKey()
        {
            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();
        }

        private void keybutton_Click(object sender, EventArgs e)
        {
            if (OnKeyClick != null)
                OnKeyClick(((SButton)sender).Text);
        }

        public string GetBackSpaceValue(string s) 
        { 
            return s.Substring(0, Math.Max(0, s.Length - 1)); 
        }
    }
}
