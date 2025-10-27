using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Subjects.Item.Comments;
using System.Reactive;

namespace BangumiNet.ViewModels;

public partial class CommentListViewModel : ViewModelBase
{
    public CommentListViewModel(ItemType? type, int? id)
    {
        ItemType = type;
        Id = id;

        Sources = [];
        PageNavigatorViewModel = new();

        LoadPageCommand = ReactiveCommand.CreateFromTask<int?>(LoadPageAsync);

        PageNavigatorViewModel.PrevPage.InvokeCommand(LoadPageCommand);
        PageNavigatorViewModel.NextPage.InvokeCommand(LoadPageCommand);
        PageNavigatorViewModel.JumpPage.InvokeCommand(LoadPageCommand);
        ReplyViewModel = new(this);
    }

    public Task LoadPageAsync(int? page)
    {
        if (ItemType is not ItemType type || Id is not int i || page is not int p) return Task.CompletedTask;
        int offset = (p - 1) * Limit;

        return type switch
        {
            Shared.ItemType.Subject => LoadSubjectComment(i, offset),
            Shared.ItemType.Episode => LoadEpisodeComment(i, offset),
            Shared.ItemType.Character => LoadCharacterComment(i, offset),
            Shared.ItemType.Person => LoadPersonComment(i, offset),
            _ => throw new NotImplementedException(),
        };
    }

    private async Task LoadSubjectComment(int id, int offset)
    {
        CommentsGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Subjects[id].Comments.GetAsCommentsGetResponseAsync(config =>
            {
                config.QueryParameters.Limit = Limit;
                config.QueryParameters.Offset = offset;
                config.QueryParameters.Type = (int?)CollectionType;
            });
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        Comments = response.Data?.Select<SubjectInterestComment, ViewModelBase>(c => new SubjectCollectionViewModel(c)).ToObservableCollection();
        PageNavigatorViewModel.UpdatePageInfo(Limit, offset, response.Total);
        Sources.Add(response);
    }
    private async Task LoadCharacterComment(int id, int offset)
    {
        List<Api.P1.P1.Characters.Item.Comments.Comments>? response = null;
        try
        {
            response = await ApiC.P1.Characters[id].Comments.GetAsync();
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        Comments = response.Select<Api.P1.P1.Characters.Item.Comments.Comments, ViewModelBase>(c => new CommentViewModel(c)).ToObservableCollection();
        PageNavigatorViewModel.UpdatePageInfo(response.Count, offset, response.Count);
        Sources.Add(response);
    }
    private async Task LoadPersonComment(int id, int offset)
    {
        List<Api.P1.P1.Persons.Item.Comments.Comments>? response = null;
        try
        {
            response = await ApiC.P1.Persons[id].Comments.GetAsync();
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        Comments = response.Select<Api.P1.P1.Persons.Item.Comments.Comments, ViewModelBase>(c => new CommentViewModel(c)).ToObservableCollection();
        PageNavigatorViewModel.UpdatePageInfo(response.Count, offset, response.Count);
        Sources.Add(response);
    }
    private async Task LoadEpisodeComment(int id, int offset)
    {
        List<Api.P1.P1.Episodes.Item.Comments.Comments>? response = null;
        try
        {
            response = await ApiC.P1.Episodes[id].Comments.GetAsync();
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        Comments = response.Select<Api.P1.P1.Episodes.Item.Comments.Comments, ViewModelBase>(c => new CommentViewModel(c)).ToObservableCollection();
        PageNavigatorViewModel.UpdatePageInfo(response.Count, offset, response.Count);
        Sources.Add(response);
    }

    [Reactive] public partial ObservableCollection<object> Sources { get; set; }
    [Reactive] public partial ObservableCollection<ViewModelBase>? Comments { get; set; }
    [Reactive] public partial ItemType? ItemType { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial CollectionType? CollectionType { get; set; }
    [Reactive] public partial PageNavigatorViewModel PageNavigatorViewModel { get; set; }
    [Reactive] public partial ReplyViewModel ReplyViewModel { get; set; }

    public ReactiveCommand<int?, Unit> LoadPageCommand { get; }

    public bool IsSinglePage => ItemType == Shared.ItemType.Character || ItemType == Shared.ItemType.Person || ItemType == Shared.ItemType.Episode;

    public static int Limit => CurrentSettings.CommentPageSize;
}
