using System.Text.RegularExpressions;

namespace CalculatorLibrary
{
    public class CalculatorParser : ICalculatorParser
    {
        private const string ValidPattern = @"^([-&]?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?([-+*/^]([-&]?&?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?))*)$";
        private const string MatchPattern = @"((?<=(\d|\.))[+\-*/^])|([-&]?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?)";
        private const char UnaryOperation = '&';

        public void ValidateExpression(string expression)
        {

        }

        public bool IsBracketsExpression(string expression) 
        {
            return true;
        }

        public List<string> ParseBracketsExpression(string expression) 
        {
            return new List<string>();
        }

        public (List<double>, List<char>) ParseBaseExpression(string expression)
        {
            List<double> numbers = new List<double>();
            List<char> operations = new List<char>();

            expression = expression.Replace("E+", "E");
            ValidateRegexExpression(expression);
            MatchCollection matches = Regex.Matches(expression, MatchPattern);

            for (int i = 0; i < matches.Count; i++)
            {
                string current = matches[i].Value;

                if (i % 2 == 1)
                {
                    operations.Add(char.Parse(current));
                }
                else
                {
                    if (StartWithUnaryOperation(current))
                    {
                        numbers.Add(0);
                        operations.Add(current[0]);
                        current = current.Substring(1);
                    }
                    
                    numbers.Add(double.Parse(current));
                }
            }

            return (numbers, operations);
        }

        private void ValidateRegexExpression(string expression)
        {
            if (!Regex.IsMatch(expression, ValidPattern))
            {
                throw new ArgumentException("Invalid expression!");
            }
        }

        private bool StartWithUnaryOperation(string number)
        {
            return number[0] == UnaryOperation;
        }
    }
}
