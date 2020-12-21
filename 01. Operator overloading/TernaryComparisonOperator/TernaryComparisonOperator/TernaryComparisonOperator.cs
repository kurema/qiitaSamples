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

        public double ValueLeft { get; private set; }
        public double ValueRight { get; private set; }
        public bool Status { get; private set; }

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
            int hashCode = -1462305666;
            hashCode = hashCode * -1521134295 + ValueLeft.GetHashCode();
            hashCode = hashCode * -1521134295 + ValueRight.GetHashCode();
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            return hashCode;
        }
    }

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
