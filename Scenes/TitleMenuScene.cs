using System;

namespace RPG
{
    public sealed class TitleMenuScene : Scene
    {
        private readonly ContextualMenu _titleMenu;

        public TitleMenuScene()
        {
            _titleMenu = new ContextualMenu(horizontal: false, centered: true, padding: 2, selectedStyle: ContextualMenu.SelectedStyle.DoubleArrow);
            _titleMenu.AddMenuItem($"Tester les menus", LaunchMenuTestScene);
            _titleMenu.AddMenuItem($"Jouer", LaunchGameScene);
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
            , 4, 2);
            _titleMenu.Execute();
        }

        #region MenuItem actions

        private void LaunchMenuTestScene() => Game.ActiveScene = new MenuTests();

        private void LaunchGameScene() => Game.ActiveScene = new GameScene();

        private void LaunchSettingsScene() => Game.ActiveScene = new SettingsScene();

        private void LaunchCreditsScene() => Game.ActiveScene = new CreditsScene();

        private void EndGame() => Game.GameInstance.EndGame();

        #endregion
    }
}
