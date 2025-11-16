using BangumiNet.Api.V0.Models;

namespace BangumiNet.ViewModels;

public partial class RelatedItemListViewModel : SubjectListViewModel
{
    public RelatedItemListViewModel(ItemType type, ItemType parentType, int? parentId)
    {
        Type = type;
        ParentType = parentType;
        ParentId = parentId;
    }

    public async Task Load(CancellationToken cancellationToken = default)
    {
        if (ParentId is not int id) return;
        IEnumerable<object>? response = null;
        try
        {
            response = Type switch
            {
                ItemType.Subject => ParentType switch
                {
                    ItemType.Subject => await ApiC.V0.Subjects[id].Subjects.GetAsync(cancellationToken: cancellationToken),
                    ItemType.Character => await ApiC.V0.Characters[id].Subjects.GetAsync(cancellationToken: cancellationToken),
                    ItemType.Person => await ApiC.V0.Persons[id].Subjects.GetAsync(cancellationToken: cancellationToken),
                    _ => throw new NotImplementedException(),
                },
                ItemType.Character => ParentType switch
                {
                    ItemType.Subject => await ApiC.V0.Subjects[id].Characters.GetAsync(cancellationToken: cancellationToken),
                    ItemType.Person => await ApiC.V0.Persons[id].Characters.GetAsync(cancellationToken: cancellationToken),
                    _ => throw new NotImplementedException(),
                },
                ItemType.Person => ParentType switch
                {
                    ItemType.Subject => await ApiC.V0.Subjects[id].Persons.GetAsync(cancellationToken: cancellationToken),
                    ItemType.Character => await ApiC.V0.Characters[id].Persons.GetAsync(cancellationToken: cancellationToken),
                    _ => throw new NotImplementedException(),
                },
                _ => throw new NotImplementedException(),
            };
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;
        SubjectViewModels = [.. response.Select(ConvertToVM)];
    }

    private static ViewModelBase ConvertToVM(object obj) => obj switch
    {
        V0_subject_relation vsr => new SubjectViewModel(vsr),
        V0_RelatedSubject vrs => new SubjectViewModel(vrs),
        RelatedCharacter rc => new CharacterViewModel(rc),
        PersonCharacter pc => new CharacterViewModel(pc),
        RelatedPerson rp => new PersonViewModel(rp),
        CharacterPerson cp => new PersonViewModel(cp),
        _ => throw new NotImplementedException(),
    };

    [Reactive] public partial ItemType Type { get; set; }
    [Reactive] public partial ItemType ParentType { get; set; }
    [Reactive] public partial int? ParentId { get; set; }
}
