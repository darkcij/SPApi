using System;
using System.Drawing;
using System.Windows.Forms;
using SPApi.Commons;
using System.ComponentModel;
using System.Collections.Generic;
using SPApi.Controls.Log;
using SPApi.Declaration.Structs;

namespace SPApi.Controls.Sockets
{
    public partial class SocketView : Form
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

        /// <summary>
        /// 상세뷰어
        /// </summary>
        private DataView _viewer = null;

        #endregion

        #region Properties

        public string Title
        {
            get { return lbl_Tilte.Text; }
            set { lbl_Tilte.SimpleInvoke("Text", value); }
        }
        
        [Browsable(false)]
        public ListView LogCtl
        {
            get { return lstLog; }
        }

        #endregion

        #region Constructor

        public SocketView()
        {
            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            
            //컴포넌트 초기화
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

        private void btnHide_Click(object sender, EventArgs e)
        {
            Hide();
            lstLog.Items.Clear();
        }
        
        private void lstLog_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstLog.SelectedIndices.Count == 0) return;
            if (lstLog.SelectedIndices[0] < 0) return;

            ShowDataView(lstLog.SelectedItems[0]);
        }

        private void lstLog_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (lstLog.SelectedIndices.Count > 0)
                    {
                        ShowDataView(lstLog.Items[lstLog.SelectedIndices[0]]);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[Socket Viewer] lstLog_KeyDown 이벤트 처리중 에러발생, Desc:{0}", ex.Message);
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
                    lvi.SubItems.Add(message.LocalEndPoint.ChkNull());
                    lvi.SubItems.Add(message.RemoteEndPoint.ChkNull());
                    lvi.SubItems.Add(message.Content.ChkNull());
                    lvi.ImageIndex = message.State.ChkNull().Equals("TX") ? 2 : 1;
                    lstLog.Items.Insert(0, lvi);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Fail, "<socklog> \n\r {0}", ex);
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

            //통신패킷 상세조회 팝업폼 할당
            _viewer = new DataView();

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
        
        private void ShowDataView(ListViewItem lvi)
        {
            _viewer.SetDataInfo(imgTrans.Images[lvi.ImageIndex], lvi.SubItems[1].Text, lvi.SubItems[2].Text, lvi.SubItems[3].Text, lvi.SubItems[4].Text);
            _viewer.ShowDialog();
        }

        #endregion
    }
}
