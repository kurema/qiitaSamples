using System;
using System.Collections.Generic;
using System.Text;

namespace kurema.StringBuilderProvider
{
    public interface IStringBuilderProvider
    {
        StringBuilder GetStringBuilder();
    }

    public abstract class TextChainBase<T> : IStringBuilderProvider where T : IStringBuilderProvider
    {
        protected TextChainBase()
        {
        }

        protected TextChainBase(T? origin, string appended)
        {
            Origin = origin;
            Appended = appended ?? throw new ArgumentNullException(nameof(appended));
        }

        public abstract StringBuilder GetStringBuilder();

        public T? Origin { get; protected set; } = default(T);
        public string? Appended { get; protected set; } = null;
    }

    public class TextChain : TextChainBase<IStringBuilderProvider>
    {
        public TextChain() : base() { }
        public TextChain(IStringBuilderProvider? origin, string appended) : base(origin, appended) { }

        public override StringBuilder GetStringBuilder()
        {
            var sb = Origin?.GetStringBuilder() ?? new StringBuilder();
            if (!string.IsNullOrEmpty(Appended)) sb.Append(Appended);
            return sb;
        }

        public static TextChain operator +(TextChain origin, string append) => new TextChain(origin, append);

        public static implicit operator string(TextChain from)=>from.ToString();
        public override string ToString() => this.GetStringBuilder().ToString();
    }

    public class TextChainBrainfuck : TextChain
    {
        public TextChainBrainfuck()
        {
        }

        public TextChainBrainfuck(IStringBuilderProvider? origin, string appended) : base(origin, appended)
        {
        }

        public static TextChainBrainfuck operator +(TextChainBrainfuck origin) => new TextChainBrainfuck(origin, "+");
        public static TextChainBrainfuck operator -(TextChainBrainfuck origin) => new TextChainBrainfuck(origin, "-");

        public static TextChainBrainfuck operator ++(TextChainBrainfuck origin) => new TextChainBrainfuck(origin, "+");
        public static TextChainBrainfuck operator --(TextChainBrainfuck origin) => new TextChainBrainfuck(origin, "-");

        public static TextChainBrainfuck operator !(TextChainBrainfuck origin) => new TextChainBrainfuck(origin, "[");
        public static TextChainBrainfuck operator ~(TextChainBrainfuck origin) => new TextChainBrainfuck(origin, "]");


        public static TextChainBrainfuck operator +(TextChainBrainfuck origin, int count) => RepeatText(origin, count, '+', '-');

        public static TextChainBrainfuck operator -(TextChainBrainfuck origin, int count) => origin + (-count);

        public static TextChainBrainfuck operator >(TextChainBrainfuck origin, int count) => RepeatText(origin, count, '>', '<');
        public static TextChainBrainfuck operator <(TextChainBrainfuck origin, int count) => origin > (-count);

        public static TextChainBrainfuck operator >>(TextChainBrainfuck origin, int count) => origin > count;
        public static TextChainBrainfuck operator <<(TextChainBrainfuck origin, int count) => origin > (-count);


        public static TextChainBrainfuck operator *(TextChainBrainfuck origin, int count) => RepeatText(origin, count, '.');
        public static TextChainBrainfuck operator /(TextChainBrainfuck origin, int count) => RepeatText(origin, count, ',');

        public static TextChainBrainfuck operator &(TextChainBrainfuck origin, int count) => RepeatText(origin, count, '[');
        public static TextChainBrainfuck operator |(TextChainBrainfuck origin, int count) => RepeatText(origin, count, ']');

        public static TextChainBrainfuck operator +(TextChainBrainfuck origin, string text) => new TextChainBrainfuck(origin, text);


        private static TextChainBrainfuck RepeatText(TextChainBrainfuck origin, int count, char positiveChar, char? negativeChar = null)
        {
            if (count > 0)
            {
                return new TextChainBrainfuck(origin, new string(positiveChar, count));
            }
            else if (count < 0)
            {
                return new TextChainBrainfuck(origin, new string(negativeChar ?? throw new ArgumentNullException(), -count));
            }
            else return origin;
        }

