using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;
using SPApi.Commons;
using SPApi.Controls.Log;
using SPApi.Declaration.Structs;
using Logger = SPApi.Controls.Log.Logger;
using SPApi.Declaration.Enums;

namespace SPApi.Controls.Sockets
{
    public partial class WsServer : UserControl
    {
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        //■■■■■■■  Delegate                                                                                              ■■■■■■■■■■
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        public delegate void delEventReceiveData(int pathIndex, string sessionID, MessageEventArgs e);
        public static event delEventReceiveData ReceiveData;                            // 수신 이벤트

        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        //■■■■■■■  Members                                                                                               ■■■■■■■■■■
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        private static WebSocketServer _webSocketServer = null;
        private static SocketView _viewer = null;
        public static int port = 5050;
        private string _servicePaths = "/ws";
        public static string[] Paths = null;
        private Thread _chkStatusThread = null;
        private bool _isStopping = false;
        private int _threadLoopCount = 0;

        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        //■■■■■■■  Properties                                                                                               ■■■■■■■■■■
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        [Browsable(false)]
        public SocketView Viewer
        {
            get { return _viewer; }
        }

        //public Dictionary<string, ClientObject> ClientList;    //클라이언트 리스트

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary> 서비스 경로를 콤마로 구분하여 최대 10개까지 등록한다. ex) "/cap,/ut,/wing" </summary>   
        public string ServicePaths
        {
            get { return _servicePaths; }
            set { _servicePaths = value.Trim(); }
        }

        public string Title
        {
            get { return slbl_Status.Text; }
            set { slbl_Status.SimpleInvoke("Text", value); }
        }

        /// <summary> 내부 WebSocketServer 객체 </summary>   
        public WebSocketServer WebSocketServer
        {
            get { return _webSocketServer; }
        }

        /// <summary> 서비스중인 Path리스트 </summary>   
        public string[] Path
        {
            get { return WsServer.Paths; }
        }

        /// <summary> 서비스Path에 연결된 클라이언트 수 </summary>   
        public int ConnectedCount(int index)
        {
            int cCount = 0;
            try
            {
                cCount = _webSocketServer.WebSocketServices[Path[index]].Sessions.Count;
            }
            catch (Exception ex) 
            {
                Logger.GetInstance.SaveLogEx(LEVEL.Error, "SPApi", $"WsServer ConnectedCount 처리중 에러발생, Desc={ex.Message}");
            }
            return cCount;
        }

        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        //■■■■■■■  함수                                                                                                  ■■■■■■■■■■
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        /// <summary>
        /// 생성자
        /// </summary>
        public WsServer()
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

        public void Start()
        {
            _isStopping = false;
            _webSocketServer = new WebSocketServer(Port);
            Paths = ServicePaths.Split(',');
            for (int i = 0; i < Paths.Length; i++)
            {
                if (i == 0) _webSocketServer.AddWebSocketService<MyWebServer0>(Paths[i]);
                else if (i == 1) _webSocketServer.AddWebSocketService<MyWebServer1>(Paths[i]);
                else if (i == 2) _webSocketServer.AddWebSocketService<MyWebServer2>(Paths[i]);
                else if (i == 3) _webSocketServer.AddWebSocketService<MyWebServer3>(Paths[i]);
                else if (i == 4) _webSocketServer.AddWebSocketService<MyWebServer4>(Paths[i]);
                else if (i == 5) _webSocketServer.AddWebSocketService<MyWebServer5>(Paths[i]);
                else if (i == 6) _webSocketServer.AddWebSocketService<MyWebServer6>(Paths[i]);
                else if (i == 7) _webSocketServer.AddWebSocketService<MyWebServer7>(Paths[i]);
                else if (i == 8) _webSocketServer.AddWebSocketService<MyWebServer8>(Paths[i]);
                else if (i == 9) _webSocketServer.AddWebSocketService<MyWebServer9>(Paths[i]);
            }
            _webSocketServer.Start();
            SetStatus(DeviceStateType.Listen);

            _chkStatusThread = new Thread(new ThreadStart(ChkStatus));
            _chkStatusThread.Start();
        }

