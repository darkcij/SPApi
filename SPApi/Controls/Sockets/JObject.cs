namespace SPApi.Controls.Sockets
{
    public class JObject
    {
        public string datatype = "";
        public string methodname = "";
        public string machcd = "";
        public string paramData = null;

        public static JObject Make(string datatype, string methodname, string machcd, string paramData)
        {
            JObject obj = new JObject()
            {
                datatype = datatype,
                methodname = methodname,
                machcd = machcd,
                paramData = paramData
            };

            return obj;
        }
    }
}
