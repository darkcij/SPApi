using System;
using System.Collections.Generic;
using SPApi.Commons;
using SPApi.Controls.Log;

namespace SPApi.Controls.ULink
{
    /// <summary>
    /// ULink 수신 처리 인터페이스
    /// </summary>
    public interface IULinkResponse
    {
        TAG[] ULinkRecvProcess(string recvData);
    }

    public enum ULINK_REQ
    {
        TAG_READ,
        ALL_READ,
        DI_READ,
        DO_WRITE
    }

    public enum ULINK_PARSER_TYPE
    {
        NONE,
        KGJ_ULink,
        KGS_ULink,
        Manual
    }


    public struct TAG
    {
        public string EventType;    //이벤트 타입 - A:프로그램 로드 시, I:요청시, U:주기적으로
        public string ReaderID;     //리더기 ID
        public string AntennaID;    //안테나 ID
        public string Tag;          //TAG
        public string Recog;        //인식 방식 - S:Single, M:Multi
        public string StartPower;   //안테나 Power 초기 셋팅값
        public string RecogPower;   //안테나 인식 Power
        public string Di;           //DI Sensor
    }
}
