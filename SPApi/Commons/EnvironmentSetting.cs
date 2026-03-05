using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPApi.Commons
{
    /// <summary>
    /// 환경변수 설정
    /// </summary>
    public class EnvironmentSetting
    {
        private const string LIB_HOME = @"\Library";

        public void SetEnvironmentVariables()
        {
            SetLibraryDir();
            SetOracle();
        }

        private void SetLibraryDir()
        {
            string lib_home = Environment.CurrentDirectory + LIB_HOME;
            Environment.SetEnvironmentVariable("PATH", lib_home, EnvironmentVariableTarget.Process);
        }

        private void SetOracle()
        {
            string oracle_home = Environment.CurrentDirectory + LIB_HOME;
            Environment.SetEnvironmentVariable("ORACLE_HOME", oracle_home, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("ORA_TZFILE", null);
            string nlsLang = Environment.GetEnvironmentVariable("NLS_LANG");
            if (nlsLang.NoE())//KOREAN_KOREA.KO16MSWIN949 , OR string.Empty
            {
                Environment.SetEnvironmentVariable("NLS_LANG", "KOREAN_KOREA.KO16MSWIN949");
            }

            Environment.SetEnvironmentVariable("NLS_DATE_FORMAT", "RR/MM/DD");
            Environment.SetEnvironmentVariable("NLS_TIME_FORMAT", "HH24:MI:SSXFF");
            Environment.SetEnvironmentVariable("NLS_TIMESTAMP_FORMAT", "RR/MM/DD HH24:MI:SSXFF");
            Environment.SetEnvironmentVariable("NLS_TIME_TZ_FORMAT", "HH24:MI:SSXFF TZR");
            Environment.SetEnvironmentVariable("NLS_TIMESTAMP_TZ_FORMAT", "RR/MM/DD HH24:MI:SSXFF TZR");
        }
    }
}
