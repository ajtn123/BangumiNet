using BangumiNet.Api.V0.Models;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;

namespace BangumiNet.ViewModels;

public partial class PersonListViewModel : ViewModelBase
{
    public void AddPersons(Paged_Person? Persons)
    {
        PersonViewModels ??= [];
        if (Persons?.Data != null)
            PersonViewModels = PersonViewModels?.UnionBy(Persons.Data.Select(x => new PersonViewModel(x)), s => s.Id).ToObservableCollection();
    }

    [Reactive] public partial ObservableCollection<PersonViewModel>? PersonViewModels { get; set; }
}
