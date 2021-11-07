using System;

namespace RPG
{
    public abstract class Scene
    {
        public int ScreenWidth { get; }
        public int ScreenHeight { get; }

        protected Scene(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;
        }

        protected Scene() : this(DisplayTools.WindowSize.X, DisplayTools.WindowSize.Y) { }

        public abstract void ExecuteScene();
    }
}
