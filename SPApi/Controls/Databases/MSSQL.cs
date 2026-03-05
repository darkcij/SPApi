using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SPApi.Commons;
using SPApi.Controls.UI;
using SPApi.Controls.Log;
using SPApi.Declaration.Structs;
using SPApi.Declaration.Enums;

namespace SPApi.Controls.Databases
{
    public partial class MSSQL : UserControl
    {
        #region Singleton

        private static MSSQL _instance = null;
        public static MSSQL GetInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new MSSQL();

                return _instance;
            }
            set { _instance = value; }
        }

        #endregion

        #region Members

        public const string PREFIX = "@";

        private string _databaseName = "MSSQL";

        private SqlConnection _connection = null;
        private SqlTransaction _transaction = null;

        private string _ip = "127.0.0.1";
        private string _port = "1433";
        private string _database = "";
        private string _userID = "";
        private string _userPW = "";

        private DatabaseView _viewer = null;

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

        [Browsable(true), Category("Extended-Set")]
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }

        [Browsable(true), Category("Extended-Set")]
        public string PORT
        {
            get { return _port; }
            set { _port = value; }
        }

        [Browsable(true), Category("Extended-Set")]
        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }

        
        [Browsable(true), Category("Extended-Set")]
        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        [Browsable(true), Category("Extended-Set")]
        public string UserPW
        {
            get { return _userPW; }
            set { _userPW = value; }
        }
        
        #endregion

        #region Constructor

        public MSSQL()
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

        private SqlConnection GetConnection()
        {
            try
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(GetConnectionString());
                }

                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();

                    _viewer.IsOpen = 
                    IsConnected = true;
                    
                    SetStatus(DeviceStateType.Connected);
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MSSQL/GetConnection] 처리중 에러발생, Desc:{0}", e.Message);

                DatabaseClose();
            }
            return _connection;
        }

        public void DatabaseOpen()
        {
            GetConnection();
        }

        public void DatabaseClose()
        {
            try
            {
                if (_connection != null)
                {
                    _connection.Close();
                }

                _viewer.IsOpen =
                IsConnected = false;

                SetStatus(DeviceStateType.Disconnected);
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MSSQL/DatabaseClose] 처리중 에러발생, Desc:{0}", e.Message);
            }
        }

        #endregion

        #region DATABASE SELECT

        public bool SelectToDataSet(PARAM_QUERY pQy, out DataSet dataSet)
        {
            bool success = false;
            dataSet = new DataSet();
            SqlCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataSet);
                    }
                    if (dataSet.Tables[0].Rows.Count > 0) success = true;
                }
            }
            catch (SqlException se)
            {
                ExceptionProcess(se);
            }
            finally
            {
                SetQueryLog(pQy.LogString, success);

                if(command != null)
                    command.Dispose();
            }
            return success;
        }

        public bool SelectCmd(PARAM_QUERY pQy, out DataTable dataTable)
        {
            bool success = false;
            dataTable = new DataTable();
            SqlCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                    if (dataTable.Rows.Count > 0) success = true;
                }
            }
            catch (SqlException se)
            {
                ExceptionProcess(se);
            }
            finally
            {
                SetQueryLog(pQy.LogString, success);

                if (command != null)
                    command.Dispose();
            }
            return success;
        }

        private bool SetCommand(string sql, ArrayList param, out SqlCommand command)
        {
            bool success = false;
            command = new SqlCommand(sql, GetConnection());

            try
            {
                if (command.Connection.State != ConnectionState.Open) return success;

                command.CommandType = CommandType.Text;
                command.CommandTimeout = 5;

                if (param != null && param.Count > 0)
                    for (int i = 0; i < param.Count; i++)
                        command.Parameters.Add(new SqlParameter(PREFIX + i.ToString(), param[i]));

                success = true;
            }
            catch (SqlException se)
            {
                ExceptionProcess(se);
            }
            return success;
        }

        #endregion

        #region DATABASE EXECUTE

        public int ExecuteCmd(PARAM_QUERY pQy)
        {
            int result = 0;    // 0: 실패&오류 , 1: 정상처리
            SqlCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command))
                    result = command.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                ExceptionProcess(se);
            }
            finally
            {
                SetQueryLog(pQy.LogString, result < 0 ? false : true);

                if (command != null)
                    command.Dispose();
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
            _transaction = _connection.BeginTransaction();
        }

        private void Commit()
        {
            _transaction.Commit();
            _transaction.Dispose();
        }

        private void Rollback()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }

        #endregion
        
        #region Method

        private void DefaultSetting()
        {
            if (DesignMode) return;

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
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder
            {
                { "Server", $"{_ip},{_port}"},
                { "Database", _database },
                { "User Id", _userID },
                { "Password", _userPW }
            };

            return con.ConnectionString;
        }

        private void ExceptionProcess(SqlException se)
        {
            Logger.GetInstance.SaveLog(LEVEL.Error, "[MSSQL] 에러발생, Desc:{0}", se.Message);
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

            Logger.GetInstance.SaveLog(LEVEL.Info, $"[MSSQL] {message}");

        }

        #endregion
    }
}
