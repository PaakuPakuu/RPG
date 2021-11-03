using System.Collections.Generic;

namespace RPG
{
    public class Map
    {
        private List<ITriggerable> _triggerables;
        private List<IDrawable> _drawables;

        public Map()
        {
            _triggerables = new List<ITriggerable>();
            _drawables = new List<IDrawable>();
        }
    }
}
