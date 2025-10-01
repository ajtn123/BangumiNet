using BangumiNet.Api.V0.Models;

namespace BangumiNet.ViewModels;

public partial class SubjectCollectionListViewModel : ViewModelBase
{
    public void AddSubjects(Paged_UserCollection? subjects)
    {
        SubjectCollectionViewModels ??= [];
        if (subjects?.Data != null)
            SubjectCollectionViewModels = SubjectCollectionViewModels?
                .UnionBy(subjects.Data.Select(x => new SubjectCollectionViewModel(x) { ParentList = this }), s => s.Id)
                .ToObservableCollection();
    }

    [Reactive] public partial ObservableCollection<SubjectCollectionViewModel>? SubjectCollectionViewModels { get; set; }
}
