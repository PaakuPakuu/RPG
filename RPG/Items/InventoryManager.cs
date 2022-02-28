using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    public class InventoryManager
    {
        private readonly Inventory _iventory;
        private readonly int _width;
        private readonly int _height;

        public InventoryManager(Inventory inventory, int width, int height)
        {
            _iventory = inventory;
            _width = width;
            _height = height;
        }


    }
}
