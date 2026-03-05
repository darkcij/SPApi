using System;
using System.ComponentModel;
using System.Windows.Forms;
using SPApi.Commons;
using SPApi.Controls.Log;
using System.Drawing;

namespace SPApi.Controls.UI
{
    public partial class LogBox : UserControl
    {
        public delegate void DelLogBoxVisibleChanged(bool visible);
        public event DelLogBoxVisibleChanged OnVisibleChange;

        public delegate void DelLogBoxHeightChanged(bool visible, int height, int top);
        public event DelLogBoxHeightChanged OnHeightChanged;

        public enum LogBoxViewType
        {
            NORMAL = 0,
            LOW = 1,
            HIGH = 2
        }

        #region Members

        private const int LOG_COUNT = 10;
        private TabPage[] _pages = new TabPage[LOG_COUNT];
        private bool[] _enabled = new bool[LOG_COUNT];
        //private int _height = 0;
        private int _default_height = 0;
        private int _extend_Height = 400;
        private Color _logBackColor = Color.White;
        private Color _logForeColor = Color.Black;
        private LogBoxViewType _viewMode = LogBoxViewType.NORMAL;

        private string _filter = string.Empty;
        private string _dateFormat = "yyyy-MM-dd HH:mm:ss";

        #endregion

        #region Properties

        [Category("Control Height Option")]
        public int Extend_Height
        {
            get { return _extend_Height; }
            set { _extend_Height = value; }
        }

        [DisplayName("01.ALL"), RefreshProperties(RefreshProperties.All)]
        public bool All
        {
            get { return _enabled[0]; }
            set { _enabled[0] = value; ChangeTabEnabled(); }
        }

        [DisplayName("02.INFO"), RefreshProperties(RefreshProperties.All)]
        public bool INFO
        {
            get { return _enabled[1]; }
            set { _enabled[1] = value; ChangeTabEnabled(); }
        }

        [DisplayName("03.ERROR"), RefreshProperties(RefreshProperties.All)]
        public bool ERROR
        {
            get { return _enabled[2]; }
            set { _enabled[2] = value; ChangeTabEnabled(); }
        }

        [DisplayName("04.ULINK"), RefreshProperties(RefreshProperties.All)]
        public bool ULINK
        {
            get { return _enabled[3]; }
            set { _enabled[3] = value; ChangeTabEnabled(); }
        }

        [DisplayName("05.PLC"), RefreshProperties(RefreshProperties.All)]
        public bool PLC
        {
            get { return _enabled[4]; }
            set { _enabled[4] = value; ChangeTabEnabled(); }
        }

        [DisplayName("06.UPLOAD"), RefreshProperties(RefreshProperties.All)]
        public bool UPLOAD
        {
            get { return _enabled[5]; }
            set { _enabled[5] = value; ChangeTabEnabled(); }
        }

        [DisplayName("07.DOWNLOAD"), RefreshProperties(RefreshProperties.All)]
        public bool DOWNLOAD
        {
            get { return _enabled[6]; }
            set { _enabled[6] = value; ChangeTabEnabled(); }
        }

        [DisplayName("08.BACKUP"), RefreshProperties(RefreshProperties.All)]
        public bool BACKUP
        {
            get { return _enabled[7]; }
            set { _enabled[7] = value; ChangeTabEnabled(); }
        }

        [DisplayName("09.RFID"), RefreshProperties(RefreshProperties.All)]
        public bool RFID
        {
            get { return _enabled[8]; }
            set { _enabled[8] = value; ChangeTabEnabled(); }
        }

        [DisplayName("10.FAIL"), RefreshProperties(RefreshProperties.All)]
        public bool FAIL
        {
            get { return _enabled[9]; }
            set { _enabled[9] = value; ChangeTabEnabled(); }
        }

        [DisplayName("10.VisibleButton"), RefreshProperties(RefreshProperties.All)]
        public bool VisibeButton
        {
            get { return btn_Up.Visible; }
            set
            {
                btn_Down.Visible = value;
                btn_Up.Visible = value;
            }
        }

        [DisplayName("11.BackColor"), DefaultValue(typeof(Color), "White"), RefreshProperties(RefreshProperties.All)]
        public Color LogBackColor
        {
            get { return _logBackColor; }
            set
            {
                if(_logBackColor != value)
                {
                    for(int i=0; i< _pages.Length; i++)
                        _pages[i].Controls[0].BackColor = value;
                }
                _logBackColor = value;
            }
        }

        [DisplayName("12.ForeColor"), DefaultValue(typeof(Color), "Black"), RefreshProperties(RefreshProperties.All)]
        public Color LogForeColor
        {
            get { return _logForeColor; }
            set
            {
                if (_logForeColor != value)
                {
                    for (int i = 0; i < _pages.Length; i++)
                        _pages[i].Controls[0].ForeColor = value;
                }
                _logForeColor = value;
            }
        }

