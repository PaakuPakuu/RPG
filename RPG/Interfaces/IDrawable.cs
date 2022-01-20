namespace RPG
{
    public interface IDrawable : ILocatable2D
    {
        string Sprite { get; }

        void Draw();
    }
}
