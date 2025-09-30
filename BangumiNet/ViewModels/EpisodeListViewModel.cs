using BangumiNet.Api.V0.Models;
using BangumiNet.Api.V0.V0.Users.Collections.Item.Episodes;
using System.Reactive;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class EpisodeListViewModel : ViewModelBase
{
    public EpisodeListViewModel(int subjectId)
    {
        SubjectId = subjectId;
        Offset = 0;
        EpisodeViewModels = [];
        Sources = [];

        this.WhenAnyValue(x => x.Offset, x => x.EpTotal).Subscribe(x => IsAllEpisodesLoaded = Offset >= EpTotal);

        LoadEpisodesCommand = ReactiveCommand.CreateFromTask(async () => await LoadEpisodes(), this.WhenAnyValue(x => x.IsAllEpisodesLoaded).Select(x => !x));
    }

    /// <returns>是否已取得全部集数</returns>
    public async Task<bool?> LoadEpisodes(int limit = 100)
    {
        if (Offset >= EpTotal) return true;

        if (ApiC.IsAuthenticated)
        {
            EpisodesGetResponse? epCole = null;
            try
            {
                epCole = await ApiC.V0.Users.Minus.Collections[SubjectId].Episodes.GetAsEpisodesGetResponseAsync(config =>
                {
                    config.QueryParameters.Limit = limit;
                    config.QueryParameters.Offset = Offset;
                    config.QueryParameters.EpisodeType = null;
                });
            }
            catch (Exception e) { Trace.TraceError(e.Message); return null; }

            if (epCole == null) return null;
            Sources.Add(epCole);
            EpTotal = epCole.Total;

            if (epCole.Data is { } episodes)
            {
                Offset += episodes.Count;
                EpisodeViewModels = EpisodeViewModels
                    .UnionBy(episodes.Select(EpisodeViewModel.InitFormCollection), e => e.Id)
                    .Select(x => { x.Parent = this; return x; })
                    .ToArray().LinkNeighbors().ToObservableCollection()!;
            }
        }
        else
        {
            Paged_Episode? epPage = null;
            try
            {
                epPage = await ApiC.V0.Episodes.GetAsync(config =>
                {
                    config.QueryParameters.Limit = limit;
                    config.QueryParameters.Offset = Offset;
                    config.QueryParameters.Type = null;
                    config.QueryParameters.SubjectId = SubjectId;
                });
            }
            catch (Exception e) { Trace.TraceError(e.Message); return null; }

            if (epPage == null) return null;
            Sources.Add(epPage);
            EpTotal = epPage.Total;

            if (epPage.Data is { } episodes)
            {
                Offset += episodes.Count;
                EpisodeViewModels = EpisodeViewModels
                    .UnionBy(episodes.Select(p => new EpisodeViewModel(p)), e => e.Id)
                    .ToArray().LinkNeighbors().ToObservableCollection()!;
            }
        }

        return Offset >= EpTotal;
    }

    [Reactive] public partial ObservableCollection<object> Sources { get; set; }
    [Reactive] public partial ObservableCollection<EpisodeViewModel> EpisodeViewModels { get; set; }
    [Reactive] public partial int SubjectId { get; set; }
    [Reactive] public partial int? EpTotal { get; set; }
    [Reactive] public partial int Offset { get; set; }
    [Reactive] public partial bool IsAllEpisodesLoaded { get; set; }

    public ReactiveCommand<Unit, bool?> LoadEpisodesCommand { get; }
}
