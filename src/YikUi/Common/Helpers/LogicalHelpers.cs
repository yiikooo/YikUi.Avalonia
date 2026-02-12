using Avalonia.LogicalTree;

namespace YikUi.Common.Helpers;

public static class LogicalHelpers
{
    public static int CalculateDistanceFromLogicalParent<T>(this ILogical? logical, int @default = -1)
        where T : ILogical
    {
        var distance = 0;
        var parent = logical;
        while (parent is not null)
        {
            if (parent is T) return distance;
            parent = parent.LogicalParent;
            distance++;
        }

        return @default;
    }
}