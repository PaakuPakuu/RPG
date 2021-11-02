using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    class Program
    {
        static void Main(string[] args)
        {
            /*int windowSizeX = 70;
            int windowSizeY = 30;

            Console.Title = "RPG";
            Console.CursorVisible = false;
            Console.SetWindowSize(windowSizeX, windowSizeY);

            // Buffer tests

            int mapSizeX = 140;
            int mapSizeY = 60;
            int windowPositionX = (mapSizeX - windowSizeX) / 2;
            int windowPositionY = (mapSizeY - windowSizeY) / 2;

            Console.SetBufferSize(mapSizeX, mapSizeY);
            for (int i = 0; i < mapSizeY; i++)
            {
                for (int j = 0; j < mapSizeX / 2; j++)
                {
                    if (i % 2 == 0)
                    {
                        Console.Write(((i + j) % 2 == 0 ? "# " : "@ "));
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                Console.WriteLine();
            }

            Console.SetWindowPosition(windowPositionX, windowPositionY);
            Console.ReadKey();*/

            Game.GameInstance.LaunchGame();
        }
    }
}
