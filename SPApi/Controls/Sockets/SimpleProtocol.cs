using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPApi.Commons;
using SPApi.Declaration.Structs;

namespace SPApi.Controls.Sockets
{
    public class SimpleProtocol : iProtocol
    {
        /// <summary>
        /// 전송 데이터 생성
        /// </summary>
        /// <param name="cmd">DATA_COMMAND</param>
        /// <param name="header">DATA_HEADER</param>
        /// <param name="type">DATA_TYPE</param>
        /// <param name="data">전송 데이터</param>
        /// <returns>send byte[]</returns>
        public string MakeSendData(SockDataObject data)
        {
            StringBuilder tempData = new StringBuilder();
            tempData.Append(ASCII_SIGNAL.STX);                  //STX
            tempData.Append(SocketProcess.DataCommandToStr(data.Command));    //COMMAND
            tempData.Append(data.Header);                       //DATA HEADER
            tempData.Append(data.Type);                         //DATA TYPE
            tempData.Append(data.SendData.ToString());          //DATA
            tempData.Append(ASCII_SIGNAL.ETX);                  //ETX
            tempData.Append(ASCII_SIGNAL.CR);                   //CR
            tempData.Append(ASCII_SIGNAL.LF);                   //LF

            return tempData.ToString();
        }

        public bool CheckReceiveData(string recv)
        {
            bool rtn = false;
            
            if (recv.Mid(recv.Length - 1, 1).Equals(ASCII_SIGNAL.LF) &&  //LF CHECK
                recv.Mid(recv.Length - 2, 1).Equals(ASCII_SIGNAL.CR) &&  //CR CHECK
                recv.Mid(recv.Length - 3, 1).Equals(ASCII_SIGNAL.ETX))   //ETX CHECK
            {
                rtn = true;
            }
            
            return rtn;
        }

        /// <summary>
        /// 통신데이터 체크
        /// </summary>
        /// <param name="recv">수신 메세지</param>
        /// <returns>CommunicationObject</returns>
        public SockDataObject ReceiveDataProcess(string recv)
        {
            SockDataObject co = new SockDataObject();
            co.State = false;
            co.SendData = new StringBuilder();

            try
            {
                //LF CHECK
                if (!recv.Mid(recv.Length - 1, 1).Equals(ASCII_SIGNAL.LF))
                {
                    co.SendData.Append("LF CHECK ERROR.");
                    return co;
                }

                //LF 삭제
                recv = recv.Remove(recv.Length - 1, 1);

                //CR CHECK
                if (!recv.Mid(recv.Length - 1, 1).Equals(ASCII_SIGNAL.CR))
                {
                    co.SendData.Append("CR CHECK ERROR.");
                    return co;
                }

                //CR 삭제
                recv = recv.Remove(recv.Length - 1, 1);

                //STX CHECK
                if (!recv.Mid(0, 1).Equals(ASCII_SIGNAL.STX))
                {
                    co.SendData.Append("STX CHECK ERROR.");
                    return co;
                }

                //STX 삭제
                recv = recv.Remove(0, 1);

                //ETX CHECK
                if (!recv.Mid(recv.Length - 1, 1).Equals(ASCII_SIGNAL.ETX))
                {
                    co.SendData.Append("ETX CHECK ERROR.");
                    return co;
                }

                //ETX 삭제
                recv = recv.Remove(recv.Length - 1, 1);

                //COMMAND 체크
                co.Command = SocketProcess.DataCommandToEnum(recv.Mid(0, 1));

                //COMMAND 삭제
                recv = recv.Remove(0, 1);

                //DATA HEADER 체크
                co.Header = recv.Mid(0, 2);

                //DATA HEADER 삭제
                recv = recv.Remove(0, 2);

                //DATA TYPE 체크
                co.Type = recv.Mid(0, 2);

                //DATA TYPE 삭제
                recv = recv.Remove(0, 2);

                //DATA 체크
                co.RecvData = new StringBuilder(recv);

                //완료
                co.State = true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CheckReceiveData:" + ex.Message);
            }

            return co;
        }
    }
}
