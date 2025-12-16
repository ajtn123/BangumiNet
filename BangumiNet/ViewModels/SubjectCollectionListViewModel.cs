using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Helpers;
using BangumiNet.Api.V0.Models;
using BangumiNet.Common;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class SubjectCollectionListViewModel : SubjectListPagedViewModel, IActivatableViewModel
{
    public SubjectCollectionListViewModel(ItemType itemType, SubjectType? subjectType = null, CollectionType? collectionType = null, string? username = null)
    {
        ItemType = itemType;
        SubjectType = subjectType;
        CollectionType = collectionType;
        Username = username ?? ApiC.CurrentUsername;

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.PageNavigator.PageIndex, x => x.PageNavigator.Total).Subscribe(x =>
            {
                if (x.Item1 == null || x.Item2 == null) PageInfoMessage = $"加载中……";
                else
                {
                    var currentPage = x.Item1; var totalPage = x.Item2;
                    var startItem = (currentPage - 1) * Limit + 1;
                    var totalItem = totalPage * Limit;
                    PageInfoMessage = $"第 {startItem}-{Math.Min((int)startItem + Limit, (int)totalItem)} 个项目，共 {totalItem} 个";
                }
            }).DisposeWith(disposables);
            this.WhenAnyValue(x => x.ItemType).Skip(1).Subscribe(itemType =>
            {
                this.RaisePropertyChanged(nameof(IsSubject));
                LoadPageCommand.Execute(1).Subscribe();
            }).DisposeWith(disposables);
            this.WhenAnyValue(x => x.SubjectType).Skip(1).Subscribe(itemType =>
            {
                if (ItemType != ItemType.Subject) return;
                LoadPageCommand.Execute(1).Subscribe();
            }).DisposeWith(disposables);
            this.WhenAnyValue(x => x.CollectionType).Skip(1).Subscribe(itemType =>
            {
                if (ItemType != ItemType.Subject) return;
                LoadPageCommand.Execute(1).Subscribe();
            }).DisposeWith(disposables);

            if (SubjectViewModels == null)
                LoadPageCommand.Execute(1).Subscribe();
        });
    }

    protected override Task LoadPageAsync(int? page, CancellationToken ct = default)
    {
        Username ??= ApiC.CurrentUsername;
        if (string.IsNullOrWhiteSpace(Username)) return Task.CompletedTask;
        if (page is not int p) return Task.CompletedTask;
        return ItemType switch
        {
            ItemType.Subject => LoadSubjects(p, ct),
            ItemType.Character => LoadCharacters(p, ct),
            ItemType.Person => LoadPersons(p, ct),
            _ => throw new NotImplementedException(),
        };
    }

    private async Task LoadSubjects(int page, CancellationToken ct)
    {
        Paged_UserCollection? response = null;
        try
        {
            response = await ApiC.V0.Users[Username!].Collections.GetAsync(config =>
            {
                config.SetPage(page, Limit);
                config.QueryParameters.SubjectType = (int?)SubjectType;
                config.QueryParameters.Type = ((int?)CollectionType).ToString();
            }, ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response is null) return;

        SubjectViewModels = response.Data?
            .Select<UserSubjectCollection, ViewModelBase>(x => new SubjectCollectionViewModel(x) { ParentList = this, IsMy = Username == ApiC.CurrentUsername })
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(response);
    }
    private async Task LoadCharacters(int page, CancellationToken ct)
    {
        Paged_UserCharacterCollection? response = null;
        try
        {
            var requestInfo = ApiC.V0.Users[Username!].Collections.Minus.Characters.ToGetRequestInformation();
            requestInfo.QueryParameters.Add("offset", (page - 1) * Limit);
            requestInfo.QueryParameters.Add("limit", Limit);
            response = await ApiC.Clients.RequestAdapter0.SendAsync(requestInfo, Paged_UserCharacterCollection.CreateFromDiscriminatorValue, cancellationToken: ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response is null) return;

        SubjectViewModels = response.Data?
            .Select<UserCharacterCollection, ViewModelBase>(x => new CharacterViewModel(x))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(response);
    }
    private async Task LoadPersons(int page, CancellationToken ct)
    {
        Paged_UserPersonCollection? response = null;
        try
        {
            var requestInfo = ApiC.V0.Users[Username!].Collections.Minus.Persons.ToGetRequestInformation();
            requestInfo.QueryParameters.Add("offset", (page - 1) * Limit);
            requestInfo.QueryParameters.Add("limit", Limit);
            response = await ApiC.Clients.RequestAdapter0.SendAsync(requestInfo, Paged_UserPersonCollection.CreateFromDiscriminatorValue, cancellationToken: ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response is null) return;

        SubjectViewModels = response.Data?
            .Select<UserPersonCollection, ViewModelBase>(x => new PersonViewModel(x))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(response);
    }

    [Reactive] public partial ItemType ItemType { get; set; }
    [Reactive] public partial SubjectType? SubjectType { get; set; }
    [Reactive] public partial CollectionType? CollectionType { get; set; }
    [Reactive] public partial string? Username { get; set; }
    [Reactive] public partial string? PageInfoMessage { get; set; }

    public bool IsSubject => ItemType == ItemType.Subject;
    public override int Limit => SettingProvider.CurrentSettings.CollectionPageSize;
    public ViewModelActivator Activator { get; } = new();
}
