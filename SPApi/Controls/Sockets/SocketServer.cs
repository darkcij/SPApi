using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SPApi.Commons;
using SPApi.Controls.Log;
using System.ComponentModel;
using SPApi.Declaration.Enums;

namespace SPApi.Controls.Sockets
{
    public partial class SocketServer : UserControl
    {
        #region Delegate

        public delegate void delEventClientConnected(object sender, string ip, string handle);

        public delegate void delEventClientAllDisconnected();
        public delegate void delEventClientDisconnected(object sender, string ip, string handle);
        
        public event delEventClientConnected IsConnected;
        public event delEventClientDisconnected IsDisconnected;
        public event delEventClientAllDisconnected AllDisconnected;

        /// <summary>
        /// 수신 이벤트
        /// </summary>
        public delegate void delEventReceiveData(object sender, SockDataObject recv);
        public delegate void delEventMultiSocketReceiveData(object sender, ClientObject obj, SockDataObject recv);
        
        public event delEventReceiveData ReceiveData;
        public event delEventMultiSocketReceiveData MultiSocketReceiveData;
        
        #endregion

        #region Members
        
        private Socket _listener = null;                        //소켓 서버
        private Thread _listenThread = null;                    //소켓 서버 쓰레드

        private SocketView _viewer = null;

        public Encoding Encoder = Encoding.Unicode;

        private iProtocol _protocol = null;
        public Protocol Protocol
        {
            set
            {
                _protocol = SocketProcess.SetProtocol(value);
            }
        }

        public iProtocol CustomProtocol
        {
            set
            {
                _protocol = value;
            }
        }
        
        public bool IsGetBytesData { get; set; }

        private int _backLog = 10;

        private SocketIdentType _clientIdType = SocketIdentType.Handle;

        #endregion

        #region Properties

        [Browsable(false)]
        public SocketView Viewer
        {
            get { return _viewer; }
        }
        
        public Dictionary<string, ClientObject> ClientList;    //클라이언트 리스트

        private int _localPort = 0;                             //소켓 서버 포트 Property
        public int LocalPort
        {
            get { return _localPort; }
            set { _localPort = value; }
        }

        private bool _listenState = false;                      //소켓 서버 상태 Property
        public bool ListenState
        {
            get { return _listenState; }
            set { _listenState = value; }
        }

        public string Title
        {
            get { return slbl_Status.Text; }
            set { slbl_Status.SimpleInvoke("Text", value); }
        }

        public SocketIdentType ClientIdType 
        {
            get { return _clientIdType; }
            set { _clientIdType = value; }
        }
        

        public int BackLog
        {
            get { return _backLog; }
            set { _backLog = value; }
        }

        #endregion

        #region SocketServer Controls

        /// <summary>
        /// 생성자
        /// </summary>
        public SocketServer()
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
        
        #region TCP_COMMUNICATION

        public void Listen()
        {
            if (ListenState)
            {
                Logger.GetInstance.SaveLog(LEVEL.Info, "[{0}] 소켓 서버가 이미 실행중 입니다.", Title);
                return;
            }

            _listenThread = new Thread(new ThreadStart(ListenProcess));
            _listenThread.Start();
        }

