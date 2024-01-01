namespace CalculatorLibrary
{
    public struct CalculationState<T>
    {
        public List<T> Expressions { get; }
        public List<Operator> Operations { get; }

        public CalculationState(List<T> expressions, List<Operator> operations)
        {
            Expressions = expressions;
            Operations = operations;
        }
    }
}
