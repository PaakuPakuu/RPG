using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public interface IMortal
    {
        int Health { get; set; }

        void TakeDamages(int amount);
        void Die();
    }
}
