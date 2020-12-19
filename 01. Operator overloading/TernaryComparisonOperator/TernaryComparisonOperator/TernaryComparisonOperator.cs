using System;
using kurema.TernaryComparisonOperator;

namespace kurema.TernaryComparisonOperator
{
    [kurema.TernaryComparisonOperator.OperatorOverloadingAttacher.OperatorOverloadingAttachTarget]
    public partial class ComparisonValueDouble : IEquatable<ComparisonValueDouble?>
    {
        internal ComparisonValueDouble(bool status, double valueLeft, double valueRight)
        {
            Status = status;
            ValueLeft = valueLeft;
            ValueRight = valueRight;
        }

        public double ValueLeft { get; init; }
        public double ValueRight { get; init; }
        public bool Status { get; init; }

        public static bool operator true(ComparisonValueDouble value) => value.Status;
        public static bool operator false(ComparisonValueDouble value) => !value.Status;

        public static ComparisonValueDouble operator <(ComparisonValueDouble left, ComparisonValueDouble right) => Combine(left, right, left.ValueRight < right.ValueLeft);
        public static ComparisonValueDouble operator >(ComparisonValueDouble left, ComparisonValueDouble right) => Combine(left, right, left.ValueRight > right.ValueLeft);
        public static ComparisonValueDouble operator <=(ComparisonValueDouble left, ComparisonValueDouble right) => Combine(left, right, left.ValueRight <= right.ValueLeft);
        public static ComparisonValueDouble operator >=(ComparisonValueDouble left, ComparisonValueDouble right) => Combine(left, right, left.ValueRight >= right.ValueLeft);
        //ここは判断に迷う。
        //( 2.ToComp() < 3 ) == ( 3.ToComp() < 4) を 2 < 3 && 3 == 3 && 3 < 4 と解釈するか ( 2 < 3 ) == ( 3 < 4 ) と解釈するか。後者かな。
        public static bool operator ==(ComparisonValueDouble left, ComparisonValueDouble right) => left?.Equals(right) ?? right is null;
        public static bool operator !=(ComparisonValueDouble left, ComparisonValueDouble right) => !(left == right);

        public static implicit operator bool(ComparisonValueDouble from) => from.Status;


        public static ComparisonValueDouble Combine(ComparisonValueDouble left, ComparisonValueDouble right, bool condition)
            => new ComparisonValueDouble(condition && left.Status && right.Status, left.ValueLeft, right.ValueRight);

        public override bool Equals(object? obj)
        {
            return Equals(obj as ComparisonValueDouble);
        }

        public bool Equals(ComparisonValueDouble? other)
        {
            return other is not null && Status == other.Status;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ValueLeft, ValueRight, Status);
        }
    }

    //    public class ComparisonValueDoubleGenerics<T1,T2>
    //    {
    //        public ComparisonValueDoubleGenerics(bool status, T1 valueLeft, T2 valueRight)
    //        {
    //            Status = status;
    //            ValueLeft = valueLeft;
    //            ValueRight = valueRight;
    //        }

    //        public T1 ValueLeft { get; init; }
    //        public T2 ValueRight { get; init; }
    //        public bool Status { get; init; }

    //        public static bool operator true(ComparisonValueDoubleGenerics<T1,T2> value) => value.Status;
    //        public static bool operator false(ComparisonValueDoubleGenerics<T1, T2> value) => !value.Status;

    //        public static ComparisonValueDoubleGenerics<T1,T3> operator <<T3> (ComparisonValueDoubleGenerics<T1, T2> left, ComparisonValueDoubleGenerics<T1, T2> right)
    //        {
    //            //https://github.com/dotnet/csharplang/discussions/612
    //        }

    //}

    //public class ComparisonValueGeneral
    //{
    //    public ComparisonValueGeneral(bool status, object valueLeft, object valueRight)
    //    {
    //        Status = status;
    //        ValueLeft = valueLeft;
    //        ValueRight = valueRight;
    //    }

    //    public object ValueLeft { get; init; }
    //    public object ValueRight { get; init; }
    //    public bool Status { get; init; }

    //    public static bool operator true(ComparisonValueGeneral value) => value.Status;
    //    public static bool operator false(ComparisonValueGeneral value) => !value.Status;

    //    private static bool Operate(string methodName,object left,object right)
    //    {
    //        var leftType = left?.GetType();
    //        var rightType = right?.GetType();
    //        if (leftType is null || rightType is null) throw new ArgumentException();
    //        var method = leftType?.GetMethod(methodName);
    //        if (method == null) throw new ArgumentException(nameof(left));
    //        var parameters = method.GetParameters();
    //        if (parameters?.Length != 2 || parameters[0].ParameterType != leftType) throw new ArgumentException(nameof(methodName));
            
    //    }

    //}

    public static class Extensions
    {
        public static ComparisonValueDouble ToComp(this double from)
        {
            return new ComparisonValueDouble(true, from, from);
        }

        public static ComparisonValueDouble ToComp(this float from)
        {
            return new ComparisonValueDouble(true, from, from);
        }

        public static ComparisonValueDouble ToComp(this sbyte from)
        {
            return new ComparisonValueDouble(true, from, from);
        }

        public static ComparisonValueDouble ToComp(this byte from)
        {
            return new ComparisonValueDouble(true, from, from);
            double a, b;
            if (a > b)
            {

            }
        }

        public static ComparisonValueDouble ToComp(this short from)
        {
            return new ComparisonValueDouble(true, from, from);
        }

        public static ComparisonValueDouble ToComp(this ushort from)
        {
            return new ComparisonValueDouble(true, from, from);
        }

        public static ComparisonValueDouble ToComp(this int from)
        {
            return new ComparisonValueDouble(true, from, from);
        }

        public static ComparisonValueDouble ToComp(this uint from)
        {
            return new ComparisonValueDouble(true, from, from);
        }

        public static ComparisonValueDouble ToComp(this long from)
        {
            return new ComparisonValueDouble(true, from, from);
        }

        public static ComparisonValueDouble ToComp(this ulong from)
        {
            return new ComparisonValueDouble(true, from, from);
        }
    }
}
