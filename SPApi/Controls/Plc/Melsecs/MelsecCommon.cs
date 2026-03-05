using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace SPApi.Controls.Plc.Melsecs
{
    /// <summary>
    /// MELSEC 데이터 전송용 구조체
    /// </summary>
    public struct MELSEC_DATA
    {
        public string process { get; set; }
        public string type { get; set; }
        public int point { get; set; }
        public string sendData { get; set; }
        public string recvData { get; set; }
    }

    /// <summary>
    /// 통신 설정 구조체
    /// </summary>
    public struct McSetting
    {
        public string Name;
        public McpSerial Serial;
        public McpTcp Tcp;
    }

    /// <summary>
    /// TCP/IP 설정 구조체
    /// </summary>
    public struct McpTcp
    {

    }

    /// <summary>
    /// 시리얼 설정 구조체
    /// </summary>
    public struct McpSerial
    {
        public string PortName { get; set; }

        public int BaudRate { get; set; }

        public int DataBit { get; set; }

        public StopBits StopBit { get; set; }

        public Parity ParityBit { get; set; }

        public string Handshake { get; set; }

        public string Delimiter { get; set; }
    }

    public struct CommObject
    {
        //송신 데이터
        public StringBuilder Data { get; set; }

        //수신 데이터
        public string Recv { get; set; }

        //전송 커맨드
        public COMMAND_TYPE Command { get; set; }

        //디바이스 타입
        public string DeviceType { get; set; }

        //디바이스 번호
        public int DeviceNo { get; set; }

        //요청 갯수
        public int DeviceCount { get; set; }

        //데이터
        public string[] Values { get; set; }

        //전문 대기
        public string WaitTime { get; set; }

        //상태
        public int State { get; set; }
    }


    public class DeviceData
    {
        public DATA_TYPE[] B;
        public DATA_TYPE[] D;
        public DATA_TYPE[] W;
    }


    /// <summary>
    /// 데이터 타입 구조체
    /// </summary>
    public struct DATA_TYPE
    {
        public string id;
        public string address;

        //설명
        public string desc;

        //값
        public string value;

        
    }

    /// <summary>
    /// 통신 타입
    /// </summary>
    public enum MCC_TYPE
    {
        Serial,
        Tcp,
        Udp
    }

    /// <summary>
    /// 프로토콜 타입
    /// </summary>
    public enum MCP_TYPE
    {
        Acpu,
        AnA_U,
        AnN_FX,
        Qn_QnH,
        QnA
    }

    /// <summary>
    /// 명령 타입
    /// </summary>
    public enum COMMAND_TYPE
    {
        Bit_Read,
        Bit_Write,
        Bit_Test,
        Word_Read,
        Word_Write,
        Word_Test
    }
}
