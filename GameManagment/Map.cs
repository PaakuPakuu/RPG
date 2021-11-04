using System.Collections.Generic;

namespace RPG
{
    public class Map
    {
        

        private readonly List<ITriggerable> _triggerables;
        private readonly List<IDrawable> _drawables;

        public string Name { get; private set; }
        public int Width { get; }
        public int Height { get; }

        public Map(string name)
        {
            _triggerables = new List<ITriggerable>();
            _drawables = new List<IDrawable>();

            Name = name;
        }
    }
}
