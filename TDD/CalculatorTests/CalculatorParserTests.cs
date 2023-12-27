using CalculatorLibrary;
using Moq;


namespace CalculatorTests
{
    [TestClass]
    public class CalculatorParserTests
    {
        ICalculatorParser parser;
        List<double> expectedNumbers;
        List<char> expectedOperations;
        string expression;

        [TestInitialize]
        public void Initialize()
        {
            parser = new CalculatorParser();
        }

        [TestMethod]
        public void given_string_when_parse_then_returnLists()
        {
            expression = "1+2";
            expectedNumbers = new List<double>{ 1, 2 };
            expectedOperations = new List<char> { '+' };

            var result = parser.ParseExpression(expression);

            CollectionAssert.AreEqual(result.Item1, expectedNumbers);
            CollectionAssert.AreEqual(result.Item2, expectedOperations);
        }
    }
}
