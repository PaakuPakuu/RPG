using System.Collections.Generic;

namespace RPG
{
    public abstract class Adventure
    {
        protected readonly List<Map> _mapList;

        protected Adventure()
        {
            _mapList = new List<Map>();
        }
    }
}
