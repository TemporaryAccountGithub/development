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
        public void given_numbersOverflow_when_dividingByZero_then_throwException()
        {
            Assert.ThrowsException<OverflowException>(() => Calculator.Calculate(double.MaxValue, 0.9, '/'));
        }

        [TestMethod]
        public void given_invalidOperator_when_Calculating_then_throwException()
        {
            Assert.ThrowsException<InvalidOperationException>(() => Calculator.Calculate(0, 0, '$'));
        }
    }
}