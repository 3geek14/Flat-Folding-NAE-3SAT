using System;
using System.Collections.Generic;

namespace Converter
{
    public class Origami
    {
        public static string BeginningOfFile(string format)
        {
            if (format.Equals("LaTeX"))
            {
                return LaTeX.BeginningOfFile;
            }
            throw new NotImplementedException();
        }

        public static string EndOfFile(string format)
        {
            if (format.Equals("LaTeX"))
            {
                return LaTeX.EndOfFile;
            }
            throw new NotImplementedException();
        }

        public static int PaperHeight(string type, int numVars)
        {
            if (type.Equals("Unassigned"))
            {
                return 9 + numVars * 5;
            }
            throw new NotImplementedException();
        }

        public static int PaperStart(string type)
        {
            if (type.Equals("Unassigned"))
            {
                return 2;
            }
            throw new NotImplementedException();
        }

        public static int NegateWidth(string type)
        {
            if (type.Equals("Unassigned"))
            {
                return 5;
            }
            throw new NotImplementedException();
        }

        public static int ClauseLeftPadding(string type)
        {
            if (type.Equals("Unassigned"))
            {
                return 2;
            }
            throw new NotImplementedException();
        }

        public static int ClauseGap1(string type)
        {
            if (type.Equals("Unassigned"))
            {
                return 3;
            }
            throw new NotImplementedException();
        }

        public static int ClauseGap2(string type)
        {
            if (type.Equals("Unassigned"))
            {
                return 5;
            }
            throw new NotImplementedException();
        }

        public static int ClauseGap3(string type)
        {
            if (type.Equals("Unassigned"))
            {
                return 5;
            }
            throw new NotImplementedException();
        }

        public static int ClauseRightPadding(string type)
        {
            if (type.Equals("Unassigned"))
            {
                return 9;
            }
            throw new NotImplementedException();
        }

        public static string Negate(string format, string type, Variable variable, int sweepLine, int paperHeight, List<Variable> variables)
        {
            if (format.Equals("LaTeX"))
            {
                if (type.Equals("Unassigned"))
                {
                    return LaTeX.Negate(variable, sweepLine, paperHeight);
                }
            }
            throw new NotImplementedException();
        }

        internal static string ClauseTop(string format, string type, int sweepLine)
        {
            if (format.Equals("LaTeX"))
            {
                if (type.Equals("Unassigned"))
                {
                    return LaTeX.ClauseTop(sweepLine);
                }
            }
            throw new NotImplementedException();
        }

        internal static string SendWireToClause(string format, string type, Variable variable, int clauseNumber, int sweepLine, int paperHeight)
        {
            if (format.Equals("LaTeX"))
            {
                if (type.Equals("Unassigned"))
                {
                    return LaTeX.SendWireToClause(variable, clauseNumber, sweepLine, paperHeight);
                }
            }
            throw new NotImplementedException();
        }

        internal static string EndWire(string format, string type, Variable variable, int paperWidth)
        {
            if (format.Equals("LaTeX"))
            {
                if (type.Equals("Unassigned"))
                {
                    return LaTeX.EndWire(variable, paperWidth);
                }
            }
            throw new NotImplementedException();
        }
    }
}