using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public abstract class Character
    {
        public string Name { get; private set; }

        public Character(string name)
        {
            Name = name;
        }
    }
}
