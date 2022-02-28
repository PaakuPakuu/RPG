using GeneralUtils;
using System;
using System.Collections.Generic;
using System.IO;

namespace RPG
{
    public class CombatScene : Scene
    {
        private const int LIFEBAR_WIDTH = 12;
        private const char FULL_LIFE = '▮';
        private const char EMPTY_LIFE = '▯';

        private readonly Fight _fight;

        private readonly ContextualMenu _actionMenu;
        private readonly ContextualMenu _attacksMenu;

        private readonly string[] _fightTemplate;

        #region Positions

        private readonly List<Point> _ennemiesLifeBarPos;
        private readonly Point _playerLifeBarPos;

        private readonly List<Point> _ennemiesNamePos;
        private readonly Point _playerNamePos;

        #endregion

        public CombatScene(Player player, params Ennemy[] ennemies)
        {
            _fight = new Fight(player, new List<Ennemy>(ennemies));

            _actionMenu = new ContextualMenu(y: 3, centered: true);
            _actionMenu.AddMenuItem("Attaquer", ShowAttacksMenu);
            _actionMenu.AddMenuItem("Fuire", LeaveFight);

            _attacksMenu = new ContextualMenu();
            _attacksMenu.AddMenuItem("Coup de poing", () => { });

            _fightTemplate = File.ReadAllLines($"{ResourcesUtils.UI_PATH}/fight_template.txt");

            // Initialize positions

            _ennemiesLifeBarPos = new List<Point>(3); // Make 3 a constant (max ennemies to fight alone)
            for (int i = 0; i < _fight.Ennemies.Count; i++)
            {
                _ennemiesLifeBarPos.Add(new Point(64, 28 + i * 2));
            }
            _playerLifeBarPos = new Point(24, 28);

            _ennemiesNamePos = new List<Point>(3);
            for (int i = 0; i < _fight.Ennemies.Count; i++)
            {
                _ennemiesNamePos.Add(new Point(43, 28 + i * 2));
            }
            _playerNamePos = new Point(4, 28);
        }

        public override void ExecuteScene()
        {
            _fight.StartFight();

            while (_fight.Fighting)
            {
                UpdateAllScreen();

                _actionMenu.Execute();

                if (!_fight.IsOver())
                {
                    _fight.NextFighter();
                } else
                {
                    _fight.EndFight();
                }
            }

            Game.ActiveScene = new TitleMenuScene();
        }

        private void UpdateAllScreen()
        {
            Console.Clear();
            DisplayTools.WriteInWindowAt(_fightTemplate, 1, 1);
            PrintAllNames();
            PrintAllLifeBars();
        }

        private void PrintLifeBar(Point position, int health, int maxHealth)
        {
            char life;
            int emptyIndex = (int)Math.Ceiling((LIFEBAR_WIDTH * (double)health) / maxHealth);

            for (int i = 0; i < LIFEBAR_WIDTH; i++)
            {
                life = (i < emptyIndex ? FULL_LIFE : EMPTY_LIFE);
                DisplayTools.WriteInWindowAt(life.ToString(), position.X + i, position.Y);
            }
        }

        private void PrintAllLifeBars()
        {
            // Print player lifebar
            PrintLifeBar(_playerLifeBarPos, _fight.Player.Health, _fight.Player.MaxHealth);

            // Print ennemies lifebar
            for (int i = 0; i < _ennemiesLifeBarPos.Count; i++)
            {
                PrintLifeBar(_ennemiesLifeBarPos[i], _fight.Ennemies[i].Health, _fight.Ennemies[i].MaxHealth);
            }
        }

        private void PrintAllNames()
        {
            DisplayTools.WriteInWindowAt(_fight.Player.Name, _playerNamePos.X, _playerNamePos.Y);

            for (int i = 0; i < _ennemiesNamePos.Count; i++)
            {
                DisplayTools.WriteInWindowAt(_fight.Ennemies[i].Name, _ennemiesNamePos[i].X, _ennemiesNamePos[i].Y);
            }
        }

        #region Actions

        private void ShowAttacksMenu()
        {
            _attacksMenu.Execute();
        }

        private void LeaveFight()
        {

        }

        #endregion
    }
}
