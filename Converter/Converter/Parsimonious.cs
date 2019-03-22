using System;
using System.Collections.Generic;

namespace Converter
{
    public class Parsimonious
    {
        public static int PaperHeight(int numVars) { return 24 + 14 * numVars; }
        public static int WireHeight(int index) { return 12 + 14 * index; }
        public static readonly int PaperStart = 6;
        public static readonly int NegateWidth = 14;
        public static readonly int ClauseLeftPadding = 5;
        public static readonly int ClauseGap1 = 9;
        public static readonly int ClauseGap2 = 14;
        public static readonly int ClauseGap3 = 14;
        public static readonly int ClauseRightPadding = 28;

        public static string Negate(Origami.Format format, Variable v, int leftX, int paperHeight, List<Variable> variables)
        {   
            string output = "";

            int prevLowerHeight = 0;
            int prevUpperHeight = 0;
            int prevX = 0;
            bool up = true;
            Func<int, int> pointX = foo => 0, pointY = foo => 0, vectorX = foo => 0;
            for(int i = 0; i < variables.Count; ++i)
            {
                Variable vi = variables[i];
                int wireCenter = Origami.WireHeight(Origami.Type.Parsimonious, vi.Index);
                prevX = vi.PrevX;
                up = vi.Up;
                pointX = x => up ? leftX + x : leftX + 12 - x;
                pointY = y => up ? wireCenter + y : wireCenter - y;
                vectorX = x => up ? x : -x;
                Func<int> adjustedPrevX = () => prevX == 0 ? 0 : prevX - 1;

                if (vi.Equals(v))
                {
                    output += Origami.DrawLine(format, new Point(adjustedPrevX(), pointY(-2)), new Point(leftX + 1, pointY(-2)));
                    output += Origami.DrawLine(format, new Point(prevX, pointY(1)), new Point(leftX, pointY(1)));
                    v.Negate();
                    v.UpdatePrevX(leftX + 12);

                    output += Origami.DrawLine(format, new Point(pointX(5), prevLowerHeight), new Point(pointX(5), wireCenter - 6));
                    output += Origami.DrawLine(format, new Point(pointX(8), prevUpperHeight), new Point(pointX(8), wireCenter - 5));
                    prevLowerHeight = wireCenter + 6;
                    prevUpperHeight = wireCenter + 5;

                    output += Origami.DrawLine(format, new Point(pointX(0), wireCenter + 1), new Vector(vectorX(1), -3));
                    output += Origami.DrawLine(format, new Point(pointX(1), wireCenter - 2), new Vector(vectorX(4), -4));
                    output += Origami.DrawLine(format, new Point(pointX(5), wireCenter - 6), new Vector(vectorX(3), 1));
                    output += Origami.DrawLine(format, new Point(pointX(8), wireCenter - 5), new Vector(vectorX(4), 4));
                    output += Origami.DrawLine(format, new Point(pointX(12), wireCenter - 1), new Vector(vectorX(-1), 3));
                    output += Origami.DrawLine(format, new Point(pointX(11), wireCenter + 2), new Vector(vectorX(-4), 4));
                    output += Origami.DrawLine(format, new Point(pointX(7), wireCenter + 6), new Vector(vectorX(-3), -1));
                    output += Origami.DrawLine(format, new Point(pointX(4), wireCenter + 5), new Vector(vectorX(-4), -4));

                    output += Origami.DrawLine(format, new Point(pointX(2), wireCenter + 0), new Vector(vectorX(4), -4));
                    output += Origami.DrawLine(format, new Point(pointX(2), wireCenter + 0), new Vector(vectorX(-1), -2));
                    output += Origami.DrawLine(format, new Point(pointX(2), wireCenter + 0), new Vector(vectorX(-2), 1));
                    output += Origami.DrawLine(format, new Point(pointX(6), wireCenter - 4), new Vector(vectorX(4), 4));
                    output += Origami.DrawLine(format, new Point(pointX(6), wireCenter - 4), new Vector(vectorX(2), -1));
                    output += Origami.DrawLine(format, new Point(pointX(6), wireCenter - 4), new Vector(vectorX(-1), -2));
                    output += Origami.DrawLine(format, new Point(pointX(10), wireCenter + 0), new Vector(vectorX(-4), 4));
                    output += Origami.DrawLine(format, new Point(pointX(10), wireCenter + 0), new Vector(vectorX(1), 2));
                    output += Origami.DrawLine(format, new Point(pointX(10), wireCenter + 0), new Vector(vectorX(2), -1));
                    output += Origami.DrawLine(format, new Point(pointX(6), wireCenter + 4), new Vector(vectorX(-4), -4));
                    output += Origami.DrawLine(format, new Point(pointX(6), wireCenter + 4), new Vector(vectorX(-2), 1));
                    output += Origami.DrawLine(format, new Point(pointX(6), wireCenter + 4), new Vector(vectorX(1), 2));
                }
                else
                {
                    output += Origami.DrawLine(format, new Point(adjustedPrevX(), pointY(-2)), new Point(leftX + 5, pointY(-2)));
                    output += Origami.DrawLine(format, new Point(prevX, pointY(1)), new Point(leftX + 4, pointY(1)));
                    vi.UpdatePrevX(leftX + 8);

                    output += Origami.DrawLine(format, new Point(pointX(5), prevLowerHeight), new Point(pointX(5), wireCenter - 2));
                    output += Origami.DrawLine(format, new Point(pointX(8), prevUpperHeight), new Point(pointX(8), wireCenter - 1));
                    prevLowerHeight = wireCenter + 2;
                    prevUpperHeight = wireCenter + 1;

                    output += Origami.DrawLine(format, new Point(pointX(5), wireCenter - 2), new Vector(vectorX(3), 1));
                    output += Origami.DrawLine(format, new Point(pointX(8), wireCenter - 1), new Vector(vectorX(-1), 3));
                    output += Origami.DrawLine(format, new Point(pointX(7), wireCenter + 2), new Vector(vectorX(-3), -1));
                    output += Origami.DrawLine(format, new Point(pointX(4), wireCenter + 1), new Vector(vectorX(1), -3));
                }
                vi.Flip();
            }
            output += Origami.DrawLine(format, new Point(pointX(7), prevLowerHeight), new Point(pointX(7), paperHeight));
            output += Origami.DrawLine(format, new Point(pointX(4), prevUpperHeight), new Point(pointX(4), paperHeight));

            return output;
        }

