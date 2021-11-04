using System;

namespace RPG
{
    public sealed class TitleMenuScene : Scene
    {
        private readonly ContextualMenu _titleMenu;

        public TitleMenuScene()
        {
            _titleMenu = new ContextualMenu(padding: 1);
            _titleMenu.AddMenuItem("Jouer", LaunchGameScene);
            _titleMenu.AddMenuItem("Options", LaunchSettingsScene);
            _titleMenu.AddMenuItem("Crédits", LaunchCreditsScene);
            _titleMenu.AddMenuItem("Quitter", EndGame);
        }

        public override void ExecuteScene()
        {
            DisplayTools.WriteInWindowAt(
@"█ Bordures de map
⍈⍇⍐⍗ Map connectors
░ Chemins pavés
▲ Fiole pleine
△ Fiole vide"
            , 1, 1);
            _titleMenu.Execute();
            Console.ReadKey();
        }

        #region MenuItem actions

        private void LaunchGameScene() => Game.ActiveScene = new GameScene();

        private void LaunchSettingsScene() => Game.ActiveScene = new SettingsScene();

        private void LaunchCreditsScene() => Game.ActiveScene = new CreditsScene();

        private void EndGame() => Game.GameInstance.EndGame();

        #endregion
    }
}
