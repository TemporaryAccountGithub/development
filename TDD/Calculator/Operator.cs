namespace CalculatorLibrary
{
    public enum CharOperator
    {
        Add = '+',
        Substruct = '-',
        Multiply = '*',
        Divide = '/',
        Root = '&',
        Power = '^'
    }

    internal enum OperatorPriority
    {
        TopPriority,
        HighPriority,
        LowPriority
    }

    public class Operator
    {
        public CharOperator OperatorSymbol;

        private static Dictionary<CharOperator, OperatorPriority> operatorPriority = new Dictionary<CharOperator, OperatorPriority>
        {
            { CharOperator.Add, OperatorPriority.LowPriority },
            { CharOperator.Substruct, OperatorPriority.LowPriority },
            { CharOperator.Multiply, OperatorPriority.HighPriority },
            { CharOperator.Divide, OperatorPriority.HighPriority },
            { CharOperator.Power, OperatorPriority.TopPriority },
            { CharOperator.Root, OperatorPriority.TopPriority }
        };

        public Operator(CharOperator charOperator)
        {
            OperatorSymbol = charOperator;
        }  

        public Operator(char charOperator) 
        {
            if (!Enum.IsDefined(typeof(CharOperator), (int)charOperator))
            {
                throw new ArgumentException("Invalid operator!");
            }

            OperatorSymbol = (CharOperator)charOperator;
        }

        internal OperatorPriority GetPriority()
        {
            return operatorPriority[OperatorSymbol];
        }

        internal bool IsUnaryOperation()
        {
            return OperatorSymbol == CharOperator.Root;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return OperatorSymbol == ((Operator)obj).OperatorSymbol;
        }
    }
}
