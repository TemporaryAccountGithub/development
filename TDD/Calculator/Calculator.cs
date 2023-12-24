namespace CalculatorLibrary
{
    public class Calculator
    {
        public static double Calculate(double firstNum, double secondNum, char op)
        {
            if (firstNum > 0 && secondNum > double.MaxValue - firstNum)
            {
                throw new OverflowException("Sum causes double overflow!");
            }

            return firstNum + secondNum;
        }
    }
}
