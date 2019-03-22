namespace Converter
{
    public struct Point
    {
        public int X { get; }
        public int Y { get; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public class Vector
    {
        public int X { get; }
        public int Y { get; }

        public Vector(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}