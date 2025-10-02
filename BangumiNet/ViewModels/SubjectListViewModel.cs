using BangumiNet.Api.V0.Models;

namespace BangumiNet.ViewModels;

public partial class SubjectListViewModel : ViewModelBase
{
    public void UpdateSubjects(Paged_Subject? subjects)
    {
        if (subjects?.Data != null)
            SubjectViewModels = [.. subjects.Data.Select(x => new SubjectViewModel(x))];
    }

    [Reactive] public partial ObservableCollection<ViewModelBase>? SubjectViewModels { get; set; }
}
