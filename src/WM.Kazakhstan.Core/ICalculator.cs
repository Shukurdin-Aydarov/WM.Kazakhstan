using System;
using System.Collections.Generic;
using System.Text;

namespace WM.Kazakhstan.Core
{
    public interface ICalculator
    {
        string InfixNotation { get; }
        string PostfixNotation { get;  }
        
        ICalculator Enter(char value);
        ICalculator Enter(string value);
        ICalculator Backspace();
        ICalculator Clear();

        double Calculate();
    }
}
