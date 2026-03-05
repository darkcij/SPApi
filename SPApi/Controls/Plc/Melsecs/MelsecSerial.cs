using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.IO.Ports;
using SPApi.Commons;
using SPApi.Controls.Log;
using SPApi.Controls.Plc.Melsecs.MCP;

namespace SPApi.Controls.Plc.Melsecs
{
    public partial class MelsecSerial : UserControl
    {
        #region Members

        public delegate void delEventCommonucationData(string TxRx, string message);
        public event delEventCommonucationData OnCommunicationLog;

        public delegate void delEventReceiveData(CommObject recvData);
        public event delEventReceiveData OnReceiveData;

        public delegate void delEventPortEnable(bool enable);
        public event delEventPortEnable OnPortEnable;

        //SERIAL
        private SerialPort _port = null;                //시리얼 포트

        //VIEWER
        private MelsecSerialView _viewer = null;        //뷰어

        //THREAD
        private Thread _sendThread = null;          //Send Thread
        private bool _sendThreadState = false;      //Send Thread 실행상태 Flag
        private bool _recvEventIsRun = false;       //수신 이벤트 처리상태
        private bool _stopFlag = false;             //통신 정지 대기신호

        private int _sendIndex;                     //현재 전송중인 Send Index
        private bool _recvFlag = false;             //수신 유무
        private int _timeOutCount;

        private StringBuilder _buffer = null;       //Received Buffer
        

        // [ 데이터 READ 구조체 List ] //
        private CommObject[] _sendList = null;    //Read Queue

        [Browsable(false)]
        public CommObject[] ReadList
        {
            get { return _sendList; }
            set { _sendList = value; }
        }

        //Write Queue
        private Queue<string> _writeQueue = null;   //Write Queue
        private object _writeQueueLock = null;      //Write Queue Lock
        
        private iMCP Parser;

        private readonly string ackMsg = ASCII_SIGNAL.ACK + "00FF";

        #endregion

        #region Properties

        private int _delaySecond = 100;    //Send Delay Second
        [DefaultValue("Melsec")]
        public int SendWait
        {
            get
            {
                return _delaySecond;
            }
            set
            {
                _delaySecond = value;
            }
        }

        private int _maxTimeOutCount = 20;             //수신 대기 카운트
        [DefaultValue("Melsec")]
        public int TimeOutCount
        {
            get
            {
                return _maxTimeOutCount;
            }
            set
            {
                _maxTimeOutCount = value;
            }
        }

        /// <summary>
        /// 타이틀 
        /// </summary>
        [DefaultValue("Melsec")]
        public string Title
        {
            get
            {
                return slbl_Status.Text;
            }
            set
            {
                slbl_Status.SimpleInvoke("Text", value);
            }
        }

        private bool _isrun = false;
        [Browsable(false)]
        //현재 통신 상태
        public bool IsRun
        {
            get { return _isrun; }
        }

        private readonly int _maxCommErrorCnt = 2;
        private int _commErrorCnt = 0;
        private bool _isCommError = false;
        [Browsable(false)]
        public bool IsCommError
        {
            get { return _isCommError; }
        }
        
        /// <summary>
        /// 포트이름
        /// </summary>
        [Category("SERIAL")]
        [DefaultValue("COM1")]
        public string PortName { get; set; }
        

        /// <summary>
        /// 통신속도
        /// </summary>
        [Category("SERIAL"), DefaultValue("9600")]
        public int Baudrate { get; set; }

        /// <summary>
        /// 데이터비트
        /// </summary>
        [Category("SERIAL")]
        public int Databit { get; set; }

        /// <summary>
        /// 패리티비트
        /// </summary>
        [Category("SERIAL"), DefaultValue("Parity.None")]
        public Parity Paritybit { get; set; }

        /// <summary>
        /// 스톱비트
        /// </summary>
        [Category("SERIAL"), DefaultValue("StopBits.One")]
        public StopBits Stopbit { get; set; }

        /// <summary>
        /// 통신에러 카운트
        /// </summary>
        [Browsable(false)]
        public ulong CommErrorCount { get; set; }

        /// <summary>
        /// 전송 카운트 정보
        /// </summary>
        [Browsable(false)]
        public string GetSendCount
        {
            get
            {
                return string.Format("Cur:{0}, Max:{1}", SendWait * _timeOutCount, SendWait * _maxTimeOutCount);
            }
        }

        #endregion

        #region Melsec Control

        /// <summary>
        /// 생성자
        /// </summary>
        public MelsecSerial()
        {
            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();
        }

        /// <summary>
        /// 로드 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Melsec_Load(object sender, EventArgs e)
        {
            //기본설정
            DefaultSetting();
        }

        #endregion

        #region Thread Process

        private void SendThreadStart()
        {
            _sendThread = new Thread(SendThreadProcess);
            _sendThread.Start();
        }

