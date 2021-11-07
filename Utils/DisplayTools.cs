using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RPG
{
    public static class DisplayTools
    {
        #region Console menus

        private const int MF_BYCOMMAND = 0x00000000;
        private const int SC_CLOSE = 0xF060;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        private static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        #endregion

        #region Console encoding

        private const string FONT_NAME = "DejaVu Sans Mono";
        private const int FONT_SIZE = 14;

        private const int LF_FACESIZE = 32;
        private const int STD_OUTPUT_HANDLE = -11;
        private const int TMPF_TRUETYPE = 4;
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        [StructLayout(LayoutKind.Sequential)]
        internal struct COORD
        {
            internal short X;
            internal short Y;

            internal COORD(short x, short y)
            {
                X = x;
                Y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private unsafe struct CONSOLE_FONT_INFO_EX
        {
            internal uint cbSize;
            internal uint nFont;
            internal COORD dwFontSize;
            internal int FontFamily;
            internal int FontWeight;
            internal fixed char FaceName[LF_FACESIZE];
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int dwType);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool SetCurrentConsoleFontEx(
            IntPtr consoleOutput,
            bool maximumWindow,
            ref CONSOLE_FONT_INFO_EX consoleCurrentFontEx);

        #endregion

        #region ANSI Codes

        public static readonly string Reset = "\u001b[0m";
        public static readonly string Bold = "\u001b[1m";
        public static readonly string Underlined = "\u001b[4m";
        public static readonly string Reversed = "\u001b[7m";

        #endregion

        public static readonly Point WindowSize = new Point(80, 35);
        //public static readonly Point WindowCenter = new Point(WindowSize.X / 2, WindowSize.Y / 2);

        public static readonly List<ConsoleKey> UniversalKeys = new List<ConsoleKey>() { ConsoleKey.Enter };
        public static readonly List<ConsoleKey> VerticalMenuKeys = new List<ConsoleKey>() { ConsoleKey.UpArrow, ConsoleKey.DownArrow };
        public static readonly List<ConsoleKey> HorizontalMenuKeys = new List<ConsoleKey>() { ConsoleKey.LeftArrow, ConsoleKey.RightArrow };

        public static void InitializeWindow()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            SetConsoleFont(FONT_NAME);
            DisableResizeCloseConsoleMenus();

            Console.Title = "RPG";
            Console.SetWindowSize(WindowSize.X, WindowSize.Y);
            Console.SetBufferSize(WindowSize.X, WindowSize.Y);
            Console.CursorVisible = false;
        }

        public static void WriteInBufferAt(string display, int x, int y)
        {
            List<string> splittedDisplay = new List<string>(display.Split('\n'));

            for (int i = 0; i < splittedDisplay.Count; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(splittedDisplay[i]);
            }
        }

        public static void WriteInWindowAt(string display, int x, int y)
            => WriteInBufferAt(display, Console.WindowLeft + x, Console.WindowTop + y);

        /// <summary>
        /// Disable resizing and closing actions on console.
        /// Source : https://social.msdn.microsoft.com/Forums/vstudio/en-US/1aa43c6c-71b9-42d4-aa00-60058a85f0eb/c-console-window-disable-resize
        /// </summary>
        /// <remarks>
        /// On Window 10, user still can stick the window on the top of the screen to maximize.
        /// </remarks>
        private static void DisableResizeCloseConsoleMenus()
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }
        }

        /// <summary>
        /// Set console font on <c>fontName</c>.
        /// Source : https://social.msdn.microsoft.com/Forums/sqlserver/en-US/c276b9ae-dc4c-484a-9a59-1ee66cf0f1cc/c-changing-console-font-programmatically?forum=csharpgeneral
        /// </summary>
        /// <param name="fontName">Font name string.</param>
        private static void SetConsoleFont(string fontName = "Lucida Console")
        {
            unsafe
            {
                IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);
                if (hnd != INVALID_HANDLE_VALUE)
                {
                    CONSOLE_FONT_INFO_EX info = new CONSOLE_FONT_INFO_EX();
                    info.cbSize = (uint)Marshal.SizeOf(info);

                    // Set console font to Lucida Console.
                    CONSOLE_FONT_INFO_EX newInfo = new CONSOLE_FONT_INFO_EX();
                    newInfo.cbSize = (uint)Marshal.SizeOf(newInfo);
                    newInfo.FontFamily = TMPF_TRUETYPE;
                    IntPtr ptr = new IntPtr(newInfo.FaceName);
                    Marshal.Copy(fontName.ToCharArray(), 0, ptr, fontName.Length);

                    // Get some settings from current font.
                    //newInfo.dwFontSize = new COORD(info.dwFontSize.X, info.dwFontSize.Y);
                    newInfo.dwFontSize = new COORD(info.dwFontSize.X, FONT_SIZE);
                    newInfo.FontWeight = info.FontWeight;
                    SetCurrentConsoleFontEx(hnd, false, ref newInfo);
                }
            }
        }
    }
}
