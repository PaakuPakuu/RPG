namespace RPG
{
    public abstract class Character
    {
        public string Name { get; private set; }

        protected Character(string name)
        {
            Name = name;
        }
    }
}
