using System;
using kurema.TernaryComparisonOperator;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Input number>");
                var response = Console.ReadLine();
                if (response.StartsWith("exit")) break;
                if (double.TryParse(response, out double number))
                {
                    if (2.ToComp() <= number <= 4.0)
                    {
                        Console.WriteLine($"{number} is between 2 and 4!");
                    }
                    else
                    {
                        Console.WriteLine($"{number} is not between 2 and 4.");
                    }
                }
            }
        }
    }
}
