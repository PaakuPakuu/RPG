using System;

namespace GeneralUtils
{
    public class MenuItem
    {
        public string Text { get; private set; }
        public Action MenuItemAction { get; private set; }
        public Point Position { get; set; }

        public MenuItem(string text, Action action)
        {
            Text = text;
            MenuItemAction = action;
            Position = new Point();
        }

        public override string ToString() => Text;
    }
}
