using System;

namespace RPG
{
    public class HealingPotion : Item, IConsumable
    {
        public int Count { get; set; }
        public int HealingPoints { get; }

        public HealingPotion(string name, int healingPoints) : base(name)
        {
            HealingPoints = Math.Max(healingPoints, 0);
        }

        public override bool Apply(Combatant target)
        {
            target.Stats.Health += HealingPoints;
            return true;
        }
    }
}
