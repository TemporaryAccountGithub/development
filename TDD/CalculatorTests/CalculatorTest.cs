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
    }
}