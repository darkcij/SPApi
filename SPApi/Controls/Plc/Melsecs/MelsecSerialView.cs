using System;
using System.Drawing;
using System.Windows.Forms;
using SPApi.Commons;

namespace SPApi.Controls.Plc.Melsecs
{
    public partial class MelsecSerialView : Form
    {
        #region Members

        private MelsecSerial _ms = null;

        private Timer tmrRefresh = null;
        
        #endregion

        #region Properties

        public MelsecSerial Serial
        {
            set
            {
                _ms = value;
                Title = string.Format("{0} Communication Viewer", _ms.Title);
            }
        }

        public string Title
        {
            get { return lbl_Tilte.Text; }
            set { lbl_Tilte.SimpleInvoke("Text", value); }
        }

        public string PortName
        {
            get { return lblPortname.Text; }
            set { lblPortname.SimpleInvoke("Text", value); }
        }

        public string BaudRate
        {
            get { return lblBaudrate.Text; }
            set { lblBaudrate.SimpleInvoke("Text", value); }
        }

        public string DataBit
        {
            get { return lblDatabit.Text; }
            set { lblDatabit.SimpleInvoke("Text", value); }
        }

        public string ParityBit
        {
            get { return lblParitybit.Text; }
            set { lblParitybit.SimpleInvoke("Text", value); }
        }

        public string StopBit
        {
            get { return lblStopbit.Text; }
            set { lblStopbit.SimpleInvoke("Text", value); }
        }
        
        public string ReadTimeOut
        {
            get { return lblReadTimeOut.Text; }
            set { lblReadTimeOut.SimpleInvoke("Text", value); }
        }

        public string ErrorCount
        {
            get { return lblErrorCount.Text; }
            set { lblErrorCount.SimpleInvoke("Text", value); }
        }
        
        public bool IsOpen
        {
            set
            {
                Color bc = value ? Color.Lime : Color.Red;
                string text = value ? "OPEN" : "CLOSE";

                lblState.SimpleInvoke("Text", text);
                lblState.SimpleInvoke("GradientEndColor", bc);
            }
        }
        
        #endregion

        #region Form

        public MelsecSerialView()
        {
            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //컴포넌트 초기화
            InitializeComponent();
        }

        private void SerialView_Load(object sender, EventArgs e)
        {
            //기본 설정
            DefaultSetting();
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            base.OnPaint(e);
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void btn_WinState_Click(object sender, EventArgs e)
        {
            switch (WindowState)
            {
                case FormWindowState.Normal:
                    btn_WinState.BackgroundImage = Properties.Resources.btn_WinScreen;
                    WindowState = FormWindowState.Maximized;
                    break;

                case FormWindowState.Maximized:
                    btn_WinState.BackgroundImage = Properties.Resources.btn_FullScreen;
                    WindowState = FormWindowState.Normal;
                    break;
            }
        }

        private void Top_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(Handle, NativeMethods.WM_NLBUTTONDOWN, NativeMethods.HT_CAPTION, (IntPtr)0);
            }
            base.OnMouseDown(e);
        }
        
        private void DefaultSetting()
        {
            RefreshTimerSetting();
        }

        private void RefreshTimerSetting()
        {
            tmrRefresh = new Timer();
            tmrRefresh.Interval = 1000;
            tmrRefresh.Tick += TmrRefresh_Tick;
        }

        private void TmrRefresh_Tick(object sender, EventArgs e)
        {
            try
            {
                DisplayUpdate();
            }
            catch
            {

            }
            finally
            {

            }
        }
        
        protected override void OnVisibleChanged(EventArgs e)
        {
            if(Visible)
            {
                tmrRefresh.Start();
            }
            else
            {
                if(tmrRefresh.Enabled) tmrRefresh.Stop();

                lstLog.InvokeToAction(delegate ()
                {
                    lstLog.Items.Clear();
                });
            }

            base.OnVisibleChanged(e);
        }

        private void DisplayUpdate()
        {
            PortName = _ms.PortName;
            BaudRate = _ms.Baudrate.ToString();
            DataBit = _ms.Databit.ToString();
            ParityBit = _ms.Paritybit.ToString();
            StopBit = _ms.Stopbit.ToString();
            ReadTimeOut = _ms.GetSendCount;
            ErrorCount = _ms.CommErrorCount.ToString();
            IsOpen = _ms.IsRun;   
        }

        public void CommunicationLog(string msg)
        {
            string dtm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ListViewItem lvi = new ListViewItem();
            lvi.SubItems.Add(dtm);
            lvi.SubItems.Add(msg);

            lstLog.SuspendLayout();

            lstLog.InvokeToAction(delegate (ListViewItem item)
            {
                if (lstLog.Items.Count > 1000) lstLog.Items.Clear();
                lstLog.Items.Insert(0, lvi);
            },
            lvi);

            lstLog.ResumeLayout();
        }

        public void CommunicationLog(string trans, string data)
        {
            string dtm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ListViewItem lvi = new ListViewItem(string.Empty, trans.Equals("TX") ? 2:1);
            lvi.SubItems.Add(dtm);
            lvi.SubItems.Add(data);

            lstLog.SuspendLayout();

            lstLog.InvokeToAction(delegate (ListViewItem item) 
            {
                if (lstLog.Items.Count > 1000) lstLog.Items.Clear();
                lstLog.Items.Insert(0, lvi);
            },
            lvi);

            lstLog.ResumeLayout();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            _ms.Start();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _ms.Stop();
        }
    }
}
