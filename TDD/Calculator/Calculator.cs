namespace CalculatorLibrary
{
    public class Calculator
    {
        private static ICalculatorParser calculatorParser = new CalculatorParser();

        public static void SetCalculatorParser(ICalculatorParser parser)
        {
            calculatorParser = parser;
        }

        public static double Calculate(string expression)
        {
            calculatorParser.ValidateExpression(expression);
            return CalculateRecursive(expression);
        }

        public static double Calculate(double firstNum, double secondNum, Operator currentOperator)
        {
            double result = 0;

            switch (currentOperator.OperatorSymbol)
            {
                case CharOperator.Add:
                    result = Add(firstNum, secondNum);
                    break;

                case CharOperator.Multiply:
                    result = Multiply(firstNum, secondNum);
                    break;

                case CharOperator.Substruct:
                    result = Substruct(firstNum, secondNum);
                    break;

                case CharOperator.Divide:
                    result = Divide(firstNum, secondNum);
                    break;

                case CharOperator.Power:
                    result = Power(firstNum, secondNum);
                    break;

                default:
                    throw new ArgumentException("Invalid operator: " + currentOperator.OperatorSymbol);
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
            foreach (OperatorPriority priority in Enum.GetValues(typeof(OperatorPriority)))
            {
                state = PerformPriorityCalculation(priority, state);
            }

            return state.Expressions.First();
        }

        private static CalculationState<double> PerformPriorityCalculation(OperatorPriority priority, CalculationState<double> state)
        {
            List<double> newNumbers = new List<double>();
            List<Operator> newOperations = new List<Operator>();
            newNumbers.Add(state.Expressions.First());
            int nextNumberIndex = 1;

            for (int i = 0; i < state.Operations.Count; i++)
            {
                Operator currentOperation = state.Operations[i];

                if (currentOperation.GetPriority() == priority)
                {
                    double result;

                    if (currentOperation.IsUnaryOperation())
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

                    if (!currentOperation.IsUnaryOperation())
                    {
                        newNumbers.Add(state.Expressions[nextNumberIndex++]);
                    }
                }
            }

            return new CalculationState<double>(newNumbers, newOperations);
        }

        private static double HandleUnaryOperation(double number, Operator operatorChar)
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
    }
}
