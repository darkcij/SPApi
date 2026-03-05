using SPApi.Commons;
using SPApi.Declaration.Structs;
using SPApi.Controls.Log;
using System;
using System.Collections.Generic;

namespace SPApi.Controls.ULink
{
    /// <summary>
    /// 곡성 ULink 태그수신값을 파싱하여 TAG[]로 변환하는 공용 클래스입니다.
    /// </summary>
    public class KgsULinkParser : IULinkResponse
    {
        /// <summary>
        /// 수신 데이터 파싱
        /// </summary>
        /// <param name="recvData">수신데이터</param>
        /// <returns>TAG 구조체</returns>
        public TAG[] ULinkRecvProcess(string recvData)
        {
            string tmp = string.Empty;
            List<TAG> tags = new List<TAG>();
            TAG t;
            int ep = 0;
            bool run = true;
            int length = 0;
            string eventType = string.Empty;
            string reader = string.Empty;
            string ant = string.Empty;
            string tag = string.Empty;
            string multi = string.Empty;
            string sPwr = string.Empty;
            string rPwr = string.Empty;
            string di = string.Empty;

            try
            {
                while (run)
                {
                    ep = recvData.IndexOf(ASCII_SIGNAL.ETX) + 1;
                    if (ep > 0)
                    {
                        tmp = recvData.Left(ep);

                        //STX CHECK
                        if (tmp.Mid(0, 1).Equals(ASCII_SIGNAL.STX))
                            tmp = tmp.Remove(0, 1);
                        else
                        {
                            recvData = recvData.Remove(0, ep);
                            continue;
                        }

                        //ETX CHECK
                        if (tmp.Right(1).Equals(ASCII_SIGNAL.ETX))
                            tmp = tmp.Remove(tmp.Length - 1, 1);
                        else
                        {
                            recvData = recvData.Remove(0, ep);
                            continue;
                        }

                        //Length Check
                        if (int.TryParse(tmp.Left(2), out length))
                            tmp = tmp.Remove(0, 2);
                        else
                        {
                            recvData = recvData.Remove(0, ep);
                            continue;
                        }

                        //Data Length Check
                        tmp = tmp.PadRight(length, '0');

                        //이벤트 타입
                        eventType = tmp.Left(1);

                        switch (eventType)
                        {
                            case "A":
                            case "I":
                            case "S":
                                //[1]전송타입,[1]안테나,[8]TAG,[1]S/M,[3]세팅파워,[3]인식파워,[10]DI신호
                                reader = tmp.Mid(1, 2);
                                ant = tmp.Mid(3, 1);
                                tag = tmp.Mid(4, 8);
                                multi = tmp.Mid(12, 1);
                                sPwr = tmp.Mid(13, 3);
                                rPwr = tmp.Mid(16, 3);
                                di = tmp.Mid(19, 10);
                                break;

                            case "D":
                                //[1]전송타입,[10]DI신호
                                reader = tmp.Mid(1, 2);
                                di = tmp.Mid(3, 10);
                                ant = "1";
                                tag = "0".PadLeft(8, '0');
                                multi = "S";
                                sPwr = "222";
                                rPwr = "333";
                                break;
                        }

                        t = new TAG
                        {
                            EventType = eventType,    //이벤트 타입 - A:프로그램 로드 시, I:요청시, U:주기적으로, S:DI 신호변경, D:DI 데이터 요청
                            ReaderID = reader,        //리더기 ID
                            AntennaID = ant,          //안테나 ID
                            Tag = tag,                //TAG
                            Recog = multi,            //인식 방식 - S:Single, M:Multi
                            StartPower = sPwr,        //안테나 Power 초기 셋팅값
                            RecogPower = rPwr,        //안테나 인식 Power
                            Di = di                  //DI Sensor
                        };

                        tags.Add(t);

                        recvData = recvData.Remove(0, ep);

                        //수신 로그 기록
                        Logger.GetInstance.SaveLog(LEVEL.Ulink
                            , "[{0}] Event:[{1}], Reader:[{2}], Antenna:[{3}], TAG:[{4}], Recog:[{5}], StartPower:[{6}], RecogPower:[{7}], DI:[{8}]"
                            , DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                            , t.EventType
                            , t.ReaderID
                            , t.AntennaID
                            , t.Tag
                            , t.Recog
                            , t.StartPower
                            , t.RecogPower
                            , t.Di);
                    }
                    else
                        run = false;
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