        public static string GenerateCodeFromBrainfuck(string brainfuck, string varName)
        {
            string? GetCode(char character, int count)
            {
                switch (character)
                {
                    case '>': return $"{varName} >>= {count};";
                    case '<': return $"{varName} <<= {count};";
                    case '.': return $"{varName} *= {count};";
                    case ',': return $"{varName} /= {count};";
                    case '+' when count == 1: return $"{varName} ++;";
                    case '+': return $"{varName} += {count};";
                    case '-' when count == 1: return $"{varName} --;";
                    case '-': return $"{varName} -= {count};";
                    case '[' when count == 1: return $"{varName} = !{varName};";
                    case '[': return $"{varName} &= {count};";
                    case ']' when count == 1: return $"{varName} = ~{varName};";
                    case ']': return $"{varName} |= {count};";
                    default: return null;
                }
            }

            char? lastChar = null;
            int lastCharCount = 0;

            var result = new TextChainAutoBreak();

            void appendCode()
            {
                if (lastChar != null)
                {
                    var code = GetCode(lastChar ?? ' ', lastCharCount);
                    if (code != null) result += code;
                }
            }

            foreach (var character in brainfuck)
            {
                if (lastChar == character)
                {
                    lastCharCount++;
                    continue;
                }

                appendCode();

                lastChar = character;
                lastCharCount = 1;
            }
            appendCode();

            return result.GetStringBuilder().ToString();
        }
    }

    public class TextChainAutoBreak : TextChainBase<IStringBuilderProvider>
    {
        public TextChainAutoBreak() : base() { }
        public TextChainAutoBreak(IStringBuilderProvider? origin, string appended) : base(origin, appended) { }


        public override StringBuilder GetStringBuilder()
        {
            var sb = Origin?.GetStringBuilder() ?? new StringBuilder();
            if (Appended is not null) sb.AppendLine(Appended);
            return sb;
        }

        public static TextChainAutoBreak operator +(TextChainAutoBreak origin, string append) => new TextChainAutoBreak(origin, append);

        public static implicit operator string(TextChainAutoBreak from) => from.ToString();
        public override string ToString() => this.GetStringBuilder().ToString();

    }

    public class TextChainAutoIndent : TextChainBase<IStringBuilderProvider>
    {
        public TextChainAutoIndent() : base() { }
        public TextChainAutoIndent(TextChainAutoIndent? origin, string appended) : base(origin, appended) { }

        public int IndentShift { get; set; } = 0;
        public string? IndentText { get; set; } = null;

        public static string IndentTextDefault = new string(' ', 4);

        public override StringBuilder GetStringBuilder()
        {
            var result = GetStringBuilderAndInfo();
            return result.Builder;
        }

        public void Indent() => IndentShift++;
        public void Unindent() => IndentShift--;

        public (StringBuilder Builder, int IndentLevel, string IndentText) GetStringBuilderAndInfo()
        {
            switch (Origin)
            {
                case TextChainAutoIndent originIndent:
                    {
                        var currentResult = originIndent.GetStringBuilderAndInfo();
                        if (Appended is not null)
                        {
                            for (int i = 0; i < currentResult.IndentLevel; i++) currentResult.Builder.Append(currentResult.IndentText);
                            currentResult.Builder.AppendLine(Appended);
                        }
                        return IndentText is null ?
                            (currentResult.Builder, currentResult.IndentLevel + IndentShift, currentResult.IndentText) :
                            (currentResult.Builder, IndentShift, IndentText);
                    }
                default:
                    {
                        var sb = Origin?.GetStringBuilder() ?? new StringBuilder();
                        if (Appended is not null) sb.AppendLine(Appended);
                        return (sb, IndentShift, IndentText ?? IndentTextDefault);
                    }
            }
        }

        public static TextChainAutoIndent operator +(TextChainAutoIndent origin, string append) => new TextChainAutoIndent(origin, append);

        public static implicit operator string(TextChainAutoIndent from) => from.ToString();
        public override string ToString() => this.GetStringBuilder().ToString();

    }

}
