using BangumiNet.Api.V0.Models;

namespace BangumiNet.ViewModels;

public partial class SubjectListViewModel : ViewModelBase
{
    public void AddSubjects(Paged_Subject? subjects)
    {
        SubjectViewModels ??= [];
        if (subjects?.Data != null)
            SubjectViewModels = SubjectViewModels?.UnionBy(subjects.Data.Select(x => new SubjectViewModel(x)), s => s.Id).ToObservableCollection();
    }

    [Reactive] public partial ObservableCollection<SubjectViewModel>? SubjectViewModels { get; set; }
}
