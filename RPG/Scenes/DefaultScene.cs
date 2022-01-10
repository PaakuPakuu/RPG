namespace RPG
{
    public abstract class DefaultScene : Scene
    {
        private readonly ContextualMenu _defaultCM;

        protected DefaultScene(int width, int height) : base(width, height)
        {
            _defaultCM = new ContextualMenu(x: DisplayTools.WindowWidth - 10, y: DisplayTools.WindowHeight - 2);
            _defaultCM.AddMenuItem(" RETOUR ", () => Game.ActiveScene = new TitleMenuScene());
        }

        protected DefaultScene() : this(DisplayTools.WindowWidth, DisplayTools.WindowHeight) { }

        public override void ExecuteScene()
        {
            DisplayTools.WriteInWindowAt("Cette scène est vide");
            _defaultCM.Execute();
        }
    }
}
