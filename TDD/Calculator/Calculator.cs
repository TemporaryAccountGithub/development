namespace CalculatorLibrary
{
    public class Calculator
    {
        private static class CharOperations
        {
            public const char Add = '+';
            public const char Substruct = '-';
            public const char Multiply = '*';
            public const char Divide = '/';
        }
        
        private static readonly char[] VALID_OPERATIONS = { CharOperations.Add, CharOperations.Multiply, CharOperations.Divide, CharOperations.Substruct };

        public static double Calculate(string expression)
        {
            if (expression == "")
            {
                throw new ArgumentException("Empty String!");
            }

            expression = expression.Replace("E+", "E");
            int operatorIndex = -1;
            double firstNum, secondNum;
            string firstNumString, secondNumString;

            foreach (char operatorChar in VALID_OPERATIONS)
            {
                int startIndex = operatorChar == CharOperations.Substruct ? 1 : 0;
                operatorIndex = expression.IndexOf(operatorChar, startIndex);
                if (operatorIndex >= 0)
                {
                    firstNumString = expression.Substring(0, operatorIndex);
                    secondNumString = expression.Substring(operatorIndex + 1);

                    if ((operatorIndex == 0) || !double.TryParse(firstNumString, out firstNum) || !double.TryParse(secondNumString, out secondNum))
                    {
                        throw new ArgumentException("Invalid expression!");
                    }

                    if (firstNumString[0] == CharOperations.Add || secondNumString[0] == CharOperations.Add)
                    {
                        throw new ArgumentException("Requirements not allow +num as number!");
                    }

                    return Calculate(firstNum, secondNum, operatorChar);
                }
            }

            throw new InvalidOperationException("No valid operator in expression");
        }

        public static double Calculate(double firstNum, double secondNum, char operatorChar)
        {
            double result = 0;

            switch (operatorChar)
            {
                case CharOperations.Add:
                    result = Add(firstNum, secondNum);
                    break;
                case CharOperations.Multiply:
                    result = Multiply(firstNum, secondNum);
                    break;
                case CharOperations.Substruct:
                    result = Substruct(firstNum, secondNum);
                    break;
                case CharOperations.Divide:
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
