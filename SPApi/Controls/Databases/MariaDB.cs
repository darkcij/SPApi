using SPApi.Commons;
using SPApi.Controls.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace SPApi.Controls.Databases
{
    public class MariaDB
    {
        public const string STATEMENT = "";

        #region Members

        private MySqlConnection _connection = null;
        private string _ip = "127.0.0.1";
        private string _port = "3306";
        private string _database = string.Empty;
        private string _userID = string.Empty;
        private string _userPW = string.Empty;

        #endregion

        #region Properties

        public bool IsConnected { get; set; }
        
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }
        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }

        public string Database
        {
            get { return _database; }
            set { _database = value; }
        }

        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        public string UserPW
        {
            get { return _userPW; }
            set { _userPW = value; }
        }

        #endregion

        #region Constructor

        public MariaDB()
        {
            
        }

        #endregion

        #region DATABASE OPEN / CLOSE

        private MySqlConnection GetConnection()
        {
            MySqlConnection connection = null;
            try
            {
                if (connection == null)
                {
                    connection = new MySqlConnection(GetConnectionString());
                }

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();

                    IsConnected = true;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MYSQL/GetConnection] 처리중 에러발생, Desc:{0}", e.Message);

                DatabaseClose();
            }

            return connection;
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
                    IsConnected = false;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MYSQL/DatabaseClose] 처리중 에러발생, Desc:{0}", e.Message);
            }
        }

        #endregion

        #region DATABASE SELECT

        public bool SelectDataTable(PARAM_QUERY pQy, out DataTable dataTable, int timeout = 5)
        {
            bool success = false;
            dataTable = new DataTable();

            MySqlCommand command = null;

            try
            {

                if (SetCommand(pQy.Query, pQy.Param, out command, timeout))
                {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                    if (dataTable.Rows.Count > 0) success = true;
                }
            }
            catch (MySqlException e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MYSQL/SelectDataTable] 처리중 에러발생, Desc:{0}", e.Message);
            }
            finally
            {
                command.Dispose();
            }

            return success;
        }

        public bool SelectToDataSet(PARAM_QUERY pQy, out DataSet dataSet, int timeout = 5)
        {
            bool success = false;
            dataSet = new DataSet();
            MySqlCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command, timeout))
                {
                    using (MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataSet);
                    }

                    if (dataSet.Tables[0].Rows.Count > 0)
                        success = true;
                }
            }
            catch (MySqlException e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MYSQL/SelectToDataSet] 처리중 에러발생, Desc:{0}", e.Message);
            }
            finally
            {
                command.Dispose();
            }

            return success;
        }


        private bool SetCommand(string sql, ArrayList param, out MySqlCommand command, int timeout = 5)
        {
            bool success = false;
            command = new MySqlCommand(sql, GetConnection());

            try
            {
                if (command.Connection.State != ConnectionState.Open)
                    return success;

                command.CommandType = CommandType.Text;
                command.CommandTimeout = timeout;

                if (param != null && param.Count > 0)
                {
                    for (int i = 0; i < param.Count; i++)
                    {
                        command.Parameters.AddWithValue("", param[i]);
                    }
                }

                success = true;
            }
            catch (MySqlException e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MYSQL/SetCommand] 처리중 에러발생, Desc:{0}", e.Message);
            }
            return success;
        }

        #endregion

        #region DATABASE EXECUTE

        public int ExecuteCmd(PARAM_QUERY pQy)
        {
            int result = -1;    // -1: 실패&오류 , 0~: 실행된 ROW 카운트
            MySqlCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command))
                    result = command.ExecuteNonQuery();

            }
            catch (MySqlException e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MYSQL/ExecuteCmd] 처리중 에러발생, Desc:{0}", e.Message);
            }
            finally
            {
                command.Dispose();
            }

            return result;
        }

        #endregion

        #region Methods

        private string GetConnectionString()
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

        #endregion
    }
}
