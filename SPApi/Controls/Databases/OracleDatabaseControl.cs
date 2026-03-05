/**************************************************************
 *  ProjectName : MDR3000
 *  Description : Oracle Database Communication Control
 *  Create Date : 2016.08.09
 *  Create Name : Jo Injae
 **************************************************************
 *  Update Date : 
 *  Update Name : 
 *  Update Desc : 
 **************************************************************/

using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using SPApi.Controls.Log;
using SPApi.Commons;

namespace SPApi.Controls.Databases
{
    public partial class OracleDatabaseControl : UserControl
    {
        #region PROPERTIES & OBJECT

        private OracleConnection _oraConnection = null;
        private OracleTransaction _oraTransaction = null;
        private readonly string _prifix = ":";

        private string _title = "DB";
        public string Title
        {
            get { return this._title; }
            set
            {
                this._title = value;
                this.lbl_Status.SimpleInvoke("Text", _title);
                this.Invalidate();
            }
        }

        public string DB_Ip { get; set; }
        public string DB_Port { get; set; }
        public string DB_Sid { get; set; }
        public string USER_ID { get; set; }
        public string USER_PW { get; set; }
        public bool DB_ConnectionState { get; set; }

        #endregion

        #region EVENT & DELEGATE

        //public delegate void delEventMessage(int type, string message); //type : 0:일반, 1:경고, ~ 9:에러
        //public event delEventMessage EventMessage;

        #endregion

        #region CONSTRUCTOR

        public OracleDatabaseControl()
        {
            InitializeComponent();
        }

        #endregion

        #region FORM EVENT
        
        private void OracleDatabaseControl_Load(object sender, EventArgs e)
        {
            this.SetOdpEnvironment();
            this.DB_ConnectionState = false;
            this.SetStatus("대기");
        }

        #endregion

        #region DATABASE OPEN & CLOSE

        private OracleConnection GetConnection()
        {
            try
            {
                if (this._oraConnection == null)
                    this._oraConnection = new OracleConnection(this.GetConnectionString());

                if (this._oraConnection.State != ConnectionState.Open)
                {
                    this._oraConnection.Open();
                    this.DB_ConnectionState = true;
                    this.SetStatus("연결");
                }
            }
            catch (Exception e) 
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[ORACLE] Error, Desc:" + e.Message);
                //if(this.EventMessage != null )
                //    this.EventMessage(9, e.Message);

                this.OracleDatabaseClose();
            }
            return this._oraConnection;
        }

        public void OracleDatabaseOpen()
        {
            this.GetConnection();
        }

