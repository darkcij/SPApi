using System;
using System.Collections;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Threading;
using SPApi.Commons;
using SPApi.Controls.Log;
using SPApi.Declaration.Structs;
using SPApi.Declaration.Enums;

namespace SPApi.Controls.Databases
{
    public partial class SQLite : UserControl
    {
        public delegate void DelActionMessage(string message, int count);
        public event DelActionMessage OnExecuteComplete;

        #region Members

        private ReaderWriterLockSlim _lock;

        #endregion

        #region Properties

        //private static SQLite Instance;
        //public static SQLite GetInstance
        //{
        //    get
        //    {
        //        if (Instance == null)
        //        {
        //            Instance = new SQLite();
        //        }
        //        return Instance;
        //    }
        //}

        public string Title
        {
            get { return slbl_Status.Text; }
            set { slbl_Status.SimpleInvoke("Text", value); }
        }
        
        private string _databaseName = @".\LocalDB.DB";
        public string DatabaseName
        {
            get { return _databaseName; }
            set { _databaseName = value; }
        }

        private string _dataSource = string.Empty;
        
        private SQLiteConnection _connection = null;
        
        private SQLiteTransaction _transaction = null;

        private DatabaseView _viewer = null;

        #endregion

        #region Constructer

        public SQLite()
        {
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
        
        private void slblStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (_viewer == null)
                    _viewer = new DatabaseView();
                else if (_viewer.Visible)
                    _viewer.SendToBack();

                _viewer.Title = "SQLite Viewer";
                _viewer.DatabaseName = "SQLite";
                _viewer.SystemInfo = _databaseName;
                _viewer.Uid = "-----";
                _viewer.Show();
            }
            catch { }
        }

        #endregion

        #region Methods

        private void DefaultSetting()
        {
            if (DesignMode) return;

            SetEnvironment();

            _viewer = new DatabaseView();

            _lock = new ReaderWriterLockSlim();
            
            //SetStatus("대기");
        }

        private void SetEnvironment()
        {
            //ODP DLL Environment 설정
            string home = Environment.CurrentDirectory + @"\Resources";
            Environment.SetEnvironmentVariable("PATH", home, EnvironmentVariableTarget.Process);
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

        /// <summary>
        /// DB OPEN
        /// </summary>
        public void Open()
        {
            GetConnection();
        }

        /// <summary>
        /// DB CLOSE
        /// </summary>
        public void Close()
        {
            if (!_connection.Equals(null))
            {
                _connection.Close();

                _viewer.IsOpen = false;

                SetStatus(DeviceStateType.Disconnected);
            }
        }

        private SQLiteConnection GetConnection()
        {
            try
            {
                _dataSource = $"Data Source={_databaseName}";

                if (_connection == null)
                {
                    _connection = new SQLiteConnection();
                }

                if (!_dataSource.Equals(_connection.ConnectionString))
                {
                    _connection.Close();
                    _connection.ConnectionString = _dataSource;
                }

                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();

                    _viewer.IsOpen = true;
                }
            }
            catch { }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                    SetStatus(DeviceStateType.Connected);
                else
                    SetStatus(DeviceStateType.Failed);
            }

