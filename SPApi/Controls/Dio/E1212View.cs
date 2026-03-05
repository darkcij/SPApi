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
using SPApi.Controls.Log;
using SPApi.Declaration.Structs;

namespace SPApi.Controls.Dio
{
    public partial class E1212View : Form
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

        //DO 명령 전송 이벤트
        public delegate void DioManualCommand(DioType type, int index, bool value);
        public event DioManualCommand OnManualCommand;

        //IP
        private string _ipAddress = string.Empty;
        public string IP
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
                lbl_IP.SimpleInvoke("Text", _ipAddress);
            }
        }

        //PORT
        private string _port = string.Empty;
        public string PORT
        {
            get { return _port; }
            set
            {
                _port = value;
                lbl_Port.SimpleInvoke("Text", _port);
            }
        }

        //STATE
        private string _state = string.Empty;
        public string STATE
        {
            get { return _state; }
            set
            {
                _state = value;
                lbl_State.SimpleInvoke("Text", _state);
            }
        }

        #endregion

        #region Constructor
        
        public E1212View()
        {
            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();

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

        /// <summary>
        /// 폼 이동 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tilte_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                NativeMethods.ReleaseCapture();
                NativeMethods.SendMessage(Handle, NativeMethods.WM_NLBUTTONDOWN, NativeMethods.HT_CAPTION, (IntPtr)0);
            }
            base.OnMouseDown(e);
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

        /// <summary>
        /// X 버튼 클릭 이번트 체러 (폼 숨기기)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHide_Click(object sender, EventArgs e)
        {
            Hide();
        }

        /// <summary>
        /// 접점 버튼 클릭 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DO_Contacts_Click(object sender, EventArgs e)
        {
            if (!STATE.Equals("Connected")) return;
            
            Button btn = (Button)sender;

            DioType type = DioType.DO;
            int index = btn.Name.Right(1).ToInt();
            bool value = CheckValue(btn.BackColor);

            if (OnManualCommand != null)
            {
                OnManualCommand(type, index, value);
            }
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
                    lvi.SubItems.Add(message.Datetime.ChkNull());
                    lvi.SubItems.Add(message.IO_Type.ChkNull());
                    lvi.SubItems.Add(message.Content.ChkNull());
                    lvi.ImageIndex = message.State.ChkNull().Equals("TX") ? 2 : 1;
                    lstLog.Items.Insert(0, lvi);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Fail, "<diolog> \n\r {0}", ex);
            }
            finally
            {
                lstLog.EndUpdate();

                lbl_MessageWaitCount.Text = _messageQueue.Count.ToString();
            }
        }
        #endregion

        #region Method

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

        /// <summary>
        /// DIO 상태 변경
        /// </summary>
        public void ChangeDIO(DioType type, int index, bool value)
        {
            StringBuilder name = new StringBuilder("btn_");
            name.Append(type.ToString());
            name.Append(index.ToString());

            Button btn = (Button)gb_DioState.Controls[name.ToString()];
            btn.SimpleInvoke("BackColor", CheckColor(value));
        }

        public void ClearDIO()
        {
            for (int i = 0; i < 8; i++)
            {
                ChangeDIO(DioType.DI, i, false);
                ChangeDIO(DioType.DO, i, false);
            }
        }
        
        /// <summary>
        /// Color 확인
        /// TRUE = LimeGreen
        /// FALSE = Red
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Color CheckColor(bool value)
        {
            return value ? Color.LimeGreen : Color.Red;
        }

        /// <summary>
        /// Bool 확인
        /// LimeGreen = TRUE
        /// Red = FALSE
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private bool CheckValue(Color color)
        {
            return color.Equals(Color.Red) ? true : false;
        }
        
        #endregion
        
    }
}
