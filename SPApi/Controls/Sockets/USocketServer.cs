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

namespace SPApi.Controls.Sockets
{
    public partial class USocketServer : UserControl
    {
        #region Members

        /// <summary>
        /// 수신 이벤트
        /// </summary>
        public delegate void delEventReceiveData(object sender, SockDataObject recv);
        public delegate void delEventMultiSocketReceiveData(object sender, ClientObject obj, SockDataObject recv);

        public event delEventReceiveData ReceiveData;
        public event delEventMultiSocketReceiveData MultiSocketReceiveData;

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

        #endregion

        #region Properties
        
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

        #endregion

        #region SocketServer Controls

        /// <summary>
        /// 생성자
        /// </summary>
        public USocketServer()
        {
            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();

            DefaultSetting();
        }

        /// <summary>
        /// 컨트롤 로드 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SocketServer_Load(object sender, EventArgs e)
        {
            
            
            //SetStatus("대기");
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
                _listener.Listen(10);

                Logger.GetInstance.SaveLog(LEVEL.Info, "[{0}] 소켓 서버 Listening...OK!", Title);

                ListenState = true;

                SetStatus("연결대기");

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

                        SetStatus("연결");
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

        private void ReadCallback(IAsyncResult ar)
        {
            string message = string.Empty;
            
            try
            {
                ClientObject clientObj = (ClientObject)ar.AsyncState;
                
                if (!ListenState)
                {
                    clientObj.Dispose();
                    return;
                }

                int bytesRead = clientObj.workSocket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    clientObj.sb.Append(Encoder.GetString(clientObj.buffer, 0, bytesRead));

                    message = clientObj.sb.ToString();

                    if (_protocol.CheckReceiveData(message))
                    {   
                        SocketProcess.SetCommunicationLog(clientObj.workSocket, "RX", message, _viewer);

                        SockDataObject data = _protocol.ReceiveDataProcess(message);
                        
                        if (ReceiveData != null)
                            ReceiveData(clientObj, data);
                        else if (MultiSocketReceiveData != null)
                            MultiSocketReceiveData(this, clientObj, data);

                        clientObj.Dispose();
                    }
                    else
                        clientObj.workSocket.BeginReceive(clientObj.buffer, 0, ClientObject.BufferSize, 0, new AsyncCallback(ReadCallback), clientObj);

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
                
                SetStatus("종료");

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

        #endregion

        #region Methods

        private void DefaultSetting()
        {
            ListenState = false;
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

        private void SetStatus(string msg)
        {
            string message = string.Empty;
            Color bgcolor = slbl_Status.BackColor;

            switch (msg)
            {
                case "대기":
                    message = "연결 대기";
                    bgcolor = Color.Silver;
                    break;
                case "연결대기":
                    message = "클라이언트 연결 대기중..";
                    bgcolor = Color.Yellow;
                    break;
                case "연결":
                    message = "클라이언트 연결 시간:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bgcolor = Color.Lime;
                    break;
                case "종료":
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

            Logger.GetInstance.SaveLog(LEVEL.Info, "[{0}] {1}", Title, message);
        }

        #endregion
    }

}
