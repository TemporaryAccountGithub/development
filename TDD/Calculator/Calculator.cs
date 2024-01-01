using static System.Net.Mime.MediaTypeNames;

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
            calculatorParser.ValidateExpression(expression);
            return CalculateRecursive(expression);
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

        private static double CalculateRecursive(string expression)
        {
            List<double> numbers = new List<double>();
            CalculationState<string> state;
            double number;

            state = calculatorParser.ParseExpression(expression);

            foreach (string rawExpression in state.Expressions)
            {
                if (!double.TryParse(rawExpression, out number))
                {
                    number = CalculateRecursive(rawExpression);
                }

                numbers.Add(number);
            }

            return CalculateFromLists(new CalculationState<double>(numbers, state.Operations));
        }

        private static double CalculateFromLists(CalculationState<double> state)
        {
            List<Func<char, bool>> priorities = new List<Func<char, bool>> { IsTopPriority, IsHighPriority, IsLastPriority };

            foreach (var priority in priorities)
            {
                state = PerformPriorityCalculation(priority, state);
            }

            return state.Expressions.First();
        }

        private static CalculationState<double> PerformPriorityCalculation(Func<char, bool> isPriority, CalculationState<double> state)
        {
            List<double> newNumbers = new List<double>();
            List<char> newOperations = new List<char>();
            newNumbers.Add(state.Expressions.First());
            int nextNumberIndex = 1;

            for (int i = 0; i < state.Operations.Count; i++)
            {
                char currentOperation = state.Operations[i];

                if (isPriority(currentOperation))
                {
                    double result;

                    if (IsUnaryOperation(currentOperation))
                    {
                        result = HandleUnaryOperation(newNumbers.Last(), currentOperation);
                        newNumbers[newNumbers.Count - 1] = result;
                    }
                    else 
                    {
                        double firstNum = newNumbers.Last();
                        double nextNumber = state.Expressions[nextNumberIndex++];
                        result = Calculate(firstNum, nextNumber, currentOperation);
                    }

                    newNumbers[newNumbers.Count - 1] = result;
                }
                else
                {
                    newOperations.Add(currentOperation);
                    
                    if (!IsUnaryOperation(currentOperation))
                    {
                        newNumbers.Add(state.Expressions[nextNumberIndex++]);
                    }
                }
            }

            return new CalculationState<double>(newNumbers, newOperations);
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
    }
}
