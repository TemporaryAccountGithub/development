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
            Assert.ThrowsException<Exception>(() => Calculator.Calculate(double.MaxValue, 0.1, '+'));
        }
    }
}