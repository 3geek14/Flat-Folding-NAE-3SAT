using System;

namespace Converter
{
    public static class LaTeX
    {
        public static readonly string BeginningOfFile =
            "\\documentclass[multi=tikzpicture]{standalone}\n" +
            "\\usepackage{tikz}\n" +
            "\\begin{document}\n" +
            "\n" +
            "\\begin{tikzpicture}[rotate=-90]\n";

        public static readonly string EndOfFile =
            "\\end{tikzpicture}\n" +
            "\n" +
            "\\end{document}";

        public static string DrawLine(Point startPoint, Point endPoint)
        {
            return $"  \\path[draw=black] ({startPoint.X},{startPoint.Y}) -- ({endPoint.X},{endPoint.Y});\n";
        }

        public static string DrawLine(Point point, Vector vector)
        {
            return $"  \\path[draw=black] ({point.X},{point.Y}) -- ++({vector.X}, {vector.Y});\n";
        }

        //public static string TwistedClause(double theta)
        //{
        //    Point xStartTop = new Point(0, 0);
        //    Point xStartBottom = xStartTop + Vector.Cartesian(0, -1);
        //    Point tmpA = xStartTop + Vector.Cartesian(2, 0);
        //    Point tmpB = Point.Intersection(tmpA, Vector.Polar(theta - 90, 1), xStartBottom, Vector.Cartesian(1, 0));
        //    Point tmpC = Point.Intersection(tmpB, Vector.Polar(theta + 45, 1), tmpA, Vector.Polar(theta - 45, 1));

        //    return
        //        "  \\fill[gray] (-1in,1in) rectangle new Point(5in, -5in)) +
        //        $"  \\path[draw=red] {xStartTop} -- {tmpA};\n" +
        //        $"  \\path[draw=black] {tmpA} -- {tmpB} -- {tmpC} -- cycle;" +
        //        $"  \\path[draw=red] {xStartBottom} -- {tmpB};";

        //    throw new NotImplementedException(); // TODO: Implement this.
        //}

        //public static string TwistedNegate(double leftX, double topY, double paperHeight, double prevTopX, double prevBottomX, string color, double theta)
        //{
        //    Point prevTop = new Point(prevTopX, topY);
        //    Point prevBottom = new Point(prevBottomX, topY - 1);

        //    Point leftTriTop = new Point(leftX, topY);
        //    Point leftTriBottom = Point.Intersection(leftTriTop, Vector.Polar(-90 + theta, 1), prevBottom, Vector.Polar(0, 1));
        //    Point leftTriMid = Point.Intersection(leftTriTop, Vector.Polar(-45 + theta, 1), leftTriBottom, Vector.Polar(45 + theta, 1));

        //    Point topTriLeft = leftTriTop + Vector.Polar(45, 3 * Math.Sqrt(2));
        //    Point topTriMid = leftTriMid + Vector.Polar(45, 3 * Math.Sqrt(2));
        //    Point topTriRight = Point.Intersection(topTriLeft, Vector.Polar(theta, 1), topTriMid, Vector.Polar(45 + theta, 1));

        //    Point rightTriTop = topTriRight + Vector.Polar(-45, 3 * Math.Sqrt(2));
        //    Point rightTriMid = topTriMid + Vector.Polar(-45, 3 * Math.Sqrt(2));
        //    Point rightTriBottom = Point.Intersection(rightTriTop, Vector.Polar(-90+theta, 1), rightTriMid, Vector.Polar(-45 + theta, 1));

        //    Point bottomTriLeft = leftTriBottom + Vector.Polar(-45, 3 * Math.Sqrt(2));
        //    Point bottomTriMid = leftTriMid + Vector.Polar(-45, 3 * Math.Sqrt(2));
        //    Point bottomTriRight = rightTriBottom + Vector.Polar(-135, 3 * Math.Sqrt(2));

        //    Point topLeft = new Point(topTriLeft.X, paperHeight);
        //    Point topRight = new Point(topTriRight.X, paperHeight);
        //    Point bottomLeft = new Point(bottomTriLeft.X, 0);
        //    Point bottomRight = new Point(bottomTriRight.X, 0);

        //    return
        //        $"  \\path[draw=black] {prevTop} -- {leftTriTop} -- {topTriLeft} -- {topLeft};\n" +
        //        $"  \\path[draw=black] {prevBottom} -- {leftTriBottom} -- {bottomTriLeft} -- {bottomLeft};\n" +
        //        $"  \\path[draw=black] {topRight} -- {topTriRight} -- {rightTriTop};\n" +
        //        $"  \\path[draw=black] {bottomRight} -- {bottomTriRight} -- {rightTriBottom};\n" +
        //        $"  \\path[draw=black] {leftTriMid} -- {topTriMid} -- {rightTriMid} -- {bottomTriMid} -- cycle;\n" +
        //        $"  \\path[draw=black] {leftTriTop} -- {leftTriMid} -- {leftTriBottom} -- cycle;\n" +
        //        $"  \\path[draw=black] {topTriLeft} -- {topTriMid} -- {topTriRight} -- cycle;\n" +
        //        $"  \\path[draw=black] {rightTriTop} -- {rightTriMid} -- {rightTriBottom} -- cycle;\n" +
        //        $"  \\path[draw=black] {bottomTriLeft} -- {bottomTriMid} -- {bottomTriRight} -- cycle;\n";
        //}
    }
}
