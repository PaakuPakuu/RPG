using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "RPG";
            Console.WriteLine("Hello World!\n");

            MenuItem opt1 = new MenuItem("option 1", () => { Console.WriteLine("Option 1 sélectionnée"); });
            MenuItem opt2 = new MenuItem("option 2", () => { Console.WriteLine("Option 2 sélectionnée"); });
            MenuItem opt3 = new MenuItem("option 3", () => { Console.WriteLine("Option 3 sélectionnée"); });

            ContextualMenu cm = new ContextualMenu(false, opt1, opt2, opt3);

            Console.CursorVisible = false;
            cm.Perform();

            Console.MoveBufferArea(0, 0, 20, 10, 15, 15);
            Console.ReadKey();
        }
    }
}
