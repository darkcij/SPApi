using SPApi.Controls.Log;
using System;
using System.Collections.Generic;

namespace SPApi.Controls.ULink
{
    /// <summary>
    /// 광주 및 IDT ULink 태그수신값을 파싱하여 TAG[]로 변환하는 공용 클래스입니다
    /// </summary>
    public class KgjULinkParser : IULinkResponse
    {
        public TAG[] ULinkRecvProcess(string recvData)
        {

            string tmp = string.Empty;
            List<TAG> tags = new List<TAG>();
            TAG t;
            string eventType = string.Empty;    // OnTagIN, OnTagReport, OnReaderError 중 하나
            string reader = string.Empty;       //리더기 ID
            string ant = string.Empty;          //안테나 ID (0~3)
            string tag = string.Empty;
            string multi = "S";                 // 인식 방식 - S:Single, M:Multi
            string sPwr = string.Empty;         //안테나 Power 초기 셋팅값
            string rPwr = string.Empty;         //안테나 인식 Power
            string di = string.Empty;           //DI Sensor



            int i, j;
            string methodName = "";
            string paramName = "";
            string paramValue = "";
            string[] strarr = recvData.Split('\n');

            try
            {
                for (int kkk = 0; kkk < strarr.Length; kkk++)
                {
                    string str = strarr[kkk];
                    i = str.IndexOf("<methodname>");
                    j = str.IndexOf("</methodname>");
                    if (i >= 0 && j >= 0) methodName = str.Substring(i + 12, j - (i + 12));
                    else break;
                    while (true)
                    {
                        i = str.IndexOf("<parameter name=\"", j);
                        if (i < 0)
                        {
                            break;
                        }
                        j = str.IndexOf("\"", i + 17);
                        paramName = str.Substring(i + 17, j - (i + 17));
                        i = str.IndexOf(">", j);
                        j = str.IndexOf("</parameter>", i + 1);
                        if (i >= 0 && j >= 0) paramValue = str.Substring(i + 1, j - (i + 1)).Trim();

                        if (methodName.Equals("OnTagIN") || methodName.Equals("OnTagReport") || methodName.Equals("OnReaderError"))
                        {
                            switch (paramName)
                            {
                                case "readerid": reader = paramValue; break;
                                case "antennaid": ant = paramValue; break;
                                case "tagid": tag = paramValue; break;
                                case "sensordata": di = paramValue; break;
                                case "sensor": di = paramValue; break;
                                default:
                                    break;
                            }
                        }
                    }
                    eventType = methodName;

                    t = new TAG();
                    t.EventType = eventType;    //이벤트 타입 - A:프로그램 로드 시, I:요청시, U:주기적으로, S:DI 신호변경, D:DI 데이터 요청
                    t.ReaderID = reader;        //리더기 ID
                    t.AntennaID = ant;          //안테나 ID
                    t.Tag = tag;                //TAG
                    t.Recog = multi;            //인식 방식 - S:Single, M:Multi
                    t.StartPower = sPwr;        //안테나 Power 초기 셋팅값
                    t.RecogPower = rPwr;        //안테나 인식 Power
                    t.Di = di;                  //DI Sensor

                    tags.Add(t);

                    //수신 로그 기록
                    Logger.GetInstance.SaveLog(LEVEL.Ulink,
                        "Event:[{0}], Reader:[{1}], Antenna:[{2}], TAG:[{3}], Recog:[{4}], StartPower:[{5}], RecogPower:[{6}], DI:[{7}]"
                        , t.EventType
                        , t.ReaderID
                        , t.AntennaID
                        , t.Tag
                        , t.Recog
                        , t.StartPower
                        , t.RecogPower
                        , t.Di);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstance.SaveLog(LEVEL.Error, ex.Message);
            }
            return tags.ToArray();
        }
    }
}
