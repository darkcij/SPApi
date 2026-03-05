using MySql.Data.MySqlClient;
using SPApi.Commons;
using SPApi.Controls.Log;
using SPApi.Declaration.Structs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SPApi.Controls.Databases
{
    public class MSSQLDB
    {
        #region Members

        public const string PREFIX = "@";

        private SqlConnection _connection = null;
        private string _ip = string.Empty;
        private string _port = "1433";
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

        public MSSQLDB()
        {
            
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

                    IsConnected = true;                    
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
                    IsConnected = false;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MSSQL/DatabaseClose] 처리중 에러발생, Desc:{0}", e.Message);
            }
        }

        #endregion

        #region DATABASE SELECT

        public bool SelectDataTable(PARAM_QUERY pQy, out DataTable dataTable, int timeout = 5)
        {
            bool success = false;
            dataTable = new DataTable();

            SqlCommand command = null;

            try
            {

                if (SetCommand(pQy.Query, pQy.Param, out command, timeout))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTable);
                    }
                    if (dataTable.Rows.Count > 0) success = true;
                }
            }
            catch (SqlException e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MSSQL/SelectDataTable] 처리중 에러발생, Desc:{0}", e.Message);
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
            SqlCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command, timeout))
                {
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataSet);
                    }

                    if (dataSet.Tables[0].Rows.Count > 0)
                        success = true;
                }
            }
            catch (SqlException e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MSSQL/SelectToDataSet] 처리중 에러발생, Desc:{0}", e.Message);
            }
            finally
            {
                command.Dispose();
            }

            return success;
        }


        private bool SetCommand(string sql, ArrayList param, out SqlCommand command, int timeout = 5)
        {
            bool success = false;
            command = new SqlCommand(sql, GetConnection());

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
            catch (SqlException e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MSSQL/SetCommand] 처리중 에러발생, Desc:{0}", e.Message);
            }
            return success;
        }

        #endregion

        #region DATABASE EXECUTE

        public int ExecuteCmd(PARAM_QUERY pQy)
        {
            int result = -1;    // -1: 실패&오류 , 0~: 실행된 ROW 카운트
            SqlCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command))
                    result = command.ExecuteNonQuery();

            }
            catch (SqlException e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[MSSQL/ExecuteCmd] 처리중 에러발생, Desc:{0}", e.Message);
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
            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
            con.Add("Server", string.Format("{0},{1}", _ip, _port));
            con.Add("Database", _database);
            con.Add("User Id", _userID);
            con.Add("Password", _userPW);
            
            return con.ConnectionString;
        }

        #endregion
    }
}
