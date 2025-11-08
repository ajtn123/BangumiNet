using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Subjects.Item.Comments;
using System.Reactive;

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

        LoadPageCommand = ReactiveCommand.CreateFromTask<int?>(LoadPageAsync);

        PageNavigator.PrevPage.InvokeCommand(LoadPageCommand);
        PageNavigator.NextPage.InvokeCommand(LoadPageCommand);
        PageNavigator.JumpPage.InvokeCommand(LoadPageCommand);
    }

    public Task LoadPageAsync(int? page, CancellationToken cancellationToken = default)
    {
        if (ItemType is not ItemType type || Id is not int i || page is not int p) return Task.CompletedTask;
        int offset = (p - 1) * Limit;

        return type switch
        {
            Shared.ItemType.Subject => LoadSubjectComment(i, offset, cancellationToken),
            Shared.ItemType.Episode => LoadEpisodeComment(i, offset, cancellationToken),
            Shared.ItemType.Character => LoadCharacterComment(i, offset, cancellationToken),
            Shared.ItemType.Person => LoadPersonComment(i, offset, cancellationToken),
            _ => throw new NotImplementedException(),
        };
    }

    private async Task LoadSubjectComment(int id, int offset, CancellationToken cancellationToken = default)
    {
        CommentsGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Subjects[id].Comments.GetAsCommentsGetResponseAsync(config =>
            {
                config.QueryParameters.Limit = Limit;
                config.QueryParameters.Offset = offset;
                config.QueryParameters.Type = (int?)CollectionType;
            }, cancellationToken);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Data?.Select<SubjectInterestComment, ViewModelBase>(c => new SubjectCollectionViewModel(c)).ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
        Sources.Add(response);
    }
    private async Task LoadCharacterComment(int id, int offset, CancellationToken cancellationToken = default)
    {
        List<Api.P1.P1.Characters.Item.Comments.Comments>? response = null;
        try
        {
            response = await ApiC.P1.Characters[id].Comments.GetAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Select<Api.P1.P1.Characters.Item.Comments.Comments, ViewModelBase>(c => new CommentViewModel(c)).ToObservableCollection();
        PageNavigator.UpdatePageInfo(response.Count, offset, response.Count);
        Sources.Add(response);
    }
    private async Task LoadPersonComment(int id, int offset, CancellationToken cancellationToken = default)
    {
        List<Api.P1.P1.Persons.Item.Comments.Comments>? response = null;
        try
        {
            response = await ApiC.P1.Persons[id].Comments.GetAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Select<Api.P1.P1.Persons.Item.Comments.Comments, ViewModelBase>(c => new CommentViewModel(c)).ToObservableCollection();
        PageNavigator.UpdatePageInfo(response.Count, offset, response.Count);
        Sources.Add(response);
    }
    private async Task LoadEpisodeComment(int id, int offset, CancellationToken cancellationToken = default)
    {
        List<Api.P1.P1.Episodes.Item.Comments.Comments>? response = null;
        try
        {
            response = await ApiC.P1.Episodes[id].Comments.GetAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Select<Api.P1.P1.Episodes.Item.Comments.Comments, ViewModelBase>(c => new CommentViewModel(c)).ToObservableCollection();
        PageNavigator.UpdatePageInfo(response.Count, offset, response.Count);
        Sources.Add(response);
    }
    public void LoadReplies(List<Reply> replies)
    {
        if (ItemType == null) throw new InvalidOperationException();
        Sources.Add(replies);
        SubjectViewModels = replies.Select<Reply, ViewModelBase>(r => new CommentViewModel(r, (ItemType)ItemType)).ToObservableCollection();
    }

    [Reactive] public partial ObservableCollection<object> Sources { get; set; }
    [Reactive] public partial ItemType? ItemType { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial CollectionType? CollectionType { get; set; }
    [Reactive] public partial ReplyViewModel ReplyViewModel { get; set; }

    public ReactiveCommand<int?, Unit> LoadPageCommand { get; }

    public static int Limit => CurrentSettings.CommentPageSize;
}