        public void OracleDatabaseClose()
        {
            try
            {
                if (this._oraConnection != null)
                    this._oraConnection.Close();

                this.DB_ConnectionState = false;
                this.SetStatus("끊김");
                
            }
            catch (Exception e) 
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[ORACLE] Error, Desc:" + e.Message);
                //if (this.EventMessage != null)
                //    this.EventMessage(9, e.Message); 
            }
        }

        #endregion

        #region DATABASE SELECT

        public bool SelectToDataSet(string sql, ArrayList param, out DataSet dataSet)
        {
            bool success = false;
            dataSet = new DataSet();
            OracleCommand oraCommand;
            
            try
            {
                if (this.SetCommand(sql, param, out oraCommand))
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
                this.OracleExceptionProcess(oe);
            }
            return success;
        }

        private bool SetCommand(string sql, ArrayList param, out OracleCommand oraCommand)
        {
            bool success = false;
            oraCommand = new OracleCommand(sql, this.GetConnection());

            try
            {
                if (oraCommand.Connection.State != ConnectionState.Open) return success;

                oraCommand.CommandType = CommandType.Text;
                oraCommand.CommandTimeout = 5;

                if (param != null && param.Count > 0)
                    for (int i = 0; i < param.Count; i++)
                        oraCommand.Parameters.Add(new OracleParameter(this._prifix + i.ToString(), param[i]));

                success = true;
            }
            catch (OracleException oe)
            {
                this.OracleExceptionProcess(oe);
            }
            return success;
        }
                    
        #endregion

        #region DATABASE EXECUTE

        public int ExecuteCmd(string sql, ArrayList param)
        {
            int result = 0;    // 0: 실패&오류 , 1: 정상처리
            OracleCommand oraCommand = null;

            try
            {
                if (this.SetCommand(sql, param, out oraCommand))
                    result = oraCommand.ExecuteNonQuery();
            }
            catch (OracleException oe)
            {
                this.OracleExceptionProcess(oe);
            }
            return result;
        }

        public void ExecuteListCmd(string[] sqls, ArrayList[] param)
        {
            this.BeginTrans();
            for (int i = 0; i < sqls.Length; i++)
            {
                if (string.IsNullOrEmpty(sqls[i])) continue;

                this.ExecuteCmd(sqls[i], param[i]);
                //Rollback 처리 고려
            }
            this.Commit();
        }

        #endregion

        #region DATABASE TRANSACTION

        private void BeginTrans()
        {
            this._oraTransaction = this._oraConnection.BeginTransaction();
        }

        private void Commit()
        {
            this._oraTransaction.Commit();
            this._oraTransaction.Dispose();
        }

        private void Rollback()
        {
            this._oraTransaction.Rollback();
            this._oraTransaction.Dispose();
        }

        #endregion

        #region PROCEDURES

        public string GetConnectionString()
        {
            return "Data Source=" + this.GetDataSource(this.DB_Ip, this.DB_Port, this.DB_Sid) + ";" +
                   "User Id=" + this.USER_ID + ";" +
                   "Password=" + this.USER_PW + ";";
        }
        
        private string GetDataSource(string ip, string port, string sid)
        {
            return "(DESCRIPTION=" +
                    "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=" + ip + ")(PORT=" + port + ")))" +
                    "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=" + sid + "))" +
                   ")";
        }

        private void SetOdpEnvironment()
        {
            //ODP DLL Environment 설정
            string oracle_home = Environment.CurrentDirectory + @"\Resources\ODP";
            Environment.SetEnvironmentVariable("PATH", oracle_home, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("ORACLE_HOME", oracle_home, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("ORA_TZFILE", null);
            String nlsLang = Environment.GetEnvironmentVariable("NLS_LANG");
            if (String.IsNullOrEmpty(nlsLang))//KOREAN_KOREA.KO16MSWIN949 , OR string.Empty
                Environment.SetEnvironmentVariable("NLS_LANG", "KOREAN_KOREA.KO16MSWIN949");
            Environment.SetEnvironmentVariable("NLS_DATE_FORMAT", "RR/MM/DD");
            Environment.SetEnvironmentVariable("NLS_TIME_FORMAT", "HH24:MI:SSXFF");
            Environment.SetEnvironmentVariable("NLS_TIMESTAMP_FORMAT", "RR/MM/DD HH24:MI:SSXFF");
            Environment.SetEnvironmentVariable("NLS_TIME_TZ_FORMAT", "HH24:MI:SSXFF TZR");
            Environment.SetEnvironmentVariable("NLS_TIMESTAMP_TZ_FORMAT", "RR/MM/DD HH24:MI:SSXFF TZR");
        }

        private void OracleExceptionProcess(OracleException oe)
        {
            Logger.GetInstance.SaveLog(LEVEL.Error, "[ORACLE] Error, Desc:" + oe.Message);
            //if (this.EventMessage != null)
            //    this.EventMessage(9, oe.Message);

            switch (oe.Number)
            {
                case 3113:
                case 3114:
                    this.OracleDatabaseClose();
                    break;
            }
        }

        private void SetStatus(string msg)
        {
            string message = string.Empty;
            Color bgcolor = this.lbl_Status.BackColor;

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
                case "끊김": 
                    message = "끊긴 시간:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bgcolor = Color.Red;
                    break;
            }

            this.lbl_Status.InvokeToAction<string, Color>(
                delegate(string mg, Color cr)
                {
                    this.tip_Info.SetToolTip(this.lbl_Status, mg);
                    this.lbl_Status.GradientEndColor = cr;
                },
                message,
                bgcolor);
        }

        #endregion
    }
}
