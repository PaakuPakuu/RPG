namespace RPG
{
    public abstract class Item : IDrawable
    {
        public string Name { get; }
        public string Sprite { get; }

        public Point Position { get; }

        protected Item(string name, string sprite)
        {
            if (name == string.Empty)
            {
                Name = "No Name";
            } else
            {
                Name = name;
            }

            Sprite = sprite;
        }

        public abstract bool Apply(Combatant target);

        public void Draw()
        {
            DisplayTools.WriteInBufferAt(Sprite, Position.X, Position.Y);
        }
    }
}