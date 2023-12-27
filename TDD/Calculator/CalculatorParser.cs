using System.Text.RegularExpressions;

namespace CalculatorLibrary
{
    public class CalculatorParser : ICalculatorParser
    {
        private const string ValidPattern = @"^(-?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?([-+*/^](-?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?))+)$";
        private const string MatchPattern = @"((?<=(\d|\.))[+\-*/^])|(-?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?)";

        public (List<double>, List<char>) ParseExpression(string expression)
        {
            List<double> numbers = new List<double>();
            List<char> operations = new List<char>();

            expression = expression.Replace("E+", "E");
            ValidateRegexExpression(expression);
            MatchCollection matches = Regex.Matches(expression, MatchPattern);

            for (int i = 0; i < matches.Count; i++)
            {
                if (i % 2 == 1)
                {
                    operations.Add(char.Parse(matches[i].Value));
                }
                else
                {
                    numbers.Add(double.Parse(matches[i].Value));
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
    }
}
