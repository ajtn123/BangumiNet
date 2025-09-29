using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.V0.Models;

namespace BangumiNet.ViewModels;

public partial class SubjectCollectionViewModel : ViewModelBase
{
    public SubjectCollectionViewModel(UserSubjectCollection collection)
    {
        UserSubjectCollection = collection;
        Comment = collection.Comment;
        Id = collection.SubjectId;
        EpisodeStatus = collection.EpStatus;
        VolumeStatus = collection.VolStatus;
        Tags = collection.Tags.ToObservableCollection();
        IsPrivate = collection.Private;
        Rating = collection.Rate;
        UpdateTime = collection.UpdatedAt;
        Type = (CollectionType?)collection.Type;
        SubjectType = (SubjectType?)collection.SubjectType;
        if (collection.Subject != null)
            Subject = new(collection.Subject);

        if (Rating == 0) Rating = null;
        if (EpisodeStatus == 0 && Subject?.Eps == null) EpisodeStatus = null;
        if (VolumeStatus == 0 && Subject?.Volumes == null) VolumeStatus = null;
    }

    [Reactive] public partial UserSubjectCollection? UserSubjectCollection { get; set; }
    [Reactive] public partial SubjectViewModel? Subject { get; set; }
    [Reactive] public partial SubjectType? SubjectType { get; set; }
    [Reactive] public partial string? Comment { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial CollectionType? Type { get; set; }
    [Reactive] public partial int? EpisodeStatus { get; set; }
    [Reactive] public partial int? VolumeStatus { get; set; }
    [Reactive] public partial int? Rating { get; set; }
    [Reactive] public partial bool? IsPrivate { get; set; }
    [Reactive] public partial DateTimeOffset? UpdateTime { get; set; }
    [Reactive] public partial ObservableCollection<string>? Tags { get; set; }
}
