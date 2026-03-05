using SPApi.Commons;
using System.Collections;
using System.Data;
using System.Text;

namespace SPApi.Declaration.Structs
{

    /// <summary>
    /// 통신메세지
    /// </summary>
    public struct CommunicationMessage
    {
        //공통
        public string Datetime;
        public string State;
        public string Content;

        //소켓로그
        public string LocalEndPoint;
        public string RemoteEndPoint;

        //DIO로그
        public string IO_Type;
    }

    /// <summary>
    /// 파라미터 쿼리 구조체
    /// </summary>
    public struct PARAM_QUERY
    {
        public string Name;
        public string Query;
        public ArrayList Param;

        public string LogString
        {
            get
            {
                if (Query.NoE()) return string.Empty;

                StringBuilder tmp = new StringBuilder(Query);
                int lastidx = 0;
                int count = 0;

                string sep = "?";

                if (Query.Contains(":0"))
                    sep = ":";
                else if (Query.Contains("@0"))
                    sep = "@";

                if (Param != null)
                {
                    if (sep.Equals("?"))
                    {
                        for (int i = 0; i < Param.Count; i++)
                        {
                            lastidx = tmp.ToString().IndexOf('?');
                            //count = tmp.Length - lastidx;
                            tmp.Replace("?", Common.GetObjToDbStr(Param[i]), lastidx, 1);
                        }
                    }
                    else if (sep.Equals(":"))
                    {
                        for (int i = Param.Count - 1; i >= 0; i--)
                        {
                            sep = string.Format(":{0}", i);
                            lastidx = tmp.ToString().LastIndexOf(sep);
                            count = tmp.Length - lastidx;
                            tmp.Replace(sep, Common.GetObjToDbStr(Param[i]), lastidx, count);
                        }
                    }
                    else if (sep.Equals("@"))
                    {
                        for (int i = Param.Count - 1; i >= 0; i--)
                        {
                            sep = string.Format("@{0}", i);
                            lastidx = tmp.ToString().LastIndexOf(sep);
                            count = tmp.Length - lastidx;
                            tmp.Replace(sep, Common.GetObjToDbStr(Param[i]), lastidx, count);
                        }
                    }
                }

                return tmp.ToString();
            }
        }
    }

    public struct PARAM_OUTPUT
    {
        public string Out_Name;
        public DbType Out_Type;
        public int Out_Length;
        public object Output;
    }

}
