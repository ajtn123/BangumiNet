using BangumiNet.Api.V0.Models;
using BangumiNet.Converters;
using System.Reactive;

namespace BangumiNet.ViewModels;

public partial class RevisionListViewModel : ViewModelBase
{
    public RevisionListViewModel(ItemViewModelBase item)
    {
        Parent = item;
        ItemType = item.ItemTypeEnum;
        ItemId = item.Id;
        Offset = 0;
        Sources = [];
        RevisionList = new();
        PageNavigatorViewModel = new();

        Title = $"修订历史 - {NameCnCvt.Convert(Parent) ?? $"{ItemType} {ItemId}"} - {Title}";

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

    public async Task LoadPageAsync(int? i, CancellationToken cancellationToken = default)
    {
        if (i is not int pageIndex || !PageNavigatorViewModel.IsInRange(i)) return;

        Paged_Revision? revs = null;
        try
        {
            revs = ItemType switch
            {
                ItemType.Subject => await ApiC.V0.Revisions.Subjects.GetAsync(config =>
                {
                    config.QueryParameters.SubjectId = ItemId;
                    config.QueryParameters.Offset = (pageIndex - 1) * Limit;
                    config.QueryParameters.Limit = Limit;
                }, cancellationToken),
                ItemType.Episode => await ApiC.V0.Revisions.Episodes.GetAsync(config =>
                {
                    config.QueryParameters.EpisodeId = ItemId;
                    config.QueryParameters.Offset = (pageIndex - 1) * Limit;
                    config.QueryParameters.Limit = Limit;
                }, cancellationToken),
                ItemType.Character => await ApiC.V0.Revisions.Characters.GetAsync(config =>
                {
                    config.QueryParameters.CharacterId = ItemId;
                    config.QueryParameters.Offset = (pageIndex - 1) * Limit;
                    config.QueryParameters.Limit = Limit;
                }, cancellationToken),
                ItemType.Person => await ApiC.V0.Revisions.Persons.GetAsync(config =>
                {
                    config.QueryParameters.PersonId = ItemId;
                    config.QueryParameters.Offset = (pageIndex - 1) * Limit;
                    config.QueryParameters.Limit = Limit;
                }, cancellationToken),
                _ => throw new NotImplementedException(),
            };
        }
        catch (ErrorDetail e)
        {
            Trace.TraceError(e.Message);
            return;
        }
        if (revs is null) return;

        if (revs.Data != null)
            RevisionList.SubjectViewModels = [.. revs.Data.Select(x => new RevisionViewModel(x))];

        Total = revs.Total;
        Offset = revs.Offset;
        PageNavigatorViewModel.UpdatePageInfo(revs);
        Sources.Add(revs);
    }

    [Reactive] public partial ItemViewModelBase? Parent { get; set; }
    [Reactive] public partial ObservableCollection<object> Sources { get; set; }
    [Reactive] public partial SubjectListViewModel RevisionList { get; set; }
    [Reactive] public partial ItemType ItemType { get; set; }
    [Reactive] public partial int? ItemId { get; set; }
    [Reactive] public partial int? Total { get; set; }
    [Reactive] public partial int? Offset { get; set; }
    [Reactive] public partial string? PageInfoMessage { get; set; }
    [Reactive] public partial PageNavigatorViewModel PageNavigatorViewModel { get; set; }

    public ReactiveCommand<int?, Unit> LoadPageCommand { get; }

    public static int Limit => CurrentSettings.RevisionPageSize;
}
