using GeneralUtils;
using System;

namespace RPG
{
    public class MenuTestScene : DefaultScene
    {
        private ContextualMenu _cm;
        private bool _goToTitleMenu;

        public MenuTestScene()
        {
            VerticalUnderligned();
            _goToTitleMenu = false;
        }

        public override void ExecuteScene()
        {
            while (!_goToTitleMenu)
            {
                _cm.AddMenuItem("Exemple 1", VerticalUnderligned);
                _cm.AddMenuItem("Exeeeemple 2", HorizontalReversed);
                _cm.AddMenuItem("Ex. 3", VerticalLeftColored);
                _cm.AddMenuItem("4___Exemple___4", VerticalRightDashed);
                _cm.AddMenuItem("Retour", Quit);
                _cm.Execute();
                Console.Clear();
            }
        }

        #region Menu actions
        private void VerticalUnderligned() => _cm = new ContextualMenu(selectedStyle: ContextualMenu.SelectedStyle.Underlined, centered: true);

        private void HorizontalReversed() => _cm = new ContextualMenu(horizontal: true, selectedStyle: ContextualMenu.SelectedStyle.Reversed, padding: 3);

        private void VerticalLeftColored() => _cm = new ContextualMenu(x: 5, y: 15, selectedStyle: ContextualMenu.SelectedStyle.Yellow, padding: 2);

        private void VerticalRightDashed() => _cm = new ContextualMenu(x: 50, y: 3, selectedStyle: ContextualMenu.SelectedStyle.Dashes, padding: 1);

        private void Quit()
        {
            Game.ActiveScene = new TitleMenuScene();
            _goToTitleMenu = true;
        }

        #endregion
    }
}
