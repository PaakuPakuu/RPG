using System;

namespace RPG
{
    public static class Game
    {
        private static bool _isRunning;

        public static Player Player { get; set; }
        public static Scene ActiveScene { get; set; }
        public static Map CurrentMap { get; set; }

        private static void InitializeGame()
        {
            CurrentMap = new Map("donjon_1"); // Temporaire : select bdd
            Player = new Player("player"); // Temporaire : select bdd
            ActiveScene = new TitleMenuScene();

            DisplayTools.InitializeGameWindow();
            _isRunning = true;
        }

        public static void LaunchGame()
        {
            InitializeGame();

            while (_isRunning)
            {
                ActiveScene.ExecuteScene();
                Console.Clear();
            }
        }

        public static void EndGame() // I love you over 3000
        {
            _isRunning = false;
        }
    }
}
