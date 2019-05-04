using System;
using WM.Kazakhstan.Core;
using Xunit;

namespace WM.Kazakhstan.Tests.Core
{
    public class CalculatorTests
    {
        [Theory()]
        [InlineData("0-8", -8)]
        [InlineData("3+4*4", 19)]
        [InlineData("251-50", 201)]
        [InlineData("251-", 251)]
        public void Calculate_SimpleExpression_ReturnsExpectedValue(string expression, double expected)
        {
            var result = new Calculator()
                .Enter(expression)
                .Calculate();
            
            Assert.Equal(expected, result, 5);
        }

        [Fact]
        public void InfixNotation_AfterClear_ReturnsZero()
        {
            var result = new Calculator()
                .Clear()
                .InfixNotation;

            Assert.Equal("0", result);
        }

        [Theory]
        [InlineData("0-8", "0;8-")]
        [InlineData("3+4*4", "3;4;4*+")]
        public void PostfixNotation_AfterEnter_ReturnsExpectedValue(string expression, string expected)
        {
            var result = new Calculator()
                .Enter(expression)
                .PostfixNotation;

            Assert.Equal(expected, result);
        }

        [Theory()]
        [InlineData("153", "153")]
        [InlineData("0153", "153")]
        public void InfixNotation_EnterDigit_ReturnsEnteredDigit(string expression, string expected)
        {
            var result = new Calculator()
                .Enter(expression)
                .InfixNotation;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void InfixNotation_EnterThenClear_ReturnsZero()
        {
            var result = new Calculator()
                .Enter("123")
                .Clear()
                .InfixNotation;

            Assert.Equal("0", result);
        }

        [Fact]
        public void InfixNotation_EnterDoubleOperators_ReturnsLastOperator()
        {
            var result = new Calculator()
                .Enter("123+*4")
                .InfixNotation;

            Assert.Equal("123*4", result);
        }

        [Fact]
        public void InfixNotation_EnterThenBackspace_ReturnsExpectedValue()
        {
            var result = new Calculator()
                .Enter("156+")
                .Backspace()
                .InfixNotation;

            Assert.Equal("156", result);
        }

        [Fact]
        public void Enter_InvalidChars_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Calculator().Enter("abc"));
        }
    }
}
