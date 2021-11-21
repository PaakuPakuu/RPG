using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public sealed class MapTestScene : Scene
    {
        private Player _player;

        private Adventure _adventure;
        private Map _currentMap;

        private bool _changeMenu;
        private ContextualMenu _pauseMenu;

        public MapTestScene()
        {
            _adventure = new AdventureTest();

            _currentMap = _adventure.CurrentMap;
            _changeMenu = false;

            _player = new Player("player");

            _pauseMenu = new ContextualMenu(centered: true, padding: 1);
            _pauseMenu.AddMenuItem("Reprendre", LeavePauseMenu);
            _pauseMenu.AddMenuItem("Quitter", LaunchTitleMenuScene);
        }

        public override void ExecuteScene()
        {
            Player.PlayerAction action;
            bool hasMoved = true;

            _currentMap.PrintMap();

            while (!_changeMenu)
            {
                if (hasMoved)
                {
                    _player.Draw();
                    hasMoved = false;
                    UpdateWindowPosition();
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
                        _currentMap.TryToTrigger(_player.Position);
                        break;
                    case Player.PlayerAction.Pause:
                        _pauseMenu.Execute();
                        break;
                }
            }
        }

        private void UpdateWindowPosition()
        {
            int windowX = _player.Position.X + _currentMap.PositionInBuffer.X - Console.WindowWidth / 2;
            int windowY = _player.Position.Y + _currentMap.PositionInBuffer.Y - Console.WindowHeight / 2;

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
            _currentMap.PrintMap();
            _player.Draw();
        }

        private void LaunchTitleMenuScene()
        {
            _changeMenu = true;
            Game.ActiveScene = new TitleMenuScene();
        }

        #endregion
    }
}
