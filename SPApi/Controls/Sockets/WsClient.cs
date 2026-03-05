using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using WebSocketSharp;
using SPApi.Commons;
using SPApi.Controls.Log;
using Logger = SPApi.Controls.Log.Logger;
using SPApi.Declaration.Structs;
using SPApi.Declaration.Enums;

namespace SPApi.Controls.Sockets
{
    public partial class WsClient : UserControl
    {
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        //■■■■■■■  Delegate                                          ■■■■■■■■■■
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        public delegate void delEventReceiveData(object sender, MessageEventArgs e);
        public event delEventReceiveData ReceiveData;                            // 수신 이벤트

        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        //■■■■■■■  Members                                           ■■■■■■■■■■
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        private WebSocket ws = null;
        private static SocketView _viewer = null;
        private string url = "ws://127.0.0.1:5050/ws";                     // ex -> ws://localhost:8080/cap
        private bool _isConnected = false;

        private String localIp = "";
        public String exceptLocalIpBand = "100;127;192;";   // 로컬IP계산시 제외할 대역
        public String priorityLocalIpBand = "";             // 로컬IP계산시 우선할 대역     

        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        //■■■■■■■  Properties                                        ■■■■■■■■■■
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        [Browsable(false)]
        public SocketView Viewer
        {
            get { return _viewer; }
        }

        /// <summary> 서버URL ex) ws://localhost:8080/cap  </summary>

        public string URL
        {
            get { return url; }
            set { url = value; }
        }

        public string Title
        {
            get { return slbl_Status.Text; }
            set { slbl_Status.SimpleInvoke("Text", value); }
        }

        public bool IsConnected
        {
            get { return _isConnected; }
        }

        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        //■■■■■■■  함수                                              ■■■■■■■■■■
        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        /// <summary>
        /// 생성자
        /// </summary>
        public WsClient()
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
            localIp = GetLocalIp();

            Thread t = new Thread(OpenConnection);
            t.Start();
            SetStatus(DeviceStateType.Wait);

        }

        private String GetLocalIp()
        {
            String returnIp = "";
            try
            {
                String[] excepts = exceptLocalIpBand.Equals("") ? null : exceptLocalIpBand.Split(';');
                String[] prioritis = priorityLocalIpBand.Equals("") ? null : priorityLocalIpBand.Split(';');
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        String tempIp = ip.ToString();
                        if (excepts != null)
                        {
                            for (int i = 0; i < excepts.Length; i++)
                            {
                                if (excepts[i].Trim().Length > 0 && tempIp.StartsWith(excepts[i]))
                                {
                                    tempIp = null; break;
                                }
                            }
                        }
                        if (prioritis != null)
                        {
                            for (int i = 0; i < prioritis.Length; i++)
                            {
                                if (prioritis[i].Trim().Length > 0 && tempIp.StartsWith(prioritis[i]))
                                {
                                    return tempIp;
                                }
                            }
                        }
                        if (tempIp != null) returnIp = tempIp;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLogEx(LEVEL.Error, "SPApi", $"WsClient GetLocalIp 처리중 에러발생, Desc={ex.Message}");
            }
            return returnIp;
        }


        private void OpenConnection()
        {
            try
            {
                ws = new WebSocket(url: url);
                ws.OnMessage += (sender, e) =>
                {
                    if (ReceiveData != null)
                    {
                        ReceiveData(ws, SPApi.Controls.Sockets.MessageEventArgs.make(e));
                    }

                    CommunicationMessage message = new CommunicationMessage()
                    {
                        Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                        State = "RX",
                        LocalEndPoint = "127.0.0.1",
                        RemoteEndPoint = url,
                        Content = e.IsBinary ? "BinaryData Received" : e.Data
                    };

                    //LogUtil.Log(0, e.Data);
                };
                ws.OnClose += (sender, e) =>
                {
                    _isConnected = false;
                    ws.Close();
                    ws = null;
                    if (!Common.IsExit)
                    {
                        Thread.Sleep(5000);
                        Thread t = new Thread(OpenConnection);
                        t.Start();
                        SetStatus(DeviceStateType.Wait);
                    }
                    else SetStatus(DeviceStateType.Stop);
                    //LogUtil.Log(0, e.Data);
                };
                ws.OnOpen += (sender, e) =>
                {
                    _isConnected = true;

                    string jstr = "{";
                    jstr = jstr + "\"datatype\" : \"" + "adminCommand" + "\"";
                    jstr = jstr + ", \"command\" : \"" + "ipInfo" + "\"";
                    jstr = jstr + ", \"ip\" : \"" + localIp + "\"";
                    jstr = jstr + "}";
                    if (ws != null)
                    {
                        ws.Send(jstr);
                        CommunicationMessage message = new CommunicationMessage()
                        {
                            Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            State = "TX",
                            LocalEndPoint = "127.0.0.1",
                            RemoteEndPoint = url,
                            Content = jstr
                        };
                    }
                    SetStatus(DeviceStateType.Connected);
                };
                ws.OnError += (sender, e) =>
                {
                    _isConnected = false;
                    SPApi.Controls.Log.Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
                    SetStatus(DeviceStateType.Stop);
                };
                ws.Connect();
            }
            catch (Exception e)
            {
                SPApi.Controls.Log.Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
        }
        /// <summary> 서버로 string 데이터를 보냅니다. </summary>
        public void Send(string sendMessage)
        {
            if (!_isConnected) return;
            ws.Send(sendMessage);
            CommunicationMessage message = new CommunicationMessage()
            {
                Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                State = "TX",
                LocalEndPoint = "127.0.0.1",
                RemoteEndPoint = url,
                Content = sendMessage
            };
        }


        public void Stop()
        {
            if (ws != null) ws.Close();
            if (!Common.IsExit)
            {
                SetStatus(DeviceStateType.Stop);
                SPApi.Controls.Log.Logger.GetInstance.SaveLog(LEVEL.Info, "[{0}] websocket client Stop.", Title);
            }
        }


        //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■
        //■■■■■■■  Display 함수                                      ■■■■■■■■■■
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

            Logger.GetInstance.SaveLog(LEVEL.Info, $"[{Title}] {message}");

        }
    }
}
