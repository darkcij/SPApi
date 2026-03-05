using System;
using System.Drawing;
using System.Windows.Forms;
using SPApi.Commons;
using System.Collections.Generic;
using SPApi.Controls.Log;
using System.ComponentModel;
using SPApi.Declaration.Structs;

namespace SPApi.Controls.Serials
{
    public partial class SerialView : Form
    {
        #region Members
        
        /// <summary>
        /// 통신 메세지 저장 Queue
        /// </summary>
        private Queue<CommunicationMessage> _messageQueue = null;

        /// <summary>
        /// 화면 갱신 타이머
        /// </summary>
        private Timer _tmrDisplay = null;
        
        #endregion

        #region Properties

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

        public string Delimiter
        {
            get { return lblDelimiter.Text; }
            set { lblDelimiter.SimpleInvoke("Text", value); }
        }

        public string ReadTimeOut
        {
            get { return lblReadTimeOut.Text; }
            set { lblReadTimeOut.SimpleInvoke("Text", value); }
        }

        public string WriteTimeOut
        {
            get { return lblWriteTimeOut.Text; }
            set { lblWriteTimeOut.SimpleInvoke("Text", value); }
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

        #region Constructor

        public SerialView()
        {
            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //컴포넌트 초기화
            InitializeComponent();

            //기본 설정
            DefaultSetting();
        }

        #endregion

        #region Form Events

        /// <summary>
        /// 폼 로드 이벤트 구현
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// 폼 종료중 이벤트 구현
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                if (_tmrDisplay != null)
                {
                    _tmrDisplay.Stop();
                    _tmrDisplay.Dispose();
                }
            }
            catch { }

            base.OnClosing(e);
        }

        #endregion

        #region Control Events

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

        #endregion

        #region Timer Events
        /// <summary>
        /// 화면 갱신 타이머 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrDisplay_Tick(object sender, EventArgs e)
        {
            if (_messageQueue == null) return;
            if (_messageQueue.Count.Equals(0)) return;

            CommunicationMessage message;

            try
            {
                lstLog.BeginUpdate();

                if (lstLog.Items.Count > 1000)
                {
                    lstLog.Items.Clear();
                    return;
                }

                for (int i = 0; i < 100; i++)
                {
                    if (_messageQueue.Count.Equals(0)) break;

                    message = _messageQueue.Dequeue();

                    if (message.Datetime.NoE()) continue;

                    ListViewItem lvi = new ListViewItem();
                    lvi.SubItems.Add(message.Datetime.ChkNull());;
                    lvi.SubItems.Add(message.Content.ChkNull());
                    lvi.ImageIndex = message.State.ChkNull().Equals("TX") ? 2 : 1;
                    lstLog.Items.Insert(0, lvi);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Fail, "<seriallog> \n\r {0}", ex);
            }
            finally
            {
                lstLog.EndUpdate();

                lbl_MessageWaitCount.Text = _messageQueue.Count.ToString();
            }
        }
        #endregion

        #region Methods

        private void DefaultSetting()
        {
            //디자인모드 Skip
            if (DesignMode) return;

            lstLog.DoubleBuffered(true);

            //통신메세지 Queue 초기화
            _messageQueue = new Queue<CommunicationMessage>();
            
            //화면갱신 타이머 초기화
            _tmrDisplay = new Timer();
            _tmrDisplay.Interval = 500;
            _tmrDisplay.Tick += TmrDisplay_Tick;
            _tmrDisplay.Start();
        }

        public void CommunicationLog(CommunicationMessage msg)
        {
            _messageQueue.Enqueue(msg);
        }
        
        #endregion

    }
}
