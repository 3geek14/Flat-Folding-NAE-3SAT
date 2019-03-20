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

        public static readonly string NewImage =
            "\\end{tikzpicture}\n" +
            "\\begin{tikzpicture}\n";

        public static readonly string EndOfFile =
            "\\end{tikzpicture}\n" +
            "\n" +
            "\\end{document}";

        public static readonly string[] Colors = {
            "red", "green", "blue", "cyan", "magenta",
            "yellow", "black", "gray", "darkgray", "lightgray",
            "brown", "lime", "olive", "orange", "pink",
            "purple", "teal", "violet"
        };

        public static string ClauseTop(int leftX, string c1, string c2, string c3)
        {
            return
                $"  \\path[draw={c1}] ({leftX},0) -- ++(5,5);\n" +
                $"  \\path[draw={c1}] ({leftX + 1},0) -- ++(5,5);\n" +
                $"  \\path[draw={c1}] ({leftX + 8},2) -- ++(2,2);\n" +
                $"  \\path[draw={c2}] ({leftX + 10},4) -- ++(1,1);\n" + // clause triangle
                $"  \\path[draw={c1}] ({leftX + 9},2) -- ++(2,2);\n" +
                $"  \\path[draw={c3}] ({leftX + 13},2) -- ++(2,2);\n" +
                $"  \\path[draw={c3}] ({leftX + 14},2) -- ++(2,2);\n" +
                $"  \\path[draw={c1}] ({leftX + 5},5) -- ++(3,-3);\n" +
                $"  \\path[draw={c1}] ({leftX + 6},5) -- ++(3,-3);\n" +
                $"  \\path[draw={c3}] ({leftX + 11},4) -- ++(2,-2);\n" +
                $"  \\path[draw={c3}] ({leftX + 11},5) -- ++(3,-3);\n" +
                $"  \\path[draw={c3}] ({leftX + 15},4) -- ++(4,-4);\n" +
                $"  \\path[draw={c3}] ({leftX + 16},4) -- ++(4,-4);\n" +
                $"  \\path[draw={c1}] ({leftX + 5},5) -- ++(1,0);\n" +
                $"  \\path[draw={c1}] ({leftX + 8},2) -- ++(1,0);\n" +
                $"  \\path[draw={c1}] ({leftX + 10},4) -- ++(1,0);\n" + // clause triangle
                $"  \\path[draw={c3}] ({leftX + 13},2) -- ++(1,0);\n" +
                $"  \\path[draw={c3}] ({leftX + 15},4) -- ++(1,0);\n" +
                $"  \\path[draw={c1}] ({leftX + 8},0) -- ++(0,2);\n" +
                $"  \\path[draw={c1}] ({leftX + 9},0) -- ++(0,2);\n" +
                $"  \\path[draw={c3}] ({leftX + 11},4) -- ++(0,1);\n" + // clause triangle
                $"  \\path[draw={c3}] ({leftX + 13},0) -- ++(0,2);\n" +
                $"  \\path[draw={c3}] ({leftX + 14},0) -- ++(0,2);\n";
        }

        public static string ClauseTop(int leftX)
        {
            return ClauseTop(leftX, "black", "black", "black");
        }

        public static string Negate(int leftX, int topY, int paperHeight, int prevX, string color)
        {
            return
                $"  \\path[draw={color}] ({leftX},{topY + 3}) -- ++(2,2);\n" +
                $"  \\path[draw={color}] ({leftX},{topY + 2}) -- ++(3,3);\n" +
                $"  \\path[draw={color}] ({leftX + 2},{topY}) -- ++(3,3);\n" +
                $"  \\path[draw={color}] ({leftX + 3},{topY}) -- ++(2,2);\n" +
                $"  \\path[draw={color}] ({leftX},{topY + 2}) -- ++(2,-2);\n" +
                $"  \\path[draw={color}] ({leftX},{topY + 3}) -- ++(3,-3);\n" +
                $"  \\path[draw={color}] ({leftX + 2},{topY + 5}) -- ++(3,-3);\n" +
                $"  \\path[draw={color}] ({leftX + 3},{topY + 5}) -- ++(2,-2);\n" +
                $"  \\path[draw={color}] ({leftX + 2},{topY}) -- ++(1,0);\n" +
                $"  \\path[draw={color}] ({leftX + 2},{topY + 5}) -- ++(1,0);\n" +
                $"  \\path[draw={color}] ({leftX},{topY + 2}) -- ++(0,1);\n" +
                $"  \\path[draw={color}] ({leftX + 5},{topY + 2}) -- ++(0,1);\n" +
                $"  \\path[draw={color}] ({leftX + 2},{topY}) -- ({leftX + 2},0);\n" +
                $"  \\path[draw={color}] ({leftX + 3},{topY}) -- ({leftX + 3},0);\n" +
                $"  \\path[draw={color}] ({leftX + 2},{topY + 5}) -- ({leftX + 2},{paperHeight});\n" +
                $"  \\path[draw={color}] ({leftX + 3},{topY + 5}) -- ({leftX + 3},{paperHeight});\n" +
                $"  \\path[draw={color}] ({leftX},{topY + 2}) -- ({prevX},{topY + 2});\n" +
                $"  \\path[draw={color}] ({leftX},{topY + 3}) -- ({prevX},{topY + 3});\n";
        }

        public static string Negate(int leftX, int topY, int paperHeight, int prevX)
        {
            return Negate(leftX, topY, paperHeight, prevX, "black");
        }

        public static string Negate(Variable v, int leftX, int paperHeight)
        {
            string value = Negate(leftX, v.WireHeight - 2, paperHeight, v.PrevX, v.Color);
            v.Negate();
            v.UpdatePrevX(leftX + 5);
            return value;
        }

        public static string SendWireToClause(int leftX, int topY, int paperHeight, int prevX, int wireNumber, string color)
        {
            int leftWireTop = 0, rightWireTop = 0;
            switch (wireNumber)
            {
                case 1:
                    leftWireTop = 5;
                    rightWireTop = 5;
                    break;
                case 2:
                    leftWireTop = 4;
                    rightWireTop = 5;
                    break;
                case 3:
                    leftWireTop = 4;
                    rightWireTop = 4;
                    break;
                default:
                    throw new IndexOutOfRangeException($"`wireNumber` cannot be {wireNumber}; it must be 1, 2, or 3.");
            }
            return
                $"  \\path[draw={color}] ({leftX},{topY + 3}) -- ++(2,2);\n" +
                $"  \\path[draw={color}] ({leftX},{topY + 2}) -- ++(3,3);\n" +
                $"  \\path[draw={color}] ({leftX + 2},{topY}) -- ++(3,3);\n" +
                $"  \\path[draw={color}] ({leftX + 3},{topY}) -- ++(2,2);\n" +
                $"  \\path[draw={color}] ({leftX},{topY + 2}) -- ++(2,-2);\n" +
                $"  \\path[draw={color}] ({leftX},{topY + 3}) -- ++(3,-3);\n" +
                $"  \\path[draw={color}] ({leftX + 2},{topY + 5}) -- ++(3,-3);\n" +
                $"  \\path[draw={color}] ({leftX + 3},{topY + 5}) -- ++(2,-2);\n" +
                $"  \\path[draw={color}] ({leftX + 2},{topY}) -- ++(1,0);\n" +
                $"  \\path[draw={color}] ({leftX + 2},{topY + 5}) -- ++(1,0);\n" +
                $"  \\path[draw={color}] ({leftX},{topY + 2}) -- ++(0,1);\n" +
                $"  \\path[draw={color}] ({leftX + 5},{topY + 2}) -- ++(0,1);\n" +
                $"  \\path[draw={color}] ({leftX + 2},{topY}) -- ({leftX + 2},{leftWireTop});\n" +
                $"  \\path[draw={color}] ({leftX + 3},{topY}) -- ({leftX + 3},{rightWireTop});\n" +
                $"  \\path[draw={color}] ({leftX + 2},{topY + 5}) -- ({leftX + 2},{paperHeight});\n" +
                $"  \\path[draw={color}] ({leftX + 3},{topY + 5}) -- ({leftX + 3},{paperHeight});\n" +
                $"  \\path[draw={color}] ({leftX},{topY + 2}) -- ({prevX},{topY + 2});\n" +
                $"  \\path[draw={color}] ({leftX},{topY + 3}) -- ({prevX},{topY + 3});\n";
        }

        public static string SendWireToClause(int leftX, int topY, int paperHeight, int prevX, int wireNumber)
        {
            return SendWireToClause(leftX, topY, paperHeight, prevX, wireNumber, "black");
        }

        public static string SendWireToClause(Variable v, int wireNumber, int leftX, int paperHeight)
        {
            string value = SendWireToClause(leftX, v.WireHeight - 2, paperHeight, v.PrevX, wireNumber, v.Color);
            v.Negate();
            v.UpdatePrevX(leftX + 5);
            return value;
        }

        public static string EndWire(int prevX, int paperWidth, int topY, string color)
        {
            return
                $"  \\path[draw={color}] ({prevX},{topY}) -- ({paperWidth},{topY});\n" +
                $"  \\path[draw={color}] ({prevX},{topY + 1}) -- ({paperWidth},{topY + 1});\n";
        }

        public static string EndWire(int prevX, int paperWidth, int topY)
        {
            return EndWire(prevX, paperWidth, topY, "black");
        }

        public static string EndWire(Variable v, int paperWidth)
        {
            if (v.PrevX == paperWidth)
            {
                return "";
            }
            string value = EndWire(v.PrevX, paperWidth, v.WireHeight, v.Color);
            v.UpdatePrevX(paperWidth);
            return value;
        }

        public static string TwistedClause(double theta)
        {
            Point xStartTop = new Point(0, 0);
            Point xStartBottom = xStartTop + Vector.Cartesian(0, -1);
            Point tmpA = xStartTop + Vector.Cartesian(2, 0);
            Point tmpB = Point.Intersection(tmpA, Vector.Polar(theta - 90, 1), xStartBottom, Vector.Cartesian(1, 0));
            Point tmpC = Point.Intersection(tmpB, Vector.Polar(theta + 45, 1), tmpA, Vector.Polar(theta - 45, 1));

            return
                "  \\fill[gray] (-1in,1in) rectangle (5in,-5in);\n" +
                $"  \\path[draw=red] {xStartTop} -- {tmpA};\n" +
                $"  \\path[draw=black] {tmpA} -- {tmpB} -- {tmpC} -- cycle;" +
                $"  \\path[draw=red] {xStartBottom} -- {tmpB};";

            throw new NotImplementedException(); // TODO: Implement this.
        }

        public static string TwistedNegate(double leftX, double topY, double paperHeight, double prevTopX, double prevBottomX, string color, double theta)
        {
            Point prevTop = new Point(prevTopX, topY);
            Point prevBottom = new Point(prevBottomX, topY - 1);

            Point leftTriTop = new Point(leftX, topY);
            Point leftTriBottom = Point.Intersection(leftTriTop, Vector.Polar(-90 + theta, 1), prevBottom, Vector.Polar(0, 1));
            Point leftTriMid = Point.Intersection(leftTriTop, Vector.Polar(-45 + theta, 1), leftTriBottom, Vector.Polar(45 + theta, 1));

            Point topTriLeft = leftTriTop + Vector.Polar(45, 3 * Math.Sqrt(2));
            Point topTriMid = leftTriMid + Vector.Polar(45, 3 * Math.Sqrt(2));
            Point topTriRight = Point.Intersection(topTriLeft, Vector.Polar(theta, 1), topTriMid, Vector.Polar(45 + theta, 1));

            Point rightTriTop = topTriRight + Vector.Polar(-45, 3 * Math.Sqrt(2));
            Point rightTriMid = topTriMid + Vector.Polar(-45, 3 * Math.Sqrt(2));
            Point rightTriBottom = Point.Intersection(rightTriTop, Vector.Polar(-90+theta, 1), rightTriMid, Vector.Polar(-45 + theta, 1));

            Point bottomTriLeft = leftTriBottom + Vector.Polar(-45, 3 * Math.Sqrt(2));
            Point bottomTriMid = leftTriMid + Vector.Polar(-45, 3 * Math.Sqrt(2));
            Point bottomTriRight = rightTriBottom + Vector.Polar(-135, 3 * Math.Sqrt(2));

            Point topLeft = new Point(topTriLeft.X, paperHeight);
            Point topRight = new Point(topTriRight.X, paperHeight);
            Point bottomLeft = new Point(bottomTriLeft.X, 0);
            Point bottomRight = new Point(bottomTriRight.X, 0);

            return
                $"  \\path[draw={color}] {prevTop} -- {leftTriTop} -- {topTriLeft} -- {topLeft};\n" +
                $"  \\path[draw={color}] {prevBottom} -- {leftTriBottom} -- {bottomTriLeft} -- {bottomLeft};\n" +
                $"  \\path[draw={color}] {topRight} -- {topTriRight} -- {rightTriTop};\n" +
                $"  \\path[draw={color}] {bottomRight} -- {bottomTriRight} -- {rightTriBottom};\n" +
                $"  \\path[draw={color}] {leftTriMid} -- {topTriMid} -- {rightTriMid} -- {bottomTriMid} -- cycle;\n" +
                $"  \\path[draw=black] {leftTriTop} -- {leftTriMid} -- {leftTriBottom} -- cycle;\n" +
                $"  \\path[draw=black] {topTriLeft} -- {topTriMid} -- {topTriRight} -- cycle;\n" +
                $"  \\path[draw=black] {rightTriTop} -- {rightTriMid} -- {rightTriBottom} -- cycle;\n" +
                $"  \\path[draw=black] {bottomTriLeft} -- {bottomTriMid} -- {bottomTriRight} -- cycle;\n";
        }
    }
}
