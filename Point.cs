using System;

namespace RPG
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public bool Equals(Point other)
        {
            throw new NotImplementedException();
        }

        public float GetDistance(Point other)
        {
            throw new NotImplementedException();

        }

        public float GetDistance(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}