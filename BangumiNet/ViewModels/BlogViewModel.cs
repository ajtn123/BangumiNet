using BangumiNet.Api.P1.Models;

namespace BangumiNet.ViewModels;

public partial class BlogViewModel : ItemViewModelBase
{
    public BlogViewModel(BlogEntry blog)
    {
        Source = blog;
        Content = blog.Content;
        Name = blog.Title;
        Image = blog.Icon;
        Id = blog.Id;
        NoReply = blog.Noreply;
        CreationTime = CommonUtils.ParseBangumiTime(blog.CreatedAt);
        UpdateTime = CommonUtils.ParseBangumiTime(blog.UpdatedAt);
        Views = blog.Views;
        IsPublic = blog.Public ?? true;
        Related = blog.Related;
        ReplyCount = blog.Replies;
        Type = blog.Type;
        Tags = blog.Tags?.ToObservableCollection();
        if (blog.User != null)
            User = new(blog.User) { Id = blog.Uid };

        Init();
    }
    public BlogViewModel(SlimBlogEntry blog)
    {
        Source = blog;
        Name = blog.Title;
        Content = blog.Summary;
        Image = blog.Icon;
        Id = blog.Id;
        CreationTime = CommonUtils.ParseBangumiTime(blog.CreatedAt);
        UpdateTime = CommonUtils.ParseBangumiTime(blog.UpdatedAt);
        IsPublic = blog.Public ?? true;
        ReplyCount = blog.Replies;
        Type = blog.Type;
        if (blog.User != null)
            User = new(blog.User) { Id = blog.Uid };

        Init();
    }

    private void Init()
    {
        ItemType = ItemType.Blog;
    }

    [Reactive] public partial string? Content { get; set; }
    [Reactive] public partial string? Image { get; set; }
    [Reactive] public partial int? NoReply { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
    [Reactive] public partial DateTimeOffset? UpdateTime { get; set; }
    [Reactive] public partial int? Views { get; set; }
    [Reactive] public partial int? Related { get; set; }
    [Reactive] public partial int? ReplyCount { get; set; }
    [Reactive] public partial int? Type { get; set; }
    [Reactive] public partial bool IsPublic { get; set; }
    [Reactive] public partial UserViewModel? User { get; set; }
    [Reactive] public partial ObservableCollection<string>? Tags { get; set; }
}
