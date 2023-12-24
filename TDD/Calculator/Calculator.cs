namespace CalculatorLibrary
{
    public class Calculator
    {
        public static double Calculate(double firstNum, double secondNum, char op)
        {
            if (op == '+')
            {
                if (firstNum > 0 && secondNum > double.MaxValue - firstNum)
                {
                    throw new OverflowException("Sum causes double overflow!");
                }

                if (firstNum < 0 && secondNum < double.MinValue - firstNum)
                {
                    throw new OverflowException("Sum causes double underflow!");
                }
                return firstNum + secondNum;
            }
            else
            {
                return firstNum * secondNum;
            }
            
        }
    }
}
