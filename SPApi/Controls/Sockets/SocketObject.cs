using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using SPApi.Commons;
using SPApi.Declaration.Structs;
using SPApi.Controls.Log;

namespace SPApi.Controls.Sockets
{

    public static class SocketProcess
    {

        public static iProtocol SetProtocol(Protocol ep)
        {
            iProtocol p = null;

            switch(ep)
            {
                case Protocol.Default:
                    p = new DefaultProtocol();
                    break;

                case Protocol.SimpleProtocol:
                    p = new SimpleProtocol();
                    break;
            }

            return p;
        }

        public static void SetCommunicationLog(Socket client, string trans, string data, SocketView view)
        {
            if (view == null) return;
            if (view.IsDisposed) return;
            
            string localEP = string.Empty;
            string remoteEP = string.Empty;

            try
            {
                IPEndPoint tmpEP = (IPEndPoint)client.LocalEndPoint;

                localEP = string.Format("{0}/{1}", tmpEP.Address.ToString(), tmpEP.Port);

                tmpEP = (IPEndPoint)client.RemoteEndPoint;

                remoteEP = string.Format("{0}/{1}", tmpEP.Address.ToString(), tmpEP.Port);
                
                CommunicationMessage message = new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = trans,
                    LocalEndPoint = localEP,
                    RemoteEndPoint = remoteEP,
                    Content = data
                };

                if (!ControlCommon.FormNullCheck(view))
                {
                    view.CommunicationLog(message);
                }
            }
            catch(Exception ex)
            {
                Logger.GetInstance.SaveLogEx(LEVEL.Error, "SPApi", $"SocketObject SetCommunicationLog 처리중 에러발생, Desc={ex.Message}");
            }
        }

        public static void SetCommunicationLog(EndPoint local, EndPoint remote, string trans, string data, SocketView view)
        {
            if (view == null) return;
            if (view.IsDisposed) return;
            if (!view.Visible) return;

            string lo = string.Empty;
            string re = string.Empty;

            try
            {
                IPEndPoint tmpEP = (IPEndPoint)local;

                lo = string.Format("{0}/{1}", tmpEP.Address.ToString(), tmpEP.Port);

                tmpEP = (IPEndPoint)remote;

                re = string.Format("{0}/{1}", tmpEP.Address.ToString(), tmpEP.Port);

                CommunicationMessage message = new CommunicationMessage()
                {
                    Datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    State = trans,
                    LocalEndPoint = lo,
                    RemoteEndPoint = re,
                    Content = data
                };
            }
            catch { }
        }

        /// <summary>
        /// DATA_COMMAND를 STRING으로 변환
        /// </summary>
        /// <param name="command">DATA_COMMAND</param>
        /// <returns>string</returns>
        public static string DataCommandToStr(DATA_COMMAND command)
        {
            string sendCommand = string.Empty;

            switch (command)
            {
                case DATA_COMMAND.ENQ: sendCommand = ASCII_SIGNAL.ENQ; break;
                case DATA_COMMAND.ACK: sendCommand = ASCII_SIGNAL.ACK; break;
                case DATA_COMMAND.NAK: sendCommand = ASCII_SIGNAL.NAK; break;
                case DATA_COMMAND.END: sendCommand = ASCII_SIGNAL.EOT; break;
            }

            return sendCommand;
        }

        /// <summary>
        /// STRING 값을 DATA_COMMAND로 변환
        /// </summary>
        /// <param name="recv">STRING</param>
        /// <returns>DATA_COMMAND</returns>
        public static DATA_COMMAND DataCommandToEnum(string recv)
        {
            //COMMAND 확인
            DATA_COMMAND command = new DATA_COMMAND();

            if (recv.Equals(ASCII_SIGNAL.ENQ)) command = DATA_COMMAND.ENQ;
            else if (recv.Equals(ASCII_SIGNAL.ACK)) command = DATA_COMMAND.ACK;
            else if (recv.Equals(ASCII_SIGNAL.NAK)) command = DATA_COMMAND.NAK;
            else if (recv.Equals(ASCII_SIGNAL.EOT)) command = DATA_COMMAND.END;

            return command;
        }
    }

    public enum Protocol
    {
        Default,
        SimpleProtocol,
        Custom
    }

    public struct SockDataObject
    {
        public DATA_COMMAND Command;
        public string Header;
        public string Type;
        public StringBuilder RecvData;
        public StringBuilder SendData;
        public bool State;
    }

    public enum DATA_COMMAND
    {
        ENQ,
        ACK,
        NAK,
        END
    }
    
    // 클라이언트 객체
    public class ClientObject : IDisposable
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Socket workSocket { get; set; }
        public StringBuilder sb { get; set; }
        public byte[] buffer { get; set; }
        public const int BufferSize = 8192;

        private bool disposed;

        #region Constructor

        public ClientObject()
        {
            buffer = new byte[BufferSize];
            sb = new StringBuilder();
            disposed = false;
        }

        #endregion

        public string GetData()
        {
            string message = sb.ToString();
            sb.Length = 0;
            return message;
        }

        public void BufferClear()
        {
            sb.Length = 0;
        }

        #region Dispose 구현

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                // managed objects
                workSocket.Shutdown(SocketShutdown.Both);
                workSocket.Close();
                workSocket = null;
                sb = null;
            }
            // unmanaged objects

            disposed = true;
        }

        #endregion
    }

    public enum SocketIdentType
    {
        None=0,
        Ip,
        Port,
        Handle
    }
}
