using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using System.Reactive;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class SubjectCollectionListViewModel : ViewModelBase, ILoadable
{
    public SubjectCollectionListViewModel(ItemType itemType, SubjectType? subjectType = null, CollectionType? collectionType = null, string? username = null)
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
        this.WhenAnyValue(x => x.ItemType).Skip(1).Subscribe(itemType =>
        {
            this.RaisePropertyChanged(nameof(IsSubject));
            _ = LoadPageAsync(1);
        });
        this.WhenAnyValue(x => x.SubjectType).Skip(1).Subscribe(itemType =>
        {
            if (ItemType != ItemType.Subject) return;
            _ = LoadPageAsync(1);
        });
        this.WhenAnyValue(x => x.CollectionType).Skip(1).Subscribe(itemType =>
        {
            if (ItemType != ItemType.Subject) return;
            _ = LoadPageAsync(1);
        });
    }

    public Task Load(CancellationToken ct = default) => LoadPageAsync(1, ct);
    public Task LoadPageAsync(int? i, CancellationToken ct = default)
    {
        Username ??= ApiC.CurrentUsername;
        if (string.IsNullOrWhiteSpace(Username)) return Task.CompletedTask;
        if (i is not int pageIndex || !PageNavigatorViewModel.IsInRange(i)) return Task.CompletedTask;
        return ItemType switch
        {
            ItemType.Subject => LoadSubjects(pageIndex, ct),
            ItemType.Character => LoadCharacters(pageIndex, ct),
            ItemType.Person => LoadPersons(pageIndex, ct),
            _ => throw new NotImplementedException(),
        };
    }

    private async Task LoadSubjects(int pageIndex, CancellationToken ct = default)
    {
        Paged_UserCollection? collection = null;
        try
        {
            collection = await ApiC.V0.Users[Username!].Collections.GetAsync(config =>
            {
                config.QueryParameters.SubjectType = (int?)SubjectType;
                config.QueryParameters.Type = ((int?)CollectionType).ToString();
                config.QueryParameters.Offset = (pageIndex - 1) * Limit;
                config.QueryParameters.Limit = Limit;
            }, ct);
        }
        catch (ErrorDetail e)
        {
            Trace.TraceError(e.Message);
            return;
        }
        if (collection is null) return;
        UpdateItems(collection);
    }
    private async Task LoadCharacters(int pageIndex, CancellationToken ct = default)
    {
        Paged_UserCharacterCollection? collection = null;
        try
        {
            var requestInfo = ApiC.V0.Users[Username!].Collections.Minus.Characters.ToGetRequestInformation();
            requestInfo.QueryParameters.Add("offset", (pageIndex - 1) * Limit);
            requestInfo.QueryParameters.Add("limit", Limit);
            collection = await ApiC.Clients.RequestAdapter0.SendAsync(requestInfo, Paged_UserCharacterCollection.CreateFromDiscriminatorValue, cancellationToken: ct);
        }
        catch (ErrorDetail e)
        {
            Trace.TraceError(e.Message);
            return;
        }
        if (collection is null) return;
        UpdateItems(collection);
    }
    private async Task LoadPersons(int pageIndex, CancellationToken ct = default)
    {
        Paged_UserPersonCollection? collection = null;
        try
        {
            var requestInfo = ApiC.V0.Users[Username!].Collections.Minus.Persons.ToGetRequestInformation();
            requestInfo.QueryParameters.Add("offset", (pageIndex - 1) * Limit);
            requestInfo.QueryParameters.Add("limit", Limit);
            collection = await ApiC.Clients.RequestAdapter0.SendAsync(requestInfo, Paged_UserPersonCollection.CreateFromDiscriminatorValue, cancellationToken: ct);
        }
        catch (ErrorDetail e)
        {
            Trace.TraceError(e.Message);
            return;
        }
        if (collection is null) return;
        UpdateItems(collection);
    }

    private void UpdateItems(Paged_UserCollection subjects)
    {
        if (subjects.Data != null)
            SubjectList.SubjectViewModels = [.. subjects.Data.Select(x => new SubjectCollectionViewModel(x) { ParentList = this, IsMy = Username == ApiC.CurrentUsername })];

        UpdatePage(subjects);
    }
    private void UpdateItems(Paged_UserCharacterCollection characters)
    {
        if (characters.Data != null)
            SubjectList.SubjectViewModels = [.. characters.Data.Select(x => new CharacterViewModel(x))];

        UpdatePage(characters);
    }
    private void UpdateItems(Paged_UserPersonCollection persons)
    {
        if (persons.Data != null)
            SubjectList.SubjectViewModels = [.. persons.Data.Select(x => new PersonViewModel(x))];

        UpdatePage(persons);
    }
    private void UpdatePage(IPagedResponse response)
    {
        Total = response.Total;
        Offset = response.Offset;
        PageNavigatorViewModel.UpdatePageInfo(response);
        Sources.Add(response);
    }

    [Reactive] public partial ObservableCollection<object> Sources { get; set; }
    [Reactive] public partial SubjectListViewModel SubjectList { get; set; }
    [Reactive] public partial ItemType ItemType { get; set; }
    [Reactive] public partial SubjectType? SubjectType { get; set; }
    [Reactive] public partial CollectionType? CollectionType { get; set; }
    [Reactive] public partial string? Username { get; set; }
    [Reactive] public partial int? Total { get; set; }
    [Reactive] public partial int? Offset { get; set; }
    [Reactive] public partial string? PageInfoMessage { get; set; }
    [Reactive] public partial PageNavigatorViewModel PageNavigatorViewModel { get; set; }

    public ReactiveCommand<int?, Unit> LoadPageCommand { get; }

    public bool IsSubject => ItemType == ItemType.Subject;

    public static int Limit => SettingProvider.CurrentSettings.CollectionPageSize;
}
