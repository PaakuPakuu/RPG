using System;

namespace RPG
{
    public interface IDrawable : ILocatable2D
    {
        Map Origin { get; }
        string Sprite { get; }

        void Draw();
    }
}
