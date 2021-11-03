using System;

namespace RPG
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

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