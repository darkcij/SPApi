using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SPApi.Commons
{
    public static class ClassExtensions
    {
        #region STRING

        /// <summary>
        /// string Right 구현
        /// </summary>
        /// <param name="value">문자열</param>
        /// <param name="length">길이</param>
        /// <returns>추출 문자열</returns>
        public static string Right(this string value, int length)
        {
            string chk = "";

            try { chk = value.Substring(value.Length - length); }
            catch { chk = ""; }

            return chk;
        }

        /// <summary>
        /// string Left 구현
        /// </summary>
        /// <param name="value">문자열</param>
        /// <param name="length">길이</param>
        /// <returns>추출 문자열</returns>
        public static string Left(this string value, int length)
        {
            string chk = "";

            try { chk = value.Substring(0, length); }
            catch { chk = ""; }

            return chk;
        }

        /// <summary>
        /// string Mid 구현
        /// </summary>
        /// <param name="value">문자열</param>
        /// <param name="startIndex">시작 인덱스</param>
        /// <param name="length">길이</param>
        /// <returns>추출 문자열</returns>
        public static string Mid(this string value, int startIndex, int length)
        {
            string chk = "";

            try { chk = value.Substring(startIndex, length); }
            catch { chk = ""; }

            return chk;
        }

        /// <summary>
        /// string NullOrEmpty 체크
        /// </summary>
        /// <param name="value">문자열</param>
        /// <returns>bool</returns>
        public static bool NoE(this string value)
        {
            bool chk = false;
            
            chk = string.IsNullOrEmpty(value);
            if(!chk) chk = string.IsNullOrWhiteSpace(value);

            return chk;
        }

        /// <summary>
        /// string Null 체크
        /// </summary>
        /// <param name="value">문자열</param>
        /// <returns>문자열/빈문자열</returns>
        public static string ChkNull(this string value)
        {
            return value.NoE() ? "" : value;
        }

        public static string ChkNull(this string value, string def)
        {
            return value.NoE() ? def : value;
        }
        
        public static bool ToLike(this string value, string pattern, string special = "*")
        {
            bool rtn = false;
            string patt = pattern.ChkNull().Trim();

            try
            {
                bool start = patt.StartsWith(special);
                bool end = patt.EndsWith(special);

                if (start && end)   //BOTH
                {
                    patt = patt.Mid(1, patt.Length - 2);
                    rtn = value.Contains(patt);
                }
                else if (start) //LEFT
                {
                    patt = patt.Mid(1, patt.Length - 1);
                    rtn = value.EndsWith(patt);
                }
                else if (end)   //RIGHT
                {
                    patt = patt.Mid(0, patt.Length - 2);
                    rtn = value.StartsWith(patt);
                }
                else //NONE
                {
                    rtn = value.Contains(patt);
                }
            }
            catch
            {

            }

            return rtn;
        }

        #endregion

        #region CONVERTING

        public static byte ToByte<T>(this T value)
        {
            byte chk = 0;

            try { chk = Convert.ToByte(value); }
            catch { chk = 0; }

            return chk;
        }

        public static short ToShort<T>(this T value)
        {
            short chk = 0;

            try { chk = Convert.ToInt16(value); }
            catch { chk = 0; }

            return chk;
        }

        public static ushort ToUshort<T>(this T value)
        {
            ushort chk = 0;
            
            try { chk = Convert.ToUInt16(value); }
            catch { chk = 0; }

            return chk;
        }

        public static int ToInt<T>(this T value)
        {
            int chk = 0;

            try { chk = Convert.ToInt32(value); }
            catch { chk = 0; }

            return chk;
        }

        public static int ToInt<T>(this T value, int Base)
        {
            int chk = 0;

            try { chk = Convert.ToInt32(value.ToString(), Base); }
            catch { chk = 0; }

            return chk;
        }

        public static long ToLong<T>(this T value)
        {
            long chk = 0;

            try { chk = Convert.ToInt64(value); }
            catch { chk = 0; }

            return chk;
        }

        public static float ToFloat<T>(this T value)
        {
            float chk = 0f;
            
            try { chk = Convert.ToSingle(value); }
            catch { chk = 0f; }

            return chk;
        }

        public static double ToDouble<T>(this T value)
        {
            double chk = 0d;
            
            try { chk = Convert.ToDouble(value); }
            catch { chk = 0d; }

            return chk;
        }

        public static decimal ToDecimal<T>(this T value)
        {
            decimal chk = 0m;

            try { chk = Convert.ToDecimal(value); }
            catch { chk = 0m; }

            return chk;
        }

        public static bool ToBool<T>(this T value)
        {
            bool chk = false;

            try { chk = Convert.ToBoolean(value); }
            catch { chk = false; }

            return chk;
        }

        public static bool ToBool(this string value)
        {
            bool chk = false;

            try
            {
                if (value.ToLower().Equals("true") || 
                    value.Equals("1"))
                    chk = true;
            }
            catch { chk = false; }

            return chk;
        }
        
        public static string ToDate(this string value, string currentFormat, string changeFormat)
        {
            string chk = value;
            DateTime dtm;

            try
            {
                if (DateTime.TryParseExact(chk, currentFormat, null, System.Globalization.DateTimeStyles.None, out dtm))
                {
                    chk = dtm.ToString(changeFormat, CultureInfo.InvariantCulture);
                }
            }
            catch
            {
                chk = value;
            }

            return chk;
        }

        public static DateTime ToDate(this string value, string format)
        {
            string chk = value;
            DateTime dtm;

            if (!DateTime.TryParseExact(chk, format, null, System.Globalization.DateTimeStyles.None, out dtm))
                dtm = DateTime.Now;

            return dtm;
        }

        public static string ObjToStr(this object value)
        {
            string chk = "";

            if (value != null)
                chk = value.ToString();

            return chk;
        }

        public static string ObjToStr(this object value, string defaultValue)
        {
            string chk = defaultValue;

            if (value != null && !value.ToString().NoE())
                chk = value.ToString();

            return chk;
        }

        public static string ToBitString(this BitArray bits)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }

            return sb.ToString();
        }

        #endregion

        #region TYPE

        /// <summary>
        /// 타입 체크
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool ChkType(this object obj, Type t)
        {
            if (obj != null)
            {
                Type t2 = obj.GetType();
                return t.Equals(t2);
            }
            else
                return false;
        }

        public static bool IsEqualTo(this Type t, object obj)
        {
            if (obj != null)
            {
                Type t2 = (Type)obj;
                return t.Equals(t2);
            }
            else
                return false;
        }

        #endregion

        #region DATA TABLE

        /// <summary>
        /// 데이터 테이블 Pivot
        /// </summary>
        public static DataTable Pivot(this DataTable dt, 
                                      string col, 
                                      params string[] ignores)
        {
            if (dt == null) return null;

            DataTable tmpDt = new DataTable();

            if (col == "") col = dt.Columns[0].ColumnName;
            
            tmpDt.Columns.Add(col);

            List<string> valList = new List<string>();

            //Creates list of columns to ignore
            List<string> igList = new List<string>();

            if (ignores.Length > 0)
                igList.AddRange(ignores);

            if (!igList.Contains(col))
                igList.Add(col);

            string tmpCol = string.Empty;
            DataRow dr = null;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                tmpCol = dr[col].ToString();

                if (!valList.Contains(tmpCol))
                {
                    valList.Add(tmpCol);
                    tmpDt.Columns.Add(tmpCol);
                }
                else
                    throw new Exception(
                        string.Format("해당 Column은 고유하지 않습니다. Column = {0}", col));
            }

            DataColumn dc = null;
            for(int i=0; i< dt.Columns.Count; i++)
            {
                dc = dt.Columns[i];

                if (!valList.Contains(dc.ColumnName) && !igList.Contains(dc.ColumnName))
                {
                    dr = tmpDt.NewRow();
                    dr[0] = dc.ColumnName;
                    tmpDt.Rows.Add(dr);
                }
            }
            
            for (int i = 0; i < tmpDt.Rows.Count; i++)
            {
                for (int j = 1; j < tmpDt.Columns.Count; j++)
                {
                    tmpDt.Rows[i][j] =
                      dt.Rows[j - 1][tmpDt.Rows[i][0].ToString()].ToString();
                }
            }

            return tmpDt;
        }


        public static DataTable Pivot(this DataTable dt, string cX, string cY, string cZ, string nulls, bool sums)
        {
            if (dt == null) return null;

            DataTable tmpDt = new DataTable();

            if (cX == "")
                cX = dt.Columns[0].ColumnName;

            tmpDt.Columns.Add(cY);

            List<string> xvList = new List<string>();
            List<string> yvList = new List<string>();

            DataRow dr = null;
            DataRow drRet = null;
            DataColumn dc = null;
            string tmpCol = string.Empty;
            string tmpRow = string.Empty;

            for (int i=0; i<dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                tmpCol = dr[cX].ToString();

                if (!xvList.Contains(tmpCol))
                {
                    xvList.Add(tmpCol);
                    tmpDt.Columns.Add(tmpCol);
                }
            }
            
            if (cY != "" && cZ != "")
            {
                for(int i=0; i<dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    if (!yvList.Contains(dr[cY].ToString()))
                        yvList.Add(dr[cY].ToString());
                }
                
                DataRow[] rows = null;
                for (int i = 0; i < yvList.Count; i++)
                {
                    tmpCol = yvList[i];

                    drRet = tmpDt.NewRow();
                    drRet[0] = tmpCol;
                    
                    rows = dt.Select(cY + "='" + tmpCol + "'");

                    for (int j = 0; j < rows.Length; j++)
                    {
                        dr = rows[j];
                        tmpRow = dr[cX].ToString();

                        for (int k = 0; k < tmpDt.Columns.Count; k++)
                        {
                            dc = tmpDt.Columns[k];
                            if (dc.ColumnName == tmpRow)
                            {
                                if (sums)
                                {
                                    try
                                    {
                                        drRet[tmpRow] = Convert.ToDecimal(drRet[tmpRow]) + Convert.ToDecimal(dr[cZ]);
                                    }
                                    catch
                                    {
                                        drRet[tmpRow] = dr[cZ];
                                    }
                                }
                                else
                                {
                                    drRet[tmpRow] = dr[cZ];
                                }
                            }
                        }
                    }
                    
                    tmpDt.Rows.Add(drRet);
                }
                
            }
            else
            {
                throw new Exception("Pivot을 수행할 열이 없습니다.");
            }

            if (nulls != "")
            {
                for (int i = 0; i < tmpDt.Rows.Count; i++)
                {
                    for (int j = 0; j < tmpDt.Columns.Count; j++)
                    {
                        dr = tmpDt.Rows[i];
                        dc = tmpDt.Columns[j];

                        if (dr[dc.ColumnName].ToString() == "")
                            dr[dc.ColumnName] = nulls;
                    }
                }
            }

            return tmpDt;
        }
        
        public static DataTable UnPivot(this DataTable dt)
        {
            DataTable tmpDt = new DataTable();

            return tmpDt;
        }

        #endregion

        #region DateTime

        public static bool IsEmpty(this DateTime dt)
        {
            return dt == default(DateTime);
        }

        public static string ToNullString(this DateTime dt, string format)
        {
            string tmp = string.Empty;
            if (dt.IsEmpty())
                tmp = "";
            else
                tmp = dt.ToString(format);
            return tmp;
        }
        
        #endregion
    }
}
