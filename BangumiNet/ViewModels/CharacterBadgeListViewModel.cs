using BangumiNet.Api.ExtraEnums;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BangumiNet.ViewModels;

public partial class CharacterBadgeListViewModel : ViewModelBase
{
    public CharacterBadgeListViewModel(ItemType type, int? id)
    {
        ParentType = type;
        Id = id;
    }

    public async Task LoadCharacters()
    {
        if (Id is not int id) return;
        if (ParentType == ItemType.Subject)
        {
            var data = await ApiC.V0.Subjects[id].Characters.GetAsync();
            CharacterViewModels = data?.Select(x => new CharacterViewModel(x)).ToObservableCollection();
        }
        else if(ParentType == ItemType.Person)
        {
            var data = await ApiC.V0.Persons[id].Characters.GetAsync();
            CharacterViewModels = data?.Select(x => new CharacterViewModel(x)).ToObservableCollection();
        }
    }

    [Reactive] public partial ObservableCollection<CharacterViewModel>? CharacterViewModels { get; set; }
    [Reactive] public partial ItemType ParentType { get; set; }
    [Reactive] public partial int? Id { get; set; }
}
