using BangumiNet.Api.V0.Models;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;

namespace BangumiNet.ViewModels;

public partial class SubjectCollectionViewModel : ViewModelBase
{
    public SubjectCollectionViewModel()
    {
        Collections = [];
    }
    [Reactive] public partial ObservableCollection<UserSubjectCollection> Collections { get; set; }
}
