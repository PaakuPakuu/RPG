namespace RPG
{
    public abstract class Item
    {
        public string Name { get; protected set; }

        protected Item(string name)
        {
            if (name == string.Empty)
            {
                Name = "No Name";
            } else
            {
                Name = name;
            }
        }

        public abstract bool Apply(Combatant target);
    }
}