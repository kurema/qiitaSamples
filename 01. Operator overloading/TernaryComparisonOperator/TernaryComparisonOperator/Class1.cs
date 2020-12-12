using System;

namespace kurema.TernaryComparisonOperator
{
    public class ComparisonValue<T1, T2> where T1 : IComparable<T1> where T2 : IComparable<T2>
    {
        public ComparisonValue(bool status, T1? valueLeft, T2? valueRight)
        {
            Status = status;
            //ValueLeft = valueLeft;
            //ValueRight = valueRight;
        }

        public ComparableValue<T1> ValueLeft { get; init; }
        public ComparableValue<T2> ValueRight { get; init; }

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

    public struct ComparableValue<T> :IComparable where T : IComparable<T>
    {
        public double ValueDouble { get; init; }
        public T ValueNative { get; init; }
        public bool AlwaysTrue { get; init; }

        public int CompareTo(object? obj)
        {
            if(obj is T native)
            {
            }
            return 0;
        }
    }

    public static class Extensions
    {
        //public static ComparisonValue<double> ToComp(this double from)
        //{
        //    return new ComparisonValue<double>();
        //}
    }
}
