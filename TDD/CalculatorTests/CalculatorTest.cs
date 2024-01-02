using CalculatorLibrary;
using Moq;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorTest
    {
        private Calculator calculator;

        [TestInitialize]
        public void Initialize() 
        {
            calculator = new Calculator();
        }

        [TestMethod]
        public void given_zeros_when_add_then_returnZero()
        {
            Assert.AreEqual(calculator.Calculate(0, 0, new Operator('+')), 0);
        }

        [TestMethod]
        public void given_positiveNumbers_when_add_then_returnSum()
        {
            Assert.AreEqual(calculator.Calculate(1, 2, new Operator('+')), 3);
        }

        [TestMethod]
        public void given_negativeNumbers_when_add_then_returnSum()
        {
            Assert.AreEqual(calculator.Calculate(-1, -2, new Operator('+')), -3);
        }

        [TestMethod]
        public void given_negativeAndPositiveNumbers_when_add_then_returnSum()
        {
            Assert.AreEqual(calculator.Calculate(-1, 2.5, new Operator('+')), 1.5);
        }

        [TestMethod]
        public void given_numbersOverflow_when_add_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(double.MaxValue, 0.1, new Operator('+')));
        }

        [TestMethod]
        public void given_numbersUnderflow_when_add_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(double.MinValue, -0.1, new Operator('+')));
        }

        [TestMethod]
        public void given_ones_when_multiply_then_returnOne()
        {
            Assert.AreEqual(calculator.Calculate(1, 1, new Operator('*')), 1);
        }

        [TestMethod]
        public void given_positiveNumbers_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(calculator.Calculate(2, 3, new Operator('*')), 6);
        }

        [TestMethod]
        public void given_negativeNumbers_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(calculator.Calculate(-2.5, -3, new Operator('*')), 7.5);
        }

        [TestMethod]
        public void given_negativeAndPositiveNumbers_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(calculator.Calculate(-2, 5, new Operator('*')), -10);
        }

        [TestMethod]
        public void given_numbersOverflow_when_multiply_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(double.MaxValue, 1.1, new Operator('*')));
        }

        [TestMethod]
        public void given_numbersUnderflow_when_multiply_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(double.MinValue, 1.1, new Operator('*')));
        }

        [TestMethod]
        public void given_zeros_when_substruct_then_returnZero()
        {
            Assert.AreEqual(calculator.Calculate(0, 0, new Operator('-')), 0);
        }

        [TestMethod]
        public void given_positiveNumbers_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(calculator.Calculate(2, 1, new Operator('-')), 1);
        }

        [TestMethod]
        public void given_negativeNumbers_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(calculator.Calculate(-2, -1, new Operator('-')), -1);
        }

        [TestMethod]
        public void given_positiveAndNegativeNumbers_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(calculator.Calculate(2, -1, new Operator('-')), 3);
        }

        [TestMethod]
        public void given_numbersOverflow_when_substruct_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(double.MaxValue, -0.1, new Operator('-')));
        }

        [TestMethod]
        public void given_numbersUnderflow_when_substruct_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(double.MinValue, 0.1, new Operator('-')));
        }

        [TestMethod]
        public void given_ones_when_division_then_returnOne()
        {
            Assert.AreEqual(calculator.Calculate(1, 1, new Operator('/')), 1);
        }

        [TestMethod]
        public void given_positiveNumbers_when_division_then_returnQuotient()
        {
            Assert.AreEqual(calculator.Calculate(3, 2, new Operator('/')), 1.5);
        }

        [TestMethod]
        public void given_negativeNumbers_when_division_then_returnQuotient()
        {
            Assert.AreEqual(calculator.Calculate(-9, -10, new Operator('/')), 0.9);
        }

        [TestMethod]
        public void given_positiveAndNegativeNumbers_when_division_then_returnQuotient()
        {
            Assert.AreEqual(calculator.Calculate(-6, 8, new Operator('/')), -0.75);
        }

        [TestMethod]
        public void given_numbers_when_dividingByZero_then_throwException()
        {
            Assert.ThrowsException<DivideByZeroException>(() => calculator.Calculate(1, 0, new Operator('/')));
        }

        [TestMethod]
        public void given_numbersOverflow_when_dividing_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(double.MaxValue, 0.9, new Operator('/')));
        }

        [TestMethod]
        public void given_numbersUnderflow_when_dividing_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(double.MinValue, 0.9, new Operator('/')));
        }

        [TestMethod]
        public void given_invalidOperator_when_Calculating_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => calculator.Calculate(0, 0, new Operator('$')));
        }

        [TestMethod]
        public void given_zerosString_when_add_then_returnZero()
        {
            Assert.AreEqual(calculator.Calculate("0+0"), 0);
        }

        [TestMethod]
        public void given_positiveNumbersString_when_add_then_returnSum()
        {
            Assert.AreEqual(calculator.Calculate("1+2"), 3);
        }

        [TestMethod]
        public void given_negativeNumbersString_when_add_then_returnSum()
        {
            Assert.AreEqual(calculator.Calculate("-1+-2"), -3);
        }

        [TestMethod]
        public void given_negativeAndPositiveNumbersString_when_add_then_returnSum()
        {
            Assert.AreEqual(calculator.Calculate("-1+2.5"), 1.5);
        }

        [TestMethod]
        public void given_numbersOverflowString_when_add_then_throwException()
        {
            string overflowExpression = double.MaxValue.ToString("R") + "+0.1";
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(overflowExpression));
        }

        [TestMethod]
        public void given_numbersUnderflowString_when_add_then_throwException()
        {
            string underflowExpression = double.MinValue.ToString("R") + "+-0.1";
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(underflowExpression));
        }

        [TestMethod]
        public void given_onesString_when_multiply_then_returnOne()
        {
            Assert.AreEqual(calculator.Calculate("1*1"), 1);
        }

        [TestMethod]
        public void given_positiveNumbersString_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(calculator.Calculate("2*3"), 6);
        }

        [TestMethod]
        public void given_negativeNumbersString_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(calculator.Calculate("-2.5*-3"), 7.5);
        }

        [TestMethod]
        public void given_negativeAndPositiveNumbersString_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(calculator.Calculate("-2*5"), -10);
        }

        [TestMethod]
        public void given_numbersOverflowString_when_multiply_then_throwException()
        {
            string overflowExpression = double.MaxValue.ToString("R") + "*1.1";
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(overflowExpression));
        }

        [TestMethod]
        public void given_numbersUnderflowString_when_multiply_then_throwException()
        {
            string underflowExpression = double.MinValue.ToString("R") + "*1.1";
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(underflowExpression));
        }

        [TestMethod]
        public void given_zerosString_when_substruct_then_returnZero()
        {
            Assert.AreEqual(calculator.Calculate("0-0"), 0);
        }

        [TestMethod]
        public void given_positiveNumbersString_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(calculator.Calculate("2-1"), 1);
        }

        [TestMethod]
        public void given_negativeNumbersString_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(calculator.Calculate("-2--1"), -1);
        }

        [TestMethod]
        public void given_positiveAndNegativeNumbersString_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(calculator.Calculate("2--1"), 3);
        }

        [TestMethod]
        public void given_numbersOverflowString_when_substruct_then_throwException()
        {
            string overflowExpression = double.MaxValue.ToString("R") + "--0.1";
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(overflowExpression));
        }

        [TestMethod]
        public void given_numbersUnderflowString_when_substruct_then_throwException()
        {
            string underflowExpression = double.MinValue.ToString("R") + "-0.1";
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(underflowExpression));
        }

        [TestMethod]
        public void given_onesString_when_division_then_returnOne()
        {
            Assert.AreEqual(calculator.Calculate("1/1"), 1);
        }

        [TestMethod]
        public void given_positiveNumbersString_when_division_then_returnQuotient()
        {
            Assert.AreEqual(calculator.Calculate("3/2"), 1.5);
        }

        [TestMethod]
        public void given_negativeNumbersString_when_division_then_returnQuotient()
        {
            Assert.AreEqual(calculator.Calculate("-9/-10"), 0.9);
        }

        [TestMethod]
        public void given_positiveAndNegativeNumbersString_when_division_then_returnQuotient()
        {
            Assert.AreEqual(calculator.Calculate("-6/8"), -0.75);
        }

        [TestMethod]
        public void given_numbersString_when_dividingByZero_then_throwException()
        {
            Assert.ThrowsException<DivideByZeroException>(() => calculator.Calculate("1/0"));
        }

        [TestMethod]
        public void given_numbersOverflowString_when_dividing_then_throwException()
        {
            string overflowExpression = double.MaxValue.ToString("R") + "/0.9";
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(overflowExpression));
        }

        [TestMethod]
        public void given_numbersUnderflowString_when_dividing_then_throwException()
        {
            string underflowExpression = double.MinValue.ToString("R") + "/0.9";
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate(underflowExpression));
        }

        [TestMethod]
        public void given_NumberDotString_when_Calculating_then_returnCalculation()
        {
            Assert.AreEqual(calculator.Calculate("5.+5"), 10);
        }

        [TestMethod]
        public void given_DotNumberString_when_Calculating_then_returnCalculation()
        {
            Assert.AreEqual(calculator.Calculate(".5+.5"), 1);
        }

        [TestMethod]
        public void given_validLongString_when_Calculate_then_returnResult()
        {
            Assert.AreEqual(calculator.Calculate("2*3+4"), 10);
        }

        [TestMethod]
        public void given_validLongStringOrderMatters_when_Calculate_then_returnResult()
        {
            Assert.AreEqual(calculator.Calculate("2+3*4"), 14);
        }

        [TestMethod]
        public void given_validLongStringStartsWithNegative_when_Calculate_then_returnResult()
        {
            Assert.AreEqual(calculator.Calculate("-2+3*4"), 10);
        }

        [TestMethod]
        public void given_validLongStringAddAndSubOperatorsAndSubAlone_when_Calculate_then_returnResult()
        {
            Assert.AreEqual(calculator.Calculate("2+-3-4"), -5);
        }

        [TestMethod]
        public void given_validComplexLongString_when_Calculate_then_returnResult()
        {
            double result = -207.5;
            string expression = "-.5-2.*2*2+-2E+2-3*2--3/2+3*2-.5";
            Assert.AreEqual(calculator.Calculate(expression), result);
        }

        [TestMethod]
        public void given_stringWithOneNumber_when_Calculate_then_returnNumber()
        {
            Assert.AreEqual(calculator.Calculate("234"), 234);
        }

        [TestMethod]
        public void given_validComplexLongString_when_CalculateWithMock_then_returnResult()
        {
            var mockParser = new Mock<ICalculatorParser>();
            mockParser.Setup(parser => parser.ParseExpression(It.IsAny<string>())).Returns(new CalculationState<string>(new List<string> { "1", "2" }, new List<Operator> { new Operator('+') }));
            calculator.SetCalculatorParser(mockParser.Object);

            double expected = 3;
            string invalidExpression = "1+2++";
            double result = calculator.Calculate(invalidExpression);

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void given_powerOperator_when_Calculate_then_returnResult()
        {
            Assert.AreEqual(calculator.Calculate(2, 3, new Operator('^')), 8);
        }

        [TestMethod]
        public void given_stringWithPower_when_Calculate_then_returnResult()
        {
            Assert.AreEqual(calculator.Calculate("2^3"), 8);
        }

        [TestMethod]
        public void given_stringWithPowerOverflow_when_Calculate_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate("20^300"));
        }

        [TestMethod]
        public void given_stringWithPowerUnderflow_when_Calculate_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => calculator.Calculate("-20^301"));
        }

        [TestMethod]
        public void given_stringWithPowerOrderMatters_when_Calculate_then_returnResult()
        {
            Assert.AreEqual(calculator.Calculate("2*2^3"), 16);
        }

        [TestMethod]
        public void given_rootNumberString_when_Calculate_then_returnResult()
        {
            Assert.AreEqual(calculator.Calculate("&4"), 2);
        }

        [TestMethod]
        public void given_rootNumberStringLong_when_Calculate_then_returnResult()
        {
            Assert.AreEqual(calculator.Calculate("2+&4*5"), 12);
        }

        [TestMethod]
        public void given_bracketsWithNumber_when_Calculate_then_returnNumber()
        {
            Assert.AreEqual(calculator.Calculate("(-123)"), -123);
        }

        [TestMethod]
        public void given_bracketsWithSimpleExpression_when_Calculate_then_returnResult()
        {
            Assert.AreEqual(calculator.Calculate("(1+2)"), 3);
        }

        [TestMethod]
        public void given_longInnerBracketsExpression_when_Calculate_then_returnResult()
        {
            string expression = "(1+(3*2)-2+3)+2+(3)*4-3/2+4*(2+3*4)+(2^3-&(3+6))";
            double result = 14 * 4 + 22 - 1.5 + 8 - 3;

            Assert.AreEqual(calculator.Calculate(expression), result);
        }
    }
}
