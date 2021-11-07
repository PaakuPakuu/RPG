namespace RPG
{
    public abstract class Item : IDrawable
    {
        public string Name
        {
            get => Name;
            private set
            {
                Name = value;
                if (value == string.Empty)
                {
                    Name = "No Name";
                }
            }
        }
        public string Sprite
        {
            get => Sprite;
            private set
            {
                Sprite = value;
                if (value == string.Empty)
                {
                    Sprite = "N";
                }
            }
        }

        public Point Position { get; }

        protected Item(string name, string sprite)
        {
            Name = name;
            Sprite = sprite;
        }

        public abstract bool Apply(Combatant target);

        public void Draw()
        {
            DisplayTools.WriteInBufferAt(Sprite, Position.X, Position.Y);
        }
    }
}