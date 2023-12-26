using System.Text.RegularExpressions;

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
       
        public static double Calculate(string expression)
        {
            if (expression == "")
            {
                throw new ArgumentException("Empty String!");
            }

            expression = expression.Replace("E+", "E");

            const string ValidPattern = @"^(-?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?([-+*/](-?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?))*)$";
            if (Regex.IsMatch(expression, ValidPattern))
            {
                return 0;
            }
            else
            {
                throw new ArgumentException("Invalid expression!");
            }
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
