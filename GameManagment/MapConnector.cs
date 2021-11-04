using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class MapConnector : ITriggerable
    {
        public Point Position { get; }

        public MapConnector(int x, int y)
        {
            Position = new Point(x, y);
        }

        public void OnTrigger()
        {
            
        }
    }
}
