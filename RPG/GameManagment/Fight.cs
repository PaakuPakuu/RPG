using System.Collections.Generic;

namespace RPG
{
    public class Fight
    {
        private int _currentFighterId;

        public int Turns { get; private set; }
        public Player Player { get; private set; }
        public List<Ennemy> Ennemies { get; private set; }

        public Combatant CurrentFighter
        {
            get
            {
                if (_currentFighterId == 0)
                {
                    return Player;
                }

                return Ennemies[_currentFighterId - 1];
            }
        }

        public bool Fighting { get; private set; }

        public Fight(Player player, List<Ennemy> ennemies)
        {
            Player = player;
            Ennemies = ennemies;
            Fighting = false;
        }

        public void StartFight()
        {
            _currentFighterId = 0;
            Fighting = true;
        }

        // 0 => player, sinon ennemy
        public bool NextFighter()
        {
            _currentFighterId++;
            _currentFighterId %= Ennemies.Count + 1;

            return _currentFighterId == 0;
        }

        public bool IsOver()
        {
            if (Player.Stats.Health <= 0)
            {
                return true;
            }

            foreach (Combatant c in Ennemies)
            {
                if (c.Stats.Health == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void EndFight()
        {
            Fighting = false;
        }
    }
}
