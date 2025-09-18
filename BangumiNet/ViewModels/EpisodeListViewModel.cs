using BangumiNet.Api.V0.Models;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;

namespace BangumiNet.ViewModels;

public partial class EpisodeListViewModel : ViewModelBase
{
    public EpisodeListViewModel(Paged_Episode? episodes)
    {
        EpisodeViewModels = [];
        PageItems = [];

        AddEpisodes(episodes);
    }

    public void AddEpisodes(Paged_Episode? episodes)
    {
        if (episodes?.Data != null)
            for (int i = 0; i < episodes.Data.Count; i++)
            {
                if (episodes.Offset is int offset)
                    if (PageItems.Contains(offset + 1)) continue;
                    else PageItems.Add(offset + i);

                EpisodeViewModels.Add(new(episodes.Data[i]));
            }
    }

    [Reactive] public partial ObservableCollection<EpisodeViewModel> EpisodeViewModels { get; set; }

    public ObservableCollection<int> PageItems { get; }
}
