namespace BangumiNet.ViewModels;

public partial class CharacterBadgeListViewModel : ViewModelBase
{
    public CharacterBadgeListViewModel(ItemType type, int? id)
    {
        ParentType = type;
        Id = id;
    }

    public async Task LoadCharacters(CancellationToken cancellationToken = default)
    {
        if (Id is not int id) return;
        if (ParentType == ItemType.Subject)
        {
            var data = await ApiC.V0.Subjects[id].Characters.GetAsync(cancellationToken: cancellationToken);
            CharacterViewModels = data?.Select(x => new CharacterViewModel(x)).ToObservableCollection();
        }
        else if (ParentType == ItemType.Person)
        {
            var data = await ApiC.V0.Persons[id].Characters.GetAsync(cancellationToken: cancellationToken);
            CharacterViewModels = data?.Select(x => new CharacterViewModel(x)).ToObservableCollection();
        }
    }

    [Reactive] public partial ObservableCollection<CharacterViewModel>? CharacterViewModels { get; set; }
    [Reactive] public partial ItemType ParentType { get; set; }
    [Reactive] public partial int? Id { get; set; }
}