        public static string ClauseTop(Origami.Format format, int leftX, List<Variable> variables)
        {
            var clauseUp = variables[0].Up;
            Func<int, int> pointX = (x) =>
             {
                 return clauseUp ? leftX + x : leftX + 58 - x;
             };
            Func<int, int> vectorX = (x) =>
             {
                 return clauseUp ? x : -x;
             };
            return
                Origami.DrawLine(format, new Point(pointX(0), 0), new Vector(vectorX(14), 14)) +
                Origami.DrawLine(format, new Point(pointX(3), 0), new Vector(vectorX(12), 12)) +
                Origami.DrawLine(format, new Point(pointX(22), 8), new Vector(vectorX(8), 8)) +
                Origami.DrawLine(format, new Point(pointX(23), 6), new Vector(vectorX(7), 7)) +
                Origami.DrawLine(format, new Point(pointX(37), 9), new Vector(vectorX(5), 5)) +
                Origami.DrawLine(format, new Point(pointX(38), 7), new Vector(vectorX(5), 5)) +

                Origami.DrawLine(format, new Point(pointX(15), 12), new Vector(vectorX(5), -5)) +
                Origami.DrawLine(format, new Point(pointX(17), 13), new Vector(vectorX(5), -5)) +
                Origami.DrawLine(format, new Point(pointX(30), 13), new Vector(vectorX(5), -5)) +
                Origami.DrawLine(format, new Point(pointX(30), 16), new Vector(vectorX(7), -7)) +
                Origami.DrawLine(format, new Point(pointX(43), 12), new Vector(vectorX(12), -12)) +
                Origami.DrawLine(format, new Point(pointX(45), 13), new Vector(vectorX(13), -13)) +

                Origami.DrawLine(format, new Point(pointX(20), 0), new Vector(vectorX(0), 7)) +
                Origami.DrawLine(format, new Point(pointX(23), 0), new Vector(vectorX(0), 6)) +
                Origami.DrawLine(format, new Point(pointX(35), 0), new Vector(vectorX(0), 8)) +
                Origami.DrawLine(format, new Point(pointX(38), 0), new Vector(vectorX(0), 7)) +

                Origami.DrawLine(format, new Point(pointX(14), 14), new Vector(vectorX(1), -2)) +
                Origami.DrawLine(format, new Point(pointX(15), 12), new Vector(vectorX(2), 1)) +
                Origami.DrawLine(format, new Point(pointX(14), 14), new Vector(vectorX(3), -1)) +
                Origami.DrawLine(format, new Point(pointX(20), 7), new Vector(vectorX(3), -1)) +
                Origami.DrawLine(format, new Point(pointX(20), 7), new Vector(vectorX(2), 1)) +
                Origami.DrawLine(format, new Point(pointX(22), 8), new Vector(vectorX(1), -2)) +

                Origami.DrawLine(format, new Point(pointX(27), 13), new Vector(vectorX(3), 0)) +
                Origami.DrawLine(format, new Point(pointX(30), 13), new Vector(vectorX(0), 3)) +

                Origami.DrawLine(format, new Point(pointX(35), 8), new Vector(vectorX(3), -1)) +
                Origami.DrawLine(format, new Point(pointX(35), 8), new Vector(vectorX(2), 1)) +
                Origami.DrawLine(format, new Point(pointX(37), 9), new Vector(vectorX(1), -2)) +
                Origami.DrawLine(format, new Point(pointX(42), 14), new Vector(vectorX(1), -2)) +
                Origami.DrawLine(format, new Point(pointX(43), 12), new Vector(vectorX(2), 1)) +
                Origami.DrawLine(format, new Point(pointX(42), 14), new Vector(vectorX(3), -1));
        }

