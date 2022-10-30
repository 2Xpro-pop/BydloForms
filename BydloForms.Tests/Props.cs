namespace BydloForms.Tests
{
    internal class Props
    {
        public int IntProp { get; set;  }
        public string StrProp { get; set; }
        public bool BoolProp { get; set; }

        public Props() { }

        public Props(int intProp, string strProp, bool boolProp)
        {
            IntProp = intProp;
            StrProp = strProp;
            BoolProp = boolProp;
        }

        public override bool Equals(object? obj)
        {
            return obj is Props other &&
                   IntProp == other.IntProp &&
                   StrProp == other.StrProp &&
                   BoolProp == other.BoolProp;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IntProp, StrProp, BoolProp);
        }
    }
}