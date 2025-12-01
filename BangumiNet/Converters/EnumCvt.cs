using Avalonia.Data.Converters;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.V0.Models;
using BangumiNet.Api.V0.V0.Search.Subjects;
using BangumiNet.Common.Attributes;
using BangumiNet.Common.Extras;
using BangumiNet.Shared.Extensions;
using System.Globalization;

namespace BangumiNet.Converters;

public class SubjectBrowserSortCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is SubjectBrowserSort type ? type.ToStringSC() : "默认";
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
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
        => value is PersonType type ? type.GetNameCn() : null;
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
public class AnimeCatCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is SubjectCategory.Anime type ? type.ToStringSC() : "全部";
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class BookCatCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is SubjectCategory.Book type ? type.ToStringSC() : "全部";
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class GameCatCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is SubjectCategory.Game type ? type.ToStringSC() : "全部";
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class RealCatCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is SubjectCategory.Real type ? type.ToStringSC() : "全部";
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class UserGroupCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is UserGroup group ? group.ToStringSC() : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class CollectionTypeCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is CollectionType type ? type.ToStringSC() : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class EpisodeCollectionTypeCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is EpisodeCollectionType type ? type.ToStringSC() : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class TimelineCategoryCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is TimelineCategory type ? type.ToStringSC() : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class CommonEnumConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Convert(value);
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
    public static string? Convert(object? value) => value switch
    {
        null => null,
        ItemType itemType => itemType.ToStringSC(),
        RelatedItemType relatedItemType => relatedItemType.ToStringSC(),
        CommentState commentState => commentState.ToStringSC(),
        TopicDisplay topicDisplay => topicDisplay.ToStringSC(),
        GroupRole groupRole => groupRole.ToStringSC(),
        GroupTopicFilterMode groupTopicFilterMode => groupTopicFilterMode.ToStringSC(),
        GroupFilterMode groupFilterMode => groupFilterMode.ToStringSC(),
        GroupSort groupSort => groupSort.ToStringSC(),
        RevisionType revisionType => revisionType.ToStringSC(),
        TimelineTypes.Daily tDaily => tDaily.ToStringSC(),
        TimelineTypes.Doujin tDoujin => tDoujin.ToStringSC(),
        TimelineTypes.Mono tMono => tMono.ToStringSC(),
        TimelineTypes.Progress tProgress => tProgress.ToStringSC(),
        TimelineTypes.Status tStatus => tStatus.ToStringSC(),
        TimelineTypes.Subject tSubject => tSubject.ToStringSC(),
        TimelineTypes.Wiki tWiki => tWiki.ToStringSC(),
        _ => value.ToString()
    };
}