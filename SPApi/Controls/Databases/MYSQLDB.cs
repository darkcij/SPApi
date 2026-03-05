using SPApi.Commons;
using SPApi.Controls.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using SPApi.Declaration.Structs;

namespace SPApi.Controls.Databases
{
    public class MYSQLDB
    {
        #region Members

        public const string PREFIX = "@";

        private MySqlConnection _connection = null;
        private string _ip = "127.0.0.1";
        private string _port = "3306";
        private string _database = "";
        private string _userID = "";
        private string _userPW = "";
        
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

        public MYSQLDB()
        {
            
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
                    IsConnected = true;
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
                        command.Parameters.AddWithValue(string.Format("{0}{1}", PREFIX, i), param[i]);
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
            MySqlConnectionStringBuilder con = new MySqlConnectionStringBuilder
            {
                { "Server", _ip },
                { "Port", _port },
                { "Database", _database },
                { "Uid", _userID },
                { "Pwd", _userPW }
            };
            con.CharacterSet = "utf8";
            con.AllowUserVariables = true;

            return con.ConnectionString;
        }

        #endregion
    }
}
