using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class ContextualMenu
    {
        private const int SELECTORS_PADDING = 2;

        private readonly Point _position;
        private readonly bool _horizontalDisplay;
        private readonly bool _centeredText;
        private readonly bool _centeredOnWindow;
        private readonly int _padding;
        private readonly BorderStyle _borderStyle;
        private readonly SelectedStyle _selectedStyle;
        private readonly List<MenuItem> _optionList;

        private readonly string[] _borderSets = new string[]
            {
                "─│┐┘└┌",
                "━┃┓┛┗┏",
                "─│╮╯╰╭",
                "═║╗╝╚╔",
                "╌╎┐┘└┌",
                "╍╏┓┛┗┏"
            };
        private readonly string[][] _selectionSets = new string[][]
            {
                new string[2] { ">", "" },
                new string[2] { ">", "<" },
                new string[2] { "-", "-" },
                new string[2] { DisplayTools.Bold, DisplayTools.Reset },
                new string[2] { DisplayTools.Underlined, DisplayTools.Reset },
                new string[2] { DisplayTools.Reversed, DisplayTools.Reset },
                new string[2] { DisplayTools.Yellow, DisplayTools.Reset },
                new string[2] { DisplayTools.Green, DisplayTools.Reset }
            };

        private int _maxMenuItemLength;

        public enum BorderStyle
        {
            None,
            Simple,
            SimpleHeavy,
            SimpleCurved,
            Double,
            Dashed,
            DashedHeavy
        }

        public enum SelectedStyle
        {
            Arrow,
            DoubleArrow,
            Dashes,
            Bold,
            Underlined,
            Reversed,
            Yellow,
            Green
        }

        public ContextualMenu(int x, int y, bool horizontal = false, bool centered = false, int padding = 0,
            BorderStyle borderStyle = BorderStyle.None, SelectedStyle selectedStyle = SelectedStyle.Reversed)
        {
            _horizontalDisplay = horizontal;
            _optionList = new List<MenuItem>();
            _position = new Point(x, y);
            _centeredText = centered && !horizontal;
            _centeredOnWindow = false;
            _borderStyle = borderStyle;
            _selectedStyle = selectedStyle;
            _padding = padding + 1;
            _maxMenuItemLength = 0;

            if (_borderSets.Length != Enum.GetValues(borderStyle.GetType()).Length - 1)
            {
                throw new Exception("The border sets number must be equal to border style number.");
            }

            if (_selectionSets.Length != Enum.GetValues(selectedStyle.GetType()).Length)
            {
                throw new Exception("The selection sets number must be equal to selection style number.");
            }
        }

        public ContextualMenu(bool horizontal = false, bool centered = false, int padding = 0,
            BorderStyle borderStyle = BorderStyle.None, SelectedStyle selectedStyle = SelectedStyle.Reversed)
            : this(0, 0, horizontal, centered, padding, borderStyle, selectedStyle)
        {
            _centeredOnWindow = true;
        }

        public void AddMenuItem(string text, Action action)
        {
            MenuItem menuItem = new MenuItem(text, action);
            _optionList.Add(menuItem);

            if (menuItem.ToString().Length > _maxMenuItemLength)
            {
                _maxMenuItemLength = menuItem.ToString().Length;
            }
        }

        public void Execute()
        {
            InitializeMenu();
            ShowMenu();
            StartSelection().MenuItemAction();
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (MenuItem menuItem in _optionList)
            {
                sb.Append(menuItem.ToString());

                if (menuItem != _optionList[^1])
                {
                    sb.Append((_horizontalDisplay ? ' ' : '\n'), _padding);
                }
            }

            return sb.ToString();
        }

        private void InitializeMenu()
        {
            if (_centeredOnWindow)
            {
                _position.X = (DisplayTools.WindowWidth - (_horizontalDisplay ? ToString().Length : _maxMenuItemLength)) / 2;
                _position.Y = (DisplayTools.WindowHeight - (_horizontalDisplay ? 0 : _optionList.Count * _padding)) / 2 + 1;
            }

            Point currentPosition;
            MenuItem previousItem;

            for (int i = 0; i < _optionList.Count; i++)
            {
                currentPosition = _optionList[i].Position;
                currentPosition.Y = _position.Y;

                if (_horizontalDisplay)
                {
                    if (i == 0)
                    {
                        currentPosition.X = _position.X;
                    } else
                    {
                        previousItem = _optionList[i - 1];
                        currentPosition.X = previousItem.Position.X + previousItem.Text.Length + _padding;
                    }
                }
                else
                {
                    currentPosition.X = _position.X + (_centeredText ? (_maxMenuItemLength - _optionList[i].Text.Length) / 2 : 0);
                    currentPosition.Y += i * _padding;
                }
            }
        }

        private void ShowMenu()
        {
            foreach (MenuItem menu in _optionList)
            {
                DisplayTools.WriteInWindowAt(menu.Text, menu.Position.X, menu.Position.Y);
            }
        }

        private MenuItem StartSelection(int startIndex = 0)
        {
            
            bool stopMenu = false;
            bool validKey = false;
            int index = startIndex;
            string leftSelector = _selectionSets[(int)_selectedStyle][0];
            string rightSelector = _selectionSets[(int)_selectedStyle][1];
            Point leftSelectorPosition;
            Point rightSelectorPosition;
            MenuItem menu;

            List<ConsoleKey> validKeys = (!_horizontalDisplay ? DisplayTools.VerticalMenuKeys : DisplayTools.HorizontalMenuKeys);
            validKeys.AddRange(DisplayTools.UniversalKeys);
            ConsoleKey keyPressed = ConsoleKey.NoName;

            while (!stopMenu)
            {
                menu = _optionList[index];
                leftSelectorPosition = menu.Position - new Point(x: SELECTORS_PADDING);
                rightSelectorPosition = menu.Position + new Point(x: menu.Text.Length + SELECTORS_PADDING - 1);

                // Write selectors
                DisplayTools.WriteInWindowAt(leftSelector, leftSelectorPosition.X, leftSelectorPosition.Y);
                DisplayTools.WriteInWindowAt(menu.Text, menu.Position.X, menu.Position.Y);
                DisplayTools.WriteInWindowAt(rightSelector, rightSelectorPosition.X, rightSelectorPosition.Y);

                while (!validKey)
                {
                    keyPressed = Console.ReadKey(true).Key;
                    validKey = validKeys.Contains(keyPressed);
                }

                if (keyPressed == ConsoleKey.LeftArrow && _horizontalDisplay || keyPressed == ConsoleKey.UpArrow)
                {
                    if (index > 0)
                    {
                        index--;
                    }
                }
                else if (keyPressed == ConsoleKey.RightArrow && _horizontalDisplay || keyPressed == ConsoleKey.DownArrow)
                {
                    if (index < _optionList.Count - 1)
                    {
                        index++;
                    }
                }

                // Erase old selectors
                DisplayTools.WriteInWindowAt("  ", leftSelectorPosition.X, leftSelectorPosition.Y);
                DisplayTools.WriteInWindowAt(menu.Text, menu.Position.X, menu.Position.Y);
                DisplayTools.WriteInWindowAt("  ", rightSelectorPosition.X, rightSelectorPosition.Y);

                stopMenu = keyPressed == ConsoleKey.Enter;
                validKey = false;
            }

            return _optionList[index];
        }
    }
}
