using System;
using kurema.TernaryComparisonOperator;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(long.MaxValue.ToComp() > (long.MaxValue - 1));

            while (true)
            {
                Console.Write("Input number>");
                var response = Console.ReadLine();
                if (response.StartsWith("exit")) break;
                if (double.TryParse(response, out double number))
                {
                    //if (new Comparison()< 2 <= number <= 4.0)
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
