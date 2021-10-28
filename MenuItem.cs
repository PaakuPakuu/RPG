using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class MenuItem
    {
        public int Id { private get; set; }
        public string Description { get; private set; }
        public Action OptionAction { get; private set; }

        public MenuItem(string description, Action action)
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
}
