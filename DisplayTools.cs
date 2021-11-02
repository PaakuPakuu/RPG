using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public static class DisplayTools
    {
        public static readonly int WindowWidth = 70;
        public static readonly int WindowHeight = 30;

        public static readonly List<ConsoleKey> UniversalKeys = new List<ConsoleKey>() { ConsoleKey.Enter };
        public static readonly List<ConsoleKey> VerticalMenuKeys = new List<ConsoleKey>() { ConsoleKey.UpArrow, ConsoleKey.DownArrow };
        public static readonly List<ConsoleKey> HorizontalMenuKeys = new List<ConsoleKey>() { ConsoleKey.LeftArrow, ConsoleKey.RightArrow };

        public static void InitializeWindow()
        {
            Console.SetWindowSize(WindowWidth, WindowHeight);
            Console.CursorVisible = false;
        }

        public static void WriteInBufferAt(string display, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(display);
        }

        public static void WriteInWindowAt(string display, int x, int y)
            => WriteInBufferAt(display, Console.WindowLeft + x, Console.WindowTop + y);

        public static void WriteInBoxAt(string display, int width, int height, int x, int y, bool centered = false)
        {
            throw new NotImplementedException();
        }
    }
}
