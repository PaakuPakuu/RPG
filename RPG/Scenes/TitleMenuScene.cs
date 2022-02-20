using GeneralUtils;
using System;

namespace RPG
{
    public sealed class TitleMenuScene : DefaultScene
    {
        private readonly ContextualMenu _titleMenu;
        private readonly ContextualMenu _testsMenu;

        public TitleMenuScene()
        {
            _titleMenu = new ContextualMenu(y: 20, centered: true, padding: 1, selectedStyle: ContextualMenu.SelectedStyle.DoubleArrow);
            _titleMenu.AddMenuItem("Jouer", LaunchGameSavesScene);
            _titleMenu.AddMenuItem("Menu des tests", ShowTestsMenu);
            _titleMenu.AddMenuItem("Options", LaunchSettingsScene);
            _titleMenu.AddMenuItem("Crédits", LaunchCreditsScene);
            _titleMenu.AddMenuItem("Quitter", EndGame);

            _testsMenu = new ContextualMenu(centered: true, padding: 2, selectedStyle: ContextualMenu.SelectedStyle.Green);
            _testsMenu.AddMenuItem("Menus", LaunchMenuTestScene);
            _testsMenu.AddMenuItem("Map", LaunchMapTestScene);
            _testsMenu.AddMenuItem("Combat", LaunchFightTestScene);
            _testsMenu.AddMenuItem("Lancé de dés", LaunchDiceAnimationTestScene);
        }

        public override void ExecuteScene()
        {
            Console.SetBufferSize(ScreenWidth, ScreenHeight);

            //            DisplayTools.WriteInWindowAt(
            //@"█ Bordures de map
            //⍈⍇⍐⍗ Map connectors
            //░ Chemins pavés
            //▲ Fiole pleine
            //△ Fiole vide"
            //, 4, 2);
            DisplayTools.WriteInWindowCenter(@"              />
             / <
O[\\\\\\\\\(O):::<=============================================-
             \ <
              \>", y: 4);
            DisplayTools.PrintBox(-1, 11, ScreenWidth - 13, 3, DisplayTools.BorderStyle.SimpleHeavy);
            DisplayTools.WriteInWindowCenter(@"__  __  ___  __  __  ____ __ __ __    ____  _____ __ __ __
||\ || // \\ ||  || ||    || || ||    || )) ||    || || || //
||\\|| ||=|| ||==|| ||==  || || ||    ||=)  ||==  || || ||<< 
|| \|| || || ||  || ||___ \\_// ||__| ||_)) ||___ \\_// || \\", y: 9);
            _titleMenu.Execute();
        }

        #region Actions
        #region Title menu actions

        private void LaunchGameSavesScene() => Game.ActiveScene = new GameSavesScene();

        private void ShowTestsMenu()
        {
            Console.Clear();
            _testsMenu.Execute();
        }

        private void LaunchSettingsScene() => Game.ActiveScene = new SettingsScene();

        private void LaunchCreditsScene() => Game.ActiveScene = new CreditsScene();

        private void EndGame() => Game.EndGame();

        #endregion
        #region Tests menu actions

        private void LaunchMenuTestScene() => Game.ActiveScene = new MenuTestScene();

        private void LaunchMapTestScene() => Game.ActiveScene = new MapTestScene();

        private void LaunchFightTestScene() => Game.ActiveScene = new CombatScene(new Player() { Name = "Paku" }, new Ennemy("Sukai"), new Ennemy("Gros Guc'"), new Ennemy("IovaSan"));

        private void LaunchDiceAnimationTestScene() => Game.ActiveScene = new DiceAnimationTest();

        #endregion
        #endregion
    }
}
