using System;
using WM.Kazakhstan.Core;

namespace WM.Kazakhstan
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = string.Empty;
            var calculator = new Calculator();

            while(input != "exit")
            {
                Console.WriteLine("Please, type exspression. For example, 3+4*4");
                Console.WriteLine("Type 'exit' to close application");

                input = Console.ReadLine();

                try
                {
                    var result = calculator
                        .Enter(input)
                        .Calculate();

                    Console.WriteLine($"Result: {result.ToString("0.##")}");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Expression contains invalid chars");
                    Console.WriteLine($"You can type the next chars: {Constants.ValidChars}");
                }
            }
        }
    }
}
