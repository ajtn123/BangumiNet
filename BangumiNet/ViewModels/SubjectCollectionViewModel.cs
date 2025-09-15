using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;

namespace BangumiNet.ViewModels;

public partial class SubjectCollectionViewModel : ViewModelBase
{
    [Reactive] public partial ObservableCollection<SubjectViewModel>? Collections { get; set; }
}
