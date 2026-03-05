using System;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
using SPApi.Commons;
using SPApi.Controls.Log;
using SPApi.Declaration.Structs;
using SPApi.Declaration.Enums;

namespace SPApi.Controls.Serials
{
    public partial class Serial : UserControl
    {
        #region Members

        private SerialView _viewer;

        #endregion

        #region Properties

        public delegate void DataReceived(string message);
        public event DataReceived OnDataReceived;

        public delegate void ByteReceived(byte[] bytes);
        public event ByteReceived OnByteReceived;

        private SerialPort _port;
        
        private string _portName = "COM1";
        [Browsable(true), Category("Setting")]
        public string PortName
        {
            get { return _portName; }
            set
            {
                _portName = value;
                slbl_Status.SimpleInvoke("Text", value);
            }
        }

        private int _baudRate = 9600;
        [Browsable(true), Category("Setting")]
        public int BaudRate
        {
            get { return _baudRate; }
            set { _baudRate = value; }
        }

        private Parity _parity = Parity.None;
        [Browsable(true), Category("Setting")]
        public Parity ParityBit
        {
            get { return _parity; }
            set { _parity = value;  }
        }

        private int _dataBit = 8;
        [Browsable(true), Category("Setting")]       
        public int DataBit
        {
            get { return _dataBit; }
            set { _dataBit = value; }
        }

        private StopBits _stopBit = StopBits.One;
        [Browsable(true), Category("Setting")]
        public StopBits StopBit
        {
            get { return _stopBit; }
            set { _stopBit = value; }
        }
        
        private int _readTimeOut = 500;
        [Browsable(true), Category("Setting")]
        public int ReadTimeOut
        {
            get { return _readTimeOut; }
            set { _readTimeOut = value; }
        }

        private int _writeTimeOut = 500;
        [Browsable(true), Category("Setting")]
        public int WriteTimeOut
        {
            get { return _writeTimeOut; }
            set { _writeTimeOut = value; }
        }

        private Handshake _handShake = Handshake.None;
        [Browsable(true), Category("Setting")]
        public Handshake HandShakeType
        {
            get { return _handShake; }
            set { _handShake = value; }
        }

        private bool _dtrEnable = false;
        [Browsable(true), Category("Setting")]
        public bool DtrEnable
        {
            get { return _dtrEnable; }
            set { _dtrEnable = value; }
        }

        private string _delimiter = "\r\n";
        [Browsable(false), Category("Setting")]
        public string Delimiter
        {
            get { return _delimiter; }
            set { _delimiter = value; }
        }

        private RECV_TYPE _recvType = RECV_TYPE.STRING;
        [Browsable(false), Category("Setting")]
        public RECV_TYPE RecvType
        {
            get { return _recvType; }
            set { _recvType = value; }
        }

        [Browsable(false)]
        public bool IsOpen
        {
            get
            {
                return _port != null ? _port.IsOpen : false;
            }
        }

        public string Title
        {
            get { return slbl_Status.Text; }
            set { slbl_Status.SimpleInvoke("Text", value); }
        }

        #endregion

        #region Enums

        public enum RECV_TYPE
        {
            STRING = 0,
            BYTE
        }

        #endregion

        #region Constructor

        public Serial()
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

        #region Controls Events

        private void slbl_Status_Click(object sender, EventArgs e)
        {
            try
            {
                if (_viewer == null || _viewer.IsDisposed)
                    _viewer = new SerialView();
                else if (_viewer.Visible)
                    _viewer.SendToBack();

                _viewer.PortName = _portName;
                _viewer.BaudRate = _baudRate.ToString();
                _viewer.DataBit = _dataBit.ToString();
                _viewer.ParityBit = _parity.ToString();
                _viewer.StopBit = _stopBit.ToString();
                _viewer.Delimiter = @_delimiter;
                _viewer.ReadTimeOut = _readTimeOut.ToString();
                _viewer.WriteTimeOut = _writeTimeOut.ToString();
                _viewer.IsOpen = IsOpen;

                _viewer.Show();
            }
            catch { }
        }

        #endregion

        #region Method

        /// <summary>
        /// 기본 설정
        /// </summary>
        private void DefaultSetting()
        {
            if (DesignMode) return;

            //Viewer 로드
            _viewer = new SerialView();
        }
        
