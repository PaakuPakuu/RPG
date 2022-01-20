namespace RPG
{
    public abstract class Combatant : Character
    {
        public CombatantStats Stats { get; private set; }

        protected Combatant(string name) : base(name)
        {
            Stats = new CombatantStats();
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