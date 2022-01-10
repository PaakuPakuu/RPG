using System;

namespace RPG
{
    public class Point
    {
        public Point(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static Point operator +(Point p1, Point p2) => new Point(p1.X + p2.X, p1.Y + p2.Y);
        public static Point operator -(Point p1, Point p2) => new Point(p1.X - p2.X, p1.Y - p2.Y);

        public bool Equals(Point other)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"({X} ; {Y})";
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