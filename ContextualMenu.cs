using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public enum BorderStyle
    {
        None,
        SimpleHard,
        SimpleCurved,
        DoubleHard,
        DoubleCurved,
        Dashed
    }

    public class ContextualMenu
    {
        private readonly bool _horizontalDisplay;
        private Point _position;
        private bool _centeredText;
        private bool _centeredOnWindow;
        private BorderStyle _borderStyle;
        private int _padding;
        private int _maxMenuItemLength;
        private List<MenuItem> _optionList;

        public ContextualMenu(int x, int y, bool horizontal = false, bool centered = true, BorderStyle borderStyle = BorderStyle.None, int padding = 0)
        {
            _horizontalDisplay = horizontal;
            _optionList = new List<MenuItem>();
            _position = new Point(x, y);
            _centeredText = centered;
            _centeredOnWindow = false;
            _borderStyle = borderStyle;
            _padding = padding + 1;
            _maxMenuItemLength = 0;
        }

        public ContextualMenu(bool horizontal = false, bool centered = true, bool arrow = true, BorderStyle borderStyle = BorderStyle.None, int padding = 0)
            : this(0, 0, horizontal, centered, borderStyle, padding)
        {
            _centeredOnWindow = true;
        }

        public void AddMenuItem(MenuItem menuItem)
        {
            _optionList.Add(menuItem);
            
            if (menuItem.ToString().Length > _maxMenuItemLength)
            {
                _maxMenuItemLength = menuItem.ToString().Length;
            }
        }

        public void AddMenuItem(string description, Action action) => AddMenuItem(new MenuItem(description, action));

        public void Execute()
        {
            if (_centeredOnWindow)
            {
                _position.X = (DisplayTools.WindowSize.X - _maxMenuItemLength) / 2;
                _position.Y = (DisplayTools.WindowSize.Y - _optionList.Count) / 2;
            }

            DisplayTools.WriteInWindowAt(ToString(), _position.X, _position.Y);

            //AskUserOption().MenuItemAction();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // factoriser ?
            foreach (MenuItem menuItem in _optionList)
            {
                if (_horizontalDisplay)
                {
                    sb.Append(menuItem.ToString());

                    if (menuItem != _optionList[^1])
                    {
                        sb.Append(' ', _padding);
                    }
                }
                else
                {
                    if (_centeredText)
                    {
                        sb.Append(' ', (_maxMenuItemLength - menuItem.ToString().Length) / 2 + 1);
                    }

                    sb.Append(menuItem.ToString());

                    if (menuItem != _optionList[^1])
                    {
                        sb.Append('\n', _padding);
                    }
                }
            }

            return sb.ToString();
        }

        private MenuItem AskUserOption()
        {
            bool stopMenu = false;
            bool validKey = false;
            int index = 0;
            int x1 = _position.X - 2;
            int x2 = _position.X + 2;
            int y = _position.Y + index + _padding;

            List<ConsoleKey> validKeys = (!_horizontalDisplay ? DisplayTools.VerticalMenuKeys : DisplayTools.HorizontalMenuKeys);
            validKeys.AddRange(DisplayTools.UniversalKeys);
            ConsoleKey keyPressed = ConsoleKey.NoName;

            while (!stopMenu)
            {
                DisplayTools.WriteInWindowAt("> ", x1, y);
                DisplayTools.WriteInWindowAt(" <", x2, y);

                while (!validKey)
                {
                    keyPressed = Console.ReadKey(true).Key;
                    validKey = validKeys.Contains(keyPressed);
                }

                DisplayTools.WriteInWindowAt("  ", x1, y);
                DisplayTools.WriteInWindowAt("  ", x2, y);

                if (keyPressed == ConsoleKey.LeftArrow && _horizontalDisplay || keyPressed == ConsoleKey.UpArrow)
                {
                    index--;
                }
                else if (keyPressed == ConsoleKey.RightArrow && !_horizontalDisplay || keyPressed == ConsoleKey.DownArrow)
                {
                    index++;
                }

                stopMenu = keyPressed == ConsoleKey.Enter;
                validKey = false;
            }

            return _optionList[index];
        }
    }
}
