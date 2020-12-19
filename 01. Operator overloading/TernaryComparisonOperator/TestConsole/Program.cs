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

    public class Value<T>
    {
        public int Content { get; private set; }
        public Value(int content) => Content = content;
        public static int operator +(Value a, Value b) => (a.Content + b.Content);
        public static Value operator -(Value a, Value b) => new Value(a.Content - b.Content);

        public static System.Collections.Generic.KeyValuePair<T1, T3>? add<T1, T2, T3>(System.Collections.Generic.KeyValuePair<T1, T2> a, System.Collections.Generic.KeyValuePair<T2, T3> b) { return null; }
}
}
