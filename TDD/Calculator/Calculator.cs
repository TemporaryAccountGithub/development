namespace CalculatorLibrary
{
    public class Calculator
    {
        public static double Calculate(double firstNum, double secondNum, char op)
        {
            double result = 0;
            switch (op) 
            {
                case '+':
                    result = Add(firstNum, secondNum);
                    break;
                case '*':
                    result = Multiply(firstNum, secondNum);
                    break;
            }

            return result;
        }

        private static double Add (double firstNum, double secondNum)
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

        private static double Multiply (double firstNum, double secondNum)
        {
            if (double.IsInfinity(firstNum * secondNum))
            {
                throw new OverflowException("Product causes double overflow!");
            }

            return firstNum * secondNum;
        }
    }
}
