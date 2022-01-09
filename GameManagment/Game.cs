using System;

namespace RPG
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Singleton implementation source : https://fr.wikipedia.org/wiki/Singleton_(patron_de_conception)#C#
    /// </remarks>
    public sealed class Game
    {
        private static readonly Lazy<Game> _lazy = new Lazy<Game>(() => new Game());

        private bool _isRunning;

        public static Scene ActiveScene { private get; set; }
        public static Map CurrentMap { get; set; } // TEMPORAIRE

        public static Game GameInstance
        {
            get { return _lazy.Value; }
        }

        private Game()
        {
            _isRunning = false;
        }

        public void LaunchGame()
        {
            DisplayTools.InitializeWindow();

            _isRunning = true;
            ActiveScene = new TitleMenuScene();
            CurrentMap = new Map("MainTown"); // Temporaire : récupérer la map en sauvegarde bdd

            while (_isRunning)
            {
                ActiveScene.ExecuteScene();
                Console.Clear();
            }
        }

        public void EndGame() // I love you over 3000
        {
            _isRunning = false;
        }
    }
}
