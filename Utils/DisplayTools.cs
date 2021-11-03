using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RPG
{
    public static class DisplayTools
    {
        private const int MF_BYCOMMAND = 0x00000000;
        private const int SC_CLOSE = 0xF060;
        private const int SC_MINIMIZE = 0xF020;
        private const int SC_MAXIMIZE = 0xF030;
        private const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        private static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        public static readonly Point WindowSize = new Point(70, 30);
        //public static readonly Point WindowCenter = new Point(WindowSize.X / 2, WindowSize.Y / 2);

        public static readonly List<ConsoleKey> UniversalKeys = new List<ConsoleKey>() { ConsoleKey.Enter };
        public static readonly List<ConsoleKey> VerticalMenuKeys = new List<ConsoleKey>() { ConsoleKey.UpArrow, ConsoleKey.DownArrow };
        public static readonly List<ConsoleKey> HorizontalMenuKeys = new List<ConsoleKey>() { ConsoleKey.LeftArrow, ConsoleKey.RightArrow };

        public static void InitializeWindow()
        {
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

        /*public static void WriteInWindowCenter(string display)
        {
            List<string> splittedDisplay = new List<string>(display.Split('\n'));
            int x = (WindowSize.X - splittedDisplay.Max(s => s.Length)) / 2;
            int y = (WindowSize.Y - splittedDisplay.Count) / 2;

            WriteInWindowAt(display, x, y);
        }*/

        /// <summary>
        /// Disable resizing and closing actions on console.
        /// Source : https://social.msdn.microsoft.com/Forums/vstudio/en-US/1aa43c6c-71b9-42d4-aa00-60058a85f0eb/c-console-window-disable-resize
        /// </summary>
        /// <remarks>
        /// On Window 10, user still can stick the window on top of the screen to maximize.
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
    }
}
