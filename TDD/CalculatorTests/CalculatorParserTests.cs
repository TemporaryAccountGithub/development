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
        public void given_validString_when_Validate_then_doesNothig()
        {
            parser.ValidateExpression("1+2");
        }

        [TestMethod]
        public void given_string_when_parse_then_returnLists()
        {
            expression = "1+2";
            expectedNumbers = new List<double>{ 1, 2 };
            expectedOperations = new List<char> { '+' };

            var result = parser.ParseBaseExpression(expression);

            CollectionAssert.AreEqual(result.Item1, expectedNumbers);
            CollectionAssert.AreEqual(result.Item2, expectedOperations);
        }

        [TestMethod]
        public void given_stringWithPower_when_parse_then_returnLists()
        {
            expression = "1^2";
            expectedNumbers = new List<double> { 1, 2 };
            expectedOperations = new List<char> { '^' };

            var result = parser.ParseBaseExpression(expression);

            CollectionAssert.AreEqual(result.Item1, expectedNumbers);
            CollectionAssert.AreEqual(result.Item2, expectedOperations);
        }

        [TestMethod]
        public void given_stringWithSqrt_when_parse_then_returnLists()
        {
            expression = "&5";
            expectedNumbers = new List<double> { 0, 5 };
            expectedOperations = new List<char> { '&' };

            var result = parser.ParseBaseExpression(expression);

            CollectionAssert.AreEqual(result.Item1, expectedNumbers);
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
            expectedNumbers = new List<double> { 2, 0, 5 };
            expectedOperations = new List<char> { '+', '&' };

            var result = parser.ParseBaseExpression(expression);

            CollectionAssert.AreEqual(result.Item1, expectedNumbers);
            CollectionAssert.AreEqual(result.Item2, expectedOperations);
        }

        [TestMethod]
        public void given_bracketsWithNumber_when_isBracketsString_then_returnTrue()
        {
            Assert.IsTrue(parser.IsBracketsExpression("(123)"));
        }

        [TestMethod]
        public void given_numberString_when_isBracketsString_then_returnFalse()
        {
            Assert.IsFalse(parser.IsBracketsExpression("123"));
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
    }
}
