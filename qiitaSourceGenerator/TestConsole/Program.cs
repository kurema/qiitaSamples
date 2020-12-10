using System;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var sb = new QiitaSourceGenerator.Helper.StringBuilderProviders.TextChainAutoBreak();
            sb += "hello";
            sb += "where";
            sb += "are you?";
            var sb2= new QiitaSourceGenerator.Helper.StringBuilderProviders.TextChain(sb," ...");
            sb2 += "hohoho";

            var builder = sb2.GetStringBuilder();

            Console.WriteLine(builder.ToString());
        }
    }
}
