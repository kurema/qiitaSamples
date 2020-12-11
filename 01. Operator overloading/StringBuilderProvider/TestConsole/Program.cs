using System;
using kurema.StringBuilderProvider;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var sb = new TextChainAutoBreak();
                //random phrase from Hamlet by William Shakespeare
                sb += "Hor. And then it started, like a guilty thing";
                sb += "Vpon a fearfull Summons. I haue heard,";
                sb += "The Cocke that is the Trumpet to the day,";

                var builder = sb.GetStringBuilder();
                Console.Write(builder.ToString());
            }
            {
                var sb = new TextChainBrainfuck();

                sb--;
                sb >>= 1;
                sb += 2;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb += 3;
                sb >>= 22;
                sb++;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb += 2;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb += 2;
                sb >>= 2;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 33;
                sb++;
                sb >>= 1;
                sb++;
                sb >>= 2;
                sb += 3;
                sb >>= 4;
                sb += 3;
                sb >>= 3;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 7;
                sb += 2;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 2;
                sb += 3;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb += 2;
                sb >>= 1;
                sb += 3;
                sb >>= 3;
                sb++;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb += 2;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb++;
                sb >>= 2;
                sb += 3;
                sb >>= 7;
                sb++;
                sb >>= 1;
                sb++;
                sb >>= 3;
                sb++;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb += 2;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 2;
                sb += 3;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb += 2;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb += 2;
                sb >>= 2;
                sb++;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb += 2;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb++;
                sb >>= 2;
                sb += 3;
                sb >>= 3;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 3;
                sb += 2;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 2;
                sb += 3;
                sb >>= 3;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 1;
                sb += 3;
                sb >>= 1;
                sb++;
                sb >>= 2;
                sb += 3;
                sb >>= 2;
                sb += 3;
                sb >>= 2;
                sb++;
                sb &= 2;
                sb >>= 2;
                sb++;
                sb = !sb;
                sb >>= 1;
                sb = ~sb;
                sb++;
                sb >>= 1;
                sb++;
                sb = !sb;
                sb <<= 1;
                sb = ~sb;
                sb <<= 1;
                sb--;
                sb = ~sb;
                sb >>= 2;
                sb = !sb;
                sb >>= 1;
                sb = ~sb;
                sb <<= 1;
                sb++;
                sb <<= 1;
                sb += 3;
                sb = !sb;
                sb <<= 1;
                sb = ~sb;
                sb <<= 2;
                sb++;
                sb = ~sb;
                sb >>= 3;
                sb = !sb;
                sb >>= 1;
                sb = ~sb;
                sb += 3;
                sb >>= 1;
                sb++;
                sb = !sb;
                sb++;
                sb = !sb;
                sb <<= 1;
                sb += 16;
                sb >>= 1;
                sb--;
                sb = ~sb;
                sb <<= 1;
                sb += 10;
                sb *= 1;
                sb <<= 1;
                sb = ~sb;

                var text = sb.GetStringBuilder().ToString();
                Console.WriteLine(text);

                using (var sw = new System.IO.StringWriter())
                {
                    var engine = new BrainfuckRunner.Library.BfEngine() { Output = sw };
                    engine.ExecuteScript(text);
                    var result = sw.GetStringBuilder().ToString();
                    Console.WriteLine(result);
                }
            }

            {
                var sb = new TextChainAutoIndent();
                sb += "namespace TestConsole";
                sb += "{";
                sb.Indent();
                sb += "class Program";
                sb += "{";
                sb.Indent();
                sb += "static void Main(string[] args)";
                sb += "{";
                sb += "}";
                sb.Unindent();
                sb += "}";
                sb.Unindent();
                sb += "}";

                var builder = sb.GetStringBuilder();
                Console.WriteLine(builder.ToString());
            }
        }
    }
}
