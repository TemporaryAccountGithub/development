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
            OperatorPriority priority = OperatorPriority.LowPriority;

            switch (OperatorSymbol) 
            {
                case CharOperator.Add:
                case CharOperator.Substruct:
                    priority = OperatorPriority.LowPriority;
                    break;

                case CharOperator.Multiply:
                case CharOperator.Divide:
                    priority = OperatorPriority.HighPriority;
                    break;

                case CharOperator.Root:
                case CharOperator.Power:
                    priority = OperatorPriority.TopPriority;
                    break;
            }

            return priority;
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
