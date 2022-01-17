using RPG;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPGEditor
{
    public class MenusScene : Scene
    {
        private readonly Point BOX_POS = new Point(2, 1);
        private readonly Point BOX_SIZE;
        private readonly int MENU_X = 6;

        private ContextualMenu _firstMenu;
        private ContextualMenu _player;

        public MenusScene() : base(DisplayTools.EditorWindowWidth, DisplayTools.EditorWindowHeight)
        {
            BOX_SIZE = new Point(ScreenWidth - 4, ScreenHeight - 2);

            _firstMenu = new ContextualMenu(x: MENU_X, padding: 1, selectedStyle: ContextualMenu.SelectedStyle.Arrow);
            _firstMenu.AddMenuItem("Attributs Joueur", PlayersMenu);
            _firstMenu.AddMenuItem("Attributs Monstres", MonstersMenu);
            _firstMenu.AddMenuItem("Générer un fichier .map", GenerateTemplate);
            _firstMenu.AddMenuItem("Quitter", CloseEditor);

            _player = new ContextualMenu(x: MENU_X, padding: 1, selectedStyle: ContextualMenu.SelectedStyle.Arrow);
            _player.AddMenuItem("Origines", LaunchOriginsScene);
            _player.AddMenuItem("Retour", BackToFirstMenu);
        }

        public override void ExecuteScene()
        {
            DisplayTools.PrintBox(BOX_POS.X, BOX_POS.Y, BOX_SIZE.X, BOX_SIZE.Y, DisplayTools.BorderStyle.SimpleHeavy);
            _firstMenu.Execute();
        }

        private void ClearScreen() => DisplayTools.ClearBox(MENU_X, BOX_POS.Y + 1, BOX_SIZE.X - 5, BOX_SIZE.Y - 2);

        #region Actions

        private void GenerateTemplate()
        {
            Console.Clear();
            Console.WriteLine("");
            string filename = Console.ReadLine();

            int.TryParse(Console.ReadLine(), out int width);
            int.TryParse(Console.ReadLine(), out int height);

            try
            {
                EditorFeatures.GenerateEmptyFileToCustom(width, height, filename);
                DisplayTools.WriteInWindowCenter(@$"                                           
   Fichier {filename} généré avec succès   
                                           ");
            }
            catch
            {
                DisplayTools.WriteInWindowCenter(@$"                          
          Une erreur est survenue   
                                    ");
            }

            Console.ReadKey();
            Console.Clear();
        }

        private void PlayersMenu()
        {
            ClearScreen();
            _player.Execute();
        }

        private void MonstersMenu() { }

        private void LaunchOriginsScene() => Editor.ActiveScene = new OriginsScene();

        private void CloseEditor() => Editor.EditorInstance.CloseEditor();

        private void BackToFirstMenu()
        {
            ClearScreen();
            _firstMenu.Execute();
        }

        #endregion
    }
}
