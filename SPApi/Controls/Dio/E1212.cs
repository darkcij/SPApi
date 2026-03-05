using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using SPApi.Commons;
using SPApi.Controls.Sockets;
using SPApi.Controls.Log;
using SPApi.Declaration.Structs;
using SPApi.Declaration.Enums;

namespace SPApi.Controls.Dio
{
    public partial class E1212 : UserControl
    {
        #region RMS Member
        
        const string DEVICE_CODE = "IO1";

        #endregion

        #region Properties

        public delegate void DataReceived(bool[] bit);
        public event DataReceived OnDiChanged;
        public event DataReceived OnDoChanged;

        private ClientObject _clientObject = null;

        //Connect 타이머
        private System.Timers.Timer _tmrConnect = null;
        private bool _tmrConnectCheck = false;

        //Send 타이머
        private System.Timers.Timer _tmrSend;
        private bool _tmrSendCheck = false;

        private string _remoteIP = string.Empty;
        [Browsable(true), Category("Extended-Net-Set")]
        [DefaultValue("")]
        public string RemoteIP
        {
            get { return _remoteIP; }
            set { _remoteIP = value; }
        }

        private int _remotePort = 0;
        [Browsable(true), Category("Extended-Net-Set")]
        [DefaultValue(0)]
        public int RemotePort
        {
            get { return _remotePort; }
            set { _remotePort = value; }
        }

        private bool _clientState = false;
        [Browsable(false)]
        public bool ClientState
        {
            get { return _clientState; }
            set { _clientState = value; }
        }

        private BitArray _bitDI = null;
        private BitArray _bitDO = null;

        private bool _doSendCheck = false;
        private int _doSendData = 0;

        [Browsable(false)]
        public int SendDO 
        {
            get { return _doSendData; } 
            set
            {
                if (_clientState)
                {
                    _doSendData = value;
                    _doSendCheck = true;
                }
            }
        }

        public string Title
        {
            get { return slbl_Status.Text; }
            set { slbl_Status.Text = value; }
        }

        [Browsable(false)]
        public bool ReadAlarmDO
        {
            get { return _bitDO.Get(0); }
        }

        [Browsable(false)]
        public string ReadDi
        {
            get { return _bitDI.ToBitString(); }
        }

        [Browsable(false)]
        public string ReadDo
        {
            get { return _bitDO.ToBitString(); }
        }

        private int _sendSequence = 0;      // 0 = DI, 1 = DO, 2 = DW
        private bool _ReceiveCheck = false;

        private int _sendNextCount = 0;

        private int _sendInterval = 300;
        public int SendInterval
        {
            get { return _sendInterval; }
            set { _sendInterval = value; }
        }

        private E1212View _viewer = null;

        #endregion

        #region Constructor

        public E1212()
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
        
        /// <summary>
        /// 라벨을 클릭하면 DetailView Show
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slblStatus_Click(object sender, EventArgs e)
        {
            try
            {
                if (_viewer == null)
                    _viewer = new E1212View();
                else if (_viewer.Visible)
                    _viewer.SendToBack();

                _viewer.IP = _remoteIP;
                _viewer.PORT = _remotePort.ToString();

                _viewer.Show();
            }
            catch { }
        }

        #endregion

        #region Method

        private void DefaultSetting()
        {
            if (DesignMode) return;

            //DIO Viewer 초기화
            _viewer = new E1212View();
            _viewer.OnManualCommand += OnManualCommand;

            //Buffer 초기화
            _bitDI = new BitArray(8, false);
            _bitDO = new BitArray(8, false);

            //Connect Timer 설정
            _tmrConnect = new System.Timers.Timer(1000);
            _tmrConnect.Elapsed += Connect_Elapsed;
            _tmrConnect.AutoReset = false;
            _tmrConnectCheck = false;

            //Send Timer 설정
            _tmrSend = new System.Timers.Timer(_sendInterval);
            _tmrSend.Elapsed += Send_Elapsed;
            _tmrSend.AutoReset = false;
            _tmrSendCheck = false;

            SetStatus(DeviceStateType.Wait);
        }

