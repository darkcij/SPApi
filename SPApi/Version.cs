namespace SPApi
{
    class Version
    {

#if (!DEBUG)
        public const string APP_MODE = "Release";
#else
        public const string APP_MODE = "Debug";
#endif

        static string _ver = "4.0.1";

        public static string Ver
        {
            get { return string.Format("Ver. {0}", _ver); }
        }

        /// <VERSION>4.0.1</VERSION>
        /// <DATE>2024-05-24</DATE>
        /// <DESCRIPT>
        /// 1. ULinkConnection 수신 I/F 처리 개선
        /// </DESCRIPT>

        /// <VERSION>4.0.0</VERSION>
        /// <DATE>2024-05-13</DATE>
        /// <DESCRIPT>
        /// 1. 4.0 버전 업그레이드
        ///    - RMS DAQ, I/F 별도 분리
        /// </DESCRIPT>

        /// <VERSION>3.0.0</VERSION>
        /// <DATE>2023-11-08</DATE>
        /// <DESCRIPT>
        /// 1. 3.0 버전 업그레이드
        /// 2. 신규 기능 개발
        ///    - Database: PostgreSQL, MongoDB
        ///    - PLC: OPC UA, OPC.NET
        ///    - RMS DAQ, I/F
        /// </DESCRIPT>

        /// <VERSION>2.0.0</VERSION>
        /// <DATE>2022-09-29</DATE>
        /// <DESCRIPT>
        /// 1. .NetFramework 버전 업그레이드 : 4.0 → 4.8
        /// </DESCRIPT>

        /// <VERSION>1.0.4</VERSION>
        /// <DATE>2022-01-10</DATE>
        /// <DESCRIPT>
        /// 1. Database/Socket/Ulink/Serial/Dio Viewer Data 처리로직 개선
        ///  - Queue Dequeue 처리한 데이터 유/무 체크 추가
        /// </DESCRIPT>

        /// <VERSION>1.0.3</VERSION>
        /// <DATE>2021-12-30</DATE>
        /// <DESCRIPT>
        /// 1. Database/Socket/Ulink/Serial/Dio Viewer Data 처리방식 변경
        ///  - Queue / Timer, 100건/500ms
        ///  - Listview Clear 후 Return 추가
        ///  - ListviewItem 초기화 방식 변경
        ///  - ListView Begin/End Update 추가
        /// </DESCRIPT>

        /// <VERSION>1.0.2</VERSION>
        /// <DATE>2021-11-11</DATE>
        /// <DESCRIPT>
        /// 1. Database Viewer Data 처리방식 변경 : Queue / Timer, 100건/200ms
        /// 2. Socket Viewer Data 처리방식 변경 : Queue / Timer, 100건/200ms
        /// </DESCRIPT>

        /// <VERSION>1.0.1</VERSION>
        /// <DATE>2021-07-27</DATE>
        /// <DESCRIPT>
        /// 1. 공장일자 / Shift 계산 타공장 호환되도록 개선
        /// 2. ULink Parser 클래스 등록
        /// 3. Websocket Server/Client 컨트롤 및 Json 통신용 JObject 추가
        /// 4. Serial 포트 설정 구조체 추가
        /// </DESCRIPT>
    }
}
