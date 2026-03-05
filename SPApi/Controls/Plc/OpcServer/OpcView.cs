using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPApi.Commons;

namespace SPApi.Controls.Plc.OpcServer
{
    public partial class OpcView : Form
    {
        #region Members

        private Opc _opc = null;

        private System.Timers.Timer _tmrRefresh;

        private bool _tmrRefreshCheck;

        private int _dbIndex = 0;

        #endregion

        #region Properties

        public string Title
        {
            set { lbl_Tilte.SimpleInvoke("Text", string.Format("{0} Viewer", value)); }
        }

        public Opc OpcServer
        {
            set { _opc = value; }
        }
        
        #endregion

        #region Form

        public OpcView()
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
        
        #endregion

        #region Form Events

        protected override void OnLoad(EventArgs e)
        {
            //기본 설정
            DefaultSetting();

            base.OnLoad(e);
        }

        protected override void OnShown(EventArgs e)
        {
            //프로세스 시작
            ProcessStart();

            base.OnShown(e);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                _opc = null;
                
                _tmrRefreshCheck = false;
                _tmrRefresh.Stop();
                _tmrRefresh.Dispose();
            }
            catch { }

            base.OnClosing(e);
        }

        #endregion

        #region Control Events

        private void btnHide_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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

        private void cb_Type_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cb_Type.InvokeToAction(delegate() { _dbIndex = cb_Type.SelectedIndex; });
        }

        #endregion

        #region Methods

        private void DefaultSetting()
        {
            dgv_Center.DoubleBuffered(true);

            SetComboBox();

            SetTimer();
        }

        private void ProcessStart()
        {
            //타이머 시작
            _tmrRefreshCheck = true;
            _tmrRefresh.Start();
        }

        private void SetTimer()
        {
            _tmrRefreshCheck = false;

            _tmrRefresh = new System.Timers.Timer(1000);
            _tmrRefresh.AutoReset = false;
            _tmrRefresh.Elapsed += tmrRefresh_Elapsed;
            _tmrRefresh.Stop();
        }

        private void SetComboBox()
        {
            if (_opc.Groups == null) return;

            if (_opc.Groups.Length < 1) return;

            for (int i = 0; i < _opc.Groups.Length; i++)
            {
                cb_Type.Items.Add(_opc.Groups[i].Name);
            }

            cb_Type.SelectedIndex = 0;
        }

        #endregion

        #region Timer Events

        private void tmrRefresh_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                dgv_Center.SimpleInvoke("DataSource", _opc.Groups[_dbIndex].Items);
                dgv_Center.InvokeToAction(delegate() { dgv_Center.Refresh(); });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (_tmrRefreshCheck)
                    _tmrRefresh.Start();
            }
        }

        #endregion

       
    }
}
