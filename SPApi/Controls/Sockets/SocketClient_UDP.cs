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

namespace SPApi.Controls.Sockets
{
    public partial class SocketClient_UDP : UserControl
    {
        #region Members
        
        // [ SOCKET ] //
        private Socket _client = null;              //UDP 소켓

        private byte[] _buffer = null;              //수신 데이터 버퍼
                
        // [ LOCAL ] //
        private EndPoint _localEP = null;           //Local EndPoint
        
        // [ REMOTE ] //
        private EndPoint _remoteEP = null;          //Remote EndPoint
        
        /// <summary>
        /// 연결 완료 이벤트
        /// </summary>
        public delegate void delEventConnected();
        public event delEventConnected Connected;

        /// <summary>
        /// 로그 메세지 이벤트
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        public delegate void delEventMessage(int type, string message);
        public event delEventMessage EventMessage;

        /// <summary>
        /// 수신 이벤트
        /// </summary>
        public delegate void delEventReceiveData(byte[] recv);
        public event delEventReceiveData ReceiveData;
        
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

        private int _bufferSize = 8192;
        /// <summary>
        /// 수신 버퍼 사이즈
        /// </summary>
        public int BufferSize
        {
            get { return _bufferSize; }
            set { _bufferSize = value; }
        }

        /// <summary>
        /// 로컬 IP
        /// </summary>
        private string _localHost = string.Empty;
        public string LocalHost
        {
            get { return _localHost; }
            set { _localHost = value; }
        }

        /// <summary>
        /// 로컬 포트
        /// </summary>
        private int _localPort = 0;
        public int LocalPort
        {
            get { return _localPort; }
            set { _localPort = value; }
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

        public string Title
        {
            get { return slbl_Status.Text; }
            set { slbl_Status.SimpleInvoke("Text", value); }
        }

        /// <summary>
        /// 전송 큐
        /// </summary>
        [Browsable(false)]
        public Queue<byte[]> SendQueue { get; set; }

        #endregion

        #region Constructor

        public SocketClient_UDP()
        {
            InitializeComponent();

            DefaultSetting();
        }

        #endregion

        #region FORM EVENT

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 기본 설정
        /// </summary>
        private void DefaultSetting()
        {
            //Buffer Size 초기화
            _buffer = new byte[_bufferSize];

            //SendQueue 초기화
            if (SendQueue == null) SendQueue = new Queue<byte[]>();

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
        private void SetStatus(string msg)
        {
            string message = string.Empty;
            Color bgcolor = slbl_Status.BackColor;

            switch (msg)
            {
                case "대기":
                    message = "소켓 연결 대기";
                    bgcolor = Color.Silver;
                    break;
                case "연결":
                    message = "소켓 연결 시간:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bgcolor = Color.Lime;
                    break;
                case "종료":
                    message = "소켓 Close 시간:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bgcolor = Color.Red;
                    break;
            }

            if (slbl_Status.GradientEndColor.Equals(bgcolor)) return;

            slbl_Status.InvokeToAction<string>(
                delegate (string mg)
                {
                    tip_Info.SetToolTip(slbl_Status, mg);
                },
                message);

            slbl_Status.SimpleInvoke("GradientEndColor", bgcolor);

            Logger.GetInstance.SaveLog(LEVEL.Info, "[{0}] {1}", Title, message);
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
                if (_client == null) Bind();
                else if (!_clientState) Bind();
                else if (!_client.Connected) Bind();
                
                //전송 처리
                if (!_IsSend)
                {
                    if (SendQueue.Count > 0)
                    {
                        SendTo(SendQueue.Dequeue());

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

                Thread.Sleep(100);
            }
        }

        #endregion
        
        #region UDP COMMUNICATION

        /// <summary>
        /// UDP 소켓 Binding
        /// </summary>
        public void Bind()
        {
            //중복실행 체크
            if (ClientState)
            {
                //Logger.GetInstance.SaveLog(LEVEL.Info, "소켓이 이미 바인딩 되어있습니다.");
                return;
            }

            //소켓 Binding
            BindProcess();
        }

        //Binding 처리
        private bool BindProcess()
        {
            bool bindSuccess = false;
            try
            {
                _localEP = new IPEndPoint(IPAddress.Parse(LocalHost), LocalPort);
                _remoteEP = new IPEndPoint(IPAddress.Parse(RemoteHost), RemotePort);

                _client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                _client.Bind(_localEP);
                
                _client.BeginReceiveFrom(_buffer, 0, _buffer.Length, SocketFlags.None, ref _remoteEP, ReceiveFrom, _remoteEP);

                ClientState = true;

                SetStatus("연결");

                Logger.GetInstance.SaveLog(LEVEL.Info, "[bindProcess] Socket Binding...OK!!!");

                bindSuccess = true;
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[bindProcess] Error, Desc: {0}", e.Message);
            }

            return bindSuccess;
        }

        /// <summary>
        /// UDP Socket 시작
        /// </summary>
        public void Start()
        {
            if(!_IsProcRun)
                _procThread.Start();
        }

        /// <summary>
        /// UDP Socket 정지
        /// </summary>
        public void Stop()
        {
            try
            {
                if (_client != null)
                    Disconnect();

                _IsProcRun = false;
                _procThread.Abort();

                Logger.GetInstance.SaveLog(LEVEL.Info, "Client Binding Stop!!!");

                Thread.Sleep(500);
            }
            catch(Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[Stop] Error, Desc: {0}", e.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                _client.Shutdown(SocketShutdown.Both);
                _client.Close();
                _client.Dispose();
                _client = null;

                Thread.Sleep(1000);

                ClientState = false;

                SendQueue.Clear();

                SetStatus("종료");
            }
            catch(Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[Disconnect] Error, Desc: {0}", ex.Message);
            }
        }
        
        //비동기 수신이벤트 처리
        private void ReceiveFrom(IAsyncResult ar)
        {
            try
            {
                if (!ClientState)
                    throw new Exception("소켓 Closed");

                int bufferLength = 0;
                byte[] receiveByte = null;

                bufferLength = _client.EndReceiveFrom(ar, ref _remoteEP);
                receiveByte = new byte[bufferLength];
                Array.Copy(_buffer, receiveByte, bufferLength);

                // 수신데이터 이벤트 처리
                if (ReceiveData != null)
                {
                    SocketProcess.SetCommunicationLog(_localEP, _remoteEP, "RX", Common.GetHexBytesToHexStr(receiveByte), _viewer);

                    ReceiveData(receiveByte);
                }
                
                _client.BeginReceiveFrom(_buffer, 0, _buffer.Length, SocketFlags.None, ref _remoteEP, ReceiveFrom, _remoteEP);
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[ReceiveFrom] Error, Desc: {0}", e.Message);

                Disconnect();
            }
        }

        public void SendTo(byte[] data)
        {
            try
            {
                if (ClientState)
                {
                    SocketProcess.SetCommunicationLog(_localEP, _remoteEP, "TX", Common.GetHexBytesToHexStr(data), _viewer);

                    _client.SendTo(data, _remoteEP);
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[SendTo] Error, Desc: {0}", e.Message);
            }
        }

        #endregion
    }
}
