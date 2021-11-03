namespace RPG
{
    public class CombatantStats
    {
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Dodge { get; set; }

        public CombatantStats()
        {
            Health = 0;
            Strength = 0;
            Defense = 0;
            Dodge = 0;
        }
    }
}