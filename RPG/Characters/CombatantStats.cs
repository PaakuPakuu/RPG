namespace RPG
{
    public class CombatantStats
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Dodge { get; set; }

        public CombatantStats()
        {
            Health = 70;
            MaxHealth = 100;
            Strength = 0;
            Defense = 0;
            Dodge = 0;
        }
    }
}