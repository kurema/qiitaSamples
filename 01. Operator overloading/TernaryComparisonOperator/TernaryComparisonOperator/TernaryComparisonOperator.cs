using System;
using kurema.TernaryComparisonOperator;

namespace kurema.TernaryComparisonOperator
{
    public class ComparisonValueDouble : IEquatable<ComparisonValueDouble?>
    {
        public ComparisonValueDouble(bool status, double valueLeft, double valurRight)
        {
            Status = status;
            ValueLeft = valueLeft;
            ValueRight = valurRight;
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

        public static ComparisonValueDouble operator <(ComparisonValueDouble left, double right) => new ComparisonValueDouble(left.ValueRight < right, left.ValueRight, right);
        public static ComparisonValueDouble operator >(ComparisonValueDouble left, double right) => new ComparisonValueDouble(left.ValueRight > right, left.ValueRight, right);
        public static ComparisonValueDouble operator <=(ComparisonValueDouble left, double right) => new ComparisonValueDouble(left.ValueRight <= right, left.ValueRight, right);
        public static ComparisonValueDouble operator >=(ComparisonValueDouble left, double right) => new ComparisonValueDouble(left.ValueRight >= right, left.ValueRight, right);
        public static ComparisonValueDouble operator ==(ComparisonValueDouble left, double right) => new ComparisonValueDouble(left.ValueRight == right, left.ValueRight, right);
        public static ComparisonValueDouble operator !=(ComparisonValueDouble left, double right) => new ComparisonValueDouble(left.ValueRight != right, left.ValueRight, right);

        public static ComparisonValueDouble operator <(double left, ComparisonValueDouble right) => new ComparisonValueDouble(left < right.ValueLeft, left, right.ValueRight);
        public static ComparisonValueDouble operator >(double left, ComparisonValueDouble right) => new ComparisonValueDouble(left > right.ValueLeft, left, right.ValueRight);
        public static ComparisonValueDouble operator <=(double left, ComparisonValueDouble right) => new ComparisonValueDouble(left <= right.ValueLeft, left, right.ValueRight);
        public static ComparisonValueDouble operator >=(double left, ComparisonValueDouble right) => new ComparisonValueDouble(left >= right.ValueLeft, left, right.ValueRight);
        public static ComparisonValueDouble operator ==(double left, ComparisonValueDouble right) => new ComparisonValueDouble(left == right.ValueLeft, left, right.ValueRight);
        public static ComparisonValueDouble operator !=(double left, ComparisonValueDouble right) => new ComparisonValueDouble(left != right.ValueLeft, left, right.ValueRight);



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


    public static class Extensions
    {
        public static ComparisonValueDouble ToComp(this double from)
        {
            return new ComparisonValueDouble(true, from, from);
        }

        public static void Main()
        {
            Console.WriteLine(2.0.ToComp() < 3.0.ToComp());
        }
    }
}
