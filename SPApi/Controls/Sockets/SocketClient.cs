using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;
using SPApi.Commons;
using SPApi.Controls.Log;
using System.ComponentModel;
using SPApi.Declaration.Enums;

namespace SPApi.Controls.Sockets
{
    public partial class SocketClient : UserControl
    {
        #region Members

        /// <summary>
        /// 연결 완료 이벤트
        /// </summary>
        public delegate void delEventConnected();
        public event delEventConnected Connected;

        /// <summary>
        /// 수신 이벤트
        /// </summary>
        /// <param name="message"></param>
        public delegate void delEventReceiveData(SockDataObject recv);
        public event delEventReceiveData ReceiveData;

        /// <summary>
        /// 소켓 객체
        /// </summary>
        private ClientObject _clientObject = null;

        /// <summary>
        /// 소켓 재연결 스레드
        /// </summary>
        private Thread _procThread = null;

        /// <summary>
        /// 소켓 재연결 가능여부 체크
        /// </summary>
        private bool _IsProcRun = false;

        /// <summary>
        /// 전송 가능여부 체크
        /// </summary>
        private bool _IsSend = false;

        /// <summary>
        /// 전송 후 수신 대기 카운트
        /// (100 * MaxWaitCount)
        /// </summary>
        public int MaxWaitCount = 100;
        private int _waitCount = 0;

        private SocketView _viewer = null;

        public Encoding Encoder = Encoding.Unicode;

        private iProtocol _protocol = null;
        public Protocol Protocol
        {
            set { _protocol = SocketProcess.SetProtocol(value); }
        }

