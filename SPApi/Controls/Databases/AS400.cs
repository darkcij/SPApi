using SPApi.Commons;
using SPApi.Controls.BackUp;
using SPApi.Controls.Log;
using SPApi.Controls.UI;
using SPApi.Declaration.Enums;
using SPApi.Declaration.Structs;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Windows.Forms;

namespace SPApi.Controls.Databases
{
    public partial class AS400: UserControl
    {
        #region Properties

        private const string _databaseName = "AS400";

        private static AS400 Instance;
        public static AS400 GetInstance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new AS400();
                }
                return Instance;
            }
        }

        private DB_MODE _runMode = DB_MODE.DATABASE;
        //[Browsable(false), Category("Extended-Set")]
        //public DB_MODE RunMode
        //{
        //    get { return _runMode; }
        //    set { _runMode = value; }
        //}
        
        private string _ip = "165.141.66.7";
        [Browsable(true), Category("Extended-Set")]
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }

        private string _port = "446";
        [Browsable(true), Category("Extended-Set")]
        public string PORT
        {
            get { return _port; }
            set { _port = value; }
        }

        private string _userID = "XCIM977";
        [Browsable(true), Category("Extended-Set")]
        [DefaultValue("")]
        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private string _userPW = "XCIM977";
        [Browsable(true), Category("Extended-Set")]
        [DefaultValue("")]
        public string UserPW
        {
            get { return _userPW; }
            set { _userPW = value; }
        }

        private bool _selecterState = false;
        [Browsable(false)]
        public bool SelecterState
        {
            get { return _selecterState; }
            set { _selecterState = value; }
        }

        private OdbcConnection _selecter = null;       
        
        private bool _uploaderState = false;
        [Browsable(false)]
        public bool UploaderState
        {
            get { return _uploaderState; }
            set { _uploaderState = value; }
        }

        private OdbcConnection _uploader = null;

        private SpBackUp _backup = null;

        private DatabaseView _selecterViewer = null;
        private DatabaseView _uploaderViewer = null;

        #endregion

        #region Constructor

        public AS400()
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

        #endregion

        #region Events

        protected override void OnLoad(EventArgs e)
        {
            DefaultSetting();

            base.OnLoad(e);
        }
        
        /// <summary>
        /// 라벨을 클릭하면 DetailView Show
        /// </summary>
        private void slblStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (ControlCommon.FormNullCheck(_selecterViewer))
                {
                    _selecterViewer = new DatabaseView();
                }
                else if (_selecterViewer.Visible)
                {
                    _selecterViewer.SendToBack();
                }

                _selecterViewer.Title = "Selecter Viewer";
                _selecterViewer.RunMode = _runMode.ToString();
                _selecterViewer.DatabaseName = _databaseName;
                _selecterViewer.SystemInfo = _ip;
                _selecterViewer.Uid = _userID;
                _selecterViewer.Show();
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Fail, "<selecter onclick> \n\r {0}", ex);
            }
        }

        private void slbl_Status2_Click(object sender, EventArgs e)
        {
            try
            {
                if (ControlCommon.FormNullCheck(_uploaderViewer))
                {
                    _uploaderViewer = new DatabaseView();
                }
                else if (_uploaderViewer.Visible)
                {
                    _uploaderViewer.SendToBack();
                }

                _uploaderViewer.Title = "Uploader Viewer";
                _uploaderViewer.RunMode = _runMode.ToString();
                _uploaderViewer.DatabaseName = _databaseName;
                _uploaderViewer.SystemInfo = _ip;
                _uploaderViewer.Uid = _userID;
                _uploaderViewer.Show();
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Fail, "<uploader onclick> \n\r {0}", ex);
            }
        }

        #endregion

        #region DATABASE OPEN / CLOSE

        public void DatabaseOpen(DB_CONNECTION_TYPE type)
        {
            GetConnection(type);
        }

        public void DatabaseClose(DB_CONNECTION_TYPE type)
        {
            OdbcConnection connection = GetDbType(type);

            try
            {
                if (connection != null)
                    connection.Close();

                if (type.Equals(DB_CONNECTION_TYPE.SELECTER))
                {
                    _selecterState = false;
                    _selecterViewer.IsOpen = _selecterState;
                }
                else if (type.Equals(DB_CONNECTION_TYPE.UPLOADER))
                {
                    _uploaderState = false;
                    _uploaderViewer.IsOpen = _uploaderState;
                }

                SetStatus(type, DeviceStateType.Disconnected);
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
        }

        private OdbcConnection GetDbType(DB_CONNECTION_TYPE type)
        {
            OdbcConnection connection = null;

            if (type.Equals(DB_CONNECTION_TYPE.UPLOADER))
                connection = _uploader;
            else
                connection = _selecter;

            return connection;
        }

        private OdbcConnection GetConnection(DB_CONNECTION_TYPE type)
        {
            OdbcConnection connection = null;
            try
            {
                connection = GetDbType(type);

                if (connection == null)
                {
                    connection = new OdbcConnection(GetConnectionString());
                }

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                    
                    if(type.Equals(DB_CONNECTION_TYPE.SELECTER))
                    {
                        if (_selecter == null) _selecter = connection;

                        _selecterState = true;
                        _selecterViewer.IsOpen = _selecterState;
                    }
                    else if(type.Equals(DB_CONNECTION_TYPE.UPLOADER))
                    {
                        if (_uploader == null) _uploader = connection;

                        _uploaderState = true;
                        _uploaderViewer.IsOpen = _uploaderState;
                    }

                    SetStatus(type, DeviceStateType.Connected);
                }

                //SP 복구 프로세스
                if(type.Equals(DB_CONNECTION_TYPE.UPLOADER) && connection.State == ConnectionState.Open)
                {
                    //SP Backup 체크
                    _backup.CheckBackup();
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);

                DatabaseClose(type);
            }

            return connection;
        }

        #endregion

        #region DATABASE SELECT

        public bool SelectCmd(PARAM_QUERY pQy, out DataTable dataTable, int timeout = 5)
        {
            bool success = false;
            dataTable = new DataTable();
            
            OdbcCommand command = null;
            
            try
            {
                
                if (SetCommand(pQy.Query, pQy.Param, DB_CONNECTION_TYPE.SELECTER, out command, timeout))
                {
                    using (OdbcDataAdapter dataAdapter = new OdbcDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                    if (dataTable.Rows.Count > 0) success = true;
                }
            }
            catch (OdbcException oe)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, oe.Message);
            }
            finally
            {
                command.Dispose();

                SetQueryLog(DB_CONNECTION_TYPE.SELECTER, pQy.LogString, success);
            }

            return success;
        }

        public bool SelectToDataSet(PARAM_QUERY pQy, out DataSet dataSet, int timeout = 5)
        {
            bool success = false;
            dataSet = new DataSet();
            OdbcCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, DB_CONNECTION_TYPE.SELECTER, out command, timeout))
                {
                    using (OdbcDataAdapter dataAdapter = new OdbcDataAdapter(command))
                    {
                        dataAdapter.Fill(dataSet);
                    }
                    if (dataSet.Tables[0].Rows.Count > 0) success = true;
                }
            }
            catch (OdbcException oe)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, oe.Message);
            }
            finally
            {
                command.Dispose();

                SetQueryLog(DB_CONNECTION_TYPE.SELECTER, pQy.LogString, success);
            }

            return success;
        }


        private bool SetCommand(string sql, ArrayList param, DB_CONNECTION_TYPE type, out OdbcCommand command, int timeout = 5)
        {
            bool success = false;
            command = new OdbcCommand(sql, GetConnection(type));

            try
            {
                if (command.Connection.State != ConnectionState.Open) return success;

                command.CommandType = CommandType.Text;
                command.CommandTimeout = timeout;

                if (param != null && param.Count > 0)
                    for (int i = 0; i < param.Count; i++)
                        command.Parameters.AddWithValue("", param[i]);

                success = true;
            }
            catch (OdbcException oe)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, oe.Message);
            }
            return success;
        }

        private bool SetCommand(string sql, ArrayList param, PARAM_OUTPUT[] outputs, DB_CONNECTION_TYPE type, out OdbcCommand command, int timeout = 5)
        {
            bool success = false;
            command = new OdbcCommand(sql, GetConnection(type));

            try
            {
                if (command.Connection.State != ConnectionState.Open) return success;

                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = timeout;

                if (param != null && param.Count > 0)
                    for (int i = 0; i < param.Count; i++)
                        command.Parameters.AddWithValue("", param[i]);

                if (outputs != null)
                    for (int i = 0; i < outputs.Length; i++)
                        command.Parameters.Add(new OdbcParameter()
                        {
                            ParameterName = outputs[i].Out_Name,
                            Size = outputs[i].Out_Length,
                            DbType = outputs[i].Out_Type,
                            Direction = ParameterDirection.Output
                        });

                success = true;
            }
            catch (OdbcException oe)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, oe.Message);
            }
            return success;
        }

        #endregion

        #region DATABASE EXECUTE

        public int ExecuteCmd(PARAM_QUERY pQy, int timeout = 5)
        {
            int result = -1;    // -1: 실패&오류 , 0~: 실행된 ROW 카운트
            OdbcCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, DB_CONNECTION_TYPE.UPLOADER, out command, timeout))
                    result = command.ExecuteNonQuery();
                //백업 처리
                else
                    _backup.BackupProcess(pQy);

                //Log.GetInstance.SaveLog(LEVEL.Info, "SQL 실행 취소");
            }
            catch (OdbcException oe)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, oe.Message);
            }
            finally
            {
                command.Dispose();

                SetQueryLog(DB_CONNECTION_TYPE.UPLOADER, pQy.LogString, result < 0 ? false : true);
            }

            return result;
        }

        public int ExecuteCmd(PARAM_QUERY pQy, PARAM_OUTPUT[] outputs, int timeout = 5)
        {
            int result = -1;    // -1: 실패&오류 , 0~: 실행된 ROW 카운트
            int outLength = outputs.Length;
            OdbcCommand command = null;
            
            try
            {
                if (SetCommand(pQy.Query, pQy.Param, outputs, DB_CONNECTION_TYPE.UPLOADER, out command, timeout))
                {
                    result = command.ExecuteNonQuery();

                    if(result > -1)
                    {
                        for (int i = 0; i < outLength; i++)
                        {
                            outputs[i].Output = command.Parameters[outputs[i].Out_Name].Value;
                        }
                    }
                }
                //백업 처리
                else
                    _backup.BackupProcess(pQy);

                //Log.GetInstance.SaveLog(LEVEL.Info, "SQL 실행 취소");
            }
            catch (OdbcException oe)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, oe.Message);
            }
            finally
            {
                command.Dispose();

                SetQueryLog(DB_CONNECTION_TYPE.UPLOADER, pQy.LogString, result < 0 ? false : true);
            }

            return result;
        }
        
        public void ExecuteListCmd(PARAM_QUERY[] pQys)
        {
            for (int i = 0; i < pQys.Length; i++)
            {
                if (pQys[i].Query.NoE()) continue;

                ExecuteCmd(pQys[i]);
            }
        }

        #endregion

        #region Method

        private void DefaultSetting()
        {
            if (DesignMode) return;

            _selecterState = false;
            _uploaderState = false;
            
            _backup = new SpBackUp();

            _selecterViewer = new DatabaseView();
            _uploaderViewer = new DatabaseView();
        }

        private void SetQueryLog(DB_CONNECTION_TYPE type, string query, bool finish)
        {
            string fin = finish ? "OK" : "FAIL";

            CommunicationMessage message = new CommunicationMessage()
            {
                Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                State = fin,
                Content = query
            };
            
            switch (type)
            {
                case DB_CONNECTION_TYPE.SELECTER:
                    if (!ControlCommon.FormNullCheck(_selecterViewer))
                    {
                        _selecterViewer.CommunicationLog(message);
                    }
                    break;

                case DB_CONNECTION_TYPE.UPLOADER:
                    if (!ControlCommon.FormNullCheck(_uploaderViewer))
                    {
                        _uploaderViewer.CommunicationLog(message);
                    }
                    break;
            }
        }

        //private string GetConnectionString()
        //{
        //    return "Dsn=" + _dsn + ";" +
        //           "uid=" + _userID + ";" +
        //           "pwd=" + _userPW;
        //}

        private string GetConnectionString()
        {
            //Driver={{iSeries Access ODBC Driver}};Protocol=TCPIP;Port=446;System={0};Uid={1};Pwd={2};", dataSource, userID, userPWD)

            OdbcConnectionStringBuilder con = new OdbcConnectionStringBuilder();
            con.Add("Driver", "{iSeries Access ODBC Driver}");
            con.Add("System", _ip);
            con.Add("Uid", _userID);
            con.Add("Pwd", _userPW);
            con.Add("Protocol", "TCPIP");
            con.Add("Port", _port);
            con.Add("DATABASE", "GKS1RDB");
            con.Add("DBQ", "*LIBL");

            //con.Add("access", "all");
            //con.Add("block size", "32");
            //con.Add("block criteria", "2");
            //con.Add("date format", "iso");
            //con.Add("date separator", "-");
            //con.Add("decimal separator", ".");
            //con.Add("errors", "ful");
            //con.Add("extended dynamic", "0");

            return con.ConnectionString;

            //return string.Format("Driver={{iSeries Access ODBC Driver}};Protocol=TCPIP;Port=446;System={0};Uid={1};Pwd={2};",
            //    _ip, _userID, _userPW);
        }

        private void SetStatus(DB_CONNECTION_TYPE type, DeviceStateType state)
        {
            string message = string.Empty;
            SLabel lbl = type.Equals(DB_CONNECTION_TYPE.SELECTER) ? slbl_Status : slbl_Status2;

            Color bgcolor = lbl.BackColor;

            switch (state)
            {
                case DeviceStateType.Wait:
                    message = "연결 대기";
                    bgcolor = Color.Silver;
                    break;
                case DeviceStateType.Connected:
                    message = $"연결 시간:{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    bgcolor = Color.Lime;
                    break;
                case DeviceStateType.Disconnected:
                    message = $"끊긴 시간:{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    bgcolor = Color.Red;
                    break;
            }

            if (lbl.GradientEndColor.Equals(bgcolor)) return;

            lbl.InvokeToAction<string>
            (
                delegate(string m)
                {
                    tip_Info.SetToolTip(lbl, m);
                }
                , message
            );

            lbl.SimpleInvoke("GradientEndColor", bgcolor);

            Logger.GetInstance.SaveLog(LEVEL.Info, $"[AS400 - {type}] {message}");
        }


        #endregion
    }
}