namespace RPG
{
    public abstract class Item
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

        protected Item(string name, string sprite)
        {
            Name = name;
            Sprite = sprite;
        }

        public abstract bool Apply(Combatant target);
    }
}