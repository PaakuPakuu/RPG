using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public sealed class MapTestScene : Scene
    {
        private Map _map;
        private bool _changeMenu;

        private Player _player;

        private ContextualMenu _pauseMenu;

        public MapTestScene()
        {
            _map = new Map("MainTown");
            _changeMenu = false;

            _player = new Player(_map, "player");

            _pauseMenu = new ContextualMenu();
            _pauseMenu.AddMenuItem("Reprendre", LeavePauseMenu);
            _pauseMenu.AddMenuItem("Quitter", LaunchTitleMenuScene);
        }

        public override void ExecuteScene()
        {
            Player.PlayerAction action;
            bool hasMoved = true;

            _map.PrintMap();

            while (!_changeMenu)
            {
                if (hasMoved)
                {
                    _player.Draw();
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

        #region Actions

        private void LeavePauseMenu()
        {
            _map.PrintMap();
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
