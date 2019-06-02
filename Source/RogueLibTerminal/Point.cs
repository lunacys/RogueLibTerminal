using System;

namespace RogueLibTerminal
{
    public struct Point : IEquatable<Point>
    {
        public static Point Zero => new Point(0);
        public static Point Unit => new Point(1);

        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(int value)
        {
            X = value;
            Y = value;
        }

        public static Point operator +(Point value1, Point value2)
        {
            return new Point(value1.X + value2.X, value1.Y + value2.Y);
        }

        public static Point operator -(Point value1, Point value2)
        {
            return new Point(value1.X - value2.X, value1.Y - value2.Y);
        }

        public static Point operator *(Point value1, Point value2)
        {
            return new Point(value1.X * value2.X, value1.Y * value2.Y);
        }

        public static Point operator /(Point source, Point divisor)
        {
            return new Point(source.X / divisor.X, source.Y / divisor.Y);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Point a, Point b)
        {
            return !a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            return (obj is Point) && Equals((Point)obj);
        }

        public bool Equals(Point other)
        {
            return ((X == other.X) && (Y == other.Y));
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }

        }

        public override string ToString()
        {
            return "{" + X + ", " + Y + "}";
        }
    }
}