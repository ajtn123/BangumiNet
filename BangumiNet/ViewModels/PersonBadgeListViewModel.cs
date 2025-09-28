namespace BangumiNet.ViewModels;

public partial class PersonBadgeListViewModel : ViewModelBase
{
    public PersonBadgeListViewModel(ItemType type, int? id)
    {
        ParentType = type;
        Id = id;
    }

    public async Task LoadPersons()
    {
        if (Id is not int id) return;
        if (ParentType == ItemType.Subject)
        {
            var data = await ApiC.V0.Subjects[id].Persons.GetAsync();
            PersonViewModels = data?.Select(x => new PersonViewModel(x)).ToObservableCollection();
        }
        else if (ParentType == ItemType.Character)
        {
            var data = await ApiC.V0.Characters[id].Persons.GetAsync();
            PersonViewModels = data?.Select(x => new PersonViewModel(x)).ToObservableCollection();
        }
    }

    [Reactive] public partial ObservableCollection<PersonViewModel>? PersonViewModels { get; set; }
    [Reactive] public partial ItemType ParentType { get; set; }
    [Reactive] public partial int? Id { get; set; }
}
