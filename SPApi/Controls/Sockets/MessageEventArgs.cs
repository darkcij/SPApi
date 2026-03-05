using WebSocketSharp;

namespace SPApi.Controls.Sockets
{
    public class MessageEventArgs
    {
        private string _data;
        private bool _isBinary;
        private bool _isClosed;
        private bool _isPing;
        private bool _isText;
        private byte[] _rawData;

        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        public bool IsBinary
        {
            get { return _isBinary; }
            set { _isBinary = value; }
        }
        public bool IsClose
        {
            get { return _isClosed; }
            set { _isClosed = value; }
        }
        public bool IsPing
        {
            get { return _isPing; }
            set { _isPing = value; }
        }
        public bool IsText
        {
            get { return _isText; }
            set { _isText = value; }
        }
        public byte[] RawData
        {
            get { return _rawData; }
            set { _rawData = value; }
        }

        public static MessageEventArgs make(WebSocketSharp.MessageEventArgs e)
        {
            MessageEventArgs e1 = new MessageEventArgs();
            e1.Data = e.Data;
            e1.IsBinary = e.IsBinary;
            e1.IsClose = e.IsClose;
            e1.IsPing = e.IsPing;
            e1.IsText = e.IsText;
            e1.RawData = e.RawData;
            return e1;
        }
    }
}