        private void OnManualCommand(DioType type, int index, bool value)
        {
            switch(type)
            {
                case DioType.DO: WriteDo(index, value); break;
            }
        }

        /// <summary>
        /// 통신 연결 타이머
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (_clientObject == null) 
                    Connect();
                else if (!_clientState) 
                    Connect();
                else if (!_clientObject.workSocket.Connected) 
                    Connect();
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, ex.Message);
            }
            finally
            {
                if (_tmrConnectCheck)
                    _tmrConnect.Start();
                else
                    _tmrConnect.Stop();
            }
        }

        /// <summary>
        /// 데이터 전송 타이머
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Send_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DioCommunicationType type = DioCommunicationType.DI_READ;

            try
            {
                if (_ReceiveCheck)
                {
                    byte[] sendBytes = null;

                    if (_doSendCheck)
                    {
                        type = DioCommunicationType.DO_WRITE;

                        sendBytes = new byte[14];
                        Array.Copy(MakeSendData(type), sendBytes, 13);
                        sendBytes[13] = (byte)_doSendData;
                        
                        _doSendCheck = false;
                    }
                    else
                    {
                        switch (_sendSequence)
                        {
                            case 0:
                                type = DioCommunicationType.DI_READ;
                                sendBytes = MakeSendData(type);
                                break;

                            case 1:
                                type = DioCommunicationType.DO_READ;
                                sendBytes = MakeSendData(type);
                                break;
                        }

                        //전송 순서 변경
                        if (_sendSequence < 1)
                            _sendSequence++;
                        else
                            _sendSequence = 0;
                    }

                    SendData(type.ToString(), sendBytes);
                    _ReceiveCheck = false;
                }
                else
                {
                    if (_sendNextCount < 10)
                        _sendNextCount++;
                    else
                    { 
                        _sendNextCount = 0;
                        _ReceiveCheck = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, ex.Message);
            }
            finally
            {
                if (_tmrSendCheck)
                    _tmrSend.Start();
                else
                    _tmrSend.Stop();
            }
        }

        #endregion

        #region Tcp Communication

        private void Connect()
        {
            if (_clientState)
            {
                //소켓 서버 이미 실행중
                return;
            }

            SetStatus(DeviceStateType.Connecting);

            ConnectProcess();
        }

        private void ConnectProcess()
        {
            try
            {
                IPEndPoint remotePoint = new IPEndPoint(IPAddress.Parse(_remoteIP), _remotePort);
                _clientObject = new ClientObject();
                _clientObject.workSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _clientObject.workSocket.Connect(remotePoint);

                if (_clientObject.workSocket.Connected)
                {
                    _clientState = true;
                    _clientObject.workSocket.BeginReceive(_clientObject.buffer, 0, ClientObject.BufferSize, 0, new AsyncCallback(ReadCallback), _clientObject);
                    SetStatus(DeviceStateType.Connected);

                    //자동 전송 시작
                    AutoSendStart();
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);

                SetStatus(DeviceStateType.Disconnected);
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            ClientObject clientObj = (ClientObject)ar.AsyncState;
            DioCommunicationType type = DioCommunicationType.DI_READ;
            try
            {
                if (!_clientState)
                {
                    Disconnect();
                    return;
                }

                int bytesRead = clientObj.workSocket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    byte[] tmpByte = new byte[bytesRead];

                    Array.Copy(clientObj.buffer, tmpByte, bytesRead);

                    if(tmpByte[0].ToString("X").Equals("66") && tmpByte[7].ToString("X").Equals("66"))
                    {
                        type = DioCommunicationType.DI_READ;
                        //DI READ
                        SetDiValue(tmpByte[9]);
                    }
                    else if (tmpByte[0].ToString("X").Equals("65") && tmpByte[7].ToString("X").Equals("65"))
                    {
                        type = DioCommunicationType.DO_READ;
                        //tmpByte[9]
                        SetDoValue(tmpByte[9]);
                    }
                    else if (tmpByte[0].ToString("X").Equals("73") && tmpByte[7].ToString("X").Equals("73"))
                    {
                        type = DioCommunicationType.DO_WRITE;
                        //DO Write 성공
                    }

                    SetCommunicationLog("RX", type.ToString(), BitConverter.ToString(tmpByte).Replace('-', ' '), _viewer);

                    _clientObject.workSocket.BeginReceive(_clientObject.buffer, 0, ClientObject.BufferSize, 0, new AsyncCallback(ReadCallback), _clientObject);
                    
                    _ReceiveCheck = true;
                }
                else
                {
                    Disconnect();
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
                Disconnect();
            }
        }

        private void SetDiValue(byte value)
        {
            BitArray old = new BitArray(_bitDI);
            bool[] tmp = new bool[8];
            bool changed = false;

            for (int i = 0; i < 8; i++)
            {
                if ((value & (byte)Math.Pow(2, i)) == 0)
                {
                    _bitDI[i] = false;
                    tmp[i] = false;
                }
                else
                {
                    _bitDI[i] = true;
                    tmp[i] = true;
                }

                if (old[i] != _bitDI[i])
                    changed = true;

                if (_viewer.Visible)
                    _viewer.ChangeDIO(DioType.DI, i, _bitDI[i]);
            }

            if (OnDiChanged != null && changed)
                OnDiChanged(tmp);
        }   

        private void SetDoValue(byte value)
        {
            BitArray old = new BitArray(_bitDO);
            bool[] tmp = new bool[8];
            bool changed = false;

            for (int i = 0; i < 8; i++)
            {
                if ((value & (byte)Math.Pow(2, i)) == 0)
                {
                    _bitDO[i] = false;
                    tmp[i] = false;
                }
                else
                {
                    _bitDO[i] = true;
                    tmp[i] = true;
                }

                if (old[i] != _bitDO[i])
                    changed = true;

                if (_viewer.Visible)
                    _viewer.ChangeDIO(DioType.DO, i, _bitDO[i]);
            }

            if (OnDoChanged != null && changed)
                OnDoChanged(tmp);
        }

        private void Disconnect()
        {
            try
            {
                //Auto Send Time 정지
                AutoSendStop();

                _clientState = false;
                if (_clientObject.workSocket != null)
                {
                    _clientObject.Dispose();
                    _clientObject = null;
                }
                SetStatus(DeviceStateType.Disconnected);

                _viewer.ClearDIO();
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
        }

        private void SendData(string type, byte[] data)
        {
            try
            {
                if (_clientObject.workSocket.Connected)
                {
                    _clientObject.workSocket.Send(data);

                    SetCommunicationLog("TX", type.ToString(), BitConverter.ToString(data).Replace('-', ' '), _viewer);
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
                Disconnect();
            }
        }

         static void SetCommunicationLog(string trans, string type, string data, E1212View view)
        {
            if (view == null) return;
            if (view.IsDisposed) return;
            
            try
            {
                CommunicationMessage message = new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = trans,
                    IO_Type = type,
                    Content = data
                };

                view.CommunicationLog(message);
            }
            catch(Exception ex)
            {
                Logger.GetInstance.SaveLogEx(LEVEL.Error, "SPApi", $"E1212 SetCommunicationLog 처리중 에러발생, Desc={ex.Message}");
            }
        }

        public void WriteDoClear()
        {
            _doSendData = 0;
            _doSendCheck = true;
        }

        public void WriteDo(int index, bool value)
        {
            byte tmpByte = 0;
            BitArray tmpBits = new BitArray(_bitDO);
            tmpBits[index] = value;

            for (int i = 0; i < 8; i++)
            {
                tmpByte += (byte)(tmpBits[i] ? Math.Pow(2, i) : 0);
            }

            _doSendData = tmpByte;
            _doSendCheck = true;
        }

        public void WriteDo(int[] indexs, bool[] values)
        {
            byte tmpByte = 0;
            BitArray tmpBits = new BitArray(_bitDO);

            for (int i = 0; i < indexs.Length; i++ )
            {
                tmpBits[indexs[i]] = values[i];
            }

            for (int i = 0; i < 8; i++)
            {
                tmpByte += (byte)(tmpBits[i] ? Math.Pow(2, i) : 0);
            }

            _doSendData = tmpByte;
            _doSendCheck = true;
        }

        public void WriteDo(bool p1, bool p2, bool p3, bool p4, bool p5, bool p6, bool p7, bool p8)
        {
            byte tmpByte = 0;
            BitArray tmpBits = new BitArray(_bitDO);

            tmpBits[0] = p1;
            tmpBits[1] = p2;
            tmpBits[2] = p3;
            tmpBits[3] = p4;
            tmpBits[4] = p5;
            tmpBits[5] = p6;
            tmpBits[6] = p7;
            tmpBits[7] = p8;

            for (int i = 0; i < 8; i++)
            {
                tmpByte += (byte)(tmpBits[i] ? Math.Pow(2, i) : 0);
            }

            _doSendData = tmpByte;
            _doSendCheck = true;
        }

        /// <summary>
        /// DIO 시작
        /// </summary>
        public void Start()
        {
            _tmrConnectCheck = true;
            _tmrConnect.Start();
        }

        /// <summary>
        /// DIO 정지
        /// </summary>
        public void Stop()
        {
            _tmrConnectCheck = false;
            _tmrConnect.Stop();

            Disconnect();
        }

        /// <summary>
        /// 자동 전송 시작
        /// </summary>
        private void AutoSendStart()
        {
            _sendSequence = 0;
            _ReceiveCheck = true;
            _tmrSendCheck = true;
            _tmrSend.Start();
        }

        /// <summary>
        /// 자동 전송 정지
        /// </summary>
        private void AutoSendStop()
        {
            _tmrSendCheck = true;
            _tmrSend.Stop();
        }

        //컨트롤 상태 변경
        private void SetStatus(DeviceStateType state)
        {
            string message = string.Empty;
            Color bgcolor = slbl_Status.BackColor;

            switch (state)
            {
                case DeviceStateType.Wait:
                    message = "DIO 연결 대기";
                    bgcolor = Color.Silver;
                    break;
                case DeviceStateType.Connecting:
                    message = $"DIO 연결 시도 :{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    bgcolor = Color.Yellow;
                    break;
                case DeviceStateType.Connected:
                    message = $"DIO 연결 성공 :{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    bgcolor = Color.Lime;
                    break;
                case DeviceStateType.Disconnected:
                    message = $"DIO 연결 끊김 :{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    bgcolor = Color.Red;
                    break;
            }

            if (slbl_Status.GradientEndColor.Equals(bgcolor)) return;

            _viewer.STATE = state.ToString();

            slbl_Status.InvokeToAction<string>
            (
                delegate(string m) 
                { 
                    tip_Info.SetToolTip(slbl_Status, m); 
                }
                , message
            );

            slbl_Status.SimpleInvoke("GradientEndColor", bgcolor);

            Logger.GetInstance.SaveLog(LEVEL.Info, string.Format("[DIO] {0}", message));
        }

        private byte[] MakeSendData(DioCommunicationType type)
        {
            string tmpData = string.Empty;

            switch(type)
            {
                case DioCommunicationType.DI_READ:
                    //DI [ 66 00 00 00 00 06 01 66 00 00 00 08 ]
                    tmpData = "660000000006016600000008";
                    break;

                case DioCommunicationType.DO_READ:
                    //DO [ 65 00 00 00 00 06 01 65 00 00 00 08 ]
                    tmpData = "650000000006016500000008";
                    break;

                case DioCommunicationType.DO_WRITE:
                    //DW [ 73 00 00 00 00 08 01 73 00 00 00 08 01 (00)DO접점 ] 
                    tmpData = "73000000000801730000000801";
                    break;
            }

            return Common.GetBytesFromString(tmpData);
        }

        #endregion
    }

    public enum DioType
    {
        DI,
        DO
    }

    public enum DioCommunicationType
    {
        DI_READ,
        DO_READ,
        DI_WRITE,
        DO_WRITE
    }
}
