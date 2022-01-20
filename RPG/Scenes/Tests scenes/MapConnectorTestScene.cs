using GeneralUtils;
using System;

namespace RPG
{
    public class MapConnectorTestScene : DefaultScene
    {
        private readonly Player _player;

        private bool _changeScene;
        private readonly ContextualMenu _pauseMenu;

        public MapConnectorTestScene()
        {
            Game.CurrentMap = new Map();

            _player = new Player("player");

            _changeScene = false;
            _pauseMenu = new ContextualMenu(centered: true, padding: 1);
            _pauseMenu.AddMenuItem("Reprendre", LeavePauseMenu);
            _pauseMenu.AddMenuItem("Quitter", LaunchTitleMenuScene);
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
                    _player.Draw();
                    UpdateWindowPosition();
                    hasMoved = false;
                }

                action = _player.WaitForAction();

                switch (action)
                {
                    case Player.PlayerAction.MoveNorth:
                    case Player.PlayerAction.MoveEast:
                    case Player.PlayerAction.MoveSouth:
                    case Player.PlayerAction.MoveWest:
                        hasMoved = _player.Move((Direction)action);
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

        private void UpdateWindowPosition()
        {
            int windowX = _player.Position.X + Game.CurrentMap.PositionInBuffer.X - Console.WindowWidth / 2;
            int windowY = _player.Position.Y + Game.CurrentMap.PositionInBuffer.Y - Console.WindowHeight / 2;

            if (windowX >= 0 && windowX <= Console.BufferWidth - Console.WindowWidth)
            {
                Console.WindowLeft = windowX;
            }

            if (windowY >= 0 && windowY <= Console.BufferHeight - Console.WindowHeight)
            {
                Console.WindowTop = windowY;
            }
        }

        #region Actions

        private void LeavePauseMenu()
        {
            Game.CurrentMap.PrintMap();
            _player.Draw();
        }

        private void LaunchTitleMenuScene()
        {
            _changeScene = true;
            Game.ActiveScene = new TitleMenuScene();
        }

        #endregion
    }
}
