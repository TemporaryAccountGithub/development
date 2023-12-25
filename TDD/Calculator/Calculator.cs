namespace CalculatorLibrary
{
    public class Calculator
    {
        public static double Calculate(string expression)
        {
            expression = expression.Replace("E+", "E");
            char[] operations = { '+', '*', '/', '-' };
            int operatorIndex = -1;
            double firstNum, secondNum;

            foreach (char opChar in operations) 
            {
                int startIndex = opChar == '-' ? 1 : 0;
                operatorIndex = expression.IndexOf(opChar, startIndex);
                if (operatorIndex >= 0 )
                {
                    if ((operatorIndex == 0) || !double.TryParse(expression.Substring(0, operatorIndex), out firstNum) || !double.TryParse(expression.Substring(operatorIndex + 1), out secondNum))
                    {
                        throw new ArgumentException("Invalid expression!");
                    }
                    return Calculate(firstNum, secondNum, opChar);
                }
            }

            throw new InvalidOperationException("No valid operator in expression");
        }

        private static double Calculate(double firstNum, double secondNum, char operatorChar)
        {
            double result = 0;

            switch (operatorChar)
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
                    throw new InvalidOperationException("Invalid operator: " + operatorChar);
            }

            return result;
        }

        private static double Add(double firstNum, double secondNum)
        {
            DoubleOverflowSumCheck(firstNum, secondNum);

            return firstNum + secondNum;
        }

        private static double Multiply(double firstNum, double secondNum)
        {
            double product = firstNum * secondNum;
            DoubleInfinityCheck(product);

            return product;
        }

        private static double Substruct(double firstNum, double secondNum)
        {
            DoubleOverflowSumCheck(firstNum, -secondNum);

            return firstNum - secondNum;
        }

        private static double Divide(double firstNum, double secondNum)
        {
            if (secondNum == 0)
            {
                throw new DivideByZeroException("Division by constant zero!");
            }

            double quotient = firstNum / secondNum;
            DoubleInfinityCheck(quotient);

            return quotient;
        }

        private static void DoubleOverflowSumCheck(double firstNum, double secondNum)
        {
            if ((firstNum > 0) && (secondNum > double.MaxValue - firstNum))
            {
                throw new OverflowException("Sum or substrcution causes double overflow!");
            }

            if ((firstNum < 0) && (secondNum < double.MinValue - firstNum))
            {
                throw new OverflowException("Sum or substrcution causes double underflow!");
            }
        }

        private static void DoubleInfinityCheck(double value)
        {
            if (double.IsInfinity(value))
            {
                throw new OverflowException("Product or division causes double overflow!");
            }
        }
    }
}
