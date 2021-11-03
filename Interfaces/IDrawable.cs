namespace RPG
{
    public interface IDrawable : ILocatable2D
    {
        public string Sprite { get; set; }

        public void Draw();
    }
}
