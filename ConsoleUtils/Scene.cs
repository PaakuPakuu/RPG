﻿namespace RPG
{
    public abstract class Scene
    {
        private readonly ContextualMenu _defaultCM;

        public int ScreenWidth { get; }
        public int ScreenHeight { get; }

        protected Scene(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;
        }

        protected Scene() : this(DisplayTools.WindowWidth, DisplayTools.WindowHeight) { }

        public abstract void ExecuteScene();
    }
}