        //컨트롤 상태 변경
        private void SetStatus(DeviceStateType state)
        {
            StringBuilder message = new StringBuilder(_portName);
            Color bgcolor = slbl_Status.BackColor;
            
            switch (state)
            {
                case DeviceStateType.Wait:
                    message.Append(" 연결 대기");
                    bgcolor = Color.Silver;
                    break;
                case DeviceStateType.Open:
                    message.AppendFormat(" Open:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    bgcolor = Color.Yellow;
                    break;
                case DeviceStateType.Close:
                    message.AppendFormat(" Close:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    bgcolor = Color.Red;
                    break;
                case DeviceStateType.Receive:
                    message.AppendFormat(" Receive:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    bgcolor = Color.Lime;
                    break;
                case DeviceStateType.Send:
                    message.AppendFormat(" Send:{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    bgcolor = Color.Blue;
                    break;
            }

            if (slbl_Status.GradientEndColor.Equals(bgcolor)) return;
           
            slbl_Status.InvokeToAction<string>
            (
                delegate(string m)
                {
                    tip_Info.SetToolTip(slbl_Status, m);
                }
                , message.ToString()
            );

            slbl_Status.SimpleInvoke("GradientEndColor", bgcolor);

            Logger.GetInstance.SaveLog(LEVEL.Info, "[{0}] {1}", _portName, message.ToString());
        }

        #endregion

        #region Serial Communication Process

        public void Start()
        {
            try
            {
                if (_port == null) _port = new SerialPort();

                if (_recvType == RECV_TYPE.BYTE)
                    _port.DataReceived += Port_ByteReceived;
                else
                    _port.DataReceived += Port_DataReceived;

                Stop();

                PortSetting();

                _port.Open();

                SetStatus(DeviceStateType.Open);
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[{0}] {1}", _portName, ex.Message);
            }
        }

        public void Stop()
        {
            try
            {
                if (_port.IsOpen)
                {
                    if (_recvType == RECV_TYPE.BYTE)
                        _port.DataReceived -= Port_ByteReceived;
                    else
                        _port.DataReceived -= Port_DataReceived;

                    _viewer.Close();

                    _port.Close();

                    SetStatus(DeviceStateType.Close);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, "[{0}] {1}", _portName, ex.Message);
            }
        }

        private void PortSetting()
        {
            _port.PortName = PortName;    //포트이름
            _port.BaudRate = BaudRate;
            _port.DataBits = DataBit;
            _port.Parity = ParityBit;
            _port.StopBits = StopBit;
            _port.ReadTimeout = ReadTimeOut;
            _port.WriteTimeout = WriteTimeOut;
            _port.NewLine = Delimiter;
            _port.Handshake = HandShakeType;
            _port.DtrEnable = DtrEnable;
        }

        public Parity GetParityBit(string value)
        {
            Parity p = Parity.None;

            switch (value)
            {
                case "E": p = Parity.Even; break;
                case "M": p = Parity.Mark; break;
                case "O": p = Parity.Odd; break;
                case "S": p = Parity.Space; break;
                case "N":
                default: p = Parity.None; break;
            }

            return p;
        }

        public StopBits GetStopBit(string value)
        {
            StopBits s = StopBits.One;

            switch (value)
            {
                case "1.5": s = StopBits.OnePointFive; break;
                case "2": s = StopBits.Two; break;
                case "1":
                default: s = StopBits.One; break;
            }

            return s;
        }

        public void DataSend(string text)
        {
            if (!_port.IsOpen) return;

            _port.Write(text);

            SetCommunicationLog("TX", text);
        }

        public void DataSend(byte[] sendBytes)
        {
            if (!_port.IsOpen) return;

            _port.Write(sendBytes, 0, sendBytes.Length);

            SetCommunicationLog("TX", Common.GetHexBytesToHexStr(sendBytes));
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!_port.IsOpen) return;
            if (_recvType != RECV_TYPE.STRING) return;

            string tmpData = string.Empty;

            try
            {
                tmpData = _port.ReadLine();

                SetCommunicationLog("RX", tmpData);

                if (OnDataReceived != null)
                {
                    OnDataReceived(tmpData);
                }
            }
            catch (TimeoutException)
            {
                if (!_port.IsOpen) return;

                tmpData = _port.ReadExisting();

                SetCommunicationLog("RX", tmpData);

                if (OnDataReceived != null)
                {
                    OnDataReceived(tmpData);
                }
            }
            catch (Exception ex)
            {
                //포트 사용중이면 버퍼 클리어
                if (_port != null && _port.IsOpen)
                {
                    _port.DiscardInBuffer();

                    Logger.GetInstance.SaveLog(LEVEL.Error
                        , "[{0}] 수신 에러가 발생하여 수신 버퍼를 초기화 합니다!!, Desc: {1}"
                        , _portName
                        , ex.Message);
                }
            }
        }

        private void Port_ByteReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!_port.IsOpen) return;
            if (_recvType != RECV_TYPE.BYTE) return;

            int recvLen = 0;
            byte[] tmpBytes = null;

            try
            {
                recvLen = _port.BytesToRead;
                tmpBytes = new byte[recvLen];

                for (int i = 0; i < recvLen; i++)
                {
                    tmpBytes[i] = _port.ReadByte().ToByte();
                }

                SetCommunicationLog("RX", Common.GetHexBytesToHexStr(tmpBytes));

                if (OnByteReceived != null)
                {
                    OnByteReceived(tmpBytes);
                }
            }
            catch (Exception ex)
            {
                //포트 사용중이면 버퍼 클리어
                if (_port != null && _port.IsOpen)
                {
                    _port.DiscardInBuffer();

                    Logger.GetInstance.SaveLog(LEVEL.Error
                        , "[{0}] 수신 에러가 발생하여 수신 버퍼를 초기화 합니다!!, Desc: {1}"
                        , _portName
                        , ex.Message);
                }
            }
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
