using System;
using System.Text;

namespace kurema.StringBuilderProvider
{
    public interface IStringBuilderProvider
    {
        StringBuilder GetStringBuilder(StringBuilder? stringBuilder = null);
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

        public abstract StringBuilder GetStringBuilder(StringBuilder? stringBuilder = null);

        public T? Origin { get; protected set; } = default(T);
        public string? Appended { get; protected set; } = null;
    }

    public class TextChain : TextChainBase<IStringBuilderProvider>
    {
        public TextChain() : base() { }
        public TextChain(IStringBuilderProvider? origin, string appended) : base(origin, appended) { }

        public override StringBuilder GetStringBuilder(StringBuilder? stringBuilder = null)
        {
            var sb = Origin?.GetStringBuilder(stringBuilder) ?? stringBuilder ?? new StringBuilder();
            if (!string.IsNullOrEmpty(Appended)) sb.Append(Appended);
            return sb;
        }

        public static TextChain operator +(TextChain origin, string append) => new TextChain(origin, append);
        public static TextChainCombined operator +(TextChain left, IStringBuilderProvider right) => new TextChainCombined(left, right);
        public static TextChainEx operator +(string append, TextChain origin) => new TextChainEx(origin, new TextChainEx.Operations.Insert(append, 0));


        public static explicit operator string(TextChain from) => from.ToString();
        public override string ToString() => this.GetStringBuilder().ToString();
    }

    public class TextChainCombined : IStringBuilderProvider
    {
        public TextChainCombined(IStringBuilderProvider left, IStringBuilderProvider right)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public IStringBuilderProvider Left { get; private set; }
        public IStringBuilderProvider Right { get; private set; }

        public StringBuilder GetStringBuilder(StringBuilder? stringBuilder = null)
        {
            var sb = Left?.GetStringBuilder();
            sb = Right?.GetStringBuilder(sb);
            return sb ?? new StringBuilder();
        }

        public static TextChain operator +(TextChainCombined origin, string append) => new TextChain(origin, append);
        public static TextChainEx operator +(string append, TextChainCombined origin) => new TextChainEx(origin, new TextChainEx.Operations.Insert(append, 0));
        public static TextChainCombined operator +(TextChainCombined left, IStringBuilderProvider right) => new TextChainCombined(left, right);

        public static explicit operator string(TextChainCombined from) => from.ToString();
        public override string ToString() => this.GetStringBuilder().ToString();

    }

    public class TextChainEx : IStringBuilderProvider
    {
        public TextChainEx(IStringBuilderProvider? origin, TextChainEx.IOperation operation)
        {
            Origin = origin;
            Operation = operation ?? throw new ArgumentNullException(nameof(operation));
        }

        public IStringBuilderProvider? Origin { get; protected set; } = null;

        public IOperation Operation { get; private set; }

        public StringBuilder GetStringBuilder(StringBuilder? stringBuilder = null)
        {
            var sb = Origin?.GetStringBuilder(stringBuilder) ?? stringBuilder ?? new StringBuilder();
            Operation?.Operate(sb);
            return sb;
        }

        public interface IOperation
        {
            void Operate(StringBuilder stringBuilder);
        }

        public static TextChain operator +(TextChainEx origin, string append) => new TextChain(origin, append);
        public static TextChainEx operator +(string append, TextChainEx origin) => new TextChainEx(origin, new Operations.Insert(append, 0));
        public static TextChainCombined operator +(TextChainEx left, IStringBuilderProvider right) => new TextChainCombined(left, right);


        public static explicit operator string(TextChainEx from) => from.ToString();
        public override string ToString() => GetStringBuilder().ToString();


        public class Operations
        {
            public class Append : IOperation
            {
                public string Appended { get; private set; }

                public Append(string appended)
                {
                    Appended = appended ?? throw new ArgumentNullException(nameof(appended));
                }

                public void Operate(StringBuilder stringBuilder)
                {
                    stringBuilder.Append(Appended);
                }
            }

            public class AppendLine : IOperation
            {
                public string Appended { get; private set; }

