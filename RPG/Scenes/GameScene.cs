using GeneralUtils;
using System;

namespace RPG
{
    public class GameScene : DefaultScene
    {
        private bool _changeScene;
        private readonly ContextualMenu _pauseMenu;

        public GameScene()
        {
            _changeScene = false;
            _pauseMenu = new ContextualMenu(centered: true, padding: 1);
            _pauseMenu.AddMenuItem("Reprendre", LeavePauseMenu);
            _pauseMenu.AddMenuItem("Quitter", LaunchTitleMenuScene);
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

        #region Actions

        private void LeavePauseMenu()
        {
            Game.CurrentMap.PrintMap();
            Game.Player.Draw();
        }

        private void LaunchTitleMenuScene()
        {
            _changeScene = true;
            Game.ActiveScene = new TitleMenuScene();
        }

        #endregion
    }
}
