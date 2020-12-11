using System;

namespace kurema.TernaryComparisonOperator
{
    public class ComparisonValue<T1, T2> where T1 : IComparable where T2 : IComparable
    {
        public ComparisonValue(bool status, T1? valueLeft, T2? valueRight)
        {
            Status = status;
            ValueLeft = valueLeft;
            ValueRight = valueRight;
        }

        public T1? ValueLeft { get; init; } = default(T1);
        public T2? ValueRight { get; init; } = default(T2);
        public bool Status { get; init; } = true;

        public static ComparisonValue<T1, double> operator <(ComparisonValue<T1, T2> left, double right)
        {
            return new ComparisonValue<T1, double>(left.Status && left.ValueRight?.CompareTo(right) is null or < 0, left.ValueLeft, right);
        }

        public static ComparisonValue<T1, double> operator >(ComparisonValue<T1, T2> left, double right)
        {
            return new ComparisonValue<T1, double>(left.Status && left.ValueRight?.CompareTo(right) is null or > 0, left.ValueLeft, right);
        }

        //private static bool CheckTwice(ComparisonValue<T1, T2> comparisonValue, bool IsComparisonValueRight, bool extraCondition)
        //{
        //    return comparisonValue.Status && ((IsComparisonValueRight ? (comparisonValue.ValueRight is null) : (comparisonValue.ValueLeft is null)) || extraCondition);
        //}
    }

    public static class Extensions
    {
        //public static ComparisonValue<double> ToComp(this double from)
        //{
        //    return new ComparisonValue<double>();
        //}
    }
}
