using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RPG
{
    public class Map
    {
        private const string MAPS_PATH = "Resources/Maps";
        private const string DEFAULT_MAP_PATH = MAPS_PATH + "/DefaultMap.map";

        private readonly List<ITriggerable> _triggerables;
        private readonly List<IDrawable> _drawables;

        private string[] _map;
        private int _width;
        private int _height;

        public int Width { get => _width; }
        public int Height { get => _height; }

        public Map(string fileName)
        {
            _triggerables = new List<ITriggerable>();
            _drawables = new List<IDrawable>();

            try
            {
                _map = File.ReadAllLines($"{MAPS_PATH}/{fileName}.map");
            }
            catch
            {
                _map = File.ReadAllLines(DEFAULT_MAP_PATH);
            }

            _width = _map.Max(s => s.Length);
            _height = _map.Length;
        }

        public void PrintMap()
        {
            Console.SetBufferSize(_width + DisplayTools.RightPadding + 1, _height + DisplayTools.BottomPadding + 1);
            DisplayTools.WriteInBufferAt(_map, 1, 1);
        }
    }
}
