using DbService;
using GeneralUtils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RPG
{
    public class GameScene : DefaultScene
    {
        private readonly RpgContext _rpgContext;

        private bool _changeScene;
        private readonly ContextualMenu _pauseMenu;
        private readonly List<List<Ennemy>> _ennemyHords;

        public GameScene()
        {
            _rpgContext = new RpgContext();

            _changeScene = false;
            _pauseMenu = new ContextualMenu(centered: true, padding: 1);
            _pauseMenu.AddMenuItem("Reprendre", LeavePauseMenu);
            _pauseMenu.AddMenuItem("Quitter", SaveAndLaunchTitleMenuScene);

            _ennemyHords = new List<List<Ennemy>>()
            {
                Manager.GetEnnemies(),

            };
        }

        private void UpdateWindowPosition()
        {
            int windowX = Game.Player.Position.X + Game.CurrentMap.PositionInBuffer.X - Console.WindowWidth / 2;
            int windowY = Game.Player.Position.Y + Game.CurrentMap.PositionInBuffer.Y - Console.WindowHeight / 2;

            if (windowX >= 0 && windowX <= Console.BufferWidth - Console.WindowWidth)
            {
                Console.WindowLeft = windowX;
            }

            if (windowY >= 0 && windowY <= Console.BufferHeight - Console.WindowHeight)
            {
                Console.WindowTop = windowY;
            }
        }

        public override void ExecuteScene()
        {
            Player.PlayerAction action;
            bool hasMoved = true;

            UpdateWindowPosition();
            Game.CurrentMap.PrintMap();

            while (!_changeScene)
            {
                if (hasMoved)
                {
                    Game.Player.Draw();
                    UpdateWindowPosition();
                    hasMoved = false;
                }

                action = Game.Player.WaitForAction();

                switch (action)
                {
                    case Player.PlayerAction.MoveNorth:
                    case Player.PlayerAction.MoveEast:
                    case Player.PlayerAction.MoveSouth:
                    case Player.PlayerAction.MoveWest:
                        hasMoved = Game.Player.Move((Direction)action);
                        break;
                    case Player.PlayerAction.OpenInventory:
                        break;
                    case Player.PlayerAction.TriggerAction:
                        break;
                    case Player.PlayerAction.Pause:
                        _pauseMenu.Execute();
                        break;
                }
            }
        }

        private void QuickSaveGame()
        {
            Joueur player = _rpgContext.Joueur.Single(j => j.IdJoueur == Game.Player.Id);

            player.PositionX = Game.Player.Position.X;
            player.PositionY = Game.Player.Position.Y;
            player.PointsVie = Game.Player.Health;
            player.Niveau = Game.Player.Level;
            player.Experience = Game.Player.Experience;
            player.Or = Game.Player.Gold;
            player.Argent = Game.Player.Silver;
            player.Destin = Game.Player.Destin;
            player.IdMapCourante = Game.CurrentMap.Id;

            _rpgContext.SaveChanges();
        }

        #region Actions

        private void LeavePauseMenu()
        {
            Game.CurrentMap.PrintMap();
            Game.Player.Draw();
        }

        private void SaveAndLaunchTitleMenuScene()
        {
            QuickSaveGame();
            _changeScene = true;
            Game.ActiveScene = new TitleMenuScene();
        }

        #endregion
    }
}
