using System;

namespace SPApi.Declaration.Structs
{
    /// <summary>
    /// Serial 포트 설정 구조체
    /// </summary>
    [Serializable]
    public class SerialPortInfo
    {
        /// <summary> 기본값 : COM1 </summary>
        public string PORT { get; set; } = "COM1";

        /// <summary> 기본값 : 9600 </summary>
        public string BAUDRATE { get; set; } = "9600";

        /// <summary> 기본값 : 8 </summary>
        public string DATABIT { get; set; } = "8";

        /// <summary> 기본값 : N </summary>
        public string PARITY { get; set; } = "N";

        /// <summary> 기본값 : 1 </summary>
        public string STOPBIT { get; set; } = "1";

        /// <summary> 기본값 : "\r\n" </summary>
        public string DELIMITER { get; set; } = "\r\n";
    }
}
