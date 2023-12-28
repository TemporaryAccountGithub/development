﻿using CalculatorLibrary;
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
        public void given_invalidStringWithSqrt_when_parse_then_throwException()
        {
            Assert.ThrowsException<ArgumentException>(() => parser.ParseBaseExpression("123&123"));
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
    }
}
