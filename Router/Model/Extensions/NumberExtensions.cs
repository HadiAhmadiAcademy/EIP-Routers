namespace Router.Model.Extensions;

public static class NumberExtensions
{
    public static bool IsInRangeOf(this decimal value, decimal start, decimal end)
    {
        return value >= start && value <= end;
    }
}