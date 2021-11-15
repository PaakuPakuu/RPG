using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public sealed class MapTestScene : Scene
    {
        private Map _map;
        private bool _changeMenu;

        public MapTestScene()
        {
            _map = new Map("MainTown");

        }

        public override void ExecuteScene()
        {
            /*while (!_changeMenu)
            {

            }*/

            _map.PrintMap();
            Console.ReadKey();
            Game.ActiveScene = new TitleMenuScene();
        }
    }
}
