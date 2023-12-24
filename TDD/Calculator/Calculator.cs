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
                case '-':
                    result = Substruct(firstNum, secondNum);
                    break;
                case '/':
                    result = Divide(firstNum, secondNum);
                    break;
                default:
                    throw new InvalidOperationException("Invalid operator: " + op);
            }

            return result;
        }

        private static double Add (double firstNum, double secondNum)
        {
            DoubleOverflowSumCheck(firstNum, secondNum);

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

        private static double Substruct (double firstNum, double secondNum)
        {
            DoubleOverflowSumCheck(firstNum, -secondNum);

            return firstNum - secondNum;
        }

        private static void DoubleOverflowSumCheck(double firstNum, double secondNum)
        {
            if (firstNum > 0 && secondNum > double.MaxValue - firstNum)
            {
                throw new OverflowException("Sum or substrcution causes double overflow!");
            }

            if (firstNum < 0 && secondNum < double.MinValue - firstNum)
            {
                throw new OverflowException("Sum or substrcution causes double underflow!");
            }
        }

        private static double Divide(double firstNum, double secondNum)
        {
            if (secondNum == 0)
            {
                throw new DivideByZeroException("Division by constant zero!");
            }

            return firstNum / secondNum;
        }
    }
}
