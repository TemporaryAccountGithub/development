using CalculatorLibrary;
using Moq;


namespace CalculatorTests
{
    [TestClass]
    public class CalculatorParserTests
    {
        ICalculatorParser parser;
        List<char> expectedOperations;
        List<string> expectedExpression;
        string expression;

        [TestInitialize]
        public void Initialize()
        {
            parser = new CalculatorParser();
        }

        [TestMethod]
        public void given_validString_when_Validate_then_doesNothig()
        {
            parser.ValidateExpression("1+2");
        }

        [TestMethod]
        public void given_string_when_parse_then_returnLists()
        {
            expression = "1+2";
            expectedExpression = new List<string>{ "1", "2" };
            expectedOperations = new List<char> { '+' };

            var result = parser.ParseExpression(expression);

            CollectionAssert.AreEqual(result.Item1, expectedExpression);
            CollectionAssert.AreEqual(result.Item2, expectedOperations);
        }

        [TestMethod]
        public void given_stringWithPower_when_parse_then_returnLists()
        {
            expression = "1^2";
            expectedExpression = new List<string> { "1", "2" };
            expectedOperations = new List<char> { '^' };

            var result = parser.ParseExpression(expression);

            CollectionAssert.AreEqual(result.Item1, expectedExpression);
            CollectionAssert.AreEqual(result.Item2, expectedOperations);
        }

        [TestMethod]
        public void given_stringWithSqrt_when_parse_then_returnLists()
        {
            expression = "&5";
            expectedExpression = new List<string> { "0", "5" };
            expectedOperations = new List<char> { '&' };

            var result = parser.ParseExpression(expression);

            CollectionAssert.AreEqual(result.Item1, expectedExpression);
            CollectionAssert.AreEqual(result.Item2, expectedOperations);
        }

        [TestMethod]
        public void given_invalidStringWithSqrt_when_ValidateExpression_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => parser.ValidateExpression("123&123"));
        }

        [TestMethod]
        public void given_longStringWithSqrt_when_parse_then_returnLists()
        {
            expression = "2+&5";
            expectedExpression = new List<string> { "2", "0", "5" };
            expectedOperations = new List<char> { '+', '&' };

            var result = parser.ParseExpression(expression);

            CollectionAssert.AreEqual(result.Item1, expectedExpression);
            CollectionAssert.AreEqual(result.Item2, expectedOperations);
        }

        [TestMethod]
        public void given_bracketsWithNumber_when_validate_then_doNothing()
        {
            parser.ValidateExpression("(123E2)");
        }

        [TestMethod]
        public void given_bracketsWithoutCloser_when_validate_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => parser.ValidateExpression("(123"));
        }

        [TestMethod]
        public void given_bracketsWrongOrder_when_validate_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => parser.ValidateExpression(")123("));
        }

        [TestMethod]
        public void given_bracketsEmpty_when_validate_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => parser.ValidateExpression("()"));
        }

        [TestMethod]
        public void given_bracketsNearOperation_when_validate_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => parser.ValidateExpression("(*12)"));
        }

        [TestMethod]
        public void given_bracketsNestedValid_when_validate_then_doNothing()
        {
            parser.ValidateExpression("(123)+6*(12+(23-4))");
        }

        [TestMethod]
        public void given_bracketsWithoutOperation_when_validate_then_rhrowException()
        {
            Assert.ThrowsException<ArgumentException>(() => parser.ValidateExpression("12(12)"));
        }

        [TestMethod]
        public void given_bracketsString_when_parseBrackets_then_returnResult()
        {
            expression = "(123)";
            expectedExpression = new List<string> { "123" };
            expectedOperations = new List<char>();

            var result = parser.ParseExpression(expression);

            CollectionAssert.AreEqual(expectedExpression, result.Item1);
            CollectionAssert.AreEqual(expectedOperations, result.Item2);
        }

        [TestMethod]
        public void given_bracketsStringWithInnerOperations_when_parseBrackets_then_returnResult()
        {
            expression = "(123+234)";
            expectedExpression = new List<string> { "123+234" };
            expectedOperations = new List<char>();

            var result = parser.ParseExpression(expression);

            CollectionAssert.AreEqual(expectedExpression, result.Item1);
            CollectionAssert.AreEqual(expectedOperations, result.Item2);
        }
    }
}
