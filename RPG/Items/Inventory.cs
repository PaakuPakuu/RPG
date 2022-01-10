using System.Collections.Generic;

namespace RPG
{
    public class Inventory
    {
        public List<Item> Items { get; }

        public int Id { get; set; }

        public Inventory()
        {
            Items = new List<Item>();
        }

        public Inventory(int columns, int rows)
        {
            Items = new List<Item>(rows * columns);
        }
    }
}
