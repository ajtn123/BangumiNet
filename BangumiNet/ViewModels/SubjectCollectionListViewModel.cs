using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.V0.Models;
using System.Reactive;

namespace BangumiNet.ViewModels;

public partial class SubjectCollectionListViewModel : ViewModelBase
{
    public SubjectCollectionListViewModel(ItemType itemType, SubjectType? subjectType, CollectionType? collectionType, string? username = null)
    {
        ItemType = itemType;
        SubjectType = subjectType;
        CollectionType = collectionType;
        Username = username ?? ApiC.CurrentUsername;
        Offset = 0;
        Sources = [];
        SubjectList = new();
        PageNavigatorViewModel = new();

        LoadPageCommand = ReactiveCommand.CreateFromTask<int?>(LoadPageAsync);

        PageNavigatorViewModel.PrevPage.InvokeCommand(LoadPageCommand);
        PageNavigatorViewModel.NextPage.InvokeCommand(LoadPageCommand);
        PageNavigatorViewModel.JumpPage.InvokeCommand(LoadPageCommand);

        this.WhenAnyValue(x => x.Offset, x => x.Total).Subscribe(x =>
        {
            if (Offset == null || Total == null) PageInfoMessage = $"加载中……";
            else PageInfoMessage = $"第 {Offset + 1}-{Math.Min((int)Offset + Limit, (int)Total)} 个项目，共 {Total} 个";
        });
    }

    public async Task LoadPageAsync(int? i)
    {
        if (string.IsNullOrWhiteSpace(Username)) return;
        if (i is not int pageIndex || !PageNavigatorViewModel.IsInRange(i)) return;
        Paged_UserCollection? collection = null;
        try
        {
            collection = await ApiC.V0.Users[Username].Collections.GetAsync(config =>
            {
                config.QueryParameters.SubjectType = (int?)SubjectType;
                config.QueryParameters.Type = ((int?)CollectionType).ToString();
                config.QueryParameters.Offset = (pageIndex - 1) * Limit;
                config.QueryParameters.Limit = Limit;
            });
        }
        catch (ErrorDetail e)
        {
            Trace.TraceError(e.Message);
            return;
        }
        if (collection is null) return;
        Sources.Add(collection);
        UpdateItems(collection);
        PageNavigatorViewModel.PageIndex = pageIndex;
        Total = collection.Total;
        Offset = collection.Offset;
        if (collection.Total != null)
            PageNavigatorViewModel.Total = (int)Math.Ceiling((double)collection.Total / Limit);
        else PageNavigatorViewModel.Total = null;
    }

    public void UpdateItems(Paged_UserCollection? subjects)
    {
        if (subjects?.Data != null)
            SubjectList.SubjectViewModels = [.. subjects.Data.Select(x => new SubjectCollectionViewModel(x) { ParentList = this, IsMy = Username == ApiC.CurrentUsername })];
    }

    [Reactive] public partial ObservableCollection<object> Sources { get; set; }
    [Reactive] public partial SubjectListViewModel SubjectList { get; set; }
    [Reactive] public partial ItemType? ItemType { get; set; }
    [Reactive] public partial SubjectType? SubjectType { get; set; }
    [Reactive] public partial CollectionType? CollectionType { get; set; }
    [Reactive] public partial string? Username { get; set; }
    [Reactive] public partial int? Total { get; set; }
    [Reactive] public partial int? Offset { get; set; }
    [Reactive] public partial string? PageInfoMessage { get; set; }
    [Reactive] public partial PageNavigatorViewModel PageNavigatorViewModel { get; set; }

    public ReactiveCommand<int?, Unit> LoadPageCommand { get; }

    public static int Limit => SettingProvider.CurrentSettings.CollectionPageSize;
}
