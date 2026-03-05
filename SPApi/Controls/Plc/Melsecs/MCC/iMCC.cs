using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPApi.Controls.Plc.Melsecs.MCC
{
    public interface iMCC
    {
        bool IsRun { get; }

        /// <summary>
        /// 통신 Process 시작
        /// </summary>
        void Start();

        /// <summary>
        /// 통신 Process 종료
        /// </summary>
        void Stop();
        
        /// <summary>
        /// 데이터 전송
        /// </summary>        
        void Send(string sdata);

        void Setting(McSetting set);

        /// <summary>
        /// 데이터 정상 전송 이벤트
        /// </summary>
        event EventHandler<McpEventArgs> DataSent;

        /// <summary>
        /// 데이터 정상 수신 이벤트
        /// </summary>
        event EventHandler<McpEventArgs> DataReceived;

        /// <summary>
        /// 데이터 에러 수신 이벤트
        /// </summary>
        event EventHandler<McpEventArgs> ErrorReceived;


    }

    public class McpEventArgs : EventArgs
    {
        public string Message
        {
            get; set;
        }

        public McpEventArgs(string message)
        {
            Message = message;
        }
    }
}
