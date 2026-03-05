using SPApi.Commons;
using SPApi.Controls.Log;
using SPApi.Controls.Sockets;
using SPApi.Declaration.Enums;
using SPApi.Declaration.Structs;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SPApi.Controls.ULink
{
    public partial class ULinkClient : UserControl
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
        public delegate void delEventReceiveData(TAG tag);
        public event delEventReceiveData ReceiveData;


        /// <summary>
        /// ULink Parser Type
        /// </summary>
        public ULINK_PARSER_TYPE ULinkParser { get; set; }

        /// <summary>
        /// 수신 처리 프로세스
        /// </summary>
        private IULinkResponse _recvProcess;

        /// <summary>
        /// 수동 수신 처리 프로세스
        /// </summary>
        /// <remarks>ULinkParser.Manual 선택필요</remarks>
        public IULinkResponse ManualRecvProcess { get; set; }

        /// <summary>
        /// 소켓 객체
        /// </summary>
        private ClientObject _clientObject = null;

        /// <summary>
        /// 소켓 재연결 스레드
        /// </summary>
        private Thread _reConnectThread = null;

        /// <summary>
        /// 소켓 재연결 가능여부 체크
        /// </summary>
        private bool _IsReConnectRun = false;

        #endregion

        #region Properties

        public string Title
        {
            get { return slbl_Status.Text; }
            set { slbl_Status.Text = value; }
        }

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
        private string _remoteIP = string.Empty;
        public string RemoteIP
        {
            get { return _remoteIP; }
            set { _remoteIP = value; }
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

        private ULinkView _viewer = null;

        #endregion

        #region Constructor

        /// <summary>
        /// 생성자
        /// </summary>
        public ULinkClient()
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
            DefaultSetting();

            base.OnLoad(e);
        }

        #endregion

        #region Control Events

        private void slbl_Status_Click(object sender, EventArgs e)
        {
            try
            {
                if (_viewer == null || _viewer.IsDisposed)
                    _viewer = new ULinkView();
                else if (_viewer.Visible)
                    _viewer.SendToBack();

                _viewer.Show();
            }
            catch { }
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// 기본 설정
        /// </summary>
        private void DefaultSetting()
        {
            if (DesignMode) return;

            Init();

            _viewer = new ULinkView();

            _reConnectThread = new Thread(Reconnect);
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

            Logger.GetInstance.SaveLog(LEVEL.Info, string.Format("[ULINK] {0}", message));
        }

        #endregion

        #region ReConnect Thread

        /// <summary>
        /// 재연결 처리
        /// </summary>
        private void Reconnect()
        {
            _IsReConnectRun = true;

            while (_IsReConnectRun)
            {
                if (_clientObject == null) Connect();
                else if (!_clientState) Connect();
                else if (!_clientObject.workSocket.Connected) Connect();

                Thread.Sleep(5000);
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
                IPEndPoint remotePoint = new IPEndPoint(IPAddress.Parse(_remoteIP), _remotePort);
                _clientObject = new ClientObject();
                _clientObject.workSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _clientObject.workSocket.Connect(remotePoint);

                if (_clientObject.workSocket.Connected)
                {
                    _clientState = true;
                    _clientObject.workSocket.BeginReceive(_clientObject.buffer, 0, ClientObject.BufferSize, 0, new AsyncCallback(ReadCallback), _clientObject);
                    SetStatus(DeviceStateType.Connected);

                    if (Connected != null)
                        Connected();
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "Desc:" + e.Message);
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
                    clientObj.sb.Append(Encoding.ASCII.GetString(clientObj.buffer, 0, bytesRead));
                    
                    message = clientObj.GetData();

                    SetCommunicationLog("RX", message);

                    TAG[] t = _recvProcess.ULinkRecvProcess(message);

                    if (ReceiveData != null && t.Length > 0)
                        for (int i = 0; i < t.Length; i++)
                            ReceiveData(t[i]);
                        
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
                Logger.GetInstance.SaveLog(LEVEL.Error, "Desc:" + e.Message);
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
                SetStatus(DeviceStateType.Disconnected);
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "Desc:" + e.Message);
            }
        }

        public void SendData(string data)
        {
            try
            {
                if (_clientObject.workSocket.Connected)
                {
                    _clientObject.workSocket.Send(Encoding.ASCII.GetBytes(data));

                    SetCommunicationLog("TX", data);
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "Desc:" + e.Message);

                Disconnect();
            }
        }

        public void Start()
        {
            Init();

            if (_reConnectThread.ThreadState == ThreadState.Unstarted)
            {
                _reConnectThread.Start();
            }
            else
            {
                //재연결 스레드 초기화
                _reConnectThread = new Thread(Reconnect);
                _reConnectThread.Start();
            }
        }

        public void Stop()
        {
            _IsReConnectRun = false;
            _reConnectThread.Abort();
            Disconnect();
        }

        /// <summary>
        /// 초기화
        /// </summary>
        public void Init()
        {
            //수신 프로세스 초기화
            IULinkResponse iULink = null;

            switch (ULinkParser)
            {
                case ULINK_PARSER_TYPE.KGJ_ULink:
                    iULink = new KgjULinkParser();
                    break;
                
                case ULINK_PARSER_TYPE.Manual:
                    iULink = ManualRecvProcess;
                    break;
                
                case ULINK_PARSER_TYPE.KGS_ULink:
                case ULINK_PARSER_TYPE.NONE:
                default:
                    iULink = new KgsULinkParser();
                    break;
            }

            _recvProcess = iULink;
        }

        private void SetCommunicationLog(string trans, string data)
        {
            if (_viewer == null) return;
            if (_viewer.IsDisposed) return;

            if (!ControlCommon.FormNullCheck(_viewer))
            {
                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = trans,
                    Content = data
                });
            }           
        }

        #endregion
    }
}
