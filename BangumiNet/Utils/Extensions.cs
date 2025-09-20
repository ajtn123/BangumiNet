using BangumiNet.Api.Interfaces;
using BangumiNet.Shared.Interfaces;
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

    public static int? GetTotal(this ICollection c)
        => c.Collect + c.Wish + c.Doing + c.Dropped + c.OnHold;

    /// <summary>
    /// 为列表中的对象设置 Prev 和 Next 属性
    /// </summary>
    /// <returns>与输入为同一个列表</returns>
    public static IList<T> LinkNeighbors<T>(this IList<T> list) where T : class, INeighboring
    {
        for (int i = 0; i < list.Count; i++)
        {
            var current = list[i];
            current.Prev = i > 0 ? list[i - 1] : null;
            current.Next = i < list.Count - 1 ? list[i + 1] : null;
        }

        return list;
    }
}
