using BangumiNet.Api.V0.Models;
using BangumiNet.Utils;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiNet.ViewModels;

public partial class EpisodeListViewModel : ViewModelBase
{
    public EpisodeListViewModel(int? subjectId)
    {
        SubjectId = subjectId;
        EpisodeViewModels = [];
        Sources = [];
    }

    private int lastOffset = 0;
    /// <param name="offset">分页 offset</param>
    /// <param name="limit">每页集数</param>
    /// <returns>是否已取得全部集数</returns>
    public async Task<bool?> LoadEpisodes(int? offset = null, int limit = 100)
    {
        offset ??= lastOffset;
        if (offset >= EpTotal) return true;
        lastOffset += limit;

        var epPage = await ApiC.V0.Episodes.GetAsync(config =>
        {
            config.QueryParameters.Limit = limit;
            config.QueryParameters.Offset = 0;
            config.QueryParameters.Type = null;
            config.QueryParameters.SubjectId = SubjectId;
        });

        if (epPage == null) return null;
        Sources.Add(epPage);
        EpTotal = epPage.Total;

        if (epPage.Data is { } episodes)
            EpisodeViewModels= EpisodeViewModels.UnionBy(episodes.Select(p => new EpisodeViewModel(p)), e => e.Id).ToArray().LinkNeighbors().ToObservableCollection()!;

        return EpisodeViewModels.Count >= EpTotal;
    }

    [Reactive] public partial ObservableCollection<Paged_Episode> Sources { get; set; }
    [Reactive] public partial ObservableCollection<EpisodeViewModel> EpisodeViewModels { get; set; }
    [Reactive] public partial int? SubjectId { get; set; }
    [Reactive] public partial int? EpTotal { get; set; }
}
