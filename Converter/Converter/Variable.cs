using System;

namespace Converter
{
    public class Variable
    {
        public int PrevX { get; private set; }
        public int WireHeight { get; }
        public int Index { get; }
        public bool Value { get; private set; }
        public string Color { get; }

        public Variable(int index, string color)
        {
            this.PrevX = 0;
            this.WireHeight = 4 + 5 * index;
            this.Index = index;
            this.Value = true;
            this.Color = color;
        }

        internal void Negate()
        {
            this.Value = !this.Value;
        }

        internal void UpdatePrevX(int newPrevX)
        {
            this.PrevX = newPrevX;
        }
    }

    public class PreVariable
    {
        public Variable Variable { get; }
        public bool Used { get; private set; }

        public PreVariable(Variable variable)
        {
            this.Variable = variable;
            this.Used = false;
        }

        public void Activate()
        {
            this.Used = true;
        }
    }
}
