using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!\n");

            Option opt1 = new Option("option 1", () => { });
            Option opt2 = new Option("option 2", () => { Console.WriteLine("Option 2 sélectionnée"); });
            Option opt3 = new Option("option 3", () => { });

            ContextualMenu cm = new ContextualMenu(false, opt1, opt2, opt3);

            cm.Perform();

        }
    }
    public class Option
    {
        public int Id { private get; set; }
        public string Description { get; private set; }
        public Action OptionAction { get; private set; }

        public Option(string description, Action action)
        {
            Id = 0;
            Description = description;
            OptionAction = action;
        }

        public override string ToString()
        {
            return $"\t- {Description} -";
        }
    }

    public class ContextualMenu
    {
        private readonly bool _horizontalDisplay;

        public List<Option> OptionList;
        public int OptionsCount { get => OptionList.Count; }

        public ContextualMenu(bool horizontal = false, params Option[] options)
        {
            if (options.Length == 0)
            {
                throw new ArgumentException($"At least one {options} parameter is needed");
            }

            OptionList = new List<Option>(options);

            for (int i = 0; i < OptionsCount; i++)
            {
                OptionList[i].Id = i + 1;
            }

            _horizontalDisplay = horizontal;
        }

        public void Perform()
        {
            Console.WriteLine(ToString() + '\n');
            GetOptionFromNumber().OptionAction();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Option option in OptionList)
            {
                if (_horizontalDisplay)
                {
                    sb.Append(option.ToString() + (option == OptionList[OptionsCount - 1] ? '\n' : '\t'));
                }
                else
                {
                    sb.AppendLine(option.ToString());
                }
            }

            return sb.ToString();
        }

        private Option GetOptionFromNumber()
        {
            bool stopMenu = false;
            bool validKey = false;
            List<ConsoleKey> validKeys = new List<ConsoleKey>() { ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.Enter };
            ConsoleKey keyPressed = ConsoleKey.NoName;
            int index = 0;

            Console.SetCursorPosition(8, 2 + index);
            Console.Write("> ");
            Console.SetCursorPosition(18, 2 + index);
            Console.Write(" <");

            while (!stopMenu)
            {
                while (!validKey)
                {
                    keyPressed = Console.ReadKey(true).Key;
                    validKey = validKeys.Contains(keyPressed);
                }

                Console.SetCursorPosition(8, 2 + index);
                Console.Write("  ");
                Console.SetCursorPosition(18, 2 + index);
                Console.Write("  ");

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    index--;
                } else if (keyPressed == ConsoleKey.DownArrow)
                {
                    index++;
                }

                Console.SetCursorPosition(8, 2 + index);
                Console.Write("> ");
                Console.SetCursorPosition(18, 2 + index);
                Console.Write(" <");

                stopMenu = keyPressed == ConsoleKey.Enter;
                validKey = false;
            }

            Console.SetCursorPosition(0, 7);
            return OptionList[index];
        }
    }
}
