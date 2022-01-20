using GeneralUtils;

namespace RPG
{
    public class DiceAnimationTest : DefaultScene
    {
        private readonly Dice _dice6;

        private readonly ContextualMenu _menu;

        private bool _leave;

        public DiceAnimationTest()
        {
            _dice6 = new Dice();

            _menu = new ContextualMenu(y: ScreenHeight - 3);
            _menu.AddMenuItem("Lancer", RollDice);
            _menu.AddMenuItem("Quitter", Leave);

            _leave = false;
        }

        public override void ExecuteScene()
        {
            while (!_leave)
            {
                _menu.Execute();
            }

            Game.ActiveScene = new TitleMenuScene();
        }

        #region Actions

        private void RollDice()
        {
            _dice6.RollWithAnimation(9, 5);
        }

        private void Leave()
        {
            _leave = true;
        }

        #endregion
    }
}
