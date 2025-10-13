using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Html.Models;
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
    }

    public async Task LoadPageAsync(int? page)
    {
        if (ItemType is not ItemType type || Id is not int i || page is not int p) return;
        int offset = (p - 1) * Limit;

        CommentsGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Subjects[i].Comments.GetAsCommentsGetResponseAsync(config =>
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

    [Reactive] public partial ObservableCollection<object> Sources { get; set; }
    [Reactive] public partial ObservableCollection<ViewModelBase>? Comments { get; set; }
    [Reactive] public partial ItemType? ItemType { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial CollectionType? CollectionType { get; set; }
    [Reactive] public partial PageNavigatorViewModel PageNavigatorViewModel { get; set; }

    public ReactiveCommand<int?, Unit> LoadPageCommand { get; }

    public static int Limit => CurrentSettings.CommentPageSize;
}
