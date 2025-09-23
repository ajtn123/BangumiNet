using Avalonia.Data.Converters;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.V0.Models;
using BangumiNet.Api.V0.V0.Search.Subjects;
using BangumiNet.ViewModels;
using System.Globalization;

namespace BangumiNet.Converters;

public class SearchSortCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => (SubjectsPostRequestBody_sort?)value switch
        {
            SubjectsPostRequestBody_sort.Match => "相关",
            SubjectsPostRequestBody_sort.Heat => "热度",
            SubjectsPostRequestBody_sort.Rank => "排名",
            SubjectsPostRequestBody_sort.Score => "评分",
            null => "默认",
            _ => throw new NotImplementedException(),
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class SearchTypeCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => (SearchType?)value switch
        {
            SearchType.Subject => "项目",
            SearchType.Character => "角色",
            SearchType.Person => "人物",
            null => null,
            _ => throw new NotImplementedException(),
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class DayOfWeekCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => (DayOfWeek?)value switch
        {
            DayOfWeek.Sunday => "星期日",
            DayOfWeek.Monday => "星期一",
            DayOfWeek.Tuesday => "星期二",
            DayOfWeek.Wednesday => "星期三",
            DayOfWeek.Thursday => "星期四",
            DayOfWeek.Friday => "星期五",
            DayOfWeek.Saturday => "星期六",
            null => null,
            _ => throw new NotImplementedException(),
        };
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class SubjectTypeCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is SubjectType type ? type.ToStringSC() : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class EpisodeTypeCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is EpisodeType type ? type.ToStringSC() : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class CharacterTypeCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is CharacterType type ? type.ToStringSC() : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class PersonTypeCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is PersonType type ? type.ToStringSC() : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class CareerCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is PersonCareer type ? type.ToStringSC() : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
