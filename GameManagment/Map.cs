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

        private int _width;
        private int _height;

        public int Width { get => _width; }
        public int Height { get => _height; }
        public Point PositionInBuffer { get; }
        public string[] MapDisplay { get; }
        public Point SpawnPosition { get; }

        public Map(string fileName)
        {
            _triggerables = new List<ITriggerable>();
            _drawables = new List<IDrawable>();

            try
            {
                MapDisplay = File.ReadAllLines($"{MAPS_PATH}/{fileName}.map");
            }
            catch
            {
                MapDisplay = File.ReadAllLines(DEFAULT_MAP_PATH);
            }

            _width = MapDisplay.Max(s => s.Length);
            _height = MapDisplay.Length;
            PositionInBuffer = new Point(DisplayTools.WidthMargin, DisplayTools.HeightMargin);
            SpawnPosition = new Point(1, 1);
        }

        public void PrintMap()
        {
            int extendedMapWidth = _width + PositionInBuffer.X * 2;
            int extendedMapHeight = _height + PositionInBuffer.Y * 2;

            int bufferWidth = (extendedMapWidth <= DisplayTools.WindowWidth ? DisplayTools.WindowWidth : extendedMapWidth);
            int bufferHeight = (extendedMapHeight<= DisplayTools.WindowHeight? DisplayTools.WindowHeight : extendedMapHeight);

            Console.SetBufferSize(bufferWidth, bufferHeight);
            DisplayTools.WriteInBufferAt(MapDisplay, PositionInBuffer.X, PositionInBuffer.Y);
        }
    }
}
