using System.Text.RegularExpressions;
using System.Collections.Generic;

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

            List<double> numbers = new List<double>();
            List<char> operations = new List<char>();
            const string ValidPattern = @"^(-?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?([-+*/](-?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?))*)$";
            const string MatchPattern = @"((?<=(\d|\.))[+\-*/])|(-?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?)";

            if (Regex.IsMatch(expression, ValidPattern))//change to not -> throw
            {
                MatchCollection matches = Regex.Matches(expression, MatchPattern);
                numbers.Add(double.Parse(matches[0].Value));

                for (int i = 1; i < matches.Count; i += 2)//seprate to functions and think about var locations
                {
                    {
                        operations.Add(matches[i].Value[0]);
                        numbers.Add(double.Parse(matches[i + 1].Value));
                    }
                }

                List<double> numbersLowPriority = new List<double>();
                numbersLowPriority.Add(numbers.First());

                for (int i = 0; i < operations.Count; i++)//do this in prev for?
                {
                    if (IsHighPriorityOperation(operations[i]))
                    {
                        double result = Calculate(numbersLowPriority.Last(), numbers[i + 1], operations[i]);
                        numbersLowPriority[numbersLowPriority.Count - 1] = (result);
                    }
                    else
                    {
                        if (operations[i] == CharOperations.Add)
                        {
                            numbersLowPriority.Add(numbers[i + 1]);
                        }
                        else //if substruct?
                        {
                            double opposite = Calculate(0, numbers[i+1], CharOperations.Substruct);
                            numbersLowPriority.Add(opposite);
                        }
                    }
                }

                return ListSum(numbersLowPriority);
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

        private static bool IsHighPriorityOperation(char operationChar) 
        {
            return operationChar == CharOperations.Multiply || operationChar == CharOperations.Divide;
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
