using BangumiNet.Api.Html.Models;
using System.Reactive;

namespace BangumiNet.ViewModels;

public partial class CommentListViewModel : ViewModelBase
{
    public CommentListViewModel(ItemType? type, int? id)
    {
        ItemType = type;
        Id = id;

        PageNavigatorViewModel = new();

        LoadPageCommand = ReactiveCommand.CreateFromTask<int?>(LoadPageAsync);

        PageNavigatorViewModel.PrevPage.InvokeCommand(LoadPageCommand);
        PageNavigatorViewModel.NextPage.InvokeCommand(LoadPageCommand);
        PageNavigatorViewModel.JumpPage.InvokeCommand(LoadPageCommand);
    }

    public async Task LoadPageAsync(int? page)
    {
        if (ItemType is not ItemType type || Id is not int i || page is not int p) return;

        var r = await ApiC.Clients.HtmlClient.GetCommentsAsync(type, i, p);
        if (r == null) return;

        Comments = r.Comments.Select<Comment, ViewModelBase>(c => new SubjectCollectionViewModel(c)).ToObservableCollection();
        PageNavigatorViewModel.Total = r.TotalPage;
        PageNavigatorViewModel.PageIndex = r.Page;
    }

    [Reactive] public partial ObservableCollection<ViewModelBase>? Comments { get; set; }
    [Reactive] public partial ItemType? ItemType { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial PageNavigatorViewModel PageNavigatorViewModel { get; set; }

    public ReactiveCommand<int?, Unit> LoadPageCommand { get; }
}
