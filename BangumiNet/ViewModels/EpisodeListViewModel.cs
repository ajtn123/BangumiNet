using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;

namespace BangumiNet.ViewModels;

public partial class EpisodeListViewModel : ViewModelBase
{
    public EpisodeListViewModel(int? eps)
    {

    }
    [Reactive] public partial ObservableCollection<EpisodeViewModel> EpisodeViewModels { get; set; }
}
