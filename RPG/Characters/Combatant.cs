namespace RPG
{
    public abstract class Combatant : Character
    {
        public HeroStats Stats { get; set; }

        protected Combatant()
        {
            Stats = new HeroStats();
        }

        public void Attack(Combatant target)
        {
            target.TakeDamage(1);
        }
        
        // Renvoie le montant réel des dégâts reçus
        public int TakeDamage(int amount)
        {
            Stats.Health -= amount;
            return amount;
        }
    }
}