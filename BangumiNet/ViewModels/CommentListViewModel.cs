using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Helpers;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Subjects.Item.Comments;

namespace BangumiNet.ViewModels;

public partial class CommentListViewModel : SubjectListPagedViewModel
{
    public CommentListViewModel(ItemType? type, int? id)
    {
        ItemType = type;
        Id = id;

        Sources = [];
        ReplyViewModel = new(this)
        {
            IsVisible = ItemType is not Shared.ItemType.Subject
        };
        PageNavigator.IsVisible = ItemType is Shared.ItemType.Subject;
    }

    protected override Task LoadPageAsync(int? page, CancellationToken cancellationToken = default)
    {
        if (ItemType is not ItemType type || Id is not int i) return Task.CompletedTask;

        return type switch
        {
            Shared.ItemType.Subject => LoadSubjectComment(i, page, cancellationToken),
            Shared.ItemType.Episode => LoadEpisodeComment(i, cancellationToken),
            Shared.ItemType.Character => LoadCharacterComment(i, cancellationToken),
            Shared.ItemType.Person => LoadPersonComment(i, cancellationToken),
            Shared.ItemType.Blog => LoadBlogComment(i, cancellationToken),
            Shared.ItemType.Index => LoadIndexComment(i, cancellationToken),
            Shared.ItemType.Timeline => LoadTimelineComment(i, cancellationToken),
            _ => throw new NotImplementedException(),
        };
    }

    private async Task LoadSubjectComment(int id, int? page, CancellationToken cancellationToken = default)
    {
        if (page is not int p) return;
        int offset = (p - 1) * Limit;

        CommentsGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Subjects[id].Comments.GetAsync(config =>
            {
                config.Paging(Limit, offset);
                config.QueryParameters.Type = (int?)CollectionType;
            }, cancellationToken);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Data?.Select<SubjectInterestComment, ViewModelBase>(c => new SubjectCollectionViewModel(c)).ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
        Sources.Add(response);
    }
    private async Task LoadCharacterComment(int id, CancellationToken cancellationToken = default)
    {
        List<Api.P1.P1.Characters.Item.Comments.Comments>? response = null;
        try
        {
            response = await ApiC.P1.Characters[id].Comments.GetAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Select<Api.P1.P1.Characters.Item.Comments.Comments, ViewModelBase>(c => new CommentViewModel(c)).ToObservableCollection();
        Sources.Add(response);
    }
    private async Task LoadPersonComment(int id, CancellationToken cancellationToken = default)
    {
        List<Api.P1.P1.Persons.Item.Comments.Comments>? response = null;
        try
        {
            response = await ApiC.P1.Persons[id].Comments.GetAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Select<Api.P1.P1.Persons.Item.Comments.Comments, ViewModelBase>(c => new CommentViewModel(c)).ToObservableCollection();
        Sources.Add(response);
    }
    private async Task LoadEpisodeComment(int id, CancellationToken cancellationToken = default)
    {
        List<Api.P1.P1.Episodes.Item.Comments.Comments>? response = null;
        try
        {
            response = await ApiC.P1.Episodes[id].Comments.GetAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Select<Api.P1.P1.Episodes.Item.Comments.Comments, ViewModelBase>(c => new CommentViewModel(c)).ToObservableCollection();
        Sources.Add(response);
    }
    private async Task LoadBlogComment(int id, CancellationToken cancellationToken = default)
    {
        List<Api.P1.P1.Blogs.Item.Comments.Comments>? response = null;
        try
        {
            response = await ApiC.P1.Blogs[id].Comments.GetAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Select<Api.P1.P1.Blogs.Item.Comments.Comments, ViewModelBase>(c => new CommentViewModel(c)).ToObservableCollection();
        Sources.Add(response);
    }
    private async Task LoadIndexComment(int id, CancellationToken cancellationToken = default)
    {
        List<Api.P1.P1.Indexes.Item.Comments.Comments>? response = null;
        try
        {
            response = await ApiC.P1.Indexes[id].Comments.GetAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Select<Api.P1.P1.Indexes.Item.Comments.Comments, ViewModelBase>(c => new CommentViewModel(c)).ToObservableCollection();
        Sources.Add(response);
    }
    private async Task LoadTimelineComment(int id, CancellationToken cancellationToken = default)
    {
        List<Api.P1.P1.Timeline.Item.Replies.Replies>? response = null;
        try
        {
            response = await ApiC.P1.Timeline[id].Replies.GetAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Select<Api.P1.P1.Timeline.Item.Replies.Replies, ViewModelBase>(c => new CommentViewModel(c)).ToObservableCollection();
        Sources.Add(response);
    }
    public void LoadReplies(List<Reply> replies)
    {
        if (ItemType == null) throw new InvalidOperationException();
        Sources.Add(replies);
        SubjectViewModels = replies.Select<Reply, ViewModelBase>(r => new CommentViewModel(r, (ItemType)ItemType, Id, ParentItemType)).ToObservableCollection();
    }

    [Reactive] public partial ObservableCollection<object> Sources { get; set; }
    [Reactive] public partial ItemType? ItemType { get; set; }
    [Reactive] public partial ItemType? ParentItemType { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial CollectionType? CollectionType { get; set; }
    [Reactive] public partial ReplyViewModel ReplyViewModel { get; set; }

    public override int Limit => CurrentSettings.CommentPageSize;
}
