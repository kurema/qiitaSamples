using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var sber = new QiitaSourceGenerator.Helper.StringBuilderChains.StringBuilderChainsEmpty();
            var sbe2 = sber + "hello";

            Console.WriteLine("Hello World!");
        }
    }
}
