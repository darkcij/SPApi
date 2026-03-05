using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SPApi.Commons;
using SPApi.Controls.UI;
using SPApi.Controls.Log;
using Oracle.ManagedDataAccess.Client;
using SPApi.Declaration.Structs;
using SPApi.Declaration.Enums;

namespace SPApi.Controls.Databases
{
    public partial class Oracle : UserControl
    {

        #region Singleton

        private static Oracle _instance = null;
        public static Oracle GetInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new Oracle();

                return _instance;
            }
            set { _instance = value; }
        }

        #endregion

        #region Members

        public const string PREFIX = ":";

        private string _databaseName = "Oracle";

        public OracleConnection _oraConnection = null;
        private OracleTransaction _oraTransaction = null;

        #endregion

        #region Properties

        public bool IsConnected { get; set; }

        public string Title
        {
            get { return _databaseName; }
            set
            {
                _databaseName = value;
                slbl_Status.Text = _databaseName;
                Invalidate();
            }
        }

        private string _ip = "127.0.0.1";
        [Browsable(true), Category("Extended-Set")]
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }

        private string _port = "1521";
        [Browsable(true), Category("Extended-Set")]
        public string PORT
        {
            get { return _port; }
            set { _port = value; }
        }

        private string _sid = "";
        [Browsable(true), Category("Extended-Set")]
        public string SID
        {
            get { return _sid; }
            set { _sid = value; }
        }

        private string _userID = "";
        [Browsable(true), Category("Extended-Set")]
        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private string _userPW = "";
        [Browsable(true), Category("Extended-Set")]
        public string UserPW
        {
            get { return _userPW; }
            set { _userPW = value; }
        }

        private DatabaseView _viewer = null;

        private OracleVersion _clientVersion = OracleVersion.VERSION12;
        [Browsable(true), Category("Extended-Set")]
        public OracleVersion ClientVersion
        {
            get{ return _clientVersion; }
            set { _clientVersion = value; }
        }

        #endregion

        #region Constructor

        public Oracle()
        {
            InitializeComponent();
        }

        #endregion

        #region UserControl Events

        protected override void OnLoad(EventArgs e)
        {
            DefaultSetting();

            base.OnLoad(e);
        }

        #endregion

        #region Control Events

        /// <summary>
        /// 라벨을 클릭하면 DetailView Show
        /// </summary>
        private void slblStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (_viewer == null)
                    _viewer = new DatabaseView();
                else if (_viewer.Visible)
                    _viewer.SendToBack();

                _viewer.Title = string.Format("{0} Viewer", _databaseName);
                _viewer.DatabaseName = _databaseName;
                _viewer.SystemInfo = _ip;
                _viewer.Uid = _userID;
                _viewer.Show();
            }
            catch { }
        }

        #endregion

        #region DATABASE OPEN / CLOSE

        private OracleConnection GetConnection()
        {
            try
            {
                if (_oraConnection == null)
                {
                    _oraConnection = new OracleConnection();
                    _oraConnection.ConnectionString = GetConnectionString();
                    _oraConnection.SqlNetAllowedLogonVersionClient = (OracleAllowedLogonVersionClient)ClientVersion.ToInt();
                }

                if (_oraConnection.State != ConnectionState.Open)
                {                   
                    _oraConnection.Open();

                    _viewer.IsOpen = 
                    IsConnected = true;
                    
                    SetStatus(DeviceStateType.Connected);
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[Oracle-GetConnection] 처리중 에러발생, Desc:{0}", e.Message);

                OracleDatabaseClose();
            }
            return _oraConnection;
        }

        public void OracleDatabaseOpen()
        {
            GetConnection();
        }

        public void OracleDatabaseClose()
        {
            try
            {
                if (_oraConnection != null)
                {
                    _oraConnection.Close();
                }

                _viewer.IsOpen =
                IsConnected = false;

                SetStatus(DeviceStateType.Disconnected);
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[Oracle-OracleDatabaseClose] 처리중 에러발생, Desc:{0}", e.Message);
            }
        }

        #endregion

        #region DATABASE SELECT

        public bool SelectToDataSet(PARAM_QUERY pQy, out DataSet dataSet)
        {
            bool success = false;
            dataSet = new DataSet();
            OracleCommand oraCommand = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out oraCommand))
                {
                    using (OracleDataAdapter oraDataAdapter = new OracleDataAdapter(oraCommand))
                    {
                        oraDataAdapter.Fill(dataSet);
                    }
                    if (dataSet.Tables[0].Rows.Count > 0) success = true;
                }
            }
            catch (OracleException oe)
            {
                OracleExceptionProcess(oe);
            }
            finally
            {
                SetQueryLog(pQy.LogString, success);

                if(oraCommand != null)
                    oraCommand.Dispose();
            }
            return success;
        }

        public bool SelectCmd(PARAM_QUERY pQy, out DataTable dataTable)
        {
            bool success = false;
            dataTable = new DataTable();
            OracleCommand oraCommand = null;
            OracleDataAdapter oraDataAdapter = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out oraCommand))
                {
                    oraDataAdapter = new OracleDataAdapter(oraCommand);
                    
                    oraDataAdapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0) success = true;
                }
            }
            catch (OracleException oe)
            {
                //재시도
                if(OracleExceptionProcess(oe))
                {
                    oraDataAdapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0) success = true;
                }
            }
            finally
            {
                SetQueryLog(pQy.LogString, success);

                if (oraCommand != null)
                    oraCommand.Dispose();

                if (oraDataAdapter != null)
                    oraDataAdapter.Dispose();
            }
            return success;
        }

        private bool SetCommand(string sql, ArrayList param, out OracleCommand oraCommand)
        {
            bool success = false;
            oraCommand = new OracleCommand(sql, GetConnection());

            try
            {
                if (oraCommand.Connection.State != ConnectionState.Open) return success;

                oraCommand.CommandType = CommandType.Text;
                oraCommand.CommandTimeout = 5;

                if (param != null && param.Count > 0)
                    for (int i = 0; i < param.Count; i++)
                        oraCommand.Parameters.Add(new OracleParameter(PREFIX + i.ToString(), param[i]));

                success = true;
            }
            catch (OracleException oe)
            {
                OracleExceptionProcess(oe);
            }
            return success;
        }

        #endregion

        #region DATABASE EXECUTE

        public int ExecuteCmd(PARAM_QUERY pQy)
        {
            int result = 0;    // -1: 오류, 0: 변경 데이터 없음 , 1~: 변경된 데이터 Row Count
            OracleCommand oraCommand = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out oraCommand))
                    result = oraCommand.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                //에러 발생시 -1;
                result = -1;

                //재시도
                if (OracleExceptionProcess(oe))
                {
                    result = oraCommand.ExecuteNonQuery();
                }
            }
            finally
            {
                SetQueryLog(pQy.LogString, result < 0 ? false : true);

                if (oraCommand != null)
                    oraCommand.Dispose();
            }
            return result;
        }

        public void ExecuteListCmd(PARAM_QUERY[] pQys)
        {
            //this.BeginTrans();
            for (int i = 0; i < pQys.Length; i++)
            {
                if (string.IsNullOrEmpty(pQys[i].Query)) continue;

                ExecuteCmd(pQys[i]);
                //Rollback 처리 고려
            }
            //this.Commit();
        }

        #endregion

        #region DATABASE TRANSACTION

        private void BeginTrans()
        {
            _oraTransaction = _oraConnection.BeginTransaction();
        }

        private void Commit()
        {
            _oraTransaction.Commit();
            _oraTransaction.Dispose();
        }

        private void Rollback()
        {
            _oraTransaction.Rollback();
            _oraTransaction.Dispose();
        }

        #endregion
        
        #region Method

        private void DefaultSetting()
        {
            if (DesignMode) return;

            //EnvironmentSetting e = new EnvironmentSetting();
            //e.SetEnvironmentVariables();

            IsConnected = false;
            SetStatus(DeviceStateType.Wait);

            _viewer = new DatabaseView();
        }

        private void SetQueryLog(string query, bool finish)
        {
            string fin = finish ? "OK" : "FAIL";

            CommunicationMessage message = new CommunicationMessage()
            {
                Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                State = fin,
                Content = query
            };

            if (!ControlCommon.FormNullCheck(_viewer))
            {
                _viewer.CommunicationLog(message);
            }
        }

        public string GetConnectionString()
        {
            //OracleConnectionStringBuilder sb = new OracleConnectionStringBuilder();
            //sb.DataSource = GetDataSource(_ip, _port, _sid);
            //sb.UserID = _userID;
            //sb.Password = _userPW;
            //
            //return sb.ConnectionString;

            return string.Format("Data Source={0};User Id={1};Password={2}"
                    , GetDataSource(_ip, _port, _sid), _userID, _userPW);
        }

        private string GetDataSource(string ip, string port, string sid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(DESCRIPTION=");
            sb.AppendFormat("(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1}))", ip, port);
            sb.AppendFormat("(CONNECT_DATA=(SERVER=DEDICATED)(SID={0})))", sid);

            return sb.ToString();

            //return "(DESCRIPTION=" +
            //        "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + ip + ")(PORT=" + port + ")))" +
            //        "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + sid + "))" +
            //       ")";
        }

        private bool OracleExceptionProcess(OracleException oe)
        {
            bool rtn = false;   //쿼리 재시도
            string msg = string.Empty;
            
            switch (oe.Number)
            {
                case 2396:  //exceeded maximum idle time, please connect again
                    OracleDatabaseClose();
                    OracleDatabaseOpen();
                    
                    rtn = true;
                    msg = "재연결 후 쿼리 재전송...";

                    break;

                case 3113:  //end-of-file on communication channel
                case 3114:  //not connected to ORACLE
                    OracleDatabaseClose();
                    OracleDatabaseOpen();

                    rtn = false;
                    msg = "재연결...";
                    break;
            }

            Logger.GetInstance.SaveLog(LEVEL.Error, "[Oracle] 에러발생!! {0}, Desc:{1}", msg, oe.Message);

            return rtn;
        }

        private void SetStatus(DeviceStateType state)
        {
            string message = string.Empty;
            SLabel lbl = slbl_Status;

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

            Logger.GetInstance.SaveLog(LEVEL.Info, $"[Oracle] {message}");
        }

        #endregion
    }
}
