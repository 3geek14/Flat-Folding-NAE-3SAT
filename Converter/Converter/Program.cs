using System;
using System.Collections.Generic;
using System.Linq;

namespace Converter
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = ParseArgs(args ?? new string[0]);
            foreach (var key in settings.Keys)
            {
                Console.WriteLine($"Key:   {key}\nValue: {settings[key]}\n------");
            }
            (int numClausees, int numVars, List<List<int>> clauses, List<Variable> variables) = ReadInput(settings);
            string output = GenerateCreasePattern(settings, clauses, variables);
            OutputResult(settings, output);
        }

        private static string GenerateCreasePattern(Dictionary<string, object> settings, List<List<int>> clauses, List<Variable> variables)
        {
            Origami.Format format = (Origami.Format)settings["Format"];
            Origami.Type type = (Origami.Type)settings["Type"];
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
                output += Origami.ClauseTop(format, type, sweepLine, variables);

                sweepLine += Origami.ClauseGap1(type);
                output += Origami.SendWireToClause(format, type, var1, 1, sweepLine, paperHeight, variables);
                sweepLine += Origami.ClauseGap2(type);
                output += Origami.SendWireToClause(format, type, var2, 2, sweepLine, paperHeight, variables);
                sweepLine += Origami.ClauseGap3(type);
                output += Origami.SendWireToClause(format, type, var3, 3, sweepLine, paperHeight, variables);

                sweepLine += Origami.ClauseRightPadding(type);
            }

            foreach (var variable in variables)
            {
                output += Origami.EndWire(format, type, variable, sweepLine);
            }

            output += Origami.EndOfFile(format);

            return output;
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

        private static Dictionary<string, object> ParseArgs(string[] args)
        {
            var settings = new Dictionary<string, object>();
            settings.Add("Type", Origami.Type.Unassigned);
            settings.Add("Input", "Console");
            settings.Add("Output", "Console");
            settings.Add("Format", Origami.Format.LaTeX);
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
                    settings["Type"] = Origami.Type.Assigned;
                }
                else if (args[i].Equals("-p") || args[i].Equals("--parsimonious"))
                {
                    settings["Type"] = Origami.Type.Parsimonious;
                }
                else if (args[i].Equals("-u") || args[i].Equals("--unassigned"))
                {
                    settings["Type"] = Origami.Type.Unassigned;
                }
                else if (args[i].Equals("-l") || args[i].Equals("--latex"))
                {
                    settings["Format"] = Origami.Format.LaTeX;
                }
                else if (args[i].Equals("-s") || args[i].Equals("--svg"))
                {
                    settings["Format"] = Origami.Format.SVG;
                }
                else if (args[i].Equals("-f") || args[i].Equals("--fold"))
                {
                    settings["Format"] = Origami.Format.FOLD;
                }
            }
            return settings;
        }

        private static (int numClauses, int numVars, List<List<int>> clauses, List<Variable> variables) ReadInput(Dictionary<string, object> settings)
        {
            if (((string)settings["Input"]).Equals("Console"))
            {
                return ReadInputFromConsole();
            }
            var input = System.IO.File.ReadAllText((string)settings["Input File"])
                .Split(',')
                .Select(number => int.Parse(number))
                .ToList();

            var clauses = new List<List<int>>();
            for (int i = 0; i < input.Count; i += 3)
            {
                clauses.Add(new List<int> { input[i], input[i + 1], input[i + 2] });
            }

            int numClauses = clauses.Count;
            var variables = clauses
                .SelectMany(literals => literals)
                .Select(literal => Math.Abs(literal))
                .Distinct()
                .OrderBy(variable => variable)
                .Select((variable, i) => new Variable(variable, i % 2 == 0))
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
                .Select((variable, k) => new Variable(variable, k % 2 == 0))
                .ToList();

            return (numClauses, numVars, clauses, variables);
        }

        private static void OutputResult(Dictionary<string, object> settings, string output)
        {
            if (((string)settings["Output"]).Equals("Console"))
            {
                Console.WriteLine(output);
            }
            else
            {
                System.IO.File.WriteAllText((string)settings["Output File"], output);
            }
        }
    }
}