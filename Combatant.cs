﻿using System;

namespace RPG
{
    public abstract class Combatant : Character
    {
        public CombatantStats Stats { get; private set; }

        public Combatant(string name) : base(name)
        {
            Stats = new CombatantStats();
        }

        public bool Attack(Combatant target)
        {
            throw new NotImplementedException();
        }
    }
}