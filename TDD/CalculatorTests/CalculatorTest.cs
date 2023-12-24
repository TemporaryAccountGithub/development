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
        public void given_numbers_when_add_then_returnSum()
        {
            Assert.AreEqual(Calculator.Calculate(1, 2, '+'), 3);
            Assert.AreEqual(Calculator.Calculate(-1, -2, '+'), -3);
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
        public void given_numbers_when_multiply_then_returnProduct()
        {
            Assert.AreEqual(Calculator.Calculate(2, 3, '*'), 6);
            Assert.AreEqual(Calculator.Calculate(-2, 5, '*'), -10);
            Assert.AreEqual(Calculator.Calculate(-2.5, -3, '*'), 7.5);
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
        public void given_numbers_when_substruct_then_returnDifference()
        {
            Assert.AreEqual(Calculator.Calculate(2, 1, '-'), 1);
            Assert.AreEqual(Calculator.Calculate(2, -1, '-'), 3);
            Assert.AreEqual(Calculator.Calculate(-2, -1, '-'), -1);
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
        public void given_numbers_when_division_then_returnQuotient()
        {
            Assert.AreEqual(Calculator.Calculate(3, 2, '/'), 1.5);
            Assert.AreEqual(Calculator.Calculate(-6, 8, '/'), -0.75);
            Assert.AreEqual(Calculator.Calculate(-9, -10, '/'), 0.9);
        }
    }
}