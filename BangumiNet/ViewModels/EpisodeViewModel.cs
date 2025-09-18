using BangumiNet.Api.V0.Models;
using ReactiveUI.SourceGenerators;
using System.Linq;

namespace BangumiNet.ViewModels;

public partial class EpisodeViewModel : ViewModelBase
{
    public EpisodeViewModel(Episode episode)
    {
        Source = episode;
        Id = episode.Id;

        Ep = episode.Ep;
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial double? Ep { get; set; }
}
