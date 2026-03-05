using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using OPCAutomation;
using SPApi.Commons;
using SPApi.Controls.Log;
using System.Text;
using SPApi.Declaration.Structs;
using SPApi.Declaration.Enums;

namespace SPApi.Controls.Plc.OpcServer
{
    public partial class Opc : UserControl
    {
        #region Enum

        public enum OPC_SERVER_ID
        {
            None = 0,
            Kepware_KEPServerEX_V4, 
            Kepware_KEPServerEX_V5,
            Takebishi_Dxp_1,
            Takebishi_Dxp_2,
            Takebishi_Melsec_1,
            Takebishi_Melsec_2,
            OPC_SimaticNET,
            Manual
        }

        #endregion

        #region Members
        public delegate void DelAsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors);
        public delegate void DelAsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors);
        public delegate void DelDataChangeComplete(int TransactionID, int GroupHandle, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps);
        public event DelAsyncReadComplete OnAsyncReadComplete;
        public event DelAsyncWriteComplete OnAsyncWriteComplete;
        public event DelDataChangeComplete OnDataChangeCompete;

        private string _title = "OPC";

        //Log
        private Logger log = Logger.GetInstance;

        //OPC 서버 선언
        private OPCServer _opcServer = null;
        
        //OPC 그룹 선언
        private OPCGroup[] _opcGroup = null;

        //OPC 서버 핸들 선언
        private Array[] _serverHandles = null;

        //OPC 체크 타이머
        private System.Timers.Timer _tmrOpc = null;
        private bool _tmrOpcCheck;

        #endregion

        #region Singleton

        ////인스턴스
        //private static Opc Instance;
        //public static Opc GetInstance
        //{
        //    get
        //    {
        //        if (Instance == null)
        //        {
        //            Instance = new Opc();
        //        }
        //        return Instance;
        //    }
        //}

        #endregion

        #region Properties

        //Title
        public string Title 
        {
            get { return _title; }
            set 
            {
                _title = value;
                slbl_Title.Text = _title; 
            }
        }

        //OPC 연결상태
        [Browsable(false)]
        public bool OpcServerConnected { get; set; }

        //OPC 서버 이름
        public OPC_SERVER_ID OpcServerName { get; set; }

        //OPC 서버 수동입력
        public string ManualServerName { get; set; }

        //OPC 노드 이름
        [Browsable(false)]
        public string OpcNodeName { get; set; }

        //OPC GROUP
        [Browsable(false)]
        public OPC_GROUP[] Groups { get; set; }

        #endregion

        #region Opc Control

        /// <summary>
        /// 생성자
        /// </summary>
        public Opc()
        {
            //Instance = this;

            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!DesignMode)
            {
                //기본설정
                DefaultSetting();
            }

            base.OnLoad(e);
        }
                
        #endregion

        #region Methods

        /// <summary>
        /// 기본설정
        /// </summary>
        private void DefaultSetting()
        {
            OpcServerConnected = false;
            
            tmrCheckInit();
        }
        
        /// <summary>
        /// 컨트롤 상태 변경
        /// </summary>
        private void SetStatus(DeviceStateType state)
        {
            string message = string.Empty;
            Color bgcolor = slbl_Title.BackColor;

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

            if (slbl_Title.GradientEndColor.Equals(bgcolor)) return;

            slbl_Title.SimpleInvoke("GradientEndColor", bgcolor);

            slbl_Title.InvokeToAction<string>
            (
                delegate(string m)
                {
                    tip_Info.SetToolTip(slbl_Title, m);
                }
                , message
            );

            log.SaveLog(LEVEL.Info, $"[OPC SERVER] {message}");

        }

        #endregion

        #region OPC TIMER

        /// <summary>
        /// 체크 타이머 초기화
        /// </summary>
        private void tmrCheckInit()
        {
            _tmrOpcCheck = false;

            _tmrOpc = new System.Timers.Timer(10000);
            _tmrOpc.AutoReset = false;
            _tmrOpc.Elapsed += tmrOpc_Elapsed;
            _tmrOpc.Stop();
        }

        /// <summary>
        /// 체크 타이머 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrOpc_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (!_tmrOpcCheck)
                    return;

                if (_opcServer == null)
                {
                    SetStatus(DeviceStateType.Disconnected);
                    Connect();
                    return;
                }

                if (!_opcServer.ServerState.Equals(1))
                {
                    SetStatus(DeviceStateType.Disconnected);
                    Connect();
                    return;
                }

                ////for (int i = 0; i < Groups.Length; i++)
                ////{
                ////    SyncRead(i);
                ////}

                //Global.FirstCheck = true;
                
            }
            catch (Exception ex)
            {
                log.SaveLog(LEVEL.Error, "연결이 끊어졌습니다. Desc=" + ex.Message);
                Disconnect();
            }
            finally
            {
                if (_tmrOpcCheck)
                    _tmrOpc.Start();
            }
        }

        

        #endregion

        #region OPC Communication

        //OPC 서버 자동연결
        public void Start()
        {
            if (_opcServer == null)
                _opcServer = new OPCServer();

            _tmrOpcCheck = true;

            Connect();

            _tmrOpc.Start();
        }

        //OPC 서버 자동연결 정지
        public void Stop()
        {
            _tmrOpcCheck = false;

            _tmrOpc.Stop();
            
            Disconnect();
        }

        //OPC 연결
        private void Connect()
        {
            try
            {
                Disconnect();

                _opcServer = new OPCServer();
                _opcServer.ServerShutDown += _opcServer_ServerShutDown;

                string srvNm = string.Empty;
                if (OpcServerName != OPC_SERVER_ID.Manual)
                    srvNm = OpcServerName.ToString().Replace('_', '.');
                else
                    srvNm = ManualServerName;

                object allOPCServers = _opcServer.GetOPCServers();

                Array list = (Array)allOPCServers;
                bool success = false;

                for (int i = 1; i <= list.Length; i++)
                {
                    if (list.GetValue(i).ToString().Equals(srvNm))
                    {
                        success = true;
                        break;
                    }
                }

                if (!success)
                {
                    log.SaveLog(LEVEL.Error, "OPC 서버를 찾을 수 없습니다.");
                    return;
                }

                _opcServer.Connect(srvNm, OpcNodeName);

                if (!_opcServer.ServerState.Equals(1))
                    Disconnect();

                //Group 정보 체크
                if (Groups == null)
                {
                    log.SaveLog(LEVEL.Error, "OPC Group 정보를 찾을 수 없습니다.");
                    return;
                }

                //Group 등록
                SetOPCGroup();

                //Items 등록
                OpcItemsAppend();

                OpcServerConnected = true;

                SetStatus(DeviceStateType.Connected);
            }
            catch (Exception ex)
            {
                log.SaveLog(LEVEL.Error,"OPC server connect failed with exception: " + ex.Message);
            }
        }

        private void _opcServer_ServerShutDown(string Reason)
        {
            //Disconnect();
        }

        //OPC 연결끊기
        private void Disconnect()
        {
            try
            {
                OpcGroupsClear();

                _opcServer.Disconnect();

                OpcServerConnected = false;
            }
            catch (Exception ex)
            {
                log.SaveLog(LEVEL.Error,"OPC Server Disconnect failed with exception: " + ex.Message);
            }
            finally
            {
                _opcGroup = null;
                _opcServer = null;

                SetStatus(DeviceStateType.Disconnected);
            }
        }

        //OPC Group 설정
        private void SetOPCGroup()
        {
            _opcServer.OPCGroups.DefaultGroupIsActive = false;
            _opcServer.OPCGroups.DefaultGroupDeadband = 0f;

            int grpCnt = Groups.Length;

            _opcGroup = new OPCGroup[grpCnt];
            
            
            for (int i = 0; i < grpCnt; i++)
            {
                _opcGroup[i] = _opcServer.OPCGroups.Add(Groups[i].Name);
                _opcGroup[i].UpdateRate = Groups[i].UpdateRate;
                _opcGroup[i].IsSubscribed = Groups[i].IsSubscribed;
                _opcGroup[i].IsActive = Groups[i].IsActive;
                _opcGroup[i].AsyncReadComplete += Opc_AsyncReadComplete;
                _opcGroup[i].AsyncWriteComplete += Opc_AsyncWriteComplete;

                if (Groups[i].IsDataChange)
                {
                    _opcGroup[i].DataChange += Groups[i].DataChange;
                    Groups[i].OnDataChange += OPCGroups_DataChange;
                }        
            }
            _serverHandles = new Array[grpCnt];
        }
        
        private int GetOpcGroupIndex(string n)
        {
            int idx = n.Right(1).ToInt() - 1;

            string tmp = n.Left(1);

            if (tmp.Equals("B")) idx += 6;
            else if (tmp.Equals("C")) idx += 12;

            return idx;
        }

        private void Opc_AsyncWriteComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array Errors)
        {
            if (OnAsyncWriteComplete != null)
                OnAsyncWriteComplete(TransactionID, NumItems, ref ClientHandles, ref Errors);
        }

        private void Opc_AsyncReadComplete(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps, ref Array Errors)
        {
            if (OnAsyncReadComplete != null)
                OnAsyncReadComplete(TransactionID, NumItems, ref ClientHandles, ref ItemValues, ref Qualities, ref TimeStamps, ref Errors);
        }

        private void OPCGroups_DataChange(int TransactionID, int GroupHandle, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            int gidx = GroupHandle;

            ChangeOpcItems(gidx, NumItems, ClientHandles, ItemValues, Qualities, TimeStamps);

            if (OnDataChangeCompete != null)
                OnDataChangeCompete(TransactionID, GroupHandle, NumItems, ref ClientHandles, ref ItemValues, ref Qualities, ref TimeStamps);
        }
        
        //OPC ID 생성
        private void MakeOpcIds()
        {
            int grpCnt = Groups.Length;

            for (int g = 0; g < grpCnt; g++)
            {
                int itemCnt = Groups[g].Items.Length;
                Groups[g].ClientHandle = new int[itemCnt];
                Groups[g].IDs = new string[itemCnt];

                for (int i = 0; i < itemCnt; i++)
                {
                    Groups[g].ClientHandle[i] = Groups[g].Items[i].ClientHandle;
                    Groups[g].IDs[i] = Groups[g].Items[i].OPCItemId;
                }
            }
        }

        //OPC Items 등록
        private void OpcItemsAppend()
        {
            try
            {
                if (_opcGroup != null)
                {
                    MakeOpcIds();

                    Array addItemServerErrors;

                    int grpCnt = _opcGroup.Length;

                    for (int g = 0; g < grpCnt; g++)
                    {
                        _opcGroup[g].OPCItems.DefaultIsActive = true;
                        _opcGroup[g].OPCItems.AddItems(Groups[g].Items.Length - 1,
                                                        Groups[g].IDs,
                                                        Groups[g].ClientHandle,
                                                        out _serverHandles[g],
                                                        out addItemServerErrors);
                    }
                }

            }
            catch (Exception ex)
            {
                log.SaveLog(LEVEL.Error, "OPC server add items failed with exception: " + ex.Message);
            }
        }

        //OPC Items 삭제
        private void OpcGroupsClear()
        {
            try
            {
                if (_opcGroup != null)
                {
                    for (int i = 0; i < _opcGroup.Length; i++)
                    {
                        _opcGroup[i].IsActive = false;
                        _opcGroup[i].IsSubscribed = false;
                    }

                    _opcServer.OPCGroups.RemoveAll();
                }

            }
            catch (Exception ex)
            {
                log.SaveLog(LEVEL.Error, "OPC server clear items failed with exception: " + ex.Message);
            }
        }

        /// <summary>
        /// 그룹 데이터 조회
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public string[] GetDatas(int group)
        {
            int itemCount = Groups[group].Items.Length;

            string[] itemValues = new string[itemCount-1];

            for(int i = 1; i< itemCount; i++)
            {
                itemValues[i - 1] = Groups[group].Items[i].OPCItemValue;
            }

            return itemValues;
        }

        /// <summary>
        /// 비동기 읽기
        /// </summary>
        private void AsyncRead(int group)
        {

        }

        /// <summary>
        /// 전체 그룹 비동기 읽기
        /// </summary>
        private void ASyncAllRead()
        {

        }

        /// <summary>
        /// 비동기 Items 쓰기
        /// </summary>
        public bool ASyncItemWrite(int group, int count, int[] items, Array values, int tid, out int cancelID)
        {
            bool success = false;
            cancelID = -99;

            try
            {
                int cid;
                Array opcError;
                OneBasedArray<int> srvHandles = new OneBasedArray<int>(count);
                OneBasedArray<object> vals = new OneBasedArray<object>(count);

                for (int i = 0; i < count; i++)
                {
                    srvHandles[i + 1] = _serverHandles[group].GetValue(items[i]).ToInt();
                    vals[i + 1] = values.GetValue(i);
                }

                _opcGroup[group].AsyncWrite(count,
                                            srvHandles.ToArray(),
                                            vals.ToArray(),
                                            out opcError,
                                            tid,
                                            out cid);

                cancelID = cid;
                
                success = true;
            }
            catch (Exception ex)
            {
                log.SaveLog(LEVEL.Error, "Opc ASync Items Write Error!!Desc:" + ex.Message);
                Disconnect();
            }

            return success;
        }

        /// <summary>
        /// 비동기 그룹 쓰기
        /// </summary>
        public bool ASyncGroupWrite(int group, Array values, int tid)
        {
            bool success = false;

            try
            {
                int cid;
                Array opcError;

                _opcGroup[group].AsyncWrite(Groups[group].Items.Length - 1,
                                            ref _serverHandles[group],
                                            ref values,
                                            out opcError,
                                            tid,
                                            out cid);
                
                success = true;
            }
            catch (Exception ex)
            {
                log.SaveLog(LEVEL.Error, "Opc ASync Group Write Error!!, Desc:" + ex.Message);
                Disconnect();
            }

            return success;
        }

        /// <summary>
        /// 동기 읽기
        /// </summary>
        public void SyncRead(int group)
        {
            try
            {
                Array opcValue;
                Array opcError;
                object opcQt;
                object opcTime;

                //Group 읽기               
                _opcGroup[group].SyncRead((short)OPCDataSource.OPCDevice, 
                                                Groups[group].Items.Length - 1,
                                                ref _serverHandles[group],
                                                out opcValue, 
                                                out opcError, 
                                                out opcQt, 
                                                out opcTime);

                //ItemValues 갱신
                ChangeOpcItems(group,
                                Groups[group].Items.Length - 1,
                                Groups[group].ClientHandle,
                                opcValue,
                                (Array)opcQt,
                                (Array)opcTime);
            }
            catch (Exception ex)
            {
                log.SaveLog(LEVEL.Error, 
                    string.Format("Opc Sync Read Error!!, Group:{0}, Desc:{1}", group, ex.Message));
                //Disconnect();
            }
        }

        public void SyncAllRead()
        {
            for (int i = 0; i < Groups.Length; i++)
            {
                SyncRead(i);
            }
        }
        
        /// <summary>
        /// 동기 Items 쓰기
        /// </summary>
        public bool SyncItemWrite(int group, int count, int[] items, Array values)
        {
            bool success = false;
            
            try
            {
                Array opcError;
                OneBasedArray<int> srvHandles = new OneBasedArray<int>(count);
                OneBasedArray<object> vals = new OneBasedArray<object>(count);
                               
                _opcGroup[group].IsActive = false;
                _opcGroup[group].IsSubscribed = false;

                for (int i = 0; i < count; i++)
                {
                    srvHandles[i + 1] = _serverHandles[group].GetValue(items[i]).ToInt();
                    vals[i + 1] = values.GetValue(i);
                }
                
                _opcGroup[group].SyncWrite(count,
                                           srvHandles.ToArray(),
                                           vals.ToArray(),
                                           out opcError);

                success = true;

            }
            catch (Exception ex)
            {
                log.SaveLog(LEVEL.Error, "Opc Sync Items Write Error!!Desc:" + ex.Message);
                Disconnect();
            }
            finally
            {
                _opcGroup[group].IsActive = true;
                _opcGroup[group].IsSubscribed = true;
            }

            return success;
        }

        /// <summary>
        /// 동기 그룹 쓰기
        /// </summary>
        public bool SyncGroupWrite(int group, Array values)
        {
            bool success = false;

            try
            {
                Array opcError;

                _opcGroup[group].IsActive = false;
                _opcGroup[group].IsSubscribed = false;

                _opcGroup[group].SyncWrite(Groups[group].Items.Length - 1,
                                            ref _serverHandles[group],
                                            ref values,
                                            out opcError);

                success = true;

            }
            catch (Exception ex)
            {
                log.SaveLog(LEVEL.Error, "Opc Sync Group Write Error!!, Desc:" + ex.Message);
                Disconnect();
            }
            finally
            {
                _opcGroup[group].IsActive = true;
                _opcGroup[group].IsSubscribed = true;
            }

            return success;
        }

        /// <summary>
        /// Item 통신 상태 확인
        /// </summary>
        /// <param name="qt"></param>
        /// <returns></returns>
        private string GetOpcItemQuality(int qt)
        {
            string tmpQt = string.Empty;

            switch(qt)
            {
                case 0: tmpQt = "Bad"; break;           //통신불가
                case 64: tmpQt = "Uncertain"; break;    //불확실
                case 192: tmpQt = "Good"; break;        //정상
            }

            return tmpQt;
        }

        /// <summary>
        /// OpcItemValue 갱신
        /// </summary>
        /// <param name="numItems"></param>
        /// <param name="clientHandles"></param>
        /// <param name="itemValues"></param>
        private void ChangeOpcItems(int group, 
                                    int numItems, 
                                    Array clientHandles, 
                                    Array itemValues,
                                    Array qualitys,
                                    Array dates)
        {
            int index = 0;
            Array tmps = null;
            StringBuilder tmp = new StringBuilder();

            for (int i = 1; i <= numItems; i++)
            {
                index = (int)clientHandles.GetValue(i);
                tmp.Clear();
                
                if(itemValues.GetValue(i).ChkType(typeof(short[])))
                {
                    tmps = (short[])itemValues.GetValue(i);
                    for (int a = 0; a < tmps.Length; a++)
                        tmp.AppendFormat("{0},", tmps.GetValue(a).ToString());
                    tmp = tmp.Remove(tmp.Length - 1, 1);
                }
                else if (itemValues.GetValue(i).ChkType(typeof(bool[])))
                {
                    tmps = (bool[])itemValues.GetValue(i);
                    for (int a = 0; a < tmps.Length; a++)
                        tmp.AppendFormat("{0},", tmps.GetValue(a).ToString());
                    tmp = tmp.Remove(tmp.Length - 1, 1);
                }
                else
                    tmp.Append(itemValues.GetValue(i).ToString());

                Groups[group].Items[index].OPCItemValue = tmp.ToString();
                Groups[group].Items[index].OPCQuality = GetOpcItemQuality(qualitys.GetValue(i).ToInt());
                Groups[group].Items[index].TimeStamp = ConvertToLocalTime(dates.GetValue(i));

                //Logger.GetInstance.SaveLog(LEVEL.Plc,
                //    string.Format("Group:{0}, Item:{1}, Name:{2}, Value:{3}, Qlty:{4}", 
                //        group, 
                //        index, 
                //        Groups[group].Items[index].OPCItemName, 
                //        Groups[group].Items[index].OPCItemValue, 
                //        Groups[group].Items[index].OPCQuality));
            }
        }

        private string ConvertToLocalTime(object date)
        {
            DateTime dt;

            if (DateTime.TryParse(date.ToString(), null, System.Globalization.DateTimeStyles.None, out dt))
                dt = dt.ToLocalTime();
            else
                dt = DateTime.Now;

            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        #endregion

        private void slbl_Status_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpcView v = new OpcView())
                {
                    v.Title = _title;
                    v.OpcServer = this;
                    v.ShowDialog();
                }
            }
            catch { }
        }
    }
}
