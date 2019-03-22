using System;

namespace Converter
{
    public class Variable
    {
        public int PrevX { get; private set; }
        public int Index { get; }
        public bool Value { get; private set; }
        public bool Up { get; private set; }
        
        public Variable(int index, bool up)
        {
            this.PrevX = 0;
            this.Index = index;
            this.Value = true;
            this.Up = up;
        }

        internal void Negate()
        {
            this.Value = !this.Value;
        }

        internal void UpdatePrevX(int newPrevX)
        {
            this.PrevX = newPrevX;
        }

        internal void Flip()
        {
            this.Up = !this.Up;
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
