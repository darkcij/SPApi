using System;
using System.Data;
using System.Data.Odbc;
using System.Collections;
using SPApi.Controls.Log;
using SPApi.Commons;
using SPApi.Controls.BackUp;
using SPApi.Declaration.Structs;
using System.Data.OleDb;

namespace SPApi.Controls.Databases
{
    public class AS400DB: IDisposable
    {
        #region Properties

        private const string _databaseName = "AS400";
        
        private string _ip = "165.141.66.7";
        public string IP
        {
            get { return _ip; }
            set { _ip = value; }
        }

        private string _userID = "XCIM977";
        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private string _userPW = "XCIM977";
        public string UserPW
        {
            get { return _userPW; }
            set { _userPW = value; }
        }

        private bool _state = false;
        public bool State
        {
            get { return _state; }
            set { _state = value; }
        }

        private OdbcConnection _connection = null;
        
        private SpBackUp _backup = null;
        
        #endregion

        #region Constructor

        public AS400DB()
        {
            DefaultSetting();
        }

        public AS400DB(string ip, string user_id, string user_pw)
        {
            _ip = ip;
            _userID = user_id;
            _userPW = user_pw;

            DefaultSetting();
        }

        #endregion

        #region DATABASE OPEN / CLOSE

        private OdbcConnection GetConnection()
        {
            OdbcConnection connection = null;
            try
            {
                connection = _connection;

                if (connection == null)
                {
                    connection = new OdbcConnection(GetConnectionString());
                }

                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();

                    if (_connection == null) _connection = connection;

                    _state = true;
                }

                //SP 복구 프로세스
                if (connection.State == ConnectionState.Open)
                {
                    //SP Backup 체크
                    _backup.CheckBackup();
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);

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
            OdbcConnection connection = _connection;

            try
            {
                if (connection != null)
                    connection.Close();

                _state = false;
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
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

                if (SetCommand(pQy.Query, pQy.Param, out command, timeout))
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
                if (SetCommand(pQy.Query, pQy.Param, out command, timeout))
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
            }

            return success;
        }

        public bool SelectToDataSet(PARAM_QUERY pQy, out DataSet dataSet, ref OdbcException error, int timeout = 5)
        {
            bool success = false;
            dataSet = new DataSet();
            OdbcCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command, timeout))
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
                //Logger.GetInstance.SaveLog(LEVEL.Error, oe.Message);
                error = oe;
            }
            finally
            {
                command.Dispose();
            }

            return success;
        }


        private bool SetCommand(string sql, ArrayList param, out OdbcCommand command, int timeout = 5)
        {
            bool success = false;
            command = new OdbcCommand(sql, GetConnection());

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

        private bool SetCommand(string sql, ArrayList param, PARAM_OUTPUT[] outputs, out OdbcCommand command, int timeout = 5)
        {
            bool success = false;
            command = new OdbcCommand(sql, GetConnection());

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

        public int ExecuteCmd(PARAM_QUERY pQy)
        {
            int result = -1;    // -1: 실패&오류 , 0~: 실행된 ROW 카운트
            OdbcCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command))
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
            }

            return result;
        }

        public int ExecuteCmd(PARAM_QUERY pQy, ref OdbcException error)
        {
            int result = -1;    // -1: 실패&오류 , 0~: 실행된 ROW 카운트
            OdbcCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command))
                    result = command.ExecuteNonQuery();
                //백업 처리
                else
                    _backup.BackupProcess(pQy);

                //Log.GetInstance.SaveLog(LEVEL.Info, "SQL 실행 취소");
            }
            catch (OdbcException oe)
            {
                //Logger.GetInstance.SaveLog(LEVEL.Error, oe.Message);
                error = oe;
            }
            finally
            {
                command.Dispose();
            }

            return result;
        }

        public int ExecuteCmd(PARAM_QUERY pQy, PARAM_OUTPUT[] outputs)
        {
            int result = -1;    // -1: 실패&오류 , 0~: 실행된 ROW 카운트
            int outLength = outputs.Length;
            OdbcCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, outputs, out command))
                {
                    result = command.ExecuteNonQuery();

                    if (result > -1)
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
            _state = false;
            _backup = new SpBackUp();
        }
        
        private string GetConnectionString()
        {
            //Driver={{iSeries Access ODBC Driver}};Protocol=TCPIP;Port=446;System={0};Uid={1};Pwd={2};", dataSource, userID, userPWD)

            OdbcConnectionStringBuilder con = new OdbcConnectionStringBuilder();
            //con.Add("DSN", "as400");
            con.Add("Driver", "{iSeries Access ODBC Driver}");
            con.Add("System", _ip);
            con.Add("Uid", _userID);
            con.Add("Pwd", _userPW);
            con.Add("Protocol", "TCPIP");
            con.Add("Port", "446");
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

            //con.Add("sort language", "ENU");

            return con.ConnectionString;

            //return string.Format("Driver={{iSeries Access ODBC Driver}};Protocol=TCPIP;Port=446;System={0};Uid={1};Pwd={2};",
            //    _ip, _userID, _userPW);
        }
        
        #endregion

        #region Interface :: IDisposable
        public void Dispose()
        {
            DatabaseClose();

            if(_connection != null)
                _connection.Dispose();
        }

        #endregion
    }
}
