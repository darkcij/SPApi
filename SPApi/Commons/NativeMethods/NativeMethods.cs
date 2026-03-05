using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace SPApi.Commons
{
    public static class NativeMethods
    {
        /// <summary>
        /// SendMessage
        /// </summary>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        //'API to allow Click through on the space around buttonarea
        public static readonly int WM_LBUTTONDBLCLK = 515;
        public static readonly int WM_LBUTTONDOWN = 513;
        public static readonly int WM_LBUTTONUP = 514;
        public static readonly int WM_MBUTTONDBLCLK = 521;
        public static readonly int WM_MBUTTONDOWN = 519;
        public static readonly int WM_MBUTTONUP = 520;
        public static readonly int WM_MOUSEMOVE = 512;
        public static readonly int WM_RBUTTONDBLCLK = 518;
        public static readonly int WM_RBUTTONDOWN = 516;
        public static readonly int WM_RBUTTONUP = 517;
        public static readonly int WM_NLBUTTONDOWN = 0xA1;
        public static readonly IntPtr HT_CAPTION = (IntPtr)0x2;


        /// <summary>
        /// ReleaseCapture
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        /// <summary>
        /// INI Write
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern int WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary>
        /// INI Read
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// INI Read
        /// </summary>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string Section, int Key, string Value, [MarshalAs(UnmanagedType.LPArray)] byte[] Result, int Size, string FileName);

        /// <summary>
        /// INI Read
        /// </summary>
        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(int Section, string Key, string Value, [MarshalAs(UnmanagedType.LPArray)] byte[] Result, int Size, string FileName);

        /// <summary>
        /// 시스템 시간 설정
        /// </summary>
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SetSystemTime(ref SYSTEMTIME st);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern long ShellExecute(long hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowcmd);
    }

    /// <summary>
    /// 시스템 시간 구조체
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME
    {
        public short wYear;
        public short wMonth;
        public short wDayOfWeek;
        public short wDay;
        public short wHour;
        public short wMinute;
        public short wSecond;
        public short wMilliseconds;
    }
}
