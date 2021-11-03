using System;

namespace RPG
{
    public class MenuItem
    {
        public string Description { get; private set; }
        public Action MenuItemAction { get; private set; }

        public MenuItem(string description, Action action)
        {
            Description = description;
            MenuItemAction = action;
        }

        public override string ToString() => Description;
    }
}
