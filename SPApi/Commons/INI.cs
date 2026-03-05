using SPApi.Declaration.Structs;
using SPApi.Customs.PropertyGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace SPApi.Commons
{
    public class INI
    {
        private static string _path = @"./Setting.ini";
        public static string IniPath
        {
            get { return _path; }
            set { _path = value; }
        }

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        public static void Write(string Section, string Key, string Value)
        {
            NativeMethods.WritePrivateProfileString(Section, Key, Value, IniPath);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        public static string Read(string Section, string Key, string Default)
        {
            StringBuilder temp = new StringBuilder(255);

            int i = NativeMethods.GetPrivateProfileString(Section, Key, Default, temp, 255, IniPath);

            return temp.ToString();
        }

        /// <summary>
        /// Read All Data From the Ini File
        /// </summary>
        /// <returns></returns>
        public static INI_INFO[] ReadAll()
        {
            List<INI_INFO> infos = new List<INI_INFO>();
            List<INI_ENTRY_INFO> entryInfos = new List<INI_ENTRY_INFO>();

            //전체 섹션 가져오기
            string[] sections = ReadSectionNames();
            string[] entrys = null;

            //각 섹션에 해당하는 엔트리 항목 및 INI 값 가져오기
            foreach(string section in sections)
            {
                entrys = null;
                entryInfos.Clear();

                entrys = ReadEntryNames(section);
                foreach (string entry in entrys)
                {
                    entryInfos.Add(new INI_ENTRY_INFO()
                    {
                        Key = entry,
                        Value = Read(section, entry, "")
                    });
                }

                infos.Add(new INI_INFO()
                {
                    Section = section,
                    Entry = entryInfos.ToArray()
                });
            }

            return infos.ToArray();
        }

        /// <summary>
        /// Read All Data(Properties conversion) From the Ini File
        /// </summary>
        /// <returns></returns>
        public static object GetProperties()
        {
            //각 섹션에 해당하는 엔트리 항목 및 INI 값 가져오기
            IDictionary dict = new Hashtable();
            
            //전체 섹션 가져오기
            string[] sections = ReadSectionNames();
            string[] entrys = null;

            //각 섹션에 해당하는 엔트리 항목 및 INI 값 가져오기
            foreach (string section in sections)
            {
                entrys = null;
                entrys = ReadEntryNames(section);

                foreach (string entry in entrys)
                {
                    dict.Add($"{section}.{entry}", Read(section, entry, ""));
                }
            }

            return PropertyChangeAttribute(dict);
        }

        /// <summary>
        /// Write All Data(Properties conversion) To the Ini File
        /// </summary>
        /// <returns></returns>
        public static void SetProperties(object pValues)
        {
            DictionaryPropertyGridAdapter adapter = (DictionaryPropertyGridAdapter)pValues;

            string[] tmps = null;

            //각 섹션에 해당하는 엔트리 항목 및 INI 값 가져오기
            foreach (DictionaryEntry pvalue in (IDictionary)adapter.GetPropertyOwner())
            {
                tmps = pvalue.Key.ToString().Split('.');
                if (tmps == null || !tmps.Length.Equals(2)) continue;
                
                Write(tmps[0], tmps[1], pvalue.Value.ToString());
            }
        }

        /// <summary>
        /// PropertyValue Category, DisplayName Attribute Change
        /// </summary>
        /// <returns></returns>
        private static DictionaryPropertyGridAdapter PropertyChangeAttribute(IDictionary dict)
        {
            DictionaryPropertyGridAdapter adapter = new DictionaryPropertyGridAdapter(dict);
            PropertyDescriptorCollection props = adapter.GetProperties(new Attribute[0]);
            CategoryAttribute cattr = (CategoryAttribute)props[0].Attributes[typeof(CategoryAttribute)];
            FieldInfo finfo = cattr.GetType().GetField("categoryValue", BindingFlags.NonPublic | BindingFlags.Instance);
            finfo.SetValue(cattr, "INI_SETTING");

            return adapter;
        }

        
        /// <summary>
        /// INI 파일 내부의 전체 Section 이름 가져오기
        /// </summary>
        /// <returns></returns>
        private static string[] ReadSectionNames()
        {
            for (int maxsize = 500; true; maxsize *= 2)
            {
                byte[] bytes = new byte[maxsize];
                int size = NativeMethods.GetPrivateProfileString(0, "", "", bytes, maxsize, IniPath);


                if (size < maxsize - 2)
                {
                    string Selected = Encoding.ASCII.GetString(bytes, 0, size - (size > 0 ? 1 : 0));
                    return Selected.Split(new char[] { '\0' });
                }
            }
        }

        /// <summary>
        /// 섹션에 해당하는 전체 Entry 이름 가져오기
        /// </summary>
        /// <returns></returns>
        private static string[] ReadEntryNames(string section)
        {
            for (int maxsize = 500; true; maxsize *= 2)
            {
                byte[] bytes = new byte[maxsize];
                int size = NativeMethods.GetPrivateProfileString(section, 0, "", bytes, maxsize, IniPath);

                if (size < maxsize - 2)
                {
                    string entries = Encoding.ASCII.GetString(bytes, 0, size - (size > 0 ? 1 : 0));
                    return entries.Split(new char[] { '\0' });
                }
            }
        }
    }
}
