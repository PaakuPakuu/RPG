using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public sealed class AdventureTest : Adventure
    {
        private Map _map1;
        private Map _map2;

        public AdventureTest()
        {
            _map1 = new Map();
            _map2 = new Map();

            _map1.ConnectToMap(_map2, new Point(8, 1), Direction.Top);
            _map2.ConnectToMap(_map1, new Point(_map2.Width - 2, _map2.Height - 2), Direction.Bottom);

            CurrentMap = _map1;
        }
    }
}
