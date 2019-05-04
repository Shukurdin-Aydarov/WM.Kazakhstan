namespace WM.Kazakhstan.Core
{
    internal static class Tools
    {
        internal static bool IsOperator(char c)
        {
            return Constants.Operators.IndexOf(c) != -1;
        }

        internal static bool IsValidChar(char c)
        {
            return Constants.ValidChars.IndexOf(c) != -1;
        }

        internal static int CompareOperatorsPriority(char left, char right)
        {
            return GetPriority(left) - GetPriority(right);
        }

        internal static int GetPriority(char operation)
        {
            switch (operation)
            {
                case Constants.Multiply:
                case Constants.Divide:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