            return _connection;
        }

        #region TRANSACTION

        /// <summary>
        /// Transaction 시작
        /// </summary>
        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        /// <summary>
        /// Transaction 커밋
        /// </summary>
        public void Commit()
        {
            _transaction.Commit();
        }

        /// <summary>
        /// Transaction 롤백
        /// </summary>
        public void Rollback()
        {
            _transaction.Rollback();
        }

        #endregion

        #region SELECT

        public bool SelectCmd(PARAM_QUERY pQy, out DataTable datatable)
        {
            bool success = false;
            datatable = new DataTable();
            SQLiteCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command))
                {
                    using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command))
                    {
                        _lock.EnterReadLock();
                        dataAdapter.Fill(datatable);
                    }
                    if (datatable.Rows.Count > 0) success = true;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
                System.Diagnostics.Debug.WriteLine("[DatabaseService - SelectToDataSet] Error!!! Desc:" + e.Message);
            }
            finally
            {
                _lock.ExitReadLock();

                if (success)
                    SetStatus(DeviceStateType.Success);
                else
                    SetStatus(DeviceStateType.Failed);

                SetQueryLog(pQy.LogString, success);
            }

            return success;
        }

        //public bool SelectToDataSet(PARAM_QUERY pQy, out DataSet dataset)
        //{
        //    bool success = false;
        //    dataset = new DataSet();
        //    SQLiteCommand command = null;

        //    try
        //    {
        //        if (SetCommand(pQy.Query, pQy.Param, out command))
        //        {
        //            using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command))
        //            {
        //                _lock.EnterReadLock();
        //                dataAdapter.Fill(dataset);
        //            }
        //            if (dataset.Tables[0].Rows.Count > 0) success = true;
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
        //        System.Diagnostics.Debug.WriteLine("[DatabaseService - SelectToDataSet] Error!!! Desc:" + e.Message);
        //    }
        //    finally
        //    {
        //        _lock.ExitReadLock();

        //        if (success) SetStatus("성공");
        //        else SetStatus("실패");

        //        SetQueryLog(pQy.LogString, success);
        //    }

        //    return success;
        //}

        #endregion

        #region EXECUTE

        public int ExecuteCmd(PARAM_QUERY pQy)
        {
            int result = -1;
            SQLiteCommand command = null;

            try
            {
                if (SetCommand(pQy.Query, pQy.Param, out command))
                {
                    _lock.EnterWriteLock();
                    result = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
                System.Diagnostics.Debug.WriteLine("[DatabaseService - ExecuteCmd] Error!!! Desc:" + e.Message);
            }
            finally
            {
                _lock.ExitWriteLock();

                if (result > -1) 
                    SetStatus(DeviceStateType.Success);
                else
                    SetStatus(DeviceStateType.Failed);

                SetQueryLog(pQy.LogString, result < 0 ? false : true);
            }

            return result;
        }

        public void ExecuteListCmd(PARAM_QUERY[] pQy)
        {
            SQLiteCommand command = null;
            int result = -1;
            string msg = string.Empty;

            try
            {
                _lock.EnterWriteLock();
                 BeginTransaction();
                for (int i = 0; i < pQy.Length; i++)
                {
                    if (pQy[i].Query.NoE()) continue;

                    result = -1;

                    try
                    {
                        if (SetCommand(pQy[i].Query, pQy[i].Param, out command))
                            result = command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);                       
                    }
                    finally
                    {
                        if (result > -1) 
                            msg = "성공";
                        else 
                            msg = "실패";

                        SetStatus(msg.Equals("성공") ? DeviceStateType.Success : DeviceStateType.Failed);
                        SetQueryLog(pQy[i].LogString, result < 0 ? false : true);

                        if(OnExecuteComplete != null)
                            OnExecuteComplete(msg, result);
                    }
                }
            }
            finally
            {
                Commit();

                if(_lock.IsWriteLockHeld)
                    _lock.ExitWriteLock();
            }
        }

        #endregion

        private bool SetCommand(string sql, ArrayList param, out SQLiteCommand command)
        {
            bool success = false;
            command = new SQLiteCommand(sql, GetConnection());

            try
            {
                if (!command.Connection.State.Equals(ConnectionState.Open)) return success;

                command.CommandType = CommandType.Text;
                command.CommandTimeout = 5;

                if (!(param == null) && param.Count > 0)
                    for (int i = 0; i < param.Count; i++)
                        command.Parameters.AddWithValue("", param[i]);

                success = true;
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
                System.Diagnostics.Debug.WriteLine("[DatabaseService - SetCommand] Error!!! Desc:" + e.Message);
            }

            return success;
        }

        private void SetStatus(DeviceStateType state)
        {
            string message = string.Empty;
            Color bgcolor = slbl_Status.BackColor;

            switch (state)
            {
                case DeviceStateType.Wait:
                    message = "연결 대기";
                    bgcolor = Color.DarkGray;
                    break;
                case DeviceStateType.Connected:
                    message = $"연결 시간:{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    bgcolor = Color.Yellow;
                    break;
                case DeviceStateType.Success:
                    message = $"성공 시간:{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    bgcolor = Color.Lime;
                    break;
                case DeviceStateType.Failed:
                    message = $"실패 시간:{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    bgcolor = Color.Orange;
                    break;
                case DeviceStateType.Disconnected:
                    message = $"끊긴 시간:{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    bgcolor = Color.Red;
                    break;
            }

            if (slbl_Status.GradientEndColor.Equals(bgcolor)) return;

            slbl_Status.InvokeToAction<string>
            (
                delegate(string m)
                {
                    tip_Info.SetToolTip(slbl_Status, m);
                }
                , message
            );

            slbl_Status.SimpleInvoke("GradientEndColor", bgcolor);

            if (state.Equals(DeviceStateType.Success) || state.Equals(DeviceStateType.Failed)) return;

            Logger.GetInstance.SaveLog(LEVEL.Info, $"[SQLite] {message}");
        }


        #endregion
    }
}
