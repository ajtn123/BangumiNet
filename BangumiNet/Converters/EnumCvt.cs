using Avalonia.Data.Converters;
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
            null => string.Empty,
            _ => throw new NotImplementedException(),
        };

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