        [DisplayName("13.ViewMode"), DefaultValue(typeof(LogBoxViewType), "NORMAL"), RefreshProperties(RefreshProperties.All)]
        public LogBoxViewType ViewMode
        {
            get { return _viewMode; }
            set
            {
                _viewMode = value;

                switch (_viewMode)
                {
                    case LogBoxViewType.NORMAL:
                        tc_Logs.Visible = true;
                        Height = _default_height;
                        Top -= _default_height - 21;
                        break;

                    case LogBoxViewType.LOW:
                        tc_Logs.Visible = false;
                        Height = 21;
                        Top += _default_height - 21;
                        break;

                    case LogBoxViewType.HIGH:
                        tc_Logs.Visible = true;
                        Height = _extend_Height;
                        Top -= _extend_Height - _default_height;
                        break;
                }
            }
        }

        [Browsable(false)]
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
            }
        }

        [Browsable(false)]
        public string DateFormat
        {
            get { return _dateFormat; }
            set { _dateFormat = value; }
        }

        

        #endregion

        #region Constructor

        /// <summary>
        /// 생성자
        /// </summary>
        public LogBox()
        {
            InitializeComponent();

            DefaultSetting();
        }

        #endregion

        #region Events

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        
        #endregion

        #region Methods
        
        private void DefaultSetting()
        {
            tc_Logs.DoubleBuffered(true);

            TabPage page = null;
            for (int i = 0; i < tc_Logs.TabCount; i++)
            {
                page = tc_Logs.TabPages[i];
                page.DoubleBuffered(true);
                page.Controls[string.Format("lst_{0}",page.Text)].DoubleBuffered(true);
                _pages[i] = page;
            }
            
            ChangeTabEnabled();
        }

        public void SetLogMessage(LEVEL lvl, string method, string msg)
        {
            ListBox l = lst_ALL;
            int idx = lvl.ToInt();
            string dtm = DateTime.Now.ToString(_dateFormat);

            if (_enabled[0])
            {
                l.InvokeToAction(
                    delegate()
                    {
                        if (l.Items.Count > 1000)
                            l.Items.Clear();

                        l.Items.Insert(0, string.Format("[{0}] <{1}> {2} ",
                            dtm, method, msg));
                    });
            }

            if(_enabled[idx])
            {
                l = GetListBox(idx);

                //Filter 체크
                if (!_filter.NoE() && lvl != LEVEL.Error)
                {
                    if (!msg.Contains(_filter)) return;
                }

                l.InvokeToAction(
                    delegate ()
                    {
                        if (l.Items.Count > 1000)
                            l.Items.Clear();

                        l.Items.Insert(0, string.Format("[{0}] <{1}> {2} ",
                            dtm, method, msg));
                    });
            }
        }

        private void ChangeTabEnabled()
        {
            for (int i = 0; i < LOG_COUNT; i++)
            {
                if (_enabled[i])
                {
                    if (tc_Logs.TabPages.Contains(_pages[i]))
                        tc_Logs.TabPages.Remove(_pages[i]);

                    tc_Logs.TabPages.Add(_pages[i]);                    
                }
                else
                {
                    if (tc_Logs.TabPages.Contains(_pages[i]))
                        tc_Logs.TabPages.Remove(_pages[i]);
                }
            }

            Refresh();
        }
        
        private ListBox GetListBox(int idx)
        {
            ListBox tmp = null;

            switch(idx)
            {
                case 1: tmp = lst_INFO; break;
                case 2: tmp = lst_ERROR; break;
                case 3: tmp = lst_ULINK; break;
                case 4: tmp = lst_PLC; break;
                case 5: tmp = lst_UPLOAD; break;
                case 6: tmp = lst_DOWNLOAD; break;
                case 7: tmp = lst_BACKUP; break;
                case 8: tmp = lst_RFID; break;
                case 9: tmp = lst_FAIL; break;
            }

            return tmp;
        }
        
        #endregion
            
        private void btn_UpDown_Click(object sender, EventArgs e)
        {
            SButton btn = (SButton)sender;
            int h = 0;
            int t = 0;
            if (btn.Text.Equals("▼"))
            {
                if(Height.Equals(_extend_Height))
                {
                    h = _default_height;
                    Height = h;

                    t = _extend_Height - _default_height;
                    Top += t;
                }
                else if (Height.Equals(_default_height))
                {                    
                    tc_Logs.Visible = false;

                    h = 21;
                    Height = h;

                    t= _default_height - 21;
                    Top += t;
                }
            }
            else
            {
                if (!tc_Logs.Visible)
                {
                    tc_Logs.Visible = true;

                    h = _default_height; ;
                    Height = h;

                    t = _default_height - 21;
                    Top -= t;
                }
                else if(Height.Equals(_default_height))
                {
                    h = _extend_Height;
                    Height = h;

                    t = _extend_Height - _default_height;
                    Top -= t;
                }
            }

            this.BringToFront();
            
            if (OnVisibleChange != null)
                OnVisibleChange(tc_Logs.Visible);

            if (OnHeightChanged != null)
                OnHeightChanged(tc_Logs.Visible, h, t);

        }

        private void LogBox_Load(object sender, EventArgs e)
        {
            _default_height = Height;
        }
    }
}
