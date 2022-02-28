using System.Collections.Generic;

namespace RPG
{
    public class Fight
    {
        private int _currentFighterId;

        private List<Combatant> _fighters;

        public int Turns { get; private set; }
        public Player Player { get; private set; }
        public List<Ennemy> Ennemies { get; private set; }

        public Combatant CurrentFighter { get => Ennemies[_currentFighterId]; }

        public bool Fighting { get; private set; }

        public Fight(Player player, List<Ennemy> ennemies)
        {
            _fighters = new List<Combatant>(ennemies)
            {
                player
            };

            Player = player;
            Ennemies = ennemies;
            Fighting = false;
        }

        public void StartFight()
        {
            _currentFighterId = 0;
            Fighting = true;
        }


        public void NextFighter()
        {
            _currentFighterId++;
            _currentFighterId %= _fighters.Count;
        }

        public bool IsOver()
        {
            foreach (Combatant c in Ennemies)
            {
                if (c.Health == 0)
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