        private void SendThreadProcess()
        {
            try
            {
                _sendThreadState = true;
                _recvFlag = true;
                _sendIndex = 0;
                _timeOutCount = 0;
                _writeQueue.Clear();

                while (_sendThreadState)
                {
                    if (_recvFlag)
                    {
                        if (_writeQueue.Count > 0)
                        {
                            lock (_writeQueueLock)
                                Send(_writeQueue.Dequeue());
                        }
                        else
                            Send(_sendList[_sendIndex].Data.ToString());

                        _timeOutCount = 0;
                        _recvFlag = false;
                    }
                    else
                    {
                        //PLC에서 데이터 수신을 1000ms 이상 받지 못했을때 데이터 재전송
                        if (_timeOutCount < _maxTimeOutCount)
                            _timeOutCount++;
                        else
                        {
                            _timeOutCount = 0;
                            _buffer.Clear();
                            _recvFlag = true;

                            if (_commErrorCnt < _maxCommErrorCnt)
                                _commErrorCnt++;
                            else
                                _isCommError = true;

                            //에러 카운트 증가
                            CommErrorCount++;

                            //버퍼 클리어
                            _port.DiscardInBuffer();
                            _buffer.Clear();
                            
                            string message = string.Format("[{0}] PLC에서 데이터를 수신받지 못해서 수신버퍼를 초기화 합니다!!, Port: {1}", Title, _port.PortName);
                            
                            Logger.GetInstance.SaveLog(LEVEL.Error, message);

                            SetCommunicationLog(CommunicationType.Common, message);

                        }
                    }
                    Thread.Sleep(_delaySecond);
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, e.Message);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 기본설정
        /// </summary>
        private void DefaultSetting()
        {
            //Queue 초기화
            _writeQueue = new Queue<string>();
            _writeQueueLock = new object();

            Parser = new Acpu();
        }

        /// <summary>
        /// Melsec Serial 설정 초기화
        /// </summary>
        private void PortSetting()
        {
            _port.PortName = PortName;
            _port.BaudRate = Baudrate;
            _port.DataBits = Databit;
            _port.Parity = Paritybit;
            _port.StopBits = Stopbit;
            _port.ReadTimeout = 500;
            _port.WriteTimeout = 500;

            //수신 버퍼 초기화
            if (_buffer != null)
                _buffer.Clear();
            else
                _buffer = new StringBuilder();
        }

        /// <summary>
        /// 전송 구조체 설정
        /// </summary>
        public void SetSendStruct(CommObject[] mds)
        {
            _sendList = mds;

            for(int i=0; i<mds.Length; i++)
            {
                _sendList[i].Data = new StringBuilder(Parser.MakeRequestData(mds[i]));
            }
        }

        /// <summary>
        /// Write 구조체 설정
        /// </summary>
        public bool SetWrite(COMMAND_TYPE t, string tag, string value)
        {
            bool chk = false;

            try
            {
                CommObject co = new CommObject();
                string dev = tag.Left(1);
                string devno = tag.Right(4);

                co.Command = t;
                co.WaitTime = "3";
                co.DeviceType = dev;
                co.DeviceNo = dev.Equals("D") ? devno.ToInt() : devno.ToInt(16);
                co.DeviceCount = 1;
                co.Values = new string[] { value };

                _writeQueue.Enqueue(Parser.MakeRequestData(co));

                chk = true;
            }
            catch { }

            return chk;
        }

        /// <summary>
        /// 통신 시작
        /// </summary>
        public void Start()
        {
            try
            {
                if (_port == null)
                {
                    _port = new SerialPort();
                    _port.DataReceived += DataReceived;
                }

                Stop();

                PortSetting();
                
                _port.Open();

                _isrun = _port.IsOpen;

                //Send Thread 시작
                if (_port.IsOpen)
                {
                    SendThreadStart();
                    SetStatus("연결");
                }

                if (OnPortEnable != null)
                    OnPortEnable(true);
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, string.Format("[{0}] Port:{1} ,Desc:{2}", Title, _port.PortName, ex.Message));
            }
        }

        /// <summary>
        /// 통신 정지
        /// </summary>
        public void Stop()
        {
            try
            {
                if (_port.IsOpen)
                {
                    //Send Thread 정지
                    _sendThreadState = false;
                    
                    if (_recvEventIsRun)
                    {
                        _stopFlag = true;
                        return;
                    }

                    _sendThread.Abort();

                    _port.Close();

                    _stopFlag = false;

                    _isrun = false;

                    SetStatus("끊김");

                    if (OnPortEnable != null)
                        OnPortEnable(false);
                }
            }
            catch(Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, ex.Message);
            }

        }
        
