namespace CalculatorLibrary
{
    public class Calculator
    {
        private static ICalculatorParser calculatorParser = new CalculatorParser();
        private static class CharOperations
        {
            public const char Add = '+';
            public const char Substruct = '-';
            public const char Multiply = '*';
            public const char Divide = '/';
            public const char Power = '^';
            public const char Root = '&';
        }

        public static void SetCalculatorParser(ICalculatorParser parser)
        {
            calculatorParser = parser;
        }

        public static double Calculate(string expression)
        {
            List<Func<char, bool>> priorities = new List<Func<char, bool>> { IsTopPriority, IsHighPriority, IsLastPriority };
            List<string> numbersStrings;
            List<char> operations;
            calculatorParser.ValidateExpression(expression);
            (numbersStrings, operations) = calculatorParser.ParseExpression(expression);
            List<double> numbers = numbersStrings.Select(double.Parse).ToList();

            foreach (var priority in priorities)
            {
                (numbers, operations) = PerformPriorityCalculation(priority, numbers, operations);
            }

            return numbers.First();
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

                case CharOperations.Power:
                    result = Power(firstNum, secondNum);
                    break;

                default:
                    throw new ArgumentException("Invalid operator: " + operatorChar);
            }

            return result;
        }

        private static double InternalCalculate(double firstNum, double secondNum, char operatorChar)
        {
            if (IsUnaryOperation(operatorChar)) 
            {
                return HandleUnaryOperation(secondNum, operatorChar);
            }
            else 
            {
                return Calculate(firstNum, secondNum, operatorChar);
            }
        }

        private static double HandleUnaryOperation(double number, char operatorChar) 
        {
            return Root(number);
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

        private static double Power(double firstNum, double secondNum) 
        {
            double result = Math.Pow(firstNum, secondNum);
            DoubleInfinityCheck(result);

            return result;
        }

        private static double Root(double number)
        {
            double result = Math.Sqrt(number);
            if (result == double.NaN)
            {
                throw new ArgumentException("Cannot root on negative numbers");
            }

            return result;
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

        private static bool IsTopPriority(char operationChar)
        {
            return operationChar == CharOperations.Power || operationChar == CharOperations.Root;
        }

        private static bool IsHighPriority(char operationChar)
        {
            return operationChar == CharOperations.Multiply || operationChar == CharOperations.Divide;
        }

        private static bool IsLastPriority(char operationChar)
        {
            return operationChar == CharOperations.Add || operationChar == CharOperations.Substruct;
        }

        private static bool IsUnaryOperation(char operationChar) 
        {
            return operationChar == CharOperations.Root;
        }

        private static (List<double>, List<char>) PerformPriorityCalculation(Func<char, bool> isPriority, List<double> numbers, List<char> operations)
        {
            List<double> newNumbers = new List<double>();
            List<char> newOpeations = new List<char>();
            newNumbers.Add(numbers.First());

            for (int i = 0; i < operations.Count; i++)
            {
                double next = numbers[i + 1];
                char currentOperation = operations[i];

                if (isPriority(currentOperation))
                {
                    double firstNum = newNumbers.Last();
                    double result = InternalCalculate(firstNum, next, currentOperation);
                    newNumbers[newNumbers.Count - 1] = result;
                }
                else
                {
                    newNumbers.Add(next);
                    newOpeations.Add(currentOperation);
                }
            }

            return (newNumbers, newOpeations);
        }
    }
}
