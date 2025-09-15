using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BangumiNet.Utils;

public static class Extensions
{
    public static int ToInt(this DayOfWeek dayOfWeek, DayOfWeek startingDay = DayOfWeek.Monday, int startingIndex = 0)
    {
        int i = dayOfWeek - startingDay;
        if (i < 0) i += 7;
        return i += startingIndex;
    }

    public static ObservableCollection<T>? ToObservableCollection<T>(this IEnumerable<T>? enumerable)
        => enumerable is not null ? [.. enumerable] : null;
}
