using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace WM.Kazakhstan.Core
{
    public class Calculator : ICalculator
    {
        private readonly StringBuilder infixNotation;

        public Calculator()
        {
            infixNotation = new StringBuilder();
            Clear();
        }

        public string InfixNotation => infixNotation.ToString();

        public string PostfixNotation => GetPostfixNotation().ToString();

        public ICalculator Backspace()
        {
            if (infixNotation.Length == 1)
                infixNotation[0] = Constants.Zero;
            else
                infixNotation.Length--;

            return this;
        }

        public double Calculate()
        {
            var postfixNotataion = GetPostfixNotation();
            var operands = new Stack<double>();

            for(var i = 0; i < postfixNotataion.Length; i++)
            {
                if (char.IsDigit(postfixNotataion[i]))
                {
                    var operand = ParseOperand(i, postfixNotataion);
                    operands.Push(double.Parse(operand));

                    i += operand.Length - 1;
                }
                else if (Tools.IsOperator(postfixNotataion[i]) && operands.Count > 1)
                {
                    var right = operands.Pop();
                    var left = operands.Pop();

                    operands.Push(Calculate(left, right, postfixNotataion[i]));
                }
            }

            Clear();

            return operands.Peek();
        }

        private double Calculate(double left, double right, char operation)
        {
            switch (operation)
            {
                case Constants.Add:
                    return left + right;
                case Constants.Subtract:
                    return left - right;
                case Constants.Multiply:
                    return left * right;
                case Constants.Divide:
                    return left / right;
                default:
                    throw new InvalidOperationException($"Undefined operation '{operation}'.");
            }
        }

        public ICalculator Clear()
        {
            infixNotation.Clear();

            infixNotation.Append(Constants.Zero);

            return this;
        }
        
        public ICalculator Enter(string value)
        {
            for (var i = 0; i < value.Length; i++)
                Enter(value[i]);

            return this;
        }

        public ICalculator Enter(char value)
        {
            if (!Tools.IsValidChar(value))
                throw new ArgumentException($"Invalid argument {nameof(value)}.");

            if (char.IsDigit(value))
                return EnterDigit(value);
                        
            return EnterOperator(value);
        }
        
        private ICalculator EnterDigit(char value)
        {
            var lastEnteredChar = infixNotation[infixNotation.Length - 1];

            if (Tools.IsOperator(lastEnteredChar) && value == Constants.Zero)
                return this;

            if (infixNotation.Length == 1 && lastEnteredChar == Constants.Zero)
            {
                infixNotation[infixNotation.Length - 1] = value;
                return this;
            }

            infixNotation.Append(value);

            return this;
        }

        private ICalculator EnterOperator(char value)
        {
            var lastEnteredChar = infixNotation[infixNotation.Length - 1];

            if (Tools.IsOperator(lastEnteredChar))
            {
                infixNotation[infixNotation.Length - 1] = value;
                return this;
            }

            infixNotation.Append(value);

            return this;
        }

        private StringBuilder GetPostfixNotation()
        {
            var postfixNotation = new StringBuilder();
            var operators = new Stack<char>();

            for(var i = 0; i < infixNotation.Length; i++)
            {
                if (char.IsDigit(infixNotation[i]))
                {
                    if (postfixNotation.Length > 0 && char.IsDigit(postfixNotation[postfixNotation.Length - 1]))
                        postfixNotation.Append(Constants.OperandsSeparator);

                    var operand = ParseOperand(i, infixNotation);
                    postfixNotation.Append(operand);

                    i += operand.Length - 1;
                }
                else
                {
                    if (operators.Count > 0 && Tools.CompareOperatorsPriority(infixNotation[i], operators.Peek()) <= 0)
                        postfixNotation.Append(operators.Pop());

                    operators.Push(infixNotation[i]);
                }
            }

            while (operators.Count > 0)
            {
                postfixNotation.Append(operators.Pop());
            }

            return postfixNotation;
        }

        private static string ParseOperand(int startIndex, StringBuilder infixNotation)
        {
            var operand = new StringBuilder();

            for (var i = startIndex; i < infixNotation.Length; i++)
            {
                var c = infixNotation[i];
                if (Tools.IsOperator(c) || Constants.OperandsSeparator == c)
                    break;

                operand.Append(c);
            }

            return operand.ToString();
        }
    }
}
