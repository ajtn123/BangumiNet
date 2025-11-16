using BangumiNet.Api.V0.Models;

namespace BangumiNet.ViewModels;

public partial class SubjectBadgeListViewModel : SubjectListViewModel
{
    public SubjectBadgeListViewModel(ItemType type, ItemType parentType, int? parentId)
    {
        Type = type;
        ParentType = parentType;
        ParentId = parentId;
    }

    public Task Load(CancellationToken cancellationToken = default) => Type switch
    {
        ItemType.Subject => LoadSubjects(cancellationToken),
        ItemType.Character => LoadCharacters(cancellationToken),
        ItemType.Person => LoadPersons(cancellationToken),
        _ => throw new NotImplementedException(),
    };

    private async Task LoadSubjects(CancellationToken cancellationToken = default)
    {
        if (ParentId is not int id) return;
        if (ParentType == ItemType.Subject)
        {
            var data = await ApiC.V0.Subjects[id].Subjects.GetAsync(cancellationToken: cancellationToken);
            SubjectViewModels = data?.Select<V0_subject_relation, ViewModelBase>(x => new SubjectViewModel(x)).ToObservableCollection();
        }
        else if (ParentType == ItemType.Character)
        {
            var data = await ApiC.V0.Characters[id].Subjects.GetAsync(cancellationToken: cancellationToken);
            SubjectViewModels = data?.Select<V0_RelatedSubject, ViewModelBase>(x => new SubjectViewModel(x)).ToObservableCollection();
        }
        else if (ParentType == ItemType.Person)
        {
            var data = await ApiC.V0.Persons[id].Subjects.GetAsync(cancellationToken: cancellationToken);
            SubjectViewModels = data?.Select<V0_RelatedSubject, ViewModelBase>(x => new SubjectViewModel(x)).ToObservableCollection();
        }
    }

    private async Task LoadCharacters(CancellationToken cancellationToken = default)
    {
        if (ParentId is not int id) return;
        if (ParentType == ItemType.Subject)
        {
            var data = await ApiC.V0.Subjects[id].Characters.GetAsync(cancellationToken: cancellationToken);
            SubjectViewModels = data?.Select<RelatedCharacter, ViewModelBase>(x => new CharacterViewModel(x)).ToObservableCollection();
        }
        else if (ParentType == ItemType.Person)
        {
            var data = await ApiC.V0.Persons[id].Characters.GetAsync(cancellationToken: cancellationToken);
            SubjectViewModels = data?.Select<PersonCharacter, ViewModelBase>(x => new CharacterViewModel(x)).ToObservableCollection();
        }
    }

    private async Task LoadPersons(CancellationToken cancellationToken = default)
    {
        if (ParentId is not int id) return;
        if (ParentType == ItemType.Subject)
        {
            var data = await ApiC.V0.Subjects[id].Persons.GetAsync(cancellationToken: cancellationToken);
            SubjectViewModels = data?.Select<RelatedPerson, ViewModelBase>(x => new PersonViewModel(x)).ToObservableCollection();
        }
        else if (ParentType == ItemType.Character)
        {
            var data = await ApiC.V0.Characters[id].Persons.GetAsync(cancellationToken: cancellationToken);
            SubjectViewModels = data?.Select<CharacterPerson, ViewModelBase>(x => new PersonViewModel(x)).ToObservableCollection();
        }
    }

    [Reactive] public partial ItemType Type { get; set; }
    [Reactive] public partial ItemType ParentType { get; set; }
    [Reactive] public partial int? ParentId { get; set; }
}
