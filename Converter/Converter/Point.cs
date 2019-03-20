using System;

namespace Converter
{
    public class PointInt
    {
        public int X { get; }
        public int Y { get; }

        public PointInt(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public class Point
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public override string ToString()
        {
            return $"({this.X}in,{this.Y}in)";
        }

        public static Point operator +(Point p, Vector v)
        {
            return new Point(p.X + v.X, p.Y + v.Y);
        }

        public static Point Intersection(Point p1, Vector v1, Point p2, Vector v2)
        {
            double a1 = v1.Y / v1.X, a2 = v2.Y / v2.X;
            double b1 = p1.Y - p1.X * a1, b2 = p2.Y - p2.X * a2;
            // a1 * x + b1 = a2 * x + b2
            // x = (b2 - b1) / (a1 - a2)
            double x = (b2 - b1) / (a1 - a2);
            double y = p1.Y - a1 * (p1.X - x);
            return new Point(x, y);
        }
    }

    public class Vector
    {
        public double X { get; }
        public double Y { get; }

        private Vector(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector Cartesian(double x, double y)
        {
            return new Vector(x, y);
        }

        public static Vector Polar(double theta, double r)
        {
            var thetaInRadians = theta * Math.PI / 180;
            return new Vector(r * Math.Cos(thetaInRadians), r * Math.Sin(thetaInRadians));
        }
    }
}