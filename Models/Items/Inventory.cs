using System.Collections.Generic;

namespace RPG
{
    public class Inventory
    {
        private List<Item> _items;

        public Inventory(int columns, int rows)
        {
            _items = new List<Item>(rows * columns);
        }
    }
}
