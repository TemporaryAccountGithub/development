using System.Text.RegularExpressions;

namespace CalculatorLibrary
{
    internal class CalculatorParser
    {
        private const string ValidPattern = @"^(-?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?([-+*/](-?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?))+)$";
        private const string MatchPattern = @"((?<=(\d|\.))[+\-*/])|(-?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?)";

        public static List<string> ParseExpression(string expression)
        {
            expression = expression.Replace("E+", "E");
            ValidateRegexExpression(expression);
            MatchCollection matches = Regex.Matches(expression, MatchPattern);

            return matches.Cast<Match>().Select(m => m.Value).ToList();
        }

        private static void ValidateRegexExpression(string expression)
        {
            if (!Regex.IsMatch(expression, ValidPattern))
            {
                throw new ArgumentException("Invalid expression!");
            }
        }
    }
}
