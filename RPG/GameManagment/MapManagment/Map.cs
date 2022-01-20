using GeneralUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RPG
{
    public class Map
    {
        private const string DEFAULT_MAP_PATH = ResourcesUtils.MAPS_PATH + "/DefaultMap.map";

        private const char WALL = '█';

        private readonly ITriggerable[,] _triggerables;
        private readonly List<IDrawable> _drawables;

        private readonly int _width;
        private readonly int _height;

        public int Width { get => _width; }
        public int Height { get => _height; }
        public Point PositionInBuffer { get; }
        public string[] MapDisplay { get; }
        public Point SpawnPosition { get; }

        public Map(string fileName = "")
        {
            // Get ascii display from .map file
            try
            {
                MapDisplay = ResourcesUtils.GetTemplate($"{ResourcesUtils.MAPS_PATH}/{fileName}.map");
            }
            catch
            {
                MapDisplay = File.ReadAllLines(DEFAULT_MAP_PATH);
            }

            _width = MapDisplay.Max(s => s.Length);
            _height = MapDisplay.Length;
            PositionInBuffer = new Point(DisplayTools.WidthMargin, DisplayTools.HeightMargin);
            SpawnPosition = new Point(1, 1);

            _triggerables = new ITriggerable[_height, _width];
            _drawables = new List<IDrawable>();
        }

        public void PrintMap()
        {
            int extendedMapWidth = _width + PositionInBuffer.X * 2;
            int extendedMapHeight = _height + PositionInBuffer.Y * 2;

            int bufferWidth = (extendedMapWidth <= DisplayTools.WindowWidth ? DisplayTools.WindowWidth : extendedMapWidth);
            int bufferHeight = (extendedMapHeight <= DisplayTools.WindowHeight? DisplayTools.WindowHeight : extendedMapHeight);

            Console.SetBufferSize(bufferWidth, bufferHeight);
            DisplayTools.WriteInBufferAt(MapDisplay, PositionInBuffer.X, PositionInBuffer.Y);
        }

        public void ConnectToMap(Map target, Point connectorPos, Direction connectorDir)
        {
            MapConnector mc = new MapConnector(this, target, connectorPos, connectorDir);

            _triggerables[connectorPos.Y, connectorPos.X] = mc;
            _drawables.Add(mc);
        }

        public bool TryToTrigger(Point position)
        {
            ITriggerable t = _triggerables[position.Y, position.X];

            if (t == null)
            {
                return false;
            }

            t.OnTrigger();
            return true;
        }

        public void DrawDrawables()
        {
            foreach (IDrawable d in _drawables)
            {
                d.Draw();
            }
        }

        public bool IsWalkable(Point position)
        {
            return MapDisplay[position.Y][position.X] != WALL;
        }
    }
}
