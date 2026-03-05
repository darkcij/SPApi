using System;
using System.Collections;
using System.Linq.Expressions;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.IO.Compression;
using System.Reflection;
using System.Globalization;
using ZXing;
using ZXing.QrCode;
using SPApi.Declaration.Enums;

namespace SPApi.Commons
{
    public static class Common
    {
        #region Members

        //공장일자 처리를 위한 변수
        private static int _timeShift = 0;
        private static string _factoryStartTime = "0700";

        //공장Shift 표기를 위한 변수
        private static string[] _shiftMark = new string[] { "", "오전", "오후", "야간" };

        #endregion

        #region Properties

        /// <summary>
        /// 프로그램 종료 체크 FLAG
        /// </summary>
        public static bool IsExit { get; set; }

        

        /// <summary>
        /// 공장일자 시작시간 "HHmm"형식의 네자리 시간
        /// </summary>
        public static string FACTORY_STARTTIME
        {
            get { return _factoryStartTime; }
            set
            {
                _factoryStartTime = value.PadRight(4, '0');
                int minute = _factoryStartTime.Left(2).ToInt() * 60 + _factoryStartTime.Right(2).ToInt();
                _timeShift = 420 - minute;
            }
        }
        
        /// <summary>
        /// 공장 Shift 문자 표기 설정
        /// </summary>
        /// <param>배열 { 0="", 1="오전", 2="오후", 3="야간" } </param>
        public static string[] FACTORY_SHIFT_MARK
        {
            get { return _shiftMark; }
            set { _shiftMark = value; }
        }
        
        #endregion

        #region 중복실행 체크

        private static Mutex _duplicate = null;

        /// <summary>
        /// 중복실행 체크 
        /// </summary>
        /// <param name="key">중복 체크용 Key</param>
        /// <returns></returns>
        public static bool IsDuplicate(string key)
        {
            bool _bCreated = false;

            try
            {
                _duplicate = new Mutex(true, key, out _bCreated);
            }
            catch
            {

            }
            
            return !_bCreated;
        }

        #endregion

        #region DATE 관련

        /// <summary>
        /// Day To Second
        /// </summary>
        public static readonly double DAY_SEC = 86400;

        public static double DateDiff(DateName Interval, DateTime Date1, DateTime Date2)
        {
            

            double diff = 0;

            switch (Interval)
            {
                case DateName.Year:
                    diff = Convert.ToDouble(
                            (DateTime.Parse(Date2.ToString("yyyy-01-01")) -
                             DateTime.Parse(Date1.ToString("yyyy-01-01"))).TotalDays / 365);
                    break;

                case DateName.Month:
                    diff = Convert.ToDouble(
                            ((DateTime.Parse(Date2.ToString("yyyy-MM-01")) -
                              DateTime.Parse(Date1.ToString("yyyy-MM-01"))).TotalDays / 365)
                           * 12);
                    break;

                case DateName.Day:
                    diff = (DateTime.Parse(Date2.ToString("yyyy-MM-dd")) -
                            DateTime.Parse(Date1.ToString("yyyy-MM-dd"))).Days;
                    break;

                case DateName.Hour:
                    diff = (DateTime.Parse(Date2.ToString("yyyy-MM-dd HH:00:00")) -
                            DateTime.Parse(Date1.ToString("yyyy-MM-dd HH:00:00"))).TotalHours;
                    break;

                case DateName.Minute:
                    diff = (DateTime.Parse(Date2.ToString("yyyy-MM-dd HH:mm:00")) -
                            DateTime.Parse(Date1.ToString("yyyy-MM-dd HH:mm:00"))).TotalMinutes;
                    break;

                case DateName.Second:
                    diff = (DateTime.Parse(Date2.ToString("yyyy-MM-dd HH:mm:ss")) -
                            DateTime.Parse(Date1.ToString("yyyy-MM-dd HH:mm:ss"))).TotalSeconds;
                    break;

                case DateName.Millisecond:
                    diff = (Date2 - Date1).TotalMilliseconds;
                    break;
            }
            return diff;
        }

        public static string SecToTime(int sec, string format)
        {
            return new DateTime(TimeSpan.FromSeconds(sec).Ticks).ToString(format);
        }
        
        #endregion

        #region FactoryDate

