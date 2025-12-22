using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Helpers;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Users.Item.Collections.Characters;
using BangumiNet.Api.P1.P1.Users.Item.Collections.Indexes;
using BangumiNet.Api.P1.P1.Users.Item.Collections.Persons;
using BangumiNet.Api.P1.P1.Users.Item.Collections.Subjects;
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
        Username = username;

        this.WhenActivated(disposables =>
        {
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
        if (page is not int p) return Task.CompletedTask;
        var offset = (p - 1) * Limit;

        if (Username == null || Username == ApiC.CurrentUsername)
            return ItemType switch
            {
                ItemType.Subject => LoadSubjects(offset, ct),
                ItemType.Character => LoadCharacters(offset, ct),
                ItemType.Person => LoadPersons(offset, ct),
                ItemType.Index => LoadIndexes(offset, ct),
                _ => throw new NotImplementedException(),
            };
        else if (Username is string username)
            return ItemType switch
            {
                ItemType.Subject => LoadSubjects(username, offset, ct),
                ItemType.Character => LoadCharacters(username, offset, ct),
                ItemType.Person => LoadPersons(username, offset, ct),
                ItemType.Index => LoadIndexes(username, offset, ct),
                _ => throw new NotImplementedException(),
            };

        // should be unreachable
        else throw new Exception();
    }

    private async Task LoadSubjects(string username, int offset, CancellationToken ct)
    {
        SubjectsGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Users[username].Collections.Subjects.GetAsync(config =>
            {
                config.Paging(Limit, offset);
                config.QueryParameters.SubjectType = (int?)SubjectType;
                config.QueryParameters.Type = (int?)CollectionType;
            }, ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response is null) return;

        SubjectViewModels = response.Data?
            .Select<SlimSubject, ViewModelBase>(x =>
            {
                var vm = new SubjectViewModel(x).SubjectCollectionViewModel;
                vm?.ParentList = this;
                vm?.IsMy = username == ApiC.CurrentUsername;
                return vm ?? new ViewModelBase();
            }).ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }
    private async Task LoadCharacters(string username, int offset, CancellationToken ct)
    {
        CharactersGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Users[username].Collections.Characters.GetAsync(config =>
            {
                config.Paging(Limit, offset);
            }, ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response is null) return;

        SubjectViewModels = response.Data?
            .Select<SlimCharacter, ViewModelBase>(x => new CharacterViewModel(x))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }
    private async Task LoadPersons(string username, int offset, CancellationToken ct)
    {
        PersonsGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Users[username].Collections.Persons.GetAsync(config =>
            {
                config.Paging(Limit, offset);
            }, ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response is null) return;

        SubjectViewModels = response.Data?
            .Select<SlimPerson, ViewModelBase>(x => new PersonViewModel(x))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }
    private async Task LoadIndexes(string username, int offset, CancellationToken ct)
    {
        IndexesGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Users[username].Collections.Indexes.GetAsync(config =>
            {
                config.Paging(Limit, offset);
            }, ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response is null) return;

        SubjectViewModels = response.Data?
            .Select<SlimIndex, ViewModelBase>(x => new IndexViewModel(x))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }
    private async Task LoadSubjects(int offset, CancellationToken ct)
    {
        Api.P1.P1.Collections.Subjects.SubjectsGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Collections.Subjects.GetAsync(config =>
            {
                config.Paging(Limit, offset);
                config.QueryParameters.SubjectType = (int?)SubjectType;
                config.QueryParameters.Type = (int?)CollectionType;
            }, ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response is null) return;

        SubjectViewModels = response.Data?
            .Select<Subject, ViewModelBase>(x =>
            {
                var vm = new SubjectViewModel(x).SubjectCollectionViewModel;
                vm?.ParentList = this;
                vm?.IsMy = true;
                return vm ?? new ViewModelBase();
            }).ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }
    private async Task LoadCharacters(int offset, CancellationToken ct)
    {
        Api.P1.P1.Collections.Characters.CharactersGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Collections.Characters.GetAsync(config =>
            {
                config.Paging(Limit, offset);
            }, ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response is null) return;

        SubjectViewModels = response.Data?
            .Select<Character, ViewModelBase>(x => new CharacterViewModel(x))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }
    private async Task LoadPersons(int offset, CancellationToken ct)
    {
        Api.P1.P1.Collections.Persons.PersonsGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Collections.Persons.GetAsync(config =>
            {
                config.Paging(Limit, offset);
            }, ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response is null) return;

        SubjectViewModels = response.Data?
            .Select<Person, ViewModelBase>(x => new PersonViewModel(x))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }
    private async Task LoadIndexes(int offset, CancellationToken ct)
    {
        Api.P1.P1.Collections.Indexes.IndexesGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Collections.Indexes.GetAsync(config =>
            {
                config.Paging(Limit, offset);
            }, ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response is null) return;

        SubjectViewModels = response.Data?
            .Select<IndexObject, ViewModelBase>(x => new IndexViewModel(x))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }

    [Reactive] public partial ItemType ItemType { get; set; }
    [Reactive] public partial SubjectType? SubjectType { get; set; }
    [Reactive] public partial CollectionType? CollectionType { get; set; }
    [Reactive] public partial string? Username { get; set; }
    [Reactive] public partial string? PageInfoMessage { get; set; }

    public bool IsSubject => ItemType == ItemType.Subject;
    public override int Limit => SettingProvider.Current.CollectionPageSize;
    public ViewModelActivator Activator { get; } = new();
}
