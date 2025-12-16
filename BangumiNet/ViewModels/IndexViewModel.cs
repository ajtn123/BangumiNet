using BangumiNet.Api.P1.Models;
using BangumiNet.Common.Extras;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;

namespace BangumiNet.ViewModels;

public partial class IndexViewModel : ItemViewModelBase
{
    public IndexViewModel(SlimIndex index)
    {
        Source = index;
        Id = index.Id;
        Name = index.Title;
        CreationTime = CommonUtils.ParseBangumiTime(index.CreatedAt);
        UpdateTime = CommonUtils.ParseBangumiTime(index.UpdatedAt);
        IsPrivate = index.Private ?? false;
        ItemCount = index.Total;
        Type = (IndexType?)index.Type;
        Stats = index.Stats;
        if (index.User != null)
            User = new(index.User) { Id = index.Uid };
    }
    public IndexViewModel(IndexObject index)
    {
        Source = index;
        Id = index.Id;
        Name = index.Title;
        CreationTime = CommonUtils.ParseBangumiTime(index.CreatedAt);
        UpdateTime = CommonUtils.ParseBangumiTime(index.UpdatedAt);
        IsPrivate = index.Private ?? false;
        ItemCount = index.Total;
        Type = (IndexType?)index.Type;
        Stats = index.Stats;
        Summary = index.Desc;
        ReplyCount = index.Replies;
        CollectionCount = index.Collects;
        Award = index.Award;
        if (index.User != null)
            User = new(index.User) { Id = index.Uid };
    }

    protected override void Activate(CompositeDisposable disposables)
    {
        RelatedItems ??= new(RelatedItemType.Subject, ItemType, Id);
        Comments ??= new(ItemType, Id);

        OpenInBrowserCommand = ReactiveCommand.Create(() => CommonUtils.OpenUrlInBrowser(UrlProvider.BangumiTvIndexUrlBase + Id)).DisposeWith(disposables);
    }

    [Reactive] public partial UserViewModel? User { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
    [Reactive] public partial DateTimeOffset? UpdateTime { get; set; }
    [Reactive] public partial bool IsPrivate { get; set; }
    [Reactive] public partial int? ItemCount { get; set; }
    [Reactive] public partial IndexType? Type { get; set; }
    [Reactive] public partial IndexStats? Stats { get; set; }
    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial int? ReplyCount { get; set; }
    [Reactive] public partial int? CollectionCount { get; set; }
    [Reactive] public partial int? Award { get; set; }
    [Reactive] public partial CommentListViewModel? Comments { get; set; }
    [Reactive] public partial RelatedItemListViewModel? RelatedItems { get; set; }

    public bool IsFull => Source is IndexObject;
    public override ItemType ItemType { get; init; } = ItemType.Index;
}
