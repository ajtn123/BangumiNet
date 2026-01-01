using BangumiNet.Api.Helpers;
using BangumiNet.Api.V0.Models;
using BangumiNet.Converters;

namespace BangumiNet.ViewModels;

public partial class RevisionListViewModel : SubjectListPagedViewModel
{
    public RevisionListViewModel(ItemViewModelBase item)
    {
        Parent = item;
        ParentItemType = item.ItemType;
        ParentId = item.Id;

        Title = $"修订历史 - {NameCnCvt.Convert(Parent) ?? $"{ParentItemType} {ParentId}"} - {Title}";
    }

    protected override async Task LoadPageAsync(int? page, CancellationToken cancellationToken = default)
    {
        if (page is not int p) return;

        Paged_Revision? response = null;
        try
        {
            response = ParentItemType switch
            {
                ItemType.Subject => await ApiC.V0.Revisions.Subjects.GetAsync(config =>
                {
                    config.QueryParameters.SubjectId = ParentId;
                    config.SetPage(p, Limit);
                }, cancellationToken),
                ItemType.Episode => await ApiC.V0.Revisions.Episodes.GetAsync(config =>
                {
                    config.QueryParameters.EpisodeId = ParentId;
                    config.SetPage(p, Limit);
                }, cancellationToken),
                ItemType.Character => await ApiC.V0.Revisions.Characters.GetAsync(config =>
                {
                    config.QueryParameters.CharacterId = ParentId;
                    config.SetPage(p, Limit);
                }, cancellationToken),
                ItemType.Person => await ApiC.V0.Revisions.Persons.GetAsync(config =>
                {
                    config.QueryParameters.PersonId = ParentId;
                    config.SetPage(p, Limit);
                }, cancellationToken),
                _ => throw new NotImplementedException(),
            };
        }
        catch (ErrorDetail e) { Trace.TraceError(e.ToString()); }
        if (response is null) return;

        SubjectViewModels = response.Data?.Select<Revision, ViewModelBase>(x => new RevisionViewModel(x, Parent)).ToObservableCollection();
        PageNavigator.UpdatePageInfo(response);
    }

    [Reactive] public partial ItemViewModelBase? Parent { get; set; }
    [Reactive] public partial ItemType ParentItemType { get; set; }
    [Reactive] public partial int? ParentId { get; set; }

    public override int Limit => Settings.RevisionPageSize;
}
