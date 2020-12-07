using System;
using System.Text;

namespace QiitaSourceGenerator.Helper.StringBuilderChains
{
    public interface IStringBuilderChain
    {
        StringBuilder GetStringBuilder();

        string GetString();
        //string GetString()
        //{
        //    return GetStringBuilder().ToString();
        //}
    }

    public class StringBuilderChainsEmpty : IStringBuilderChain
    {
        public string GetString() => "";

        public StringBuilder GetStringBuilder()
        {
            return new StringBuilder();
        }

        public static StringBuilderChainAppend operator +(StringBuilderChainsEmpty @base, string appended)
        {
            return new StringBuilderChainAppend(@base, appended);
        }
    }

    public class StringBuilderChainsEmptyAutoBreak : IStringBuilderChain
    {
        public string GetString() => "";
        public StringBuilder GetStringBuilder() => new StringBuilder();
        public static StringBuilderChainAppendAutoBreak operator +(StringBuilderChainsEmptyAutoBreak @base, string appended) => new StringBuilderChainAppendAutoBreak(@base, appended);
    }

    public class StringBuilderChainAppend : IStringBuilderChain
    {
        public StringBuilderChainAppend(IStringBuilderChain @base, string appended)
        {
            Appended = appended ?? throw new ArgumentNullException(nameof(appended));
            Base = @base ?? throw new ArgumentNullException(nameof(@base));
        }

        public string Appended { get; private set; }
        public IStringBuilderChain Base { get; private set; }

        public StringBuilder GetStringBuilder()
        {
            var sb = Base.GetStringBuilder();
            sb.Append(Appended);
            return sb;
        }

        public string GetString() => GetStringBuilder().ToString();

        public static StringBuilderChainAppend operator +(StringBuilderChainAppend @base, string appended)
        {
            return new StringBuilderChainAppend(@base, appended);
        }
    }

    public class StringBuilderChainAppendAutoBreak : StringBuilderChainAppend
    {
        public StringBuilderChainAppendAutoBreak(IStringBuilderChain @base, string appended) : base(@base, appended)
        {
        }

        //newなので保存している型で呼び出されます。
        public new StringBuilder GetStringBuilder()
        {
            var sb = Base.GetStringBuilder();
            sb.AppendLine(Appended);
            return sb;
        }

        public static StringBuilderChainAppendAutoBreak operator +(StringBuilderChainAppendAutoBreak @base, string appended)
        {
            return new StringBuilderChainAppendAutoBreak(@base, appended);
        }

    }


    public class StringBuilderChainAppendAutoBreakWithIndent : StringBuilderChainAppendAutoBreak
    {
        string? _IndentString;
        public string IndentString
        {
            get => _IndentString ?? (this.Base as StringBuilderChainAppendAutoBreakWithIndent)?._IndentString ?? IndentStringDefault;
            set
            {
                if (_IndentString != value)
                {
                    _IndentString = value;
                    IndentLevel = 0;
                }
            }
        }
        public static string IndentStringDefault = "  ";
        public int IndentLevel { get; set; } = 0;

        public void Indent() => IndentLevel++;
        public void Unindent() => Math.Max(IndentLevel - 1, 0);

        public StringBuilderChainAppendAutoBreakWithIndent(IStringBuilderChain @base, string appended) : base(@base, appended)
        {
        }
    }
}