        /// <summary>
        /// 공장일자 문자로 구하기
        /// </summary>
        /// <param name="format">반환 포멧</param>
        /// <returns>String 형식</returns>
        public static string GetFactoryDateToString(string format = "yyyy-MM-dd")
        {
            string rtn = string.Empty;
            long nTime = DateTime.Now.AddMinutes(_timeShift).ToString("HHmmss").ToLong();

            if (nTime >= 0 && nTime < 70000)
                rtn = DateTime.Now.AddDays(-1).ToString(format);
            else
                rtn = DateTime.Now.ToString(format);

            return rtn;
        }

        /// <summary>
        /// 공장일자 구하기
        /// </summary>
        /// <returns>DateTime 형식</returns>
        public static DateTime GetFactoryDateToDateTime()
        {
            DateTime rtn;
            long nTime = DateTime.Now.AddMinutes(_timeShift).ToString("HHmmss").ToLong();

            if (nTime >= 0 && nTime < 70000)
                rtn = DateTime.Now.AddDays(-1);
            else
                rtn = DateTime.Now;

            return rtn;
        }

        /// <summary>
        /// 공장일자 구하기
        /// </summary>
        /// <param name="date">일자 지정</param>
        /// <returns>DateTime 형식</returns>
        public static DateTime GetFactoryDateToDateTime(DateTime date)
        {
            DateTime rtn;
            long nTime = DateTime.Now.AddMinutes(_timeShift).ToString("HHmmss").ToLong();

            if (nTime >= 0 && nTime < 70000)
                rtn = date.AddDays(-1);
            else
                rtn = date;

            return rtn;
        }

        #endregion

        #region SHIFT

        /// <summary>
        /// Shift 가져오기
        /// </summary>
        /// <param name="sTime">값이 없으면 현재시간, 있으면 설정시간("HHmmss")</param>
        /// <returns>0(""), 1(오전), 2(오후), 3(야간)</returns>
        public static int GetShift(string sTime = "")
        {
            if (sTime.NoE())
            {
                sTime = DateTime.Now.ToString("HHmmss");
            }

            return FunShift(sTime);
        }

        /// <summary>
        /// Shift 문자로 구하기 ( 현재시간 )
        /// </summary>
        /// <param name="sTime">값이 없으면 현재시간, 있으면 설정시간("HHmmss")</param>
        /// <returns>오전, 오후, 야간</returns>
        public static string GetShiftToString(string time = "")
        {
            return _shiftMark[GetShift(time)];
        }

        /// <summary>
        /// Shift 문자로 구하기 ( 특정 Shift )
        /// </summary>
        /// <param name="shift">Shift 번호(1, 2, 3)</param>
        /// <returns>1=오전, 2=오후, 3=야간</returns>
        public static string GetShiftToString(int shift)
        {
            return _shiftMark[shift];
        }
        
        /// <summary>
        /// Shift 구하기
        /// </summary>
        /// <param name="sTime">시간값</param>
        /// <returns>0(""), 1(오전), 2(오후), 3(야간)</returns>
        private static int FunShift(string sTime)
        {
            int rtn = 0;
            long nTime = string.Format("{0}{1}", DateTime.Now.ToString("yyyyMMdd"), sTime)
                            .ToDate("yyyyMMddHHmmss").AddMinutes(_timeShift).ToString("HHmmss")
                            .ToLong();

            if (nTime >= 70000 && nTime <= 145959)
                rtn = 1;
            else if (nTime >= 150000 && nTime <= 225959)
                rtn = 2;
            else
                rtn = 3;

            return rtn;
        }

        #endregion

        #region BAN

        /// <summary>
        /// 반 인덱스 구하기
        /// </summary>
        /// <param name="ban"></param>
        /// <returns></returns>
        public static int GetBanToIndex(string ban)
        {
            int idx = 0;

            switch (ban)
            {
                case "A": idx = 1; break;
                case "B": idx = 2; break;
                case "C": idx = 3; break;
                case "D": idx = 4; break;
            }

            return idx;
        }

        /// <summary>
        /// 반정보 구하기
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetBanFromIndex(int index)
        {
            string ban = string.Empty;

            switch (index)
            {
                case 1: ban = "A"; break;
                case 2: ban = "B"; break;
                case 3: ban = "C"; break;
                case 4: ban = "D"; break;
            }

            return ban;
        }

