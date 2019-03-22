using System;
using System.Collections.Generic;

namespace Converter
{
    public class Unassigned
    {
        public static int PaperHeight(int numVars) { return 11 + 5 * numVars; }
        public static int WireHeight(int index) { return 6 + 5 * index; }
        public static readonly int PaperStart = 2;
        public static readonly int NegateWidth = 5;
        public static readonly int ClauseLeftPadding = 2;
        public static readonly int ClauseGap1 = 3;
        public static readonly int ClauseGap2 = 5;
        public static readonly int ClauseGap3 = 5;
        public static readonly int ClauseRightPadding = 9;

        public static string Negate(Origami.Format format, Variable v, int leftX, int paperHeight, List<Variable> variables)
        {
            int topY = Origami.WireHeight(Origami.Type.Unassigned, v.Index);
            int prevX = v.PrevX;
            v.Negate();
            v.UpdatePrevX(leftX + Origami.NegateWidth(Origami.Type.Unassigned));
            return
                Origami.DrawLine(format, new Point(leftX, topY + 1), new Vector(2, 2)) +
                Origami.DrawLine(format, new Point(leftX, topY), new Vector(3, 3)) +
                Origami.DrawLine(format, new Point(leftX + 2, topY - 2), new Vector(3, 3)) +
                Origami.DrawLine(format, new Point(leftX + 3, topY - 2), new Vector(2, 2)) +
                Origami.DrawLine(format, new Point(leftX, topY), new Vector(2, -2)) +
                Origami.DrawLine(format, new Point(leftX, topY + 1), new Vector(3, -3)) +
                Origami.DrawLine(format, new Point(leftX + 2, topY + 3), new Vector(3, -3)) +
                Origami.DrawLine(format, new Point(leftX + 3, topY + 3), new Vector(2, -2)) +
                Origami.DrawLine(format, new Point(leftX + 2, topY - 2), new Vector(1, 0)) +
                Origami.DrawLine(format, new Point(leftX + 2, topY + 3), new Vector(1, 0)) +
                Origami.DrawLine(format, new Point(leftX, topY), new Vector(0, 1)) +
                Origami.DrawLine(format, new Point(leftX + 5, topY), new Vector(0, 1)) +
                Origami.DrawLine(format, new Point(leftX + 2, topY - 2), new Point(leftX + 2, 0)) +
                Origami.DrawLine(format, new Point(leftX + 3, topY - 2), new Point(leftX + 3, 0)) +
                Origami.DrawLine(format, new Point(leftX + 2, topY + 3), new Point(leftX + 2, paperHeight)) +
                Origami.DrawLine(format, new Point(leftX + 3, topY + 3), new Point(leftX + 3, paperHeight)) +
                Origami.DrawLine(format, new Point(leftX, topY), new Point(prevX, topY)) +
                Origami.DrawLine(format, new Point(leftX, topY + 1), new Point(prevX, topY + 1));
        }