        public iProtocol CustomProtocol
        {
            set { _protocol = value; }
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// 소켓 연결 상태
        /// </summary>
        private bool _clientState = false;
        public bool ClientState
        {
            get { return _clientState; }
            set { _clientState = value; }
        }

        /// <summary>
        /// 원격지 IP
        /// </summary>
        private string _remoteHost = string.Empty;
        public string RemoteHost
        {
            get { return _remoteHost; }
            set { _remoteHost = value; }
        }

        /// <summary>
        /// 원격지 포트
        /// </summary>
        private int _remotePort = 0;
        public int RemotePort
        {
            get { return _remotePort; }
            set { _remotePort = value; }
        }

        /// <summary>
        /// 전송대기시간 (밀리세컨드)
        /// </summary>
        private int _sendwaittime = 100;
        public int SendWaitTime
        {
            get { return _sendwaittime; }
            set { _sendwaittime = value; }
        }

        public string Title
        {
            get { return slbl_Status.Text; }
            set { slbl_Status.SimpleInvoke("Text", value); }
        }

        /// <summary>
        /// 전송 큐
        /// </summary>
        [Browsable(false)]
        public Queue<SockDataObject> SendQueue { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 생성자
        /// </summary>
        public SocketClient()
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

        #region UserControl Events

        protected override void OnLoad(EventArgs e)
        {
            //기본 설정
            DefaultSetting();

            base.OnLoad(e);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// 기본 설정
        /// </summary>
        private void DefaultSetting()
        {
            if (DesignMode) return;

            _viewer = new SocketView();

            //재연결 스레드 초기화
            _procThread = new Thread(ClientProcess);
        }

        private void slbl_Status_Click(object sender, EventArgs e)
        {
            try
            {
                if (_viewer == null || _viewer.IsDisposed)
                {
                    _viewer = new SocketView();
                    _viewer.Title = string.Format("{0} Viewer", Title);
                }
                else if (_viewer.Visible)
                    _viewer.SendToBack();

                _viewer.Show();
            }
            catch { }
        }

        /// <summary>
        /// 컨트롤 상태 설정
        /// </summary>
        /// <param name="msg"></param>
        private void SetStatus(DeviceStateType state)
        {
            string message = string.Empty;
            Color bgcolor = slbl_Status.BackColor;

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

            if (slbl_Status.GradientEndColor.Equals(bgcolor)) return;

            slbl_Status.InvokeToAction<string>(
                delegate(string mg)
                {
                    tip_Info.SetToolTip(slbl_Status, mg);
                },
                message);

            slbl_Status.SimpleInvoke("GradientEndColor", bgcolor);

            Logger.GetInstance.SaveLog(LEVEL.Info, $"[{Title}] {message}");

        }

        #endregion

        #region Process Thread

        /// <summary>
        /// 소켓 프로세스(재연결, 전송)
        /// </summary>
        private void ClientProcess()
        {
            _IsProcRun = true;
            _waitCount = 0;
            _IsSend = false;

            while (_IsProcRun)
            {
                //재연결 처리
                if (_clientObject == null) Connect();
                else if (!_clientState) Connect();
                else if (!_clientObject.workSocket.Connected) Connect();

                
                //전송 처리
                if (!_IsSend)
                {
                    if (SendQueue.Count > 0)
                    {
                        SendData(SendQueue.Dequeue());

                        _waitCount = 0;
                        _IsSend = true;

                        System.Diagnostics.Debug.WriteLine("SEND_QUEUE_COUNT: " + SendQueue.Count.ToString());
                    }
                }
                else
                {
                    if (MaxWaitCount > _waitCount)
                        _waitCount++;
                    else
                    {
                        _waitCount = 0;
                        _IsSend = false;
                    }
                }

                Thread.Sleep(SendWaitTime);
            }
        }

        #endregion

        #region TCP_COMMUNICATION

        private void Connect()
        {
            if (_clientState)
            {
                //소켓 서버 이미 실행중
                return;
            }

            ConnectProcess();
        }

        private void ConnectProcess()
        {
            try
            {
                _clientObject = new ClientObject();
                _clientObject.workSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress rAddr = null;
                if (IPAddress.TryParse(_remoteHost, out rAddr))
                    _clientObject.workSocket.Connect(new IPEndPoint(rAddr, _remotePort));
                else
                    _clientObject.workSocket.Connect(_remoteHost, _remotePort);
                
                Thread.Sleep(500);

                if (_clientObject.workSocket.Connected)
                {
                    IPEndPoint lp = (IPEndPoint)_clientObject.workSocket.LocalEndPoint;
                    IPEndPoint ep = (IPEndPoint)_clientObject.workSocket.RemoteEndPoint;

                    Logger.GetInstance.SaveLog(LEVEL.Info,
                        "[{0}] 서버 연결. LocalAddress: {1}, RemoteAddress: {2}", Title, lp.Address, ep.Address);

                    _clientState = true;

                    _clientObject.workSocket.BeginReceive(_clientObject.buffer, 0, ClientObject.BufferSize, 0, new AsyncCallback(ReadCallback), _clientObject);

                    SetStatus(DeviceStateType.Connected);

                    if (Connected != null)
                        Connected();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("[TCP Client - ConnectProcess] Error!!!, Desc:" + e.Message);

                SetStatus(DeviceStateType.Disconnected);
            }
        }

        private void ReadCallback(IAsyncResult ar)
        {
            string message = null;
            ClientObject clientObj = (ClientObject)ar.AsyncState;

            try
            {
                if (!_clientState)
                {
                    //Disconnect();
                    return;
                }

                int bytesRead = clientObj.workSocket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    clientObj.sb.Append(Encoder.GetString(clientObj.buffer, 0, bytesRead));

                    message = clientObj.sb.ToString();

                    if (_protocol.CheckReceiveData(message))
                    {
                        clientObj.BufferClear();

                        if (ReceiveData != null)
                        {
                            SocketProcess.SetCommunicationLog(clientObj.workSocket, "RX", message, _viewer);
                            
                            SockDataObject data = _protocol.ReceiveDataProcess(message);
                            
                            ReceiveData(data);

                            _IsSend = false;
                        }
                    }
                    
                    //System.Diagnostics.Debug.WriteLine("Client Object Length:" + clientObj.sb.Length.ToString());

                    _clientObject.workSocket.BeginReceive(_clientObject.buffer, 0, ClientObject.BufferSize, 0, new AsyncCallback(ReadCallback), _clientObject);
                }
                else
                {
                    Disconnect();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("[TCP Client - ReadCallback] Error!!!, Desc:" + e.Message);
                Disconnect();
            }
        }

        private void Disconnect()
        {
            try
            {
                _clientState = false;

                if (_clientObject.workSocket != null)
                {
                    _clientObject.Dispose();
                }

                SendQueue.Clear();

                SetStatus(DeviceStateType.Disconnected);

                Thread.Sleep(500);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("[TCP Client - DisConnect] Error!!!, Desc:" + e.Message);
            }
        }

        public void SendData(SockDataObject data)
        {
            try
            {
                if (_clientObject.workSocket.Connected)
                {
                    string sdata = _protocol.MakeSendData(data);
                    
                    _clientObject.workSocket.Send(Encoder.GetBytes(sdata));
                    

                    SocketProcess.SetCommunicationLog(_clientObject.workSocket, "TX", sdata, _viewer);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("[TCP Client - SendData] Error!!!, Desc:" + e.Message);
                Disconnect();
            }
        }

        public void SendData(byte[] data)
        {
            try
            {
                if (_clientObject.workSocket.Connected)
                {
                    _clientObject.workSocket.Send(data);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("[TCP Client - SendData] Error!!!, Desc:" + e.Message);
                Disconnect();
            }
        }

        public void Start()
        {
            _procThread.Start();
        }

        public void Stop()
        {
            try
            {
                _IsProcRun = false;
                _procThread.Abort();
                Disconnect();
            }
            catch
            {

            }
        }

        #endregion
    }
}
