using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class Player : Character, IMortal

    {
        public int Health { get; set; }

        public Player(string name) : base(name)
        {

        }

        public void Die()
        {
            throw new NotImplementedException();
        }

        public void TakeDamages(int amount)
        {
            throw new NotImplementedException();
        }
    }
}
