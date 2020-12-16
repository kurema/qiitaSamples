using System;
using Xunit;
using kurema.TernaryComparisonOperator;
using kurema.StringBuilderProvider;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public void TernaryComparison1()
        {
            Assert.True(1.0.ToComp() < 2 < 5);
            Assert.True(1.0.ToComp() < 2.0 < 5.0);
            Assert.True(1.0 < 2.0.ToComp() < 5.0);
            Assert.True(1.0.ToComp() < 2.0 < 3.0 < 4 < 5.0f);
            Assert.False(3.0.ToComp() < 2.0 < 1.0);
            Assert.False(3.0.ToComp() < 4.0 < 1.0);
        }

        [Fact]
        public void StringBuilderProvider1()
        {
            {
                var text = new TextChain();
                text += "1";
                text += "2";
                text += "3";
                Assert.Equal("123", text);
            }
            {
                var text = new TextChainAutoBreak();
                text += "1";
                text += "2";
                text += "3";
                Assert.Equal("1" + Environment.NewLine + "2" + Environment.NewLine + "3" + Environment.NewLine, text);
            }
            {
                var text = new TextChainAutoIndent();
                text.IndentText = " ";
                text.Indent();
                text += "1";
                text.Indent();
                text += "2";
                for (var i = 0; i < 3; i++) text.Unindent();
                text += "3";
                Assert.Equal(" 1" + Environment.NewLine + "  2" + Environment.NewLine + "3" + Environment.NewLine, text);
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

                using (var sw = new System.IO.StringWriter())
                {
                    var engine = new BrainfuckRunner.Library.BfEngine() { Output = sw };
                    engine.ExecuteScript(text);
                    var result = sw.GetStringBuilder().ToString();

                    Assert.Equal(text, result);
                }
            }

        }
    }
}
