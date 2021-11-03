using System;

namespace RPG
{
    public sealed class TitleMenuScene : Scene
    {
        private ContextualMenu _titleMenu;

        public TitleMenuScene()
        {
            _titleMenu = new ContextualMenu(padding: 1);
            _titleMenu.AddMenuItem("Jouer", LaunchGameScene);
            _titleMenu.AddMenuItem("Options", LaunchSettingsScene);
            _titleMenu.AddMenuItem("Crédits", LaunchCreditsScene);
            _titleMenu.AddMenuItem("Quitter", () => { Environment.Exit(0); }); // TODO : à changer lorsqu'on manipulera les bases de données
        }

        public override void ExecuteScene()
        {
            //DisplayTools.WriteInWindowAt("▁▂▃▄▅▆▇█▉", 10, 2);
            _titleMenu.Execute();
            Console.ReadKey();
        }

        #region MenuItem actions

        private void LaunchGameScene() => Game.ActiveScene = new GameScene();

        private void LaunchSettingsScene() => Game.ActiveScene = new SettingsScene();

        private void LaunchCreditsScene() => Game.ActiveScene = new CreditsScene();

        #endregion
    }
}
