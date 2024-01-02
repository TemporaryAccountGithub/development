namespace CalculatorLibrary
{
    public interface ICalculatorParser
    {
        void ValidateExpression (string expression);
        CalculationState<string> ParseExpression(string expression);
    }
}
