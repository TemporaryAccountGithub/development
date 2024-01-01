using CalculatorLibrary;

namespace CalculatorTests
{
    [TestClass]
    public class OperatorTests
    {
        [TestMethod]
        public void given_validOperator_when_createNewOperator_then_createCorrectly()
        {
            Operator myOperator = new Operator('+');

            Assert.AreEqual(myOperator.OperatorSymbol, CharOperator.Add);
        }

        [TestMethod]
        public void given_invalidOperator_when_createNewOperator_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => new Operator('%'));
        }

        [TestMethod]
        public void given_twoOperatorSameSymbol_when_checkEqual_then_returnTrue()
        {
            Assert.AreEqual(new Operator('+'), new Operator('+'));
        }

        [TestMethod]
        public void given_twoOperatorDifferentSymbol_when_checkEqual_then_returnFalse()
        {
            Assert.AreNotEqual(new Operator('+'), new Operator('*'));
        }
    }
}
