using System;
using System.Windows.Forms;
using SPApi.Commons;

namespace SPApi.Controls.Dialog
{
    public partial class MsgBox : Form
    {
        //인스턴스
        private static MsgBox Instance;
        public static MsgBox GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new MsgBox();
                }
                return Instance;
            }
        }

        private DialogResult _result;

        public MsgBox()
        {
            Instance = this;

            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();
        }

        public void ShowMessage(string msg, string caption, bool autoHide, int hideTime = 3)
        {
            this.SimpleInvoke("Text", caption);
            lbl_Msg.SimpleInvoke("Text", msg);

            btn_Yes.SimpleInvoke("Visible", false);
            btn_No.SimpleInvoke("Visible", false);
            btn_Ok.SimpleInvoke("Visible", true);

            tmrHide.Interval = hideTime * 1000;
            tmrHide.Start();

            ShowDialog();
        }

        public DialogResult ShowConfirm(string msg, string caption, bool defYes)
        {
            this.SimpleInvoke("Text", caption);
            lbl_Msg.SimpleInvoke("Text", msg);

            btn_Yes.SimpleInvoke("Visible", true);
            btn_No.SimpleInvoke("Visible", true);
            btn_Ok.SimpleInvoke("Visible", false);

            if (defYes)
                btn_Yes.SimpleInvoke("TabIndex", 0);
            else
                btn_No.SimpleInvoke("TabIndex", 0);

            tmrHide.Start();

            ShowDialog();

            return _result;
        }

        private void btn_Yes_Click(object sender, EventArgs e)
        {
            _result = DialogResult.Yes;
            tmrHide.Stop();
            Hide();
        }

        private void btn_No_Click(object sender, EventArgs e)
        {
            _result = DialogResult.No;
            tmrHide.Stop();
            Hide();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            tmrHide.Stop();
            Hide();
        }

        private void tmrHide_Tick(object sender, EventArgs e)
        {
            if (btn_Ok.Visible)
                btn_Ok.PerformClick();
            else
                btn_No.PerformClick();
        }

        private void FormMsgBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Y: btn_Yes.PerformClick(); break;
                case Keys.N: btn_No.PerformClick(); break;
                case Keys.O: btn_Ok.PerformClick(); break;
            }
        }
    }
}