        private void ChkStatus()
        {
            try
            {
                _threadLoopCount = 16;
                while (!_isStopping)
                {
                    _threadLoopCount++;
                    if (_threadLoopCount == 20)     // 10초마다 체크
                    {
                        _threadLoopCount = 0;
                        if (_webSocketServer.WebSocketServices.SessionCount > 0) SetStatus(DeviceStateType.Connected);
                        else SetStatus(DeviceStateType.Listen);
                    }
                    Thread.Sleep(500);
                }
            }
            catch (Exception e)
            {
                SPApi.Controls.Log.Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
        }


        /// <summary> 클라이언트에 데이터를 보냅니다. </summary>
        /// <param name="path">서비스경로 ex) /rfid </param>
        /// <param name="stringData"></param>
        public void SendBroadcasing(string path, String stringData)
        {
            try
            {
                if (_webSocketServer.WebSocketServices[path].Sessions.Count == 0) return;
                _webSocketServer.WebSocketServices[path].Sessions.Broadcast(stringData);
                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "TX",
                    LocalEndPoint = "127.0.0.1:",
                    RemoteEndPoint = "unKnown",
                    Content = stringData
                });
            }
            catch (InvalidOperationException e)
            {
                SPApi.Controls.Log.Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
            catch (Exception e)
            {
                SPApi.Controls.Log.Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
        }

        /// <summary> 클라이언트에 데이터를 보냅니다. </summary>
        /// <param name="path">서비스경로 ex) /rfid </param>
        /// <param name="stringData"></param>
        public void SendTo(string path, string sessionID, String stringData)
        {
            try
            {
                if (_webSocketServer.WebSocketServices[path].Sessions.Count == 0) return;
                _webSocketServer.WebSocketServices[path].Sessions.SendTo(stringData, sessionID);
                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "TX",
                    LocalEndPoint = "127.0.0.1:",
                    RemoteEndPoint = "sID:" + sessionID,
                    Content = stringData
                });

            }
            catch (InvalidOperationException e)
            {
                SPApi.Controls.Log.Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
            catch (Exception e)
            {
                SPApi.Controls.Log.Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
        }

        public void Stop()
        {
            _isStopping = true;
            _webSocketServer.Stop();
            SetStatus(DeviceStateType.Stop);
            SPApi.Controls.Log.Logger.GetInstance.SaveLog(LEVEL.Info, "[{0}] websocket server Stop.", Title);
        }


        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        //■■■■■■■  Display 함수                                                                                          ■■■■■■■■■■
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        private void DefaultSetting()
        {
            if (DesignMode) return;

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
                    message = "waiting for connection";
                    bgcolor = Color.Silver;
                    break;
                case DeviceStateType.Listen:
                    message = "Waiting for client connection";          // 클라이언트 연결 대기중..
                    bgcolor = Color.Yellow;
                    break;
                case DeviceStateType.Connected:
                    message = $"client connection time:{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                    bgcolor = Color.Lime;
                    break;
                case DeviceStateType.Stop:
                    message = $"Server Close Time:{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
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

        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        //■■■■■■■  메시지 수신처리                                     ■■■■■■■■■■
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        private class MyWebServer0 : WebSocketBehavior
        {
            protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
            {
                const int IDX = 0;
                if (ReceiveData != null)
                {
                    ReceiveData(IDX, base.ID, SPApi.Controls.Sockets.MessageEventArgs.make(e));
                }

                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "RX",
                    LocalEndPoint = "127.0.0.1:" + port + Paths[IDX],
                    RemoteEndPoint = "unKnown",
                    Content = e.IsBinary ? "BinaryData Received" : e.Data
                });
            }
        }
        private class MyWebServer1 : WebSocketBehavior
        {
            protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
            {
                const int IDX = 1;
                if (ReceiveData != null)
                {
                    ReceiveData(IDX, base.ID, SPApi.Controls.Sockets.MessageEventArgs.make(e));
                }

                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "RX",
                    LocalEndPoint = "127.0.0.1:" + port + Paths[IDX],
                    RemoteEndPoint = "unKnown",
                    Content = e.IsBinary ? "BinaryData Received" : e.Data
                });
            }
        }
        private class MyWebServer2 : WebSocketBehavior
        {
            protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
            {
                const int IDX = 2;
                if (ReceiveData != null)
                {
                    ReceiveData(IDX, base.ID, SPApi.Controls.Sockets.MessageEventArgs.make(e));
                }
                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "RX",
                    LocalEndPoint = "127.0.0.1:" + port + Paths[IDX],
                    RemoteEndPoint = "unKnown",
                    Content = e.IsBinary ? "BinaryData Received" : e.Data
                });
            }
        }
        private class MyWebServer3 : WebSocketBehavior
        {
            protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
            {
                const int IDX = 3;
                if (ReceiveData != null)
                {
                    ReceiveData(IDX, base.ID, SPApi.Controls.Sockets.MessageEventArgs.make(e));
                }
                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "RX",
                    LocalEndPoint = "127.0.0.1:" + port + Paths[IDX],
                    RemoteEndPoint = "unKnown",
                    Content = e.IsBinary ? "BinaryData Received" : e.Data
                });
            }
        }
        private class MyWebServer4 : WebSocketBehavior
        {
            protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
            {
                const int IDX = 4;
                if (ReceiveData != null)
                {
                    ReceiveData(IDX, base.ID, SPApi.Controls.Sockets.MessageEventArgs.make(e));
                }
                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "RX",
                    LocalEndPoint = "127.0.0.1:" + port + Paths[IDX],
                    RemoteEndPoint = "unKnown",
                    Content = e.IsBinary ? "BinaryData Received" : e.Data
                });
            }
        }
        private class MyWebServer5 : WebSocketBehavior
        {
            protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
            {
                const int IDX = 5;
                if (ReceiveData != null)
                {
                    ReceiveData(IDX, base.ID, SPApi.Controls.Sockets.MessageEventArgs.make(e));
                }
                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "RX",
                    LocalEndPoint = "127.0.0.1:" + port + Paths[IDX],
                    RemoteEndPoint = "unKnown",
                    Content = e.IsBinary ? "BinaryData Received" : e.Data
                });
            }
        }
        private class MyWebServer6 : WebSocketBehavior
        {
            protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
            {
                const int IDX = 6;
                if (ReceiveData != null)
                {
                    ReceiveData(IDX, base.ID, SPApi.Controls.Sockets.MessageEventArgs.make(e));
                }
                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "RX",
                    LocalEndPoint = "127.0.0.1:" + port + Paths[IDX],
                    RemoteEndPoint = "unKnown",
                    Content = e.IsBinary ? "BinaryData Received" : e.Data
                });
            }
        }
        private class MyWebServer7 : WebSocketBehavior
        {
            protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
            {
                const int IDX = 7;
                if (ReceiveData != null)
                {
                    ReceiveData(IDX, base.ID, SPApi.Controls.Sockets.MessageEventArgs.make(e));
                }
                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "RX",
                    LocalEndPoint = "127.0.0.1:" + port + Paths[IDX],
                    RemoteEndPoint = "unKnown",
                    Content = e.IsBinary ? "BinaryData Received" : e.Data
                });
            }
        }
        private class MyWebServer8 : WebSocketBehavior
        {
            protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
            {
                const int IDX = 8;
                if (ReceiveData != null)
                {
                    ReceiveData(IDX, base.ID, SPApi.Controls.Sockets.MessageEventArgs.make(e));
                }
                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "RX",
                    LocalEndPoint = "127.0.0.1:" + port + Paths[IDX],
                    RemoteEndPoint = "unKnown",
                    Content = e.IsBinary ? "BinaryData Received" : e.Data
                });
            }
        }
        private class MyWebServer9 : WebSocketBehavior
        {
            protected override void OnMessage(WebSocketSharp.MessageEventArgs e)
            {
                const int IDX = 9;
                if (ReceiveData != null)
                {
                    ReceiveData(IDX, base.ID, SPApi.Controls.Sockets.MessageEventArgs.make(e));
                }
                _viewer.CommunicationLog(new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = "RX",
                    LocalEndPoint = "127.0.0.1:" + port + Paths[IDX],
                    RemoteEndPoint = "unKnown",
                    Content = e.IsBinary ? "BinaryData Received" : e.Data
                });
            }
        }
    }
}
