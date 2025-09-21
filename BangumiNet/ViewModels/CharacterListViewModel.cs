using BangumiNet.Api.V0.Models;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;

namespace BangumiNet.ViewModels;

public partial class CharacterListViewModel : ViewModelBase
{
    public void AddCharacters(Paged_Character? Characters)
    {
        CharacterViewModels ??= [];
        if (Characters?.Data != null)
            CharacterViewModels = CharacterViewModels?.UnionBy(Characters.Data.Select(x => new CharacterViewModel(x)), s => s.Id).ToObservableCollection();
    }

    [Reactive] public partial ObservableCollection<CharacterViewModel>? CharacterViewModels { get; set; }
}