        public static string ClauseTop(Origami.Format format, int leftX, List<Variable> variables)
        {
            return
                Origami.DrawLine(format, new Point(leftX, 0), new Vector(5, 5)) +
                Origami.DrawLine(format, new Point(leftX + 1, 0), new Vector(5, 5)) +
                Origami.DrawLine(format, new Point(leftX + 8, 2), new Vector(2, 2)) +
                Origami.DrawLine(format, new Point(leftX + 10, 4), new Vector(1, 1)) + // clause triangle
                Origami.DrawLine(format, new Point(leftX + 9, 2), new Vector(2, 2)) +
                Origami.DrawLine(format, new Point(leftX + 13, 2), new Vector(2, 2)) +
                Origami.DrawLine(format, new Point(leftX + 14, 2), new Vector(2, 2)) +
                Origami.DrawLine(format, new Point(leftX + 5, 5), new Vector(3, -3)) +
                Origami.DrawLine(format, new Point(leftX + 6, 5), new Vector(3, -3)) +
                Origami.DrawLine(format, new Point(leftX + 11, 4), new Vector(2, -2)) +
                Origami.DrawLine(format, new Point(leftX + 11, 5), new Vector(3, -3)) +
                Origami.DrawLine(format, new Point(leftX + 15, 4), new Vector(4, -4)) +
                Origami.DrawLine(format, new Point(leftX + 16, 4), new Vector(4, -4)) +
                Origami.DrawLine(format, new Point(leftX + 5, 5), new Vector(1, 0)) +
                Origami.DrawLine(format, new Point(leftX + 8, 2), new Vector(1, 0)) +
                Origami.DrawLine(format, new Point(leftX + 10, 4), new Vector(1, 0)) + // clause triangle
                Origami.DrawLine(format, new Point(leftX + 13, 2), new Vector(1, 0)) +
                Origami.DrawLine(format, new Point(leftX + 15, 4), new Vector(1, 0)) +
                Origami.DrawLine(format, new Point(leftX + 8, 0), new Vector(0, 2)) +
                Origami.DrawLine(format, new Point(leftX + 9, 0), new Vector(0, 2)) +
                Origami.DrawLine(format, new Point(leftX + 11, 4), new Vector(0, 1)) + // clause triangle
                Origami.DrawLine(format, new Point(leftX + 13, 0), new Vector(0, 2)) +
                Origami.DrawLine(format, new Point(leftX + 14, 0), new Vector(0, 2));
        }

        public static string SendWireToClause(Origami.Format format, Variable v, int wireNumber, int leftX, int paperHeight, List<Variable> variables)
        {
            int topY = Origami.WireHeight(Origami.Type.Unassigned, v.Index);
            int prevX = v.PrevX;

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

            v.Negate();
            v.UpdatePrevX(leftX + 5);

            return
                Origami.DrawLine(format, new Point(leftX, topY + 1), new Vector(2, 2)) +
                Origami.DrawLine(format, new Point(leftX, topY), new Vector(3, 3)) +
                Origami.DrawLine(format, new Point(leftX + 2, topY - 2), new Vector(3, 3)) +
                Origami.DrawLine(format, new Point(leftX + 3, topY - 2), new Vector(2, 2)) +
                Origami.DrawLine(format, new Point(leftX, topY), new Vector(2, -2)) +
                Origami.DrawLine(format, new Point(leftX, topY + 1), new Vector(3, -3)) +
                Origami.DrawLine(format, new Point(leftX + 2, topY + 3), new Vector(3, -3)) +
                Origami.DrawLine(format, new Point(leftX + 3, topY + 3), new Vector(2, -2)) +
                Origami.DrawLine(format, new Point(leftX + 2, topY - 2), new Vector(1, 0)) +
                Origami.DrawLine(format, new Point(leftX + 2, topY + 3), new Vector(1, 0)) +
                Origami.DrawLine(format, new Point(leftX, topY), new Vector(0, 1)) +
                Origami.DrawLine(format, new Point(leftX + 5, topY), new Vector(0, 1)) +
                Origami.DrawLine(format, new Point(leftX + 2, topY - 2), new Point(leftX + 2, leftWireTop)) +
                Origami.DrawLine(format, new Point(leftX + 3, topY - 2), new Point(leftX + 3, rightWireTop)) +
                Origami.DrawLine(format, new Point(leftX + 2, topY + 3), new Point(leftX + 2, paperHeight)) +
                Origami.DrawLine(format, new Point(leftX + 3, topY + 3), new Point(leftX + 3, paperHeight)) +
                Origami.DrawLine(format, new Point(leftX, topY), new Point(prevX, topY)) +
                Origami.DrawLine(format, new Point(leftX, topY + 1), new Point(prevX, topY + 1));
        }

        public static string EndWire(Origami.Format format, Variable v, int paperWidth)
        {
            if (v.PrevX == paperWidth)
            {
                return "";
            }
            int topY = Origami.WireHeight(Origami.Type.Unassigned, v.Index);
            int prevX = v.PrevX;
            v.UpdatePrevX(paperWidth);
            return
                Origami.DrawLine(format, new Point(prevX, topY), new Point(paperWidth, topY)) +
                Origami.DrawLine(format, new Point(prevX, topY + 1), new Point(paperWidth, topY + 1));
        }
    }
}