        #endregion

        #region IP

        //로컬 IP 정보 가져오기
        public static ArrayList GetLocalIpArray
        {
            get
            {

                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                ArrayList ipList = new ArrayList();
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily.Equals(System.Net.Sockets.AddressFamily.InterNetwork))
                        ipList.Add(ip.ToString());
                }
                return ipList;
            }
        }

        public static string GetLocalIpAddress
        {
            get
            {
                UnicastIPAddressInformation uip = null;

                NetworkInterface[] ni = NetworkInterface.GetAllNetworkInterfaces();

                for(int i=0; i<ni.Length; i++)
                {
                    if (ni[i].OperationalStatus != OperationalStatus.Up)
                        continue;

                    IPInterfaceProperties properties = ni[i].GetIPProperties();

                    if (properties.GatewayAddresses.Count == 0)
                        continue;

                    for(int j=0; j< properties.UnicastAddresses.Count; j++)
                    {
                        if (properties.UnicastAddresses[j].Address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
                            continue;

                        if (IPAddress.IsLoopback(properties.UnicastAddresses[j].Address))
                            continue;

                        if (!properties.UnicastAddresses[j].IsDnsEligible)
                        {
                            if (uip == null)
                                uip = properties.UnicastAddresses[j];
                            continue;
                        }

                        if (properties.UnicastAddresses[j].PrefixOrigin != PrefixOrigin.Dhcp)
                        {
                            if (uip == null || !uip.IsDnsEligible)
                                uip = properties.UnicastAddresses[j];
                            continue;
                        }

                        return properties.UnicastAddresses[j].Address.ToString();
                    }
                }

                return uip != null
                    ? uip.Address.ToString()
                    : string.Empty;
            }
        }

        #endregion

        #region Reflection

        /// <summary>
        /// 메서드 이름 얻기
        /// </summary>
        /// <param name="level">1=현재, 2=이전, 3=...</param>
        /// <returns>해당 레벨의 메서드 이름</returns>
        public static string GetMethodName(int level)
        {
            return new StackTrace().GetFrame(level).GetMethod().Name;
        }

        //Property Name 얻기
        public static string GetName<T>(T item) where T : class
        {
            return typeof(T).GetProperties()[0].Name;
        }

        public static string GetName2<T>(Expression<Func<T>> expr)
        {
            return ((MemberExpression)expr.Body).Member.Name;
        }

        public static string GetName3<T>(Func<T> expr)
        {
            return expr.Target.GetType().Module.ResolveField(BitConverter.ToInt32(expr.Method.GetMethodBody().GetILAsByteArray(), 2)).Name;
        }

        #endregion

        #region DATA 형 변환

        /// <summary>
        /// 데이터 타입 변경
        /// </summary>
        public static string ChangeType(string type, int padLen, string data)
        {
            return type.Equals("S") ?
                        data.PadLeft(padLen, '0') :
                        data.PadRight(padLen, ' ');
        }

        /// <summary>
        /// 비트 문자열을 2자리씩 잘라서 바이트 배열로 변환
        /// </summary>
        /// <param name="bitString">비트 문자열</param>
        /// <returns>바이트 배열</returns>
        public static byte[] GetBytesFromString(string bitString)
        {
            return Enumerable.Range(0, bitString.Length / 2).
                Select(pos => Convert.ToByte(
                    bitString.Substring(pos * 2, 2)
                    , 16)
                ).ToArray();
        }

        /// <summary>
        /// HOST 데이터 형에 맞춰 문자열 변환
        /// </summary>
        /// <param name="psType">S:숫자형, A:문자형</param>
        /// <param name="psData">Raw 데이터</param>
        /// <param name="pnLenth">최대로 채워야 할 문자열</param>
        /// <returns></returns>
        public static string FunChangeType(string psType, string psData, int pnLenth)
        {
            //'psType  : S:숫자형, A:문자형
            //'psData  : Raw 데이터
            //'pnLengh : 최대로 채워야 할 문자열

            string ret = "";

            if (psData == "")
            {
                if (psType == "S")
                    ret = ret.PadLeft(pnLenth, '0');
                else if (psType == "A")
                    ret = ret.PadRight(pnLenth, ' ');
            }
            else
            {
                if (psType == "S")
                {
                    if (psData.ToLong() < 0)
                    {
                        psData = Math.Abs(psData.ToInt()).ToString();
                        ret = (string.Format("1{0}", psData.PadLeft(pnLenth - 1, '0'))).Right(pnLenth);
                    }
                    else
                        ret = (psData.PadLeft(pnLenth, '0')).Right(pnLenth);
                }
                else if (psType == "A")
                    ret = (psData.PadRight(pnLenth, ' ')).Left(pnLenth);
            }

            return ret;
        }

        //최대값, 최소값, 길이 맞춤
        public static string DasValueCheck(string type, string value, int len)
        {
            string tmp = value;

            if (type.Equals("S"))
            {

                //int tmps = value.ToInt<string>();
                //
                //if (tmps < 0) tmp = "0";
                if (tmp.Length > len) tmp = "9".PadLeft(len, '9');
            }
            else
            {
                if (tmp.Length > len) tmp = value.Left(len);
                else tmp = value;

            }
            return FunChangeType(type, tmp, len);
        }

        public static string GetObjToDbStr(Object obj)
        {
            string tmp = string.Empty;

            if (obj.ChkType(typeof(string)))
                tmp = string.Format("'{0}'", obj.ToString());
            else
                tmp = obj.ToString();

            return tmp;
        }

        /// <summary>
        /// 문자열을 Hex 문자열로 변환
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStrToHexDecA(string value)
        {
            byte[] bts = Encoding.ASCII.GetBytes(value.ToCharArray());
            byte[] tmp = new byte[bts.Length];
            return BitConverter.ToInt16(bts, 0).ToString();
        }

        /// <summary>
        /// 문자열을 Hex 문자열로 변환
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStrToHexDec(string value)
        {
            string s = "";

            for (int i = value.Length - 1; i > -1; i--)
            {
                s += value.Substring(i, 1);
            }

            byte[] bts = Encoding.ASCII.GetBytes(s.ToCharArray());
            //byte[] tmp = new byte[bts.Length];
            //bts.Reverse();
            return BitConverter.ToInt16(bts, 0).ToString();
        }

        /// <summary>
        /// 문자열을 16Bit Ascii 값으로 변환
        /// </summary>
        /// <param name="value"></param>
        public static short[] StrToShort(string value, bool reverse = false)
        {
            List<short> list = new List<short>();

            if (value.Length % 2 == 1) value += " ";

            char[] cs = value.ToCharArray();
            char[] hl = new char[2];
            byte[] bts = new byte[2];

            Array.Reverse(cs);

            for (int i = 0; i < cs.Length; i += 2)
            {
                hl[0] = cs[i];
                hl[1] = cs[i + 1];

                bts = Encoding.ASCII.GetBytes(hl);

                if (reverse) Array.Reverse(bts);

                list.Insert(0, BitConverter.ToInt16(bts, 0));
            }

            

            return list.ToArray();
        }
        
        /// <summary>
        /// Hex 문자열을 Ascii 문자열로 변환 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetHexDecToStr(string value, bool reverse = false)
        {
            string tmp = Convert.ToInt16(value).ToString("X");
            byte[] bt = GetBytesFromString(tmp);
            char[] cr = Encoding.ASCII.GetChars(bt);
            string rtn = string.Empty;

            if (reverse) Array.Reverse(cr);

            for (int i = 0; i < cr.Length; i++)
                rtn += cr[i].ToString();// +rtn;

            return rtn;
        }

        /// <summary>
        /// Hex 문자열을 Ascii 문자열로 변환 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetHexStringToAscii(string value, bool reverse = false)
        {
            byte[] bt = GetBytesFromString(value);
            char[] cr = Encoding.ASCII.GetChars(bt);
            string rtn = string.Empty;

            if (reverse) Array.Reverse(cr);

            for (int i = 0; i < cr.Length; i++)
                rtn += cr[i].ToString();// +rtn;

            return rtn;
        }

        /// <summary>
        /// Hex 문자열을 Ascii 문자열로 변환 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetHexDecToStr(string[] value, bool reverse = false)
        {
            string tmp = string.Empty;
            StringBuilder rtn = new StringBuilder();

            for (int i = 0; i < value.Length; i++)
            {
                tmp = Convert.ToInt16(value[i]).ToString("X");
                byte[] bt = GetBytesFromString(tmp);
                char[] cr = Encoding.ASCII.GetChars(bt);

                if (reverse) Array.Reverse(cr);
                
                for (int j = 0; j < cr.Length; j++)
                    rtn.Append(cr[j].ToString());// +rtn;
            }

            return rtn.ToString();
        }

        /// <summary>
        /// Hex ByteArray를 Hex 문자열로 변환
        /// </summary>
        public static string GetHexBytesToHexStr(byte[] value)
        {
            StringBuilder tmp = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
                tmp.Append(value[i].ToString("X").PadLeft(2, '0'));

            return tmp.ToString();
        }
        
        public static string GetUWordToBitStr(string value, bool reverse = false)
        {
            byte[] s = BitConverter.GetBytes(value.ToUshort());
            BitArray b = new BitArray(s);
            b.Length = 16;

            StringBuilder tmp = new StringBuilder();

            if (reverse)
                for (int i = 0; i < b.Length; i++)
                    tmp.Append(b[i] ? "1" : "0");
            else
                for (int i = b.Length - 1; i >= 0; i--)
                    tmp.Append(b[i] ? "1" : "0");

            return tmp.ToString();
        }

        public static string GetBitStrToUWord(string value, bool reverse = false)
        {
            string tmp = value.PadLeft(16, '0');

            char[] chrs = tmp.ToCharArray();
            if (reverse) Array.Reverse(chrs);

            bool[] tmps = new bool[16];
            int pos = tmp.Length - 1;

            for (int i = 0; i < tmp.Length; i++)
                tmps[i] = chrs[pos--].Equals('0') ? false : true;

            BitArray b = new BitArray(tmps);

            byte[] bs = new byte[2];
            b.CopyTo(bs, 0);

            return BitConverter.ToUInt16(bs, 0).ToString();
        }

        /// <summary>
        /// 2개의 Word(16Bit)를 합쳐서 Dword(32bit) 생성
        /// </summary>
        public static int W2ToDw(short low, short high)
        {
            List<byte> bs = new List<byte>(4);
            bs.AddRange(BitConverter.GetBytes(low));
            bs.AddRange(BitConverter.GetBytes(high));

            return BitConverter.ToInt32(bs.ToArray(), 0);
        }

        /// <summary>
        /// Dword(32bit)를 2개의 Word(16bit)로 나눔
        /// </summary>
        public static short[] DwToW2(int dw)
        {
            byte[] bs = BitConverter.GetBytes(dw);
            byte[] low = new byte[] { bs[0], bs[1] };
            byte[] high = new byte[] { bs[2], bs[3] };

            return new short[] { BitConverter.ToInt16(low, 0), BitConverter.ToInt16(high, 0) };
        }

        #endregion

        #region Serialization

        /// <summary>
        /// XML 직렬화
        /// </summary>
        public static string XmlSerialization<T>(T obj, bool compression = false, Encoding encoding = null)
        {
            string rtn = string.Empty;
            StringBuilder temp = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter writer = new StringWriter(temp))
            {
                xmlSerializer.Serialize(writer, obj);
            }

            rtn = temp.ToString();

            if (compression) rtn = Compress(rtn, encoding);

            return rtn;
        }

        /// <summary>
        /// XML 역직렬화
        /// </summary>
        public static T XmlDeserialization<T>(string data, bool decompression = false, Encoding encoding = null)
        {
            T obj;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            if (decompression) data = Decompress(data, encoding);

            using (StringReader reader = new StringReader(data))
            {
                obj = (T)xmlSerializer.Deserialize(reader);
            }
            
            return obj;
        }

        /// <summary>
        /// Binary 직렬화
        /// </summary>
        public static string BinSerialization(object obj)
        {
            string temp = string.Empty;
            BinaryFormatter binSerializer = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                binSerializer.Serialize(stream, obj);
                temp = Convert.ToBase64String(stream.ToArray());
            }
            
            return temp;
        }

        /// <summary>
        /// Binary 역직렬화
        /// </summary>
        public static T BinDeserialization<T>(string data)
        {
            T obj;
            byte[] bs = Convert.FromBase64String(data);
            BinaryFormatter binSerializer = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(bs))
            {
                obj = (T)binSerializer.Deserialize(stream);
            }

            return obj;
        }

        #endregion

        #region Compression/Decompression

        public static string Compress(string data, Encoding source = null)
        {
            if (source == null) source = Encoding.Default;

            byte[] buffer = source.GetBytes(data);

            string rtn = string.Empty;

            using (MemoryStream mStream = new MemoryStream())
            {
                using (GZipStream gStream = new GZipStream(mStream, CompressionMode.Compress, true))
                {
                    gStream.Write(buffer, 0, buffer.Length);
                }

                mStream.Position = 0;

                byte[] compressedData = new byte[mStream.Length];
                mStream.Read(compressedData, 0, compressedData.Length);

                byte[] gBuffer = new byte[compressedData.Length + 4];
                Buffer.BlockCopy(compressedData, 0, gBuffer, 4, compressedData.Length);
                Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gBuffer, 0, 4);
                
                rtn = Convert.ToBase64String(gBuffer);
            }
            return rtn;
        }

        public static string Decompress(string data, Encoding source = null)
        {
            if (source == null) source = Encoding.Default;

            byte[] gBuffer = Convert.FromBase64String(data);

            string rtn = string.Empty;

            using (MemoryStream mStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gBuffer, 0);

                mStream.Write(gBuffer, 4, gBuffer.Length - 4);

                byte[] buffer = new byte[dataLength];

                mStream.Position = 0;

                using (GZipStream gStream = new GZipStream(mStream, CompressionMode.Decompress))
                {
                    gStream.Read(buffer, 0, buffer.Length);
                }

                rtn = source.GetString(buffer);
            }

            return rtn;
        }

        #endregion
        
        #region Bitmap

        public static Bitmap ReplaceColor(Bitmap source,
                                          Color toReplace,
                                          Color replacement)
        {
            var target = new Bitmap(source.Width, source.Height);

            for (int x = 0; x < source.Width; ++x)
            {
                for (int y = 0; y < source.Height; ++y)
                {
                    var color = source.GetPixel(x, y);
                    target.SetPixel(x, y, color == toReplace ? replacement : color);
                }
            }

            return target;
        }

        public static unsafe Bitmap ReplaceColor2(Bitmap source,
                                                  Color toReplace,
                                                  Color replacement)
        {
            const int pixelSize = 4; // 32 bits per pixel

            Bitmap target = new Bitmap(
              source.Width,
              source.Height,
              PixelFormat.Format32bppArgb);

            BitmapData sourceData = null, targetData = null;

            try
            {
                sourceData = source.LockBits(
                  new Rectangle(0, 0, source.Width, source.Height),
                  ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                targetData = target.LockBits(
                  new Rectangle(0, 0, target.Width, target.Height),
                  ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                for (int y = 0; y < source.Height; ++y)
                {
                    byte* sourceRow = (byte*)sourceData.Scan0 + (y * sourceData.Stride);
                    byte* targetRow = (byte*)targetData.Scan0 + (y * targetData.Stride);

                    for (int x = 0; x < source.Width; ++x)
                    {
                        byte b = sourceRow[x * pixelSize + 0];
                        byte g = sourceRow[x * pixelSize + 1];
                        byte r = sourceRow[x * pixelSize + 2];
                        byte a = sourceRow[x * pixelSize + 3];

                        if (toReplace.R == r && toReplace.G == g && toReplace.B == b)
                        {
                            r = replacement.R;
                            g = replacement.G;
                            b = replacement.B;
                        }

                        targetRow[x * pixelSize + 0] = b;
                        targetRow[x * pixelSize + 1] = g;
                        targetRow[x * pixelSize + 2] = r;
                        targetRow[x * pixelSize + 3] = a;
                    }
                }
            }
            finally
            {
                if (sourceData != null)
                    source.UnlockBits(sourceData);

                if (targetData != null)
                    target.UnlockBits(targetData);
            }

            source.Dispose();

            return target;
        }

        #endregion

        #region Process

        /// <summary>
        /// 현재 실행파일 이름 가져오기
        /// </summary>
        public static string CurrentExeName
        {
            get { return Process.GetCurrentProcess().ProcessName; }
        }

        /// <summary>
        /// 프로세스 체크
        /// </summary>
        public static void ProcessWatchDog(string path, string name)
        {
            Process[] pss = Process.GetProcessesByName(name);
            
            if(pss.Length < 1)
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.WorkingDirectory = path;
                psi.FileName = name;
                psi.CreateNoWindow = false;
                psi.UseShellExecute = true;

                try { Process.Start(psi); }
                catch { }
            }
        }

        public static void ProcessStart(string path, string name, string param = "")
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.WorkingDirectory = path;
            psi.FileName = name;
            psi.CreateNoWindow = false;
            psi.UseShellExecute = true;
            if(!param.NoE()) psi.Arguments = param;

            try { Process.Start(psi); }
            catch { }
        }

        public static void ProcessKill(string name)
        {
            Process[] pss = Process.GetProcessesByName(name);

            if(pss.Length > 0)
            {
                for (int i = 0; i < pss.Length; i++)
                    pss[i].Kill();

                Thread.Sleep(100);
            }
        }

        #endregion

        #region Ping

        public static bool PingTest(string ip, int timeout = 100)
        {
            bool chk = false;

            using (Ping p = new Ping())
            {
                PingReply pr = p.Send(ip, timeout);

                if (pr.Status == IPStatus.Success)
                    chk = true;
            }

            return chk;
        }

        #endregion

        #region Version

        /// <summary>
        /// Version Text로부터 Build된 일시를 구합니다.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetAssemblyVersion()
        {
            string strVersionText = Assembly.GetExecutingAssembly().FullName
                    .Split(',')[1]
                    .Trim()
                    .Split('=')[1];

            //Build Number = 2000년 1월 1일부터 빌드된 날짜까지의 총 일(Days).
            int intDays = Convert.ToInt32(strVersionText.Split('.')[2]);
            DateTime refDate = new DateTime(2000, 1, 1);
            DateTime bDate = refDate.AddDays(intDays);

            //Revision NUmber 자정으로부터 Build된 시간까지의 지나간 초(Second).
            int intSeconds = Convert.ToInt32(strVersionText.Split('.')[3]);
            intSeconds = intSeconds * 2;
            bDate = bDate.AddSeconds(intSeconds);

            //시차조정
            DaylightTime daylingTime = TimeZone.CurrentTimeZone.GetDaylightChanges(bDate.Year);

            if (TimeZone.IsDaylightSavingTime(bDate, daylingTime))
                bDate = bDate.Add(daylingTime.Delta);

            return bDate;
        }

        /// <summary> 
        /// Assembly의 Build된 일시를 구합니다. 
        /// </summary> 
        /// <returns></returns> 
        public static DateTime GetBuildDateTime()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return File.GetLastWriteTime(assembly.Location);
        }

        public static string GetFileVersion(string file)
        {
            return FileVersionInfo.GetVersionInfo(file).FileVersion;
        }

        #endregion

        #region DLL 

        /// <summary>
        /// DLL 로드
        /// </summary>
        /// <param name="dllName">DLL 이름</param>
        /// <param name="assembly">반환할 Assembly 객체</param>
        /// <returns>DLL 로드 여부</returns>
        public static bool AssemblyLoad<T>(string fullPath, string dllName, string typeName, out T assembly)
        {
            bool chk = false;

            assembly = default(T);

            try
            {
                string file = string.Format(@"{0}/{1}.dll", fullPath, dllName);

                if (!File.Exists(file)) return chk;
                
                string tName = string.Format("{0}.{1}", dllName, typeName);

                assembly = (T)Assembly.LoadFile(file).CreateInstance(tName);

                chk = true;
            }
            catch
            {
            }

            return chk;
        }

        #endregion

        #region Barcode

        public static Bitmap MakeBarcode(BarcodeType type, string contents, int width, int height)
        {
            Bitmap barcode = null;

            try
            {
                QrCodeEncodingOptions options = new QrCodeEncodingOptions()
                {
                    CharacterSet = "UTF-8",
                    Width = width,
                    Height = height
                };

                BarcodeWriter writer = new BarcodeWriter();
                writer.Format = (BarcodeFormat)type;
                writer.Options = options;
                barcode = writer.Write(contents);

            }
            catch
            {
                barcode = null;
            }

            return barcode;
        }

        #endregion
    }
}
