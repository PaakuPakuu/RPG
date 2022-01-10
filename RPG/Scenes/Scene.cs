namespace RPG
{
    public abstract class Scene
    {
        private readonly ContextualMenu _defaultCM;

        public int ScreenWidth { get; }
        public int ScreenHeight { get; }

        protected Scene(int width, int height)
        {
            _defaultCM = new ContextualMenu(x: DisplayTools.WindowWidth - 10, y: DisplayTools.WindowHeight - 2);
            _defaultCM.AddMenuItem(" RETOUR ", () => Game.ActiveScene = new TitleMenuScene());

            ScreenWidth = width;
            ScreenHeight = height;
        }

        protected Scene() : this(DisplayTools.WindowWidth, DisplayTools.WindowHeight) { }

        public virtual void ExecuteScene()
        {
            DisplayTools.WriteInWindowAt("Cette scène est vide");
            _defaultCM.Execute();
        }
    }
}
