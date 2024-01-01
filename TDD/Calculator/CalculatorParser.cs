using System.Text.RegularExpressions;

namespace CalculatorLibrary
{
    public class CalculatorParser : ICalculatorParser
    {
        private const string ValidPattern = @"^((&?\()*[-&]?(\d+(\.\d*)?|(\.\d+))([Ee][+]?\d+)?(\))*([-+*/^]((&?\()*[-&]?(\d+(\.\d*)?|(\.\d+))([Ee][+]?\d+)?(\))*))*)$";
        private const string MatchPattern = @"&?\(((?>[^()]+|\((?<DEPTH>)|\)(?<-DEPTH>))*)(?(DEPTH)(?!))\)|((?<=(\d|\.|\)))[+\-*/^])|([-&]?(\d+(\.\d*)?|(\.\d+))([Ee]\d+)?)";
        private const char UnaryOperation = '&';

        public void ValidateExpression(string expression)
        {
            ValidateRegexExpression(expression);
            ValidateBrackets(expression);
        }

        public CalculationState<string> ParseExpression(string expression)
        {
            List<string> expressions = new List<string>();
            List<Operator> operations = new List<Operator>();

            expression = expression.Replace("E+", "E");
            MatchCollection matches = Regex.Matches(expression, MatchPattern);

            for (int i = 0; i < matches.Count; i++)
            {
                string current = matches[i].Value;

                if (i % 2 == 1)
                {
                    operations.Add(new Operator(char.Parse(current)));
                }
                else
                {
                    if (StartWithUnaryOperation(current))
                    {
                        operations.Add(new Operator(current[0]));
                        current = current.Substring(1);
                    }
                    else if (BracketsExpression(current))
                    {
                        current = current.Substring(1, current.Length - 2);
                    }

                    expressions.Add(current);
                }
            }

            return new CalculationState<string>(expressions, operations);
        }

        private void ValidateRegexExpression(string expression)
        {
            if (!Regex.IsMatch(expression, ValidPattern))
            {
                ValidateFail();
            }
        }

        private void ValidateBrackets(string expression)
        {
            int openBrackets = 0;
            foreach (char c in expression)
            {
                if (c == '(')
                {
                    openBrackets++;
                }
                else
                {
                    if (c == ')')
                    {
                        openBrackets--;
                        if (openBrackets < 0)
                        {
                            ValidateFail();
                        }
                    }
                }
            }

            if (openBrackets != 0)
            {
                ValidateFail();
            }
        }

        private bool StartWithUnaryOperation(string expression)
        {
            return expression.StartsWith(UnaryOperation);
        }

        private bool BracketsExpression(string expression)
        {
            return expression.StartsWith("(") && expression.EndsWith(")");
        }

        private void ValidateFail()
        {
            throw new ArgumentException("Invalid expression!");
        }
    }
}
