using BangumiNet.Api.V0.Models;

namespace BangumiNet.ViewModels;

public partial class SubjectListViewModel : ViewModelBase
{
    public void UpdateItems(Paged_Subject? subjects)
    {
        if (subjects?.Data != null)
            SubjectViewModels = [.. subjects.Data.Select(x => new SubjectViewModel(x))];
    }
    public void UpdateItems(Paged_Character? characters)
    {
        if (characters?.Data != null)
            SubjectViewModels = [.. characters.Data.Select(x => new CharacterViewModel(x))];
    }
    public void UpdateItems(Paged_Person? persons)
    {
        if (persons?.Data != null)
            SubjectViewModels = [.. persons.Data.Select(x => new PersonViewModel(x))];
    }

    [Reactive] public partial ObservableCollection<ViewModelBase>? SubjectViewModels { get; set; }
}
