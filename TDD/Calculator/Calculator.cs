﻿namespace CalculatorLibrary
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
        }

        public static void SetCalculatorParser(ICalculatorParser parser)
        {
            calculatorParser = parser;
        }

        public static double Calculate(string expression)
        {
            List<double> numbersToAdd = new List<double>();
            List<string> expressionValues = calculatorParser.ParseExpression(expression);

            numbersToAdd.Add(double.Parse(expressionValues[0]));

            for (int i = 1; i < expressionValues.Count - 1; i += 2)
            {
                {
                    double nextNumber = double.Parse(expressionValues[i + 1]);
                    char operationChar = char.Parse(expressionValues[i]);

                    switch (operationChar)
                    {
                        case CharOperations.Multiply:
                        case CharOperations.Divide:
                        case CharOperations.Power:
                            double firstNum = numbersToAdd.Last();
                            double result = Calculate(firstNum, nextNumber, operationChar);
                            numbersToAdd[numbersToAdd.Count - 1] = result;
                            break;

                        case CharOperations.Substruct:
                            double opposite = Calculate(0, nextNumber, operationChar);
                            numbersToAdd.Add(opposite);
                            break;

                        case CharOperations.Add:
                            numbersToAdd.Add(nextNumber);
                            break;
                    }
                }
            }

            return ListSum(numbersToAdd);
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
            return Math.Pow(firstNum, secondNum);
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

        private static double ListSum(List<double> list)
        {
            double sum = 0;
            
            foreach (double number in list) 
            {
                sum = Calculate(sum, number, CharOperations.Add);
            }

            return sum;
        }
    }
}