                public AppendLine(string appended)
                {
                    Appended = appended ?? throw new ArgumentNullException(nameof(appended));
                }

                public void Operate(StringBuilder stringBuilder)
                {
                    stringBuilder.AppendLine(Appended);
                }
            }

            public class Insert : IOperation
            {
                public Insert(string value, int index)
                {
                    Value = value ?? throw new ArgumentNullException(nameof(value));
                    Index = index;
                }

                public string Value { get; private set; }
                public int Index { get; private set; }

                public void Operate(StringBuilder stringBuilder)
                {
                    stringBuilder.Insert(Index, Value);
                }
            }

            public class ReplaceString : IOperation
            {
                public ReplaceString(string oldValue, string newValue, int? startIndex, int? count)
                {
                    OldValue = oldValue ?? throw new ArgumentNullException(nameof(oldValue));
                    NewValue = newValue ?? throw new ArgumentNullException(nameof(newValue));
                    StartIndex = startIndex;
                    Count = count;
                }

                public ReplaceString(string oldValue, string newValue)
                {
                    OldValue = oldValue ?? throw new ArgumentNullException(nameof(oldValue));
                    NewValue = newValue ?? throw new ArgumentNullException(nameof(newValue));
                }

                public string OldValue { get; private set; }
                public string NewValue { get; private set; }
                public int? StartIndex { get; private set; }
                public int? Count { get; private set; }

                public void Operate(StringBuilder stringBuilder)
                {
                    if (StartIndex is null || Count is null) stringBuilder.Replace(OldValue, NewValue);
                    else stringBuilder.Replace(OldValue, NewValue, StartIndex ?? 0, Count ?? 0);
                }
            }
        }
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


        public override StringBuilder GetStringBuilder(StringBuilder? stringBuilder = null)
        {
            var sb = Origin?.GetStringBuilder(stringBuilder) ?? stringBuilder ?? new StringBuilder();
            if (Appended is not null) sb.AppendLine(Appended);
            return sb;
        }

        public static TextChainAutoBreak operator +(TextChainAutoBreak origin, string append) => new TextChainAutoBreak(origin, append);

        public static explicit operator string(TextChainAutoBreak from) => from.ToString();
        public override string ToString() => this.GetStringBuilder().ToString();

        public static TextChainCombined operator +(TextChainAutoBreak left, IStringBuilderProvider right) => new TextChainCombined(left, right);
    }

    public class TextChainAutoIndent : TextChainBase<IStringBuilderProvider>
    {
        public TextChainAutoIndent() : base() { }
        public TextChainAutoIndent(TextChainAutoIndent? origin, string appended) : base(origin, appended) { }

        public int IndentShift { get; set; } = 0;
        public string? IndentText { get; set; } = null;

        public const string IndentTextDefault = "    ";

        public override StringBuilder GetStringBuilder(StringBuilder? stringBuilder = null)
        {
            var result = GetStringBuilderAndInfo(stringBuilder);
            return result.Builder;
        }

        public void Indent() => IndentShift++;
        public void Unindent() => IndentShift--;

        public (StringBuilder Builder, int IndentLevel, string IndentText) GetStringBuilderAndInfo(StringBuilder? stringBuilder = null)
        {
            switch (Origin)
            {
                case TextChainAutoIndent originIndent:
                    {
                        var currentResult = originIndent.GetStringBuilderAndInfo(stringBuilder);
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
                        var sb = Origin?.GetStringBuilder(stringBuilder) ?? stringBuilder ?? new StringBuilder();
                        if (Appended is not null) sb.AppendLine(Appended);
                        return (sb, IndentShift, IndentText ?? IndentTextDefault);
                    }
            }
        }

        public static TextChainAutoIndent operator +(TextChainAutoIndent origin, string append) => new TextChainAutoIndent(origin, append);

        public static explicit operator string(TextChainAutoIndent from) => from.ToString();
        public override string ToString() => this.GetStringBuilder().ToString();

        public static TextChainCombined operator +(TextChainAutoIndent left, IStringBuilderProvider right) => new TextChainCombined(left, right);
    }

}
