using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public sealed class Game
    {
        private static Game _instance = null;
        private static readonly object padlock = new object();

        private bool _isRunning;

        public static Scene ActiveScene { private get; set; }

        public static Game GameInstance
        {
            get
            {
                lock (padlock)
                {
                    if (_instance == null)
                    {
                        _instance = new Game();
                    }

                    return _instance;
                }
            }

            // SOURCE : https://jlambert.developpez.com/tutoriels/dotnet/implementation-pattern-singleton-csharp
            // singeton thread-friendly
        }

        private Game()
        {
            ActiveScene = new TitleMenuScene();
            _isRunning = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void LaunchGame()
        {
            DisplayTools.InitializeWindow();
            _isRunning = true;

            while (_isRunning)
            {
                ActiveScene.ExecuteScene();
            }
        }
    }
}
