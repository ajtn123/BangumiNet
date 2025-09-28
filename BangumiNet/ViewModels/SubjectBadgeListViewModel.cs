namespace BangumiNet.ViewModels;

public partial class SubjectBadgeListViewModel : ViewModelBase
{
    public SubjectBadgeListViewModel(ItemType type, int? id)
    {
        ParentType = type;
        Id = id;
    }

    public async Task LoadSubjects()
    {
        if (Id is not int id) return;
        if (ParentType == ItemType.Subject)
        {
            var data = await ApiC.V0.Subjects[id].Subjects.GetAsync();
            SubjectViewModels = data?.Select(x => new SubjectViewModel(x)).ToObservableCollection();
        }
        else if (ParentType == ItemType.Character)
        {
            var data = await ApiC.V0.Characters[id].Subjects.GetAsync();
            SubjectViewModels = data?.Select(x => new SubjectViewModel(x)).ToObservableCollection();
        }
        else if (ParentType == ItemType.Person)
        {
            var data = await ApiC.V0.Persons[id].Subjects.GetAsync();
            SubjectViewModels = data?.Select(x => new SubjectViewModel(x)).ToObservableCollection();
        }
    }

    [Reactive] public partial ObservableCollection<SubjectViewModel>? SubjectViewModels { get; set; }
    [Reactive] public partial ItemType ParentType { get; set; }
    [Reactive] public partial int? Id { get; set; }
}
