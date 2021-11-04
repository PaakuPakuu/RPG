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
        public static Adventure Adventure { get; }

        public static Game GameInstance
        {
            get { return _lazy.Value; }
        }

        private Game()
        {
            ActiveScene = new TitleMenuScene();
            _isRunning = false;
        }

        public void LaunchGame()
        {
            DisplayTools.InitializeWindow();
            _isRunning = true;

            while (_isRunning)
            {
                ActiveScene.ExecuteScene();
                _isRunning = false;
            }
        }

        public void EndGame()
        {
            _isRunning = false;
        }
    }
}
