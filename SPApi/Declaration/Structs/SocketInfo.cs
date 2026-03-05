namespace SPApi.Declaration.Structs
{
    /// <summary>
    /// 클라이언트 정보
    /// </summary>
    public struct ClientInfo
    {
        public string IP { get; set; }
        public string PORT { get; set; }
        public bool STATE { get; set; }
    }

    //ASCII SIGNAL
    public static class ASCII_SIGNAL
    {
        public static readonly string NULL = ((char)0x00).ToString();
        public static readonly string STX = ((char)0x02).ToString();
        public static readonly string ETX = ((char)0x03).ToString();
        public static readonly string EOT = ((char)0x04).ToString();
        public static readonly string ENQ = ((char)0x05).ToString();
        public static readonly string ACK = ((char)0x06).ToString();
        public static readonly string LF = ((char)0x0A).ToString();
        public static readonly string CR = ((char)0x0D).ToString();
        public static readonly string NAK = ((char)0x0F).ToString();
    }
}
