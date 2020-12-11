using System;
using System.Collections.Generic;
using System.Text;

namespace QiitaSourceGenerator.Helper.StringBuilderProviders
{

    public interface IStringBuilderProvider
    {
        StringBuilder GetStringBuilder();
    }

    public abstract class TextChainBase<T> : IStringBuilderProvider where T : IStringBuilderProvider
    {
        protected TextChainBase()
        {
            Appended = string.Empty;
        }

        protected TextChainBase(T? origin, string appended)
        {
            Origin = origin;
            Appended = appended ?? throw new ArgumentNullException(nameof(appended));
        }

        public abstract StringBuilder GetStringBuilder();

        public T? Origin { get; protected set; }
        public string Appended { get; protected set; }
    }

    public class TextChain : TextChainBase<IStringBuilderProvider>
    {
        public TextChain() : base() { }
        public TextChain(IStringBuilderProvider? origin, string appended) : base(origin, appended) { }

        public override StringBuilder GetStringBuilder()
        {
            var sb = Origin?.GetStringBuilder() ?? new StringBuilder();
            if (Appended != string.Empty) sb.Append(Appended);
            return sb;
        }

        public static TextChain operator +(TextChain origin, string append) => new TextChain(origin, append);
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

        public static TextChainBrainfuck operator *(TextChainBrainfuck origin, int count) => RepeatText(origin, count, '.');
        public static TextChainBrainfuck operator /(TextChainBrainfuck origin, int count) => RepeatText(origin, count, ',');

        public static TextChainBrainfuck operator &(TextChainBrainfuck origin, int count) => RepeatText(origin, count, '[');
        public static TextChainBrainfuck operator |(TextChainBrainfuck origin, int count) => RepeatText(origin, count, ']');

        public static TextChainBrainfuck operator +(TextChainBrainfuck origin, string text) => new TextChainBrainfuck(origin, text);


        private static TextChainBrainfuck RepeatText(TextChainBrainfuck origin, int count, char positiveChar, char? negativeChar=null)
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
    }

    public class TextChainAutoBreak : TextChainBase<IStringBuilderProvider>
    {
        public TextChainAutoBreak() : base() { }
        public TextChainAutoBreak(IStringBuilderProvider? origin, string appended) : base(origin, appended) { }


        public override StringBuilder GetStringBuilder()
        {
            var sb = Origin?.GetStringBuilder() ?? new StringBuilder();
            if (Appended != string.Empty) sb.AppendLine(Appended);
            return sb;
        }

        public static TextChainAutoBreak operator +(TextChainAutoBreak origin, string append) => new TextChainAutoBreak(origin, append);
    }

    public class TextChainAutoIndent : TextChainBase<TextChainAutoIndent>
    {
        public TextChainAutoIndent() : base() { }
        public TextChainAutoIndent(TextChainAutoIndent? origin, string appended) : base(origin, appended) { }

        public int IndentShift { get; set; }
        public int Indent { get => Math.Max((Origin?.Indent ?? 0) + IndentShift, 0); }

        public override StringBuilder GetStringBuilder()
        {
            var sb = Origin?.GetStringBuilder() ?? new StringBuilder();
            if (Appended != string.Empty)
            {
                sb.AppendLine(Appended);
            }
            return sb;
        }

        public static TextChainAutoIndent operator +(TextChainAutoIndent origin, string append) => new TextChainAutoIndent(origin, append);
    }

}
