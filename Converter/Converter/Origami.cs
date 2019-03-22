using System;
using System.Collections.Generic;

namespace Converter
{
    public class Origami
    {
        public enum Format { LaTeX, SVG, FOLD };
        public enum Type { Unassigned, Parsimonious, Assigned };

        #region Format-Specific Methods
        public static string BeginningOfFile(Format format)
        {
            switch (format)
            {
                case Format.LaTeX:
                    return LaTeX.BeginningOfFile;
                case Format.SVG:
                    throw new NotImplementedException();
                case Format.FOLD:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        public static string DrawLine(Format format, Point startPoint, Point endPoint)
        {
            switch (format)
            {
                case Format.LaTeX:
                    return LaTeX.DrawLine(startPoint, endPoint);
                case Format.SVG:
                    throw new NotImplementedException();
                case Format.FOLD:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        public static string DrawLine(Format format, Point point, Vector vector)
        {
            switch (format)
            {
                case Format.LaTeX:
                    return LaTeX.DrawLine(point, vector);
                case Format.SVG:
                    throw new NotImplementedException();
                case Format.FOLD:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        public static string EndOfFile(Format format)
        {
            switch (format)
            {
                case Format.LaTeX:
                    return LaTeX.EndOfFile;
                case Format.SVG:
                    throw new NotImplementedException();
                case Format.FOLD:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion
        #region Lengths
        public static int PaperHeight(Type type, int numVars)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.PaperHeight(numVars);
                case Type.Parsimonious:
                    return Parsimonious.PaperHeight(numVars);
                default:
                    throw new NotImplementedException();
            }
        }

        public static int WireHeight(Type type, int index)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.WireHeight(index);
                case Type.Parsimonious:
                    return Parsimonious.WireHeight(index);
                default:
                    throw new NotImplementedException();
            }
        }

        public static int PaperStart(Type type)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.PaperStart;
                case Type.Parsimonious:
                    return Parsimonious.PaperStart;
                default:
                    throw new NotImplementedException();
            }
        }

        public static int NegateWidth(Type type)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.NegateWidth;
                case Type.Parsimonious:
                    return Parsimonious.NegateWidth;
                default:
                    throw new NotImplementedException();
            }
        }

        public static int ClauseLeftPadding(Type type)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.ClauseLeftPadding;
                case Type.Parsimonious:
                    return Parsimonious.ClauseLeftPadding;
                default:
                    throw new NotImplementedException();
            }
        }

        public static int ClauseGap1(Type type)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.ClauseGap1;
                case Type.Parsimonious:
                    return Parsimonious.ClauseGap1;
                default:
                    throw new NotImplementedException();
            }
        }

        public static int ClauseGap2(Type type)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.ClauseGap2;
                case Type.Parsimonious:
                    return Parsimonious.ClauseGap2;
                default:
                    throw new NotImplementedException();
            }
        }

        public static int ClauseGap3(Type type)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.ClauseGap3;
                case Type.Parsimonious:
                    return Parsimonious.ClauseGap3;
                default:
                    throw new NotImplementedException();
            }
        }

        public static int ClauseRightPadding(Type type)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.ClauseRightPadding;
                case Type.Parsimonious:
                    return Parsimonious.ClauseRightPadding;
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion
        #region Components
        public static string Negate(Format format, Type type, Variable variable, int sweepLine, int paperHeight, List<Variable> variables)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.Negate(format, variable, sweepLine, paperHeight, variables);
                case Type.Parsimonious:
                    return Parsimonious.Negate(format, variable, sweepLine, paperHeight, variables);
                default:
                    throw new NotImplementedException();
            }
        }

        public static string ClauseTop(Format format, Type type, int sweepLine, List<Variable> variables)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.ClauseTop(format, sweepLine, variables);
                case Type.Parsimonious:
                    return Parsimonious.ClauseTop(format, sweepLine, variables);
                default:
                    throw new NotImplementedException();
            }
        }

        public static string SendWireToClause(Format format, Type type, Variable variable, int clauseNumber, int sweepLine, int paperHeight, List<Variable> variables)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.SendWireToClause(format, variable, clauseNumber, sweepLine, paperHeight, variables);
                case Type.Parsimonious:
                    return Parsimonious.SendWireToClause(format, variable, clauseNumber, sweepLine, paperHeight, variables);
                default:
                    throw new NotImplementedException();
            }
        }

        public static string EndWire(Format format, Type type, Variable variable, int paperWidth)
        {
            switch (type)
            {
                case Type.Unassigned:
                    return Unassigned.EndWire(format, variable, paperWidth);
                case Type.Parsimonious:
                    return Parsimonious.EndWire(format, variable, paperWidth);
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion
    }
}