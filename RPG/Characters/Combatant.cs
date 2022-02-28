namespace RPG
{
    public abstract class Combatant : Character
    {
        #region Stats

        public int Attack { get; set; }
        public int Parade { get; set; }
        public int Impact { get; set; }
        public int Courage { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        #endregion
        
        // Renvoie le montant réel des dégâts reçus
        public int TakeDamage(int amount)
        {
            Health -= amount;

            // appliquer la défense

            return amount;
        }
    }
}