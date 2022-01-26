using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GeneralUtils
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

        // Colors
        public static readonly string Yellow = "\u001b[33m";
        public static readonly string Green = "\u001b[32m";
        public static readonly string Grey = "\u001b[90m";
        public static readonly string Red = "\u001b[31m";

        #endregion

        public static readonly int WindowWidth = 80;
        public static readonly int WindowHeight = 35;
        public static readonly int EditorWindowWidth = 100;
        public static readonly int EditorWindowHeight = 40;
        //public static readonly Point WindowCenter = new Point(WindowSize.X / 2, WindowSize.Y / 2);
        public static readonly int WidthMargin = 4;
        public static readonly int HeightMargin = 2;

        private static readonly int TEXT_ANIMATION_INTERVAL = 10;
        private static readonly int TEXT_ANIMATION_MAX_ROWS = 4;
        private static readonly int TEXT_ANIMATION_MAX_WIDTH = (int)(WindowWidth * (3f / 4));

        public static readonly List<ConsoleKey> UniversalKeys = new List<ConsoleKey>() { ConsoleKey.Enter };
        public static readonly List<ConsoleKey> VerticalMenuKeys = new List<ConsoleKey>() { ConsoleKey.UpArrow, ConsoleKey.DownArrow };
        public static readonly List<ConsoleKey> HorizontalMenuKeys = new List<ConsoleKey>() { ConsoleKey.LeftArrow, ConsoleKey.RightArrow };

        public enum BorderStyle
        {
            Simple,
            SimpleHeavy,
            SimpleCurved,
            Double,
            Dashed,
            DashedHeavy
        }

        private static readonly string[] _borderSets = new string[]
            {
                "─│┐┘└┌",
                "━┃┓┛┗┏",
                "─│╮╯╰╭",
                "═║╗╝╚╔",
                "╌╎┐┘└┌",
                "╍╏┓┛┗┏"
            };

        public static void InitializeGameWindow()
        {
            Console.OutputEncoding = Encoding.UTF8;
            SetConsoleFont(FONT_NAME);
            DisableResizeConsoleMenus();

            Console.Title = "RPG";
            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.SetBufferSize(WindowWidth, WindowHeight);
            Console.CursorVisible = false;
        }

        public static void InitializeEditorWindow()
        {
            Console.OutputEncoding = Encoding.UTF8;
            SetConsoleFont(FONT_NAME);
            DisableResizeConsoleMenus();

            Console.Title = "RPG Editor";
            Console.SetWindowSize(EditorWindowWidth, EditorWindowHeight);
            Console.SetBufferSize(EditorWindowWidth, EditorWindowHeight);
            Console.CursorVisible = false;
        }

        #region Console menus changes methods

        /// <summary>
        /// Disable resizing and closing actions on console.
        /// Source : https://social.msdn.microsoft.com/Forums/vstudio/en-US/1aa43c6c-71b9-42d4-aa00-60058a85f0eb/c-console-window-disable-resize
        /// </summary>
        /// <remarks>
        /// On Window 10, user still can stick the window on the top of the screen to maximize.
        /// </remarks>
        private static void DisableResizeConsoleMenus()
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                //DeleteMenu(sysMenu, SC_CLOSE, MF_BYCOMMAND);
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

        #endregion

        #region Write on console methods

        #region Not animated

        public static void WriteInBufferAt(string[] display, int x, int y, bool animated = false)
        {
            if (animated)
            {
                for (int i = 0; i < display.Length; i++)
                {
                    for (int j = 0; j < display[i].Length; j++)
                    {
                        Console.SetCursorPosition(x + j, y + i);
                        Console.Write(display[i][j]);
                        System.Threading.Thread.Sleep(TEXT_ANIMATION_INTERVAL);
                    }
                }
            } else
            {
                for (int i = 0; i < display.Length; i++)
                {
                    Console.SetCursorPosition(x, y + i);
                    Console.Write(display[i]);
                }
            }
        }

        public static void WriteInBufferAt(string display, int x, int y)
        {
            WriteInBufferAt(display.Split('\n'), x, y);
        }

        public static void WriteInWindowAt(string[] display, int x, int y)
        {
            WriteInBufferAt(display, Console.WindowLeft + x, Console.WindowTop + y);
        }

        public static void WriteInWindowAt(string display, int x, int y)
        {
            WriteInBufferAt(display, Console.WindowLeft + x, Console.WindowTop + y);
        }

        public static void WriteInWindowCenter(string[] display, int x = -1, int y = -1, bool animated = false)
        {
            int newX = (x == -1 ? (WindowWidth - display.Max(l => l.Length)) / 2 : x);
            int newY = (y == -1 ? (WindowHeight - display.Length) / 2 : y);

            WriteInBufferAt(display, newX, newY, animated);
        }

        public static void WriteInWindowCenter(string display, int x = -1, int y = -1, bool animated = false)
        {
            WriteInWindowCenter(display.Split('\n'), x, y, animated);
        }

        #endregion

        #region Animated

        private static string[] SplitByLength(string display, int maxLength)
        {
            List<string> splitted = new List<string>();

            while (display.Length >= maxLength)
            {
                
                splitted.Add(display.Substring(0, maxLength));
                display = display[maxLength..];
            }

            if (!string.IsNullOrEmpty(display))
            {
                splitted.Add(display);
            }

            return splitted.ToArray();
        }

        private static string[] SplitByLength(string[] display)
        {
            List<string> splitted = new List<string>();

            foreach (string line in display)
            {
                splitted.AddRange(SplitByLength(line, TEXT_ANIMATION_MAX_WIDTH));
            }

            return splitted.ToArray();
        }

        //private static string[] SplitByLengthAndLine(string display)
        //{
        //    string[] splitted = display.Split('\n');
        //    List<string> allSplitted = new List<string>();

        //    foreach (string sub in splitted)
        //    {
        //        allSplitted.AddRange(SplitByLength(sub));
        //    }

        //    return allSplitted.ToArray();
        //}

        public static void WriteInWindowAnimated(string[] display)
        {
            List<string> displayList = new List<string>(SplitByLength(display));
            string[] lines;
            int indexToDelete;
            int positionY = WindowHeight - TEXT_ANIMATION_MAX_ROWS - 1;

            PrintBox(-1, positionY - 1, TEXT_ANIMATION_MAX_WIDTH + 8, 6, BorderStyle.DashedHeavy);
            Console.CursorVisible = true;

            while (displayList.Count > 0)
            {
                indexToDelete = Math.Min(displayList.Count, TEXT_ANIMATION_MAX_ROWS);
                lines = displayList.GetRange(0, indexToDelete).ToArray();
                WriteInWindowCenter(lines, y: positionY, animated: true);
                Console.ReadKey(true);
                ClearBox(-1, positionY - 1, TEXT_ANIMATION_MAX_WIDTH + 8, TEXT_ANIMATION_MAX_ROWS);
                displayList.RemoveRange(0, indexToDelete);
            }

            ClearBox(-1, positionY, TEXT_ANIMATION_MAX_WIDTH + 9, TEXT_ANIMATION_MAX_ROWS + 1);
            Console.CursorVisible = false;
        }

        #endregion

        #endregion

        public static void PrintBox(int x, int y, int width, int height, BorderStyle borderStyle)
        {
            string borderSet = _borderSets[(int)borderStyle];
            StringBuilder sb = new StringBuilder();

            sb.Append(borderSet[0], width).AppendLine();
            for (int i = 1; i < height - 1; i++)
            {
                sb.Append(borderSet[1]).Append(' ', width - 2).Append($"{borderSet[1]}\n");
            }
            sb.Append(borderSet[0], width);

            // Vertices
            sb[0] = borderSet[5];
            sb[width - 1] = borderSet[2];
            sb[((width + 1) * (height - 1)) + 1] = borderSet[4];
            sb[((width + 1) * height) - 1] = borderSet[3];
            
            WriteInWindowCenter(sb.ToString(), x, y);
        }

        public static void ClearBox(int x, int y, int width, int height)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < height; i++)
            {
                sb.Append(' ', width);
                if (i != height - 1)
                {
                    sb.AppendLine();
                }
            }

            WriteInWindowCenter(sb.ToString(), x, y);
        }
    }
}
