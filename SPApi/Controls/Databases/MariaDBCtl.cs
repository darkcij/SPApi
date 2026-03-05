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
using MySql.Data.MySqlClient;

namespace SPApi.Controls.Databases
{
    public partial class MariaDBCtl : UserControl
    {
        #region Members

        private string _databaseName = "MYSQL";

        private MySqlConnection _connection = null;
        private MySqlTransaction _transaction = null;
        private string _ip = "127.0.0.1";
        private string _port = "3306";
        private string _database = string.Empty;
        private string _userID = string.Empty;
        private string _userPW = string.Empty;

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

        private DatabaseView _dbView = null;

        #endregion

        #region Construction

        public MariaDBCtl()
        {
            InitializeComponent();
        }

        #endregion

        #region UserControl Events

        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode) return;

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
                if (_dbView == null)
                    _dbView = new DatabaseView();
                else if (_dbView.Visible)
                    _dbView.SendToBack();

                _dbView.Title = string.Format("{0} Viewer", _databaseName);
                _dbView.DatabaseName = _databaseName;
                _dbView.SystemInfo = _ip;
                _dbView.Uid = _userID;
                _dbView.Show();
            }
            catch { }
        }

        #endregion

        #region DATABASE OPEN / CLOSE

        private MySqlConnection GetConnection()
        {
            try
            {
                if (_connection == null)
                {
                    _connection = new MySqlConnection(GetConnectionString());
                }

                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();

                    _dbView.IsOpen = 
                    IsConnected = true;
                    
                    SetStatus("연결");
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MYSQL/GetConnection] 처리중 에러발생, Desc:{0}", e.Message);

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

                _dbView.IsOpen =
                IsConnected = false;

                SetStatus("끊김");
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MYSQL/DatabaseClose] 처리중 에러발생, Desc:{0}", e.Message);
            }
        }

        #endregion

        #region DATABASE SELECT

        public bool SelectToDataSet(PARAM_QUERY pQy, out DataSet dataSet)
        {
            bool success = false;
            dataSet = new DataSet();
            MySqlCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command))
                {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataSet);
                    }
                    if (dataSet.Tables[0].Rows.Count > 0) success = true;
                }
            }
            catch (MySqlException se)
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
            MySqlCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command))
                {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                    if (dataTable.Rows.Count > 0) success = true;
                }
            }
            catch (MySqlException se)
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

        private bool SetCommand(string sql, ArrayList param, out MySqlCommand command)
        {
            bool success = false;
            command = new MySqlCommand(sql, GetConnection());

            try
            {
                if (command.Connection.State != ConnectionState.Open) return success;

                command.CommandType = CommandType.Text;
                command.CommandTimeout = 5;

                if (param != null && param.Count > 0)
                    for (int i = 0; i < param.Count; i++)
                        command.Parameters.Add("", param[i]);

                success = true;
            }
            catch (MySqlException se)
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
            MySqlCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command))
                    result = command.ExecuteNonQuery();
            }
            catch (MySqlException se)
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
            IsConnected = false;

            SetStatus("대기");

            _dbView = new DatabaseView();
        }

        private void SetQueryLog(string query, bool finish)
        {
            string fin = finish ? "OK" : "FAIL";

            _dbView.CommunicationLog(fin, query);
        }

        public string GetConnectionString()
        {
            MySqlConnectionStringBuilder con = new MySqlConnectionStringBuilder();
            con.Add("Server", _ip);
            con.Add("Port", _port);
            con.Add("Database", _database);
            con.Add("Uid", _userPW);
            con.Add("Pwd", _userPW);
            con.CharacterSet = "utf8";
            con.AllowUserVariables = true;

            return con.ConnectionString;
        }

        private void ExceptionProcess(MySqlException se)
        {
            Logger.GetInstance.SaveLog(LEVEL.Error, "[MYSQL] 에러발생, Desc:{0}", se.Message);

            //switch (se.Number)
            //{
            //    case 3113:
            //    case 3114:
            //        DatabaseClose();
            //        break;
            //}
        }
        
        private void SetStatus(string msg)
        {
            string message = string.Empty;
            SLabel lbl = slbl_Status;

            Color bgcolor = lbl.BackColor;

            switch (msg)
            {
                case "대기":
                    message = "연결 대기";
                    bgcolor = Color.Silver;
                    break;
                case "연결":
                    message = "연결 시간:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bgcolor = Color.Lime;
                    break;
                case "성공":
                    message = "성공 시간:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bgcolor = Color.Yellow;
                    break;
                case "실패":
                    message = "실패 시간:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bgcolor = Color.Orange;
                    break;
                case "끊김":
                    message = "끊긴 시간:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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

            Logger.GetInstance.SaveLog(LEVEL.Info, "[Oracle] {0}", message);
        }

        #endregion
    }
}
