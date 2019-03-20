using System;
using System.Collections.Generic;
using System.Linq;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> settings = ParseArgs(args ?? new string[0]);
            foreach(var key in settings.Keys)
            {
                Console.WriteLine($"Key:   {key}\nValue: {settings[key]}\n------");
            }
            (int numClausees, int numVars, List<List<int>> clauses, List<Variable> variables) = ReadInput(settings);
            string output = GenerateCreasePattern(settings, clauses, variables);
            OutputResult(settings, output);
        }

        private static string GenerateCreasePattern(Dictionary<string, string> settings, List<List<int>> clauses, List<Variable> variables)
        {
            string format = settings["Format"];
            string type = settings["Type"];
            string output = "";

            output += Origami.BeginningOfFile(format);

            int paperHeight = Origami.PaperHeight(type, variables[variables.Count - 1].Index);
            int sweepLine = Origami.PaperStart(type);

            foreach (var clause in clauses)
            {
                Variable var1 = variables.Where(variable => variable.Index == Math.Abs(clause[0])).Single();
                Variable var2 = variables.Where(variable => variable.Index == Math.Abs(clause[1])).Single();
                Variable var3 = variables.Where(variable => variable.Index == Math.Abs(clause[2])).Single();
                bool wireToNegate = false;

                (wireToNegate, var1, var2, var3) = WireToNegateAndOrder(clause, var1, var2, var3, clause[0] > 0, clause[1] > 0, clause[2] > 0);

                if (wireToNegate)
                {
                    output += Origami.Negate(format, type, var2, sweepLine, paperHeight, variables);
                    sweepLine += Origami.NegateWidth(type);
                }

                sweepLine += Origami.ClauseLeftPadding(type);
                output += Origami.ClauseTop(format, type, sweepLine);

                sweepLine += Origami.ClauseGap1(type);
                output += Origami.SendWireToClause(format, type, var1, 1, sweepLine, paperHeight);
                sweepLine += Origami.ClauseGap2(type);
                output += Origami.SendWireToClause(format, type, var2, 2, sweepLine, paperHeight);
                sweepLine += Origami.ClauseGap3(type);
                output += Origami.SendWireToClause(format, type, var3, 3, sweepLine, paperHeight);

                sweepLine += Origami.ClauseRightPadding(type);
            }

            foreach (var variable in variables)
            {
                output += Origami.EndWire(format, type, variable, sweepLine);
            }
            
            output += Origami.EndOfFile(format);

            return output;
        }

        static void Main2(string[] args)
        {
            (int numClauses, int numVars, List<List<int>> clauses, List<Variable> bar) = ReadInputFromConsole();

            string latex = "";
            latex += LaTeX.BeginningOfFile;

            var variables = Enumerable
                .Repeat(true, numVars)
                .Select((foo, i) => new Variable(i + 1, i < LaTeX.Colors.Length ? LaTeX.Colors[i] : "black"))
                .ToList();

            int paperHeight = variables[numVars - 1].WireHeight + 5;

            int rightEdge = 2;

            foreach (var clause in clauses)
            {
                Variable var1 = variables[Math.Abs(clause[0]) - 1];
                Variable var2 = variables[Math.Abs(clause[1]) - 1];
                Variable var3 = variables[Math.Abs(clause[2]) - 1];
                bool wireToNegate = false;
                (wireToNegate, var1, var2, var3) = WireToNegateAndOrder(clause, var1, var2, var3);

                if (wireToNegate)
                {
                    latex += LaTeX.Negate(var2, rightEdge, paperHeight);
                    rightEdge += 5;
                }

                rightEdge += 2;
                latex += LaTeX.ClauseTop(rightEdge, var1.Color, var2.Color, var3.Color);
                rightEdge += 3;

                latex += LaTeX.SendWireToClause(var1, 1, rightEdge, paperHeight);
                rightEdge += 5;
                latex += LaTeX.SendWireToClause(var2, 2, rightEdge, paperHeight);
                rightEdge += 5;
                latex += LaTeX.SendWireToClause(var3, 3, rightEdge, paperHeight);
                rightEdge += 9;
            }

            foreach (var variable in clauses
                .SelectMany(vars => vars)
                .Select(i => variables[Math.Abs(i) - 1]))
            {
                latex += LaTeX.EndWire(variable, rightEdge);
            }

            latex += LaTeX.EndOfFile;

            System.IO.File.WriteAllText("output.tex", latex);

            Console.WriteLine(latex);

#if DEBUG
            Console.WriteLine("Press [Enter] to end.");
            Console.ReadLine();
#endif
        }

        private static (bool, Variable, Variable, Variable) WireToNegateAndOrder(List<int> clause, Variable var1, Variable var2, Variable var3)
        {
            if (var1.Value)
            {
                if (var2.Value)
                {
                    if (var3.Value)
                    { // T, T, T
                        return (true, var1, var2, var3);
                    }
                    else
                    { // T, T, F
                        return (false, var1, var3, var2);
                    }
                }
                else
                {
                    if (var3.Value)
                    { // T, F, T
                        return (false, var1, var2, var3);
                    }
                    else
                    { // T, F, F
                        return (false, var2, var1, var3);
                    }
                }
            }
            else
            {
                if (var2.Value)
                {
                    if (var3.Value)
                    { // F, T, T
                        return (false, var2, var1, var3);
                    }
                    else
                    { // F, T, F
                        return (false, var1, var2, var3);
                    }
                }
                else
                {
                    if (var3.Value)
                    { // F, F, T
                        return (false, var1, var3, var2);
                    }
                    else
                    { // F, F, F
                        return (true, var1, var2, var3);
                    }
                }
            }
        }

        private static (bool, Variable, Variable, Variable) WireToNegateAndOrder(List<int> clause, Variable var1, Variable var2, Variable var3, bool val1, bool val2, bool val3)
        {
            if (var1.Value == val1)
            {
                if (var2.Value == val2)
                {
                    if (var3.Value == val3)
                    { // T, T, T
                        return (true, var1, var2, var3);
                    }
                    else
                    { // T, T, F
                        return (false, var1, var3, var2);
                    }
                }
                else
                {
                    if (var3.Value == val3)
                    { // T, F, T
                        return (false, var1, var2, var3);
                    }
                    else
                    { // T, F, F
                        return (false, var2, var1, var3);
                    }
                }
            }
            else
            {
                if (var2.Value == val2)
                {
                    if (var3.Value == val3)
                    { // F, T, T
                        return (false, var2, var1, var3);
                    }
                    else
                    { // F, T, F
                        return (false, var1, var2, var3);
                    }
                }
                else
                {
                    if (var3.Value == val3)
                    { // F, F, T
                        return (false, var1, var3, var2);
                    }
                    else
                    { // F, F, F
                        return (true, var1, var2, var3);
                    }
                }
            }
        }

        private static Dictionary<string, string> ParseArgs(string[] args)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();
            settings.Add("Type", "Unassigned");
            settings.Add("Input", "Console");
            settings.Add("Output", "Console");
            settings.Add("Format", "LaTeX");
            for (int i = 0; i < args.Length; ++i)
            {
                if (args[i].Equals("-i") || args[i].Equals("--input"))
                {
                    settings["Input"] = "File";
                    settings.Add("Input File", args[i + 1]);
                    ++i;
                }
                else if (args[i].Equals("-o") || args[i].Equals("--output"))
                {
                    settings["Output"] = "File";
                    settings.Add("Output File", args[i + 1]);
                    ++i;
                }
                else if (args[i].Equals("-a") || args[i].Equals("--assigned"))
                {
                    settings["Type"] = "Assigned";
                }
                else if (args[i].Equals("-p") || args[i].Equals("--parsimonious"))
                {
                    settings["Type"] = "Parsimonious";
                }
                else if (args[i].Equals("-s") || args[i].Equals("--svg"))
                {
                    settings["Format"] = "SVG";
                }
                else if (args[i].Equals("-f") || args[i].Equals("--fold"))
                {
                    settings["Format"] = "FOLD";
                }
            }
            return settings;
        }

        private static (int numClauses, int numVars, List<List<int>> clauses, List<Variable> variables) ReadInput(Dictionary<string, string> settings)
        {
            if (settings["Input"].Equals("Console"))
            {
                return ReadInputFromConsole();
            }
            var input = System.IO.File.ReadAllText(settings["Input File"])
                .Split(',')
                .Select(number => int.Parse(number))
                .ToList();
            
            var clauses = new List<List<int>>();
            for (int i = 0; i < input.Count; i+=3)
            {
                clauses.Add(new List<int> { input[i], input[i + 1], input[i + 2] });
            }

            int numClauses = clauses.Count;
            var variables = clauses
                .SelectMany(literals => literals)
                .Select(literal => Math.Abs(literal))
                .Distinct()
                .OrderBy(variable => variable)
                .Select(variable => new Variable(variable, "black"))
                .ToList();
            int numVars = variables.Count;

            return (numClauses, numVars, clauses, variables);
        }

        private static (int numClauses, int numVars, List<List<int>> clauses, List<Variable> variables) ReadInputFromConsole()
        {
            string input;
            int numClauses, numVars;
            do
            {
                Console.WriteLine("How many clauses are there?");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out numClauses));
            do
            {
                Console.WriteLine("How many variables are there?");
                input = Console.ReadLine();
            } while (!int.TryParse(input, out numVars));

            var clauses = new List<List<int>>(numVars);

            int i = 0, j = 0;
            do
            {
                int literal;
                do
                {
                    Console.WriteLine($"In clause {i + 1}, what is variable {j + 1}?");
                    input = Console.ReadLine();
                } while (!int.TryParse(input, out literal) ||
                literal > numVars || literal < -numVars || literal == 0);

                if (j == 0)
                {
                    clauses.Add(new List<int>());
                }
                clauses[i].Add(literal);
                ++j;
                if (j == 3)
                {
                    ++i;
                    j = 0;
                }
            } while (i < numClauses);

            var variables = clauses
                .SelectMany(literals => literals)
                .Select(literal => Math.Abs(literal))
                .Distinct()
                .OrderBy(variable => variable)
                .Select(variable => new Variable(variable, "black"))
                .ToList();

            return (numClauses, numVars, clauses, variables);
        }

        private static void OutputResult(Dictionary<string, string> settings, string output)
        {
            if (settings["Output"].Equals("Console"))
            {
                Console.WriteLine(output);
            }
            else
            {
                System.IO.File.WriteAllText(settings["Output File"], output);
            }
        }
    }
}