        public static string SendWireToClause(Origami.Format format, Variable v, int wireNumber, int leftX, int paperHeight, List<Variable> variables)
        {
            string output = "";
            
            int prevLowerHeight = 0, prevUpperHeight = 0;
            switch (wireNumber)
            {
                case 1:
                    prevLowerHeight = 14;
                    prevUpperHeight = 13;
                    break;
                case 2:
                    prevLowerHeight = 16;
                    prevUpperHeight = 13;
                    break;
                case 3:
                    prevLowerHeight = 14;
                    prevUpperHeight = 13;
                    break;
                default:
                    throw new IndexOutOfRangeException($"`wireNumber` cannot be {wireNumber}; it must be 1, 2, or 3.");
            }

            int prevX = 0;
            bool up = true;
            Func<int, int> pointX = foo => 0, pointY = foo => 0, vectorX = foo => 0;
            for (int i = 0; i < variables.Count; ++i)
            {
                Variable vi = variables[i];
                int wireCenter = Origami.WireHeight(Origami.Type.Parsimonious, vi.Index);
                prevX = vi.PrevX;
                up = vi.Up;
                pointX = x => up ? leftX + x : leftX + 12 - x;
                pointY = y => up ? wireCenter + y : wireCenter - y;
                vectorX = x => up ? x : -x;
                Func<int> adjustedPrevX = () => prevX == 0 ? 0 : prevX - 1;

                if (vi.Equals(v))
                {
                    output += Origami.DrawLine(format, new Point(adjustedPrevX(), pointY(-2)), new Point(leftX + 1, pointY(-2)));
                    output += Origami.DrawLine(format, new Point(prevX, pointY(1)), new Point(leftX, pointY(1)));
                    v.Negate();
                    v.UpdatePrevX(leftX + 12);

                    output += Origami.DrawLine(format, new Point(pointX(5), prevLowerHeight), new Point(pointX(5), wireCenter - 6));
                    output += Origami.DrawLine(format, new Point(pointX(8), prevUpperHeight), new Point(pointX(8), wireCenter - 5));
                    prevLowerHeight = wireCenter + 6;
                    prevUpperHeight = wireCenter + 5;

                    output += Origami.DrawLine(format, new Point(pointX(0), wireCenter + 1), new Vector(vectorX(1), -3));
                    output += Origami.DrawLine(format, new Point(pointX(1), wireCenter - 2), new Vector(vectorX(4), -4));
                    output += Origami.DrawLine(format, new Point(pointX(5), wireCenter - 6), new Vector(vectorX(3), 1));
                    output += Origami.DrawLine(format, new Point(pointX(8), wireCenter - 5), new Vector(vectorX(4), 4));
                    output += Origami.DrawLine(format, new Point(pointX(12), wireCenter - 1), new Vector(vectorX(-1), 3));
                    output += Origami.DrawLine(format, new Point(pointX(11), wireCenter + 2), new Vector(vectorX(-4), 4));
                    output += Origami.DrawLine(format, new Point(pointX(7), wireCenter + 6), new Vector(vectorX(-3), -1));
                    output += Origami.DrawLine(format, new Point(pointX(4), wireCenter + 5), new Vector(vectorX(-4), -4));

                    output += Origami.DrawLine(format, new Point(pointX(2), wireCenter + 0), new Vector(vectorX(4), -4));
                    output += Origami.DrawLine(format, new Point(pointX(2), wireCenter + 0), new Vector(vectorX(-1), -2));
                    output += Origami.DrawLine(format, new Point(pointX(2), wireCenter + 0), new Vector(vectorX(-2), 1));
                    output += Origami.DrawLine(format, new Point(pointX(6), wireCenter - 4), new Vector(vectorX(4), 4));
                    output += Origami.DrawLine(format, new Point(pointX(6), wireCenter - 4), new Vector(vectorX(2), -1));
                    output += Origami.DrawLine(format, new Point(pointX(6), wireCenter - 4), new Vector(vectorX(-1), -2));
                    output += Origami.DrawLine(format, new Point(pointX(10), wireCenter + 0), new Vector(vectorX(-4), 4));
                    output += Origami.DrawLine(format, new Point(pointX(10), wireCenter + 0), new Vector(vectorX(1), 2));
                    output += Origami.DrawLine(format, new Point(pointX(10), wireCenter + 0), new Vector(vectorX(2), -1));
                    output += Origami.DrawLine(format, new Point(pointX(6), wireCenter + 4), new Vector(vectorX(-4), -4));
                    output += Origami.DrawLine(format, new Point(pointX(6), wireCenter + 4), new Vector(vectorX(-2), 1));
                    output += Origami.DrawLine(format, new Point(pointX(6), wireCenter + 4), new Vector(vectorX(1), 2));
                }
                else
                {
                    output += Origami.DrawLine(format, new Point(adjustedPrevX(), pointY(-2)), new Point(leftX + 5, pointY(-2)));
                    output += Origami.DrawLine(format, new Point(prevX, pointY(1)), new Point(leftX + 4, pointY(1)));
                    vi.UpdatePrevX(leftX + 8);

                    output += Origami.DrawLine(format, new Point(pointX(5), prevLowerHeight), new Point(pointX(5), wireCenter - 2));
                    output += Origami.DrawLine(format, new Point(pointX(8), prevUpperHeight), new Point(pointX(8), wireCenter - 1));
                    prevLowerHeight = wireCenter + 2;
                    prevUpperHeight = wireCenter + 1;

                    output += Origami.DrawLine(format, new Point(pointX(5), wireCenter - 2), new Vector(vectorX(3), 1));
                    output += Origami.DrawLine(format, new Point(pointX(8), wireCenter - 1), new Vector(vectorX(-1), 3));
                    output += Origami.DrawLine(format, new Point(pointX(7), wireCenter + 2), new Vector(vectorX(-3), -1));
                    output += Origami.DrawLine(format, new Point(pointX(4), wireCenter + 1), new Vector(vectorX(1), -3));
                }
                vi.Flip();
            }
            output += Origami.DrawLine(format, new Point(pointX(7), prevLowerHeight), new Point(pointX(7), paperHeight));
            output += Origami.DrawLine(format, new Point(pointX(4), prevUpperHeight), new Point(pointX(4), paperHeight));

            return output;
        }

        public static string EndWire(Origami.Format format, Variable v, int paperWidth)
        {
            if (v.PrevX == paperWidth)
            {
                return "";
            }
            int wireHeight = Origami.WireHeight(Origami.Type.Parsimonious, v.Index);
            int prevX = v.PrevX;
            bool up = v.Up;

            v.UpdatePrevX(paperWidth);

            int topWire, bottomWire, topPrevX, bottomPrevX;
            if (up)
            {
                topWire = wireHeight - 2;
                bottomWire = wireHeight + 1;
                topPrevX = prevX - 1;
                bottomPrevX = prevX;
            }
            else
            {
                topWire = wireHeight - 1;
                bottomWire = wireHeight + 2;
                topPrevX = prevX;
                bottomPrevX = prevX - 1;
            }

            return
                Origami.DrawLine(format, new Point(topPrevX, topWire), new Point(paperWidth, topWire)) +
                Origami.DrawLine(format, new Point(bottomPrevX, bottomWire), new Point(paperWidth, bottomWire));
        }
    }
}