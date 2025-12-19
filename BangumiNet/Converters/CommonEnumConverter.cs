using Avalonia.Data.Converters;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.V0.V0.Search.Subjects;
using BangumiNet.Common;
using BangumiNet.Common.Attributes;
using BangumiNet.Common.Extras;
using System.Globalization;

namespace BangumiNet.Converters;

public class CommonEnumConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Convert(value);
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
    public static string? Convert(object? value) => value switch
    {
        null => null,
        ItemType itemType => itemType.GetNameCn(),
        RelatedItemType relatedItemType => relatedItemType.GetNameCn(),
        CommentState commentState => commentState.ToStringSC(),
        TopicDisplay topicDisplay => topicDisplay.ToStringSC(),
        GroupRole groupRole => groupRole.ToStringSC(),
        GroupTopicFilterMode groupTopicFilterMode => groupTopicFilterMode.ToStringSC(),
        GroupFilterMode groupFilterMode => groupFilterMode.ToStringSC(),
        GroupSort groupSort => groupSort.ToStringSC(),
        SubjectBrowserSort subjectBrowserSort => subjectBrowserSort.ToStringSC(),
        SubjectsPostRequestBody_sort subjectsPostRequestBody_Sort => subjectsPostRequestBody_Sort.ToStringSC(),
        RevisionType revisionType => revisionType.ToStringSC(),
        IndexType indexType => indexType.GetNameCn(),
        SubjectType subjectType => subjectType.GetNameCn(),
        PersonType personType => personType.GetNameCn(),
        CharacterType characterType => characterType.GetNameCn(),
        EpisodeType episodeType => episodeType.GetNameCn(),
        FilterMode filterMode => filterMode.ToStringSC(),
        CollectionType collectionType => collectionType.ToStringSC(),
        EpisodeCollectionType episodeCollectionType => episodeCollectionType.ToStringSC(),
        TimelineCategory timelineCategory => timelineCategory.ToStringSC(),
        UserGroup userGroup => userGroup.ToStringSC(),
        AnimeType animeType => animeType.GetNameCn(),
        BookType bookType => bookType.GetNameCn(),
        GameType gameType => gameType.GetNameCn(),
        RealType realType => realType.GetNameCn(),
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