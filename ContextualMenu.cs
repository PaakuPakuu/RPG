using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class ContextualMenu
    {
        private readonly bool _horizontalDisplay;

        private List<MenuItem> _optionList;
        private Point _position;
        private bool _centeredOnWindow;

        public ContextualMenu(int x, int y, bool horizontal = false)
        {
            _optionList = new List<MenuItem>();
            _horizontalDisplay = horizontal;
            _position = new Point(x, y);
            _centeredOnWindow = false;
        }

        public ContextualMenu(bool horizontal = false) : this(0, 0, horizontal)
        {
            _centeredOnWindow = true;
        }

        public void AddMenuItem(MenuItem menuItem) => _optionList.Add(menuItem);

        public void AddMenuItem(string description, Action action) => AddMenuItem(new MenuItem(description, action));

        public void Perform()
        {
            Console.WriteLine(ToString() + '\n');
            AskUserOption().MenuItemAction();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (MenuItem option in _optionList)
            {
                if (_horizontalDisplay)
                {
                    sb.Append(option.ToString()).Append(option == _optionList[^1] ? '\n' : '\t');
                }
                else
                {
                    sb.AppendLine(option.ToString());
                }
            }

            return sb.ToString();
        }

        private MenuItem AskUserOption()
        {
            bool stopMenu = false;
            bool validKey = false;
            int index = 0;

            List<ConsoleKey> validKeys = (!_horizontalDisplay ? DisplayTools.VerticalMenuKeys : DisplayTools.HorizontalMenuKeys);
            validKeys.AddRange(DisplayTools.UniversalKeys);
            ConsoleKey keyPressed = ConsoleKey.NoName;

            DisplayTools.WriteInWindowAt("> ", 8, 2);
            DisplayTools.WriteInWindowAt(" <", 18, 2);

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

                if (keyPressed == ConsoleKey.LeftArrow && _horizontalDisplay || keyPressed == ConsoleKey.UpArrow)
                {
                    index--;
                }
                else if (keyPressed == ConsoleKey.RightArrow && !_horizontalDisplay || keyPressed == ConsoleKey.DownArrow)
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

            return _optionList[index];
        }
    }
}