        private void ListenProcess()
        {
            try
            {
                _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _listener.Bind(new IPEndPoint(IPAddress.Any, LocalPort));
                _listener.Listen(_backLog);

                Logger.GetInstance.SaveLog(LEVEL.Info, "[{0}] 소켓 서버 Listening...OK!", Title);

                ListenState = true;

                SetStatus(DeviceStateType.Listen);

                while (ListenState)
                {
                    Socket client = (Socket)_listener.Accept();

                    if (client.Connected)
                    {
                        ClientObject clientObject = new ClientObject();
                        clientObject.workSocket = client;
                        clientObject.workSocket.BeginReceive(clientObject.buffer, 0, ClientObject.BufferSize, 0, new AsyncCallback(ReadCallback), clientObject);

                        IPEndPoint lp = (IPEndPoint)clientObject.workSocket.LocalEndPoint;
                        IPEndPoint ep = (IPEndPoint)clientObject.workSocket.RemoteEndPoint;

                        Logger.GetInstance.SaveLog(LEVEL.Info, 
                            "[{0}] 클라이언트 연결성공!! ClientHandle: {1}, LocalAddress: {2}, RemoteAddress: {3}",
                            Title, client.Handle, lp.Address, ep.Address);

                        string key = GetClientKey(clientObject);

                        if (ClientList.ContainsKey(key))
                        {
                            ClientList[key] = clientObject;
                        }
                        else
                            ClientList.Add(key, clientObject);

                        SetStatus(DeviceStateType.Connected);

                        if(IsConnected != null)
                        {
                            IsConnected(clientObject, ep.Address.ToString(), client.Handle.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
            finally
            {
                Stop();
            }
        }

        private string GetClientKey(ClientObject client)
        {
            IPEndPoint ep = null;
            string key = string.Empty;

            switch (_clientIdType)
            {
                case SocketIdentType.Port:
                    ep = (IPEndPoint)client.workSocket.RemoteEndPoint;
                    if (ep != null) key = ep.Port.ToString();
                    break;

                case SocketIdentType.Handle:
                    key = client.workSocket.Handle.ToString();
                    break;

                case SocketIdentType.None:
                case SocketIdentType.Ip:
                default:
                    ep = (IPEndPoint)client.workSocket.RemoteEndPoint;
                    key = ep.Address.ToString();
                    break;
            }

            return key;
        }

        private void ReadCallback(IAsyncResult ar)
        {
            string message = null;
            ClientObject clientObj = (ClientObject)ar.AsyncState;
            
            string eAddr = string.Empty;

            if (clientObj == null) return;
            if (clientObj.workSocket == null) return;
            if (!clientObj.workSocket.Connected) return;
            
            try
            {
                eAddr = GetClientKey(clientObj);

                if (!ListenState)
                {
                    ClientListRemove(eAddr);
                    return;
                }

                int bytesRead = clientObj.workSocket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    if (IsGetBytesData)
                    {
                        byte[] tmp = new byte[bytesRead];
                        Array.Copy(clientObj.buffer, 0, tmp, 0, bytesRead);
                        clientObj.sb.Append(Common.GetHexBytesToHexStr(tmp));
                    }
                    else
                        clientObj.sb.Append(Encoder.GetString(clientObj.buffer, 0, bytesRead));

                    message = clientObj.sb.ToString();

                    if (_protocol.CheckReceiveData(message))
                    {
                        clientObj.BufferClear();
                        
                        SocketProcess.SetCommunicationLog(clientObj.workSocket, "RX", message, _viewer);

                        SockDataObject data = _protocol.ReceiveDataProcess(message);
                        
                        if (ReceiveData != null)
                            ReceiveData(clientObj, data);
                        else if (MultiSocketReceiveData != null)
                            MultiSocketReceiveData(this, clientObj, data);
                    }

                    clientObj.workSocket.BeginReceive(clientObj.buffer, 0, ClientObject.BufferSize, 0, new AsyncCallback(ReadCallback), clientObj);

                }
                else
                {
                    ClientListRemove(eAddr);

                    Logger.GetInstance.SaveLog(LEVEL.Info, string.Format("[{0}] 클라이언트 연결 종료", Title));
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);

                ClientListRemove(eAddr);
            }
        }

        private void ClientListRemove(string key)
        {
            try
            {
                if (!key.NoE())
                {
                    if (ClientList.ContainsKey(key))
                    {
                        ClientObject client = ClientList[key];

                        if (IsDisconnected != null)
                        {
                            IsDisconnected(client, ((IPEndPoint)client.workSocket.RemoteEndPoint).ToString(), client.workSocket.Handle.ToString());
                        }

                        ClientList.Remove(key);

                        client.Dispose();
                    }
                }

                if (ClientList.Count.Equals(0))
                {
                    SetStatus(DeviceStateType.Listen);

                    if (AllDisconnected != null)
                    {
                        AllDisconnected();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
        }

        public void Stop()
        {
            try
            {
                ListenState = false;
                if (_listener != null)
                {
                    //_listener.Shutdown(SocketShutdown.Both);
                    _listener.Close();
                    _listener = null;
                }

                //클라이언트 종료 처리
                foreach(ClientObject co in ClientList.Values)
                {
                    co.workSocket.Close();
                }

                ClientList.Clear();

                if (AllDisconnected != null)
                {
                    AllDisconnected();
                }

                SetStatus(DeviceStateType.Stop);

                Logger.GetInstance.SaveLog(LEVEL.Info, "[{0}] 소켓 서버 Listen Stop.", Title);
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
        }

        public void SendData(object sender, SockDataObject data)
        {
            try
            {
                ClientObject client = (ClientObject)sender;

                if (client.workSocket.Connected)
                {
                    string sdata = _protocol.MakeSendData(data);

                    client.workSocket.Send(Encoder.GetBytes(sdata));

                    SocketProcess.SetCommunicationLog(client.workSocket, "TX", sdata, _viewer);
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
        }

        public void BroadCasting(SockDataObject data)
        {
            try
            {
                string sdata = _protocol.MakeSendData(data);

                foreach(ClientObject obj in ClientList.Values)
                {
                    if(obj.workSocket.Connected)
                        obj.workSocket.Send(Encoder.GetBytes(sdata));

                    SocketProcess.SetCommunicationLog(obj.workSocket, "TX", sdata, _viewer);
                }
            }
            catch(Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
        }

        #endregion

        #region Methods

        private void DefaultSetting()
        {
            if (DesignMode) return;
            
            ClientList = new Dictionary<string, ClientObject>();
            ListenState = false;

            _viewer = new SocketView();
            _viewer.Title = string.Format("{0} Viewer", Title);
        }

        private void slbl_Status_Click(object sender, EventArgs e)
        {
            try
            {
                if (_viewer == null)
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
                case DeviceStateType.Listen:
                    message = "클라이언트 연결 대기중..";
                    bgcolor = Color.Yellow;
                    break;
                case DeviceStateType.Connected:
                    message = "클라이언트 연결 시간:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bgcolor = Color.Lime;
                    break;
                case DeviceStateType.Stop:
                    message = "서버 Close 시간:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
    }

}
