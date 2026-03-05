using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using SPApi.Controls.Log;

namespace SPApi.Controls.Plc.Melsecs.MCC
{
    public class cSerial:iMCC
    {
        public event EventHandler<McpEventArgs> DataReceived;
        public event EventHandler<McpEventArgs> DataSent;
        public event EventHandler<McpEventArgs> ErrorReceived;

        private SerialPort port = null;

        private const int MAX_TIME_OUT = 100;

        public bool IsRun
        {
            get
            {
                return port.IsOpen;
            }
        }

        public McSetting Set
        {
            get;
            set;
        }

        #region Constructor

        public cSerial()
        {
            DefaultSetting();
        }

        public cSerial(McSetting set)
        {
            Set = set;

            DefaultSetting();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 기본설정
        /// </summary>
        private void DefaultSetting()
        {

        }

        /// <summary>
        /// 전송
        /// </summary>
        /// <returns></returns>
        public void Send(string sdata)
        {
            if (!port.IsOpen) return;

            port.Write(sdata);

            Logger.GetInstance.SaveLog(LEVEL.Ulink, sdata);
        }

        /// <summary>
        /// 통신 시작
        /// </summary>
        public void Start()
        {
            try
            {
                if (port == null)
                {
                    port = new SerialPort();
                    port.DataReceived += Port_DataReceived;
                }

                PortSetting();

                Stop();

                port.Open();
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, string.Format("[{0}] {1}", port.PortName, ex.Message));
            }
        }

        /// <summary>
        /// 통신 정지
        /// </summary>
        public void Stop()
        {
            if (port.IsOpen)
                port.Close();
        }


        /// <summary>
        /// 수신 이벤트 처리
        /// </summary>
        /// <param name="message"></param>
        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string tmpData = string.Empty;

            try
            {
                tmpData = port.ReadExisting();
                
                //SetStatus("Receive");

                if (DataReceived != null)
                    DataReceived(port, new McpEventArgs(tmpData));
            }
            catch (Exception ex)
            {
                port.DiscardInBuffer();
                Logger.GetInstance.SaveLog(LEVEL.Error,
                    string.Format("[{0}] 수신 에러가 발생하여 수신 버퍼를 초기화 합니다!!, Desc: {1}", port.PortName, ex.Message));
            }
        }

        private void PortSetting()
        {
            port.PortName = Set.Serial.PortName;
            port.BaudRate = Set.Serial.BaudRate;
            port.DataBits = Set.Serial.DataBit;
            port.Parity = Set.Serial.ParityBit;
            port.StopBits = Set.Serial.StopBit;
            port.ReadTimeout = 100;
            port.WriteTimeout = 500;
        }

        public void Setting(McSetting set)
        {
            Set = set;
        }

        #endregion


        private Parity GetParityBit(string value)
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

        private StopBits GetStopBit(string value)
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
    }
}
