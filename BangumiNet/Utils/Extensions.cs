using Avalonia.Media;
using BangumiNet.Api.Interfaces;
using BangumiNet.Shared.Interfaces;

namespace BangumiNet.Utils;

public static class Extensions
{
    public static int ToInt(this DayOfWeek dayOfWeek, DayOfWeek startingDay = DayOfWeek.Monday, int startingIndex = 0)
    {
        int i = dayOfWeek - startingDay;
        if (i < 0) i += 7;
        return i += startingIndex;
    }

    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        => [.. enumerable];

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

    public static string ToSymbol(this SearchFilterRelation relation)
        => relation switch
        {
            SearchFilterRelation.GreaterThan => ">",
            SearchFilterRelation.GreaterThanOrEqualTo => ">=",
            SearchFilterRelation.LessThan => "<",
            SearchFilterRelation.LessThanOrEqualTo => "<=",
            SearchFilterRelation.EqualTo => "=",
            _ => throw new NotImplementedException(),
        };

    /// <summary>
    /// 将 <see cref="DateOnly"/> 转换为Bangumi 的日期 string.
    /// </summary>
    /// <returns><c>yyyy-MM-dd</c></returns>
    public static string ToBangumiString(this DateTimeOffset date)
        => date.ToString("yyyy-MM-dd");

    public static T? TryGet<T>(this IDictionary<string, T> dict, string key)
    {
        if (dict.TryGetValue(key, out var value))
            return value;
        else return default;
    }

    public static string ToOpaqueString(this Color color)
    {
        var opaque = new Color(byte.MaxValue, color.R, color.G, color.B);
        return opaque.ToString().ToLower().Replace("#ff", "#");
    }
}
