using CalculatorLibrary;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorTest
    {
        [TestMethod]
        public void given_zeros_when_add_then_returnZero()
        {
            Assert.AreEqual(Calculator.Calculate(0, 0, '+'), 0);
        }

        [TestMethod]
        public void given_positiveNumbers_when_add_then_returnSum()
        {
            Assert.AreEqual(Calculator.Calculate(1, 2, '+'), 3);
        }

        [TestMethod]
        public void given_negativeNumbers_when_add_then_returnSum()
        {
            Assert.AreEqual(Calculator.Calculate(-1, -2, '+'), -3);
        }

        [TestMethod]
        public void given_negativeAndPositiveNumbers_when_add_then_returnSum()
        {
            Assert.AreEqual(Calculator.Calculate(-1, 2.5, '+'), 1.5);
        }

        [TestMethod]
        public void given_numbersOverflow_when_add_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(double.MaxValue, 0.1, '+'));
        }

        [TestMethod]
        public void given_numbersUnderflow_when_add_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(double.MinValue, -0.1, '+'));
        }

        [TestMethod]
        public void given_ones_when_multiply_then_returnOne()
        {
            Assert.AreEqual(Calculator.Calculate(1, 1, '*'), 1);
        }

        [TestMethod]
        public void given_positiveNumbers_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(Calculator.Calculate(2, 3, '*'), 6);
        }

        [TestMethod]
        public void given_negativeNumbers_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(Calculator.Calculate(-2.5, -3, '*'), 7.5);
        }

        [TestMethod]
        public void given_negativeAndPositiveNumbers_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(Calculator.Calculate(-2, 5, '*'), -10);
        }

        [TestMethod]
        public void given_numbersOverflow_when_multiply_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(double.MaxValue, 1.1, '*'));
        }

        [TestMethod]
        public void given_numbersUnderflow_when_multiply_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(double.MinValue, 1.1, '*'));
        }

        [TestMethod]
        public void given_zeros_when_substruct_then_returnZero()
        {
            Assert.AreEqual(Calculator.Calculate(0, 0, '-'), 0);
        }

        [TestMethod]
        public void given_positiveNumbers_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(Calculator.Calculate(2, 1, '-'), 1);
        }

        [TestMethod]
        public void given_negativeNumbers_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(Calculator.Calculate(-2, -1, '-'), -1);
        }

        [TestMethod]
        public void given_positiveAndNegativeNumbers_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(Calculator.Calculate(2, -1, '-'), 3);
        }

        [TestMethod]
        public void given_numbersOverflow_when_substruct_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(double.MaxValue, -0.1, '-'));
        }

        [TestMethod]
        public void given_numbersUnderflow_when_substruct_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(double.MinValue, 0.1, '-'));
        }

        [TestMethod]
        public void given_ones_when_division_then_returnOne()
        {
            Assert.AreEqual(Calculator.Calculate(1, 1, '/'), 1);
        }

        [TestMethod]
        public void given_positiveNumbers_when_division_then_returnQuotient()
        {
            Assert.AreEqual(Calculator.Calculate(3, 2, '/'), 1.5);
        }

        [TestMethod]
        public void given_negativeNumbers_when_division_then_returnQuotient()
        {
            Assert.AreEqual(Calculator.Calculate(-9, -10, '/'), 0.9);
        }

        [TestMethod]
        public void given_positiveAndNegativeNumbers_when_division_then_returnQuotient()
        {
            Assert.AreEqual(Calculator.Calculate(-6, 8, '/'), -0.75);
        }

        [TestMethod]
        public void given_numbers_when_dividingByZero_then_throwException()
        {
            Assert.ThrowsException<DivideByZeroException>(() => Calculator.Calculate(1, 0, '/'));
        }

        [TestMethod]
        public void given_numbersOverflow_when_dividing_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(double.MaxValue, 0.9, '/'));
        }

        [TestMethod]
        public void given_numbersUnderflow_when_dividing_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(double.MinValue, 0.9, '/'));
        }

        [TestMethod]
        public void given_invalidOperator_when_Calculating_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => Calculator.Calculate(0, 0, '$'));
        }

        [TestMethod]
        public void given_zerosString_when_add_then_returnZero()
        {
            Assert.AreEqual(Calculator.Calculate("0+0"), 0);
        }

        [TestMethod]
        public void given_positiveNumbersString_when_add_then_returnSum()
        {
            Assert.AreEqual(Calculator.Calculate("1+2"), 3);
        }

        [TestMethod]
        public void given_negativeNumbersString_when_add_then_returnSum()
        {
            Assert.AreEqual(Calculator.Calculate("-1+-2"), -3);
        }

        [TestMethod]
        public void given_negativeAndPositiveNumbersString_when_add_then_returnSum()
        {
            Assert.AreEqual(Calculator.Calculate("-1+2.5"), 1.5);
        }

        [TestMethod]
        public void given_numbersOverflowString_when_add_then_throwException()
        {
            string overflowExpression = double.MaxValue.ToString("R") + "+0.1";
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(overflowExpression));
        }

        [TestMethod]
        public void given_numbersUnderflowString_when_add_then_throwException()
        {
            string underflowExpression = double.MinValue.ToString("R") + "+-0.1";
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(underflowExpression));
        }

        [TestMethod]
        public void given_onesString_when_multiply_then_returnOne()
        {
            Assert.AreEqual(Calculator.Calculate("1*1"), 1);
        }

        [TestMethod]
        public void given_positiveNumbersString_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(Calculator.Calculate("2*3"), 6);
        }

        [TestMethod]
        public void given_negativeNumbersString_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(Calculator.Calculate("-2.5*-3"), 7.5);
        }

        [TestMethod]
        public void given_negativeAndPositiveNumbersString_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(Calculator.Calculate("-2*5"), -10);
        }

        [TestMethod]
        public void given_numbersOverflowString_when_multiply_then_throwException()
        {
            string overflowExpression = double.MaxValue.ToString("R") + "*1.1";
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(overflowExpression));
        }

        [TestMethod]
        public void given_numbersUnderflowString_when_multiply_then_throwException()
        {
            string underflowExpression = double.MinValue.ToString("R") + "*1.1";
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(underflowExpression));
        }

        [TestMethod]
        public void given_zerosString_when_substruct_then_returnZero()
        {
            Assert.AreEqual(Calculator.Calculate("0-0"), 0);
        }

        [TestMethod]
        public void given_positiveNumbersString_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(Calculator.Calculate("2-1"), 1);
        }

        [TestMethod]
        public void given_negativeNumbersString_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(Calculator.Calculate("-2--1"), -1);
        }

        [TestMethod]
        public void given_positiveAndNegativeNumbersString_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(Calculator.Calculate("2--1"), 3);
        }

        [TestMethod]
        public void given_numbersOverflowString_when_substruct_then_throwException()
        {
            string overflowExpression = double.MaxValue.ToString("R") + "--0.1";
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(overflowExpression));
        }

        [TestMethod]
        public void given_numbersUnderflowString_when_substruct_then_throwException()
        {
            string underflowExpression = double.MinValue.ToString("R") + "-0.1";
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(underflowExpression));
        }

        [TestMethod]
        public void given_onesString_when_division_then_returnOne()
        {
            Assert.AreEqual(Calculator.Calculate("1/1"), 1);
        }

        [TestMethod]
        public void given_positiveNumbersString_when_division_then_returnQuotient()
        {
            Assert.AreEqual(Calculator.Calculate("3/2"), 1.5);
        }

        [TestMethod]
        public void given_negativeNumbersString_when_division_then_returnQuotient()
        {
            Assert.AreEqual(Calculator.Calculate("-9/-10"), 0.9);
        }

        [TestMethod]
        public void given_positiveAndNegativeNumbersString_when_division_then_returnQuotient()
        {
            Assert.AreEqual(Calculator.Calculate("-6/8"), -0.75);
        }

        [TestMethod]
        public void given_numbersString_when_dividingByZero_then_throwException()
        {
            Assert.ThrowsException<DivideByZeroException>(() => Calculator.Calculate("1/0"));
        }

        [TestMethod]
        public void given_numbersOverflowString_when_dividing_then_throwException()
        {
            string overflowExpression = double.MaxValue.ToString("R") + "/0.9";
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(overflowExpression));
        }

        [TestMethod]
        public void given_numbersUnderflowString_when_dividing_then_throwException()
        {
            string underflowExpression = double.MinValue.ToString("R") + "/0.9";
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(underflowExpression));
        }

        [TestMethod]
        public void given_invalidOperatorString_when_Calculating_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => Calculator.Calculate("0$0"));
        }

        [TestMethod]
        public void given_invalidOperatorWithRealOperatorString_when_Calculating_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => Calculator.Calculate("0$/0"));
        }

        [TestMethod]
        public void given_oneNumberString_when_Calculating_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => Calculator.Calculate("123+"));
        }

        [TestMethod]
        public void given_OperationAndOneNumberString_when_Calculating_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => Calculator.Calculate("*123"));
        }

        [TestMethod]
        public void given_OperationAndExpressionString_when_Calculating_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => Calculator.Calculate("*123+12"));
        }

        [TestMethod]
        public void given_MultipleOperationsString_when_Calculating_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => Calculator.Calculate("123+*123"));
        }

        [TestMethod]
        public void given_MultipleAddOperationsString_when_Calculating_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => Calculator.Calculate("123++123"));
        }

        [TestMethod]
        public void given_emptyString_when_Calculating_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => Calculator.Calculate(""));
        }

        [TestMethod]
        public void given_NumberDotString_when_Calculating_then_returnCalculation()
        {
            Assert.AreEqual(Calculator.Calculate("5.+5"), 10);
        }

        [TestMethod]
        public void given_DotNumberString_when_Calculating_then_returnCalculation()
        {
            Assert.AreEqual(Calculator.Calculate(".5+.5"), 1);
        }

        [TestMethod]
        public void given_MultipleSubOperationOnStartString_when_Calculating_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => Calculator.Calculate("--5-3"));
        }

        [TestMethod]
        public void given_validLongString_when_Calculate_then_returnResult()
        {
            string expression = "2*3+4";
            double result = 10;
            Assert.AreEqual(Calculator.Calculate(expression), result);
        }

        [TestMethod]
        public void given_validLongStringOrderMatters_when_Calculate_then_returnResult()
        {
            string expression = "2+3*4";
            double result = 14;
            Assert.AreEqual(Calculator.Calculate(expression), result);
        }

        [TestMethod]
        public void given_validLongStringStartsWithNegative_when_Calculate_then_returnResult()
        {
            string expression = "-2+3*4";
            double result = 10;
            Assert.AreEqual(Calculator.Calculate(expression), result);
        }

        [TestMethod]
        public void test()
        {
            double result = -14.5;
            string str = "-2*2*2+-2-3*2--3/2";
            Assert.AreEqual(Calculator.Calculate(str), result);
        }
    }
}