        /// <summary>
        /// 전송
        /// </summary>
        /// <returns></returns>
        private void Send(string sdata)
        {
            if (!_port.IsOpen) return;
            if (!_sendThreadState) return;

            _port.Write(sdata);

            if (OnCommunicationLog != null) OnCommunicationLog("TX", sdata);

            SetCommunicationLog(CommunicationType.TX, sdata);
        }

        private bool ReceiveEndCheck(string b)
        {
            bool chk = false;

            if (b.Contains(ASCII_SIGNAL.STX) && b.Contains(ASCII_SIGNAL.ETX))
                chk = true;
            else if (b.Equals(ackMsg))
                chk = true;
            return chk;
        }
        
        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string tmpData = string.Empty;
            string message = string.Empty;

            _recvEventIsRun = true;

            try
            {
                _buffer.Append(_port.ReadExisting());

                if (!ReceiveEndCheck(_buffer.ToString())) return;

                tmpData = _buffer.ToString();
                _buffer.Clear();

                if (OnCommunicationLog != null) OnCommunicationLog("RX", tmpData);
                
                SetCommunicationLog(CommunicationType.RX, tmpData);

                if (!tmpData.Equals(ackMsg))
                {
                    if (OnReceiveData != null)
                    {
                        CommObject co = ReadList[_sendIndex];
                        co.Recv = tmpData;

                        if (Parser.RecvParseResponseData(tmpData, ref co))
                        {
                            co.State = 6;
                            Send(Parser.MakeResponseData(co));
                            OnReceiveData(co);
                        }
                        else
                        {
                            co.State = 15;
                            Send(Parser.MakeResponseData(co));

                            //에러 카운트 증가
                            CommErrorCount++;

                            Logger.GetInstance.SaveLog(LEVEL.Plc, "[수신 패킷 NAK 발생] Line: {0}", Title);

                            //_port.DiscardInBuffer();
                        }
                    }

                    if (_sendIndex < (_sendList.Length - 1))
                        _sendIndex++;
                    else
                        _sendIndex = 0;
                }

                _commErrorCnt = 0;
                _isCommError = false;
                _recvFlag = true;
            }
            catch (Exception ex)
            {
                _port.DiscardInBuffer();

                //에러 카운트 증가
                CommErrorCount++;

                message = string.Format("[{0}] 수신 에러가 발생하여 수신 버퍼를 초기화 합니다!!, Port: {1}, Desc: {2}", Title, _port.PortName, ex.Message);

                Logger.GetInstance.SaveLog(LEVEL.Error, message);

                SetCommunicationLog(CommunicationType.Common, message);
            }
            finally
            {
                _recvEventIsRun = false;

                if (_stopFlag) Stop();
            }
        }

        /// <summary>
        /// 컨트롤 상태 변경
        /// </summary>
        /// <param name="msg"></param>
        private void SetStatus(string msg)
        {
            string message = string.Empty;
            Color bgcolor = slbl_Status.BackColor;

            switch (msg)
            {
                case "대기":
                    message = Title + " 연결 대기";
                    bgcolor = Color.Silver;
                    break;
                case "연결":
                    message = Title + " 연결 시간:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bgcolor = Color.Lime;
                    break;
                case "끊김":
                    message = Title + " 끊긴 시간:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bgcolor = Color.Red;
                    break;
            }

            if (slbl_Status.GradientEndColor.Equals(bgcolor)) return;

            slbl_Status.InvokeToAction<string>
            (
                delegate (string m)
                {
                    tip_Info.SetToolTip(slbl_Status, m);
                }
                , message
            );

            slbl_Status.SimpleInvoke("GradientEndColor", bgcolor);

            SetCommunicationLog(CommunicationType.Common, message);
            
            Logger.GetInstance.SaveLog(LEVEL.Info, string.Format("[Melsec] {0}", message));
        }

        private void slbl_Status_Click(object sender, EventArgs e)
        {
            try
            {
                if (_viewer == null)
                {
                    _viewer = new MelsecSerialView();
                    _viewer.Serial = this;
                    _viewer.Show();
                }
                else if (_viewer.Visible)
                    _viewer.SendToBack();
                else if (!_viewer.Visible)
                    _viewer.Show();
            }
            catch { }
                
        }

        private void SetCommunicationLog(CommunicationType ctype, string message)
        {
            if (_viewer == null) return;
            if (_viewer.IsDisposed) return;
            if (!_viewer.Visible) return;
            
            switch (ctype)
            {
                case CommunicationType.Common:
                    _viewer.CommunicationLog(message);
                    break;

                case CommunicationType.TX:
                case CommunicationType.RX:
                    _viewer.CommunicationLog(ctype.ToString(), message);
                    break;
            }
        }
        
        enum CommunicationType
        {
            Common = 0,
            TX = 1,
            RX = 2
        }

        #endregion
    }
}
