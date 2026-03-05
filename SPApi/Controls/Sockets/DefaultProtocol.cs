using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPApi.Commons;

namespace SPApi.Controls.Sockets
{
    public class DefaultProtocol : iProtocol
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
            return data.SendData.ToString();
        }

        public bool CheckReceiveData(string recv)
        {
            bool rtn = false;

            if (recv.Length > 0)
                rtn = true;

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
            
            try
            {
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
