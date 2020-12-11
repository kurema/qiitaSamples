using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var sb = new QiitaSourceGenerator.Helper.StringBuilderProviders.TextChainAutoBreak();
                sb += "hello";
                sb += "where";
                sb += "are you?" + "and me";

                var builder = sb.GetStringBuilder();
                Console.WriteLine(builder.ToString());
            }
            {
                var sb = new QiitaSourceGenerator.Helper.StringBuilderProviders.TextChainBrainfuck();
                sb++;
                sb--;
                sb += 2;
                sb += -3;
                sb -= -5;
                sb -= 4;
                sb = !sb;
                sb = ~sb;
                sb *= 1;
                sb /= 1;
                sb &= 1;
                sb |= 1;

                var builder = sb.GetStringBuilder();
                Console.WriteLine(builder.ToString());
            }


        }
    }
}
