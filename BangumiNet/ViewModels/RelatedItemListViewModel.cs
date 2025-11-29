using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Helpers;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using System.Reactive;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class RelatedItemListViewModel : SubjectListViewModel
{
    public RelatedItemListViewModel(RelatedItemType type, ItemType parentType, int? parentId)
    {
        Type = type;
        ParentType = parentType;
        ParentId = parentId;

        this.WhenAnyValue(x => x.Offset, x => x.Total).Subscribe(x => IsFullyLoaded = Offset >= Total);
        LoadPageCommand = ReactiveCommand.CreateFromTask(LoadPage, this.WhenAnyValue(x => x.IsFullyLoaded).Select(x => !x));
    }

    public async Task Load(CancellationToken cancellationToken = default)
    {
        if (ParentId is not int id) return;
        IEnumerable<object>? response = null;
        try
        {
            response = ParentType switch
            {
                ItemType.Subject => Type switch
                {
                    RelatedItemType.Subject => await ApiC.V0.Subjects[id].Subjects.GetAsync(cancellationToken: cancellationToken),
                    RelatedItemType.Character => await ApiC.V0.Subjects[id].Characters.GetAsync(cancellationToken: cancellationToken),
                    RelatedItemType.Person => await ApiC.V0.Subjects[id].Persons.GetAsync(cancellationToken: cancellationToken),
                    _ => throw new NotImplementedException(),
                },
                ItemType.Character => Type switch
                {
                    RelatedItemType.CharacterCast => await ApiC.V0.Characters[id].Subjects.GetAsync(cancellationToken: cancellationToken),
                    RelatedItemType.Person => await ApiC.V0.Characters[id].Persons.GetAsync(cancellationToken: cancellationToken),
                    _ => throw new NotImplementedException(),
                },
                ItemType.Person => Type switch
                {
                    RelatedItemType.PersonWork => await ApiC.V0.Persons[id].Subjects.GetAsync(cancellationToken: cancellationToken),
                    RelatedItemType.PersonCast => await ApiC.V0.Persons[id].Characters.GetAsync(cancellationToken: cancellationToken),
                    _ => throw new NotImplementedException(),
                },
                _ => throw new NotImplementedException(),
            };
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;
        Total = Offset = response.Count();
        SubjectViewModels = [.. response.Select(ConvertToVM)];
    }

    /// <returns>是否已取得全部项目</returns>
    public async Task<bool> LoadPage(CancellationToken ct = default)
    {
        if (Offset >= Total) return true;
        if (ParentId is not int id) return false;

        IPagedDataResponse<IEnumerable<object>>? response = null;
        try
        {
            response = ParentType switch
            {
                ItemType.Subject => Type switch
                {
                    RelatedItemType.Character => await ApiC.P1.Subjects[id].Characters.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Collector => await ApiC.P1.Subjects[id].Collects.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Comment => await ApiC.P1.Subjects[id].Comments.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Episode => await ApiC.P1.Subjects[id].Episodes.GetAsync(c => c.Paging(CurrentSettings.EpisodePageSize, Offset), ct),
                    RelatedItemType.Index => await ApiC.P1.Subjects[id].Indexes.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Recommendation => await ApiC.P1.Subjects[id].Recs.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Subject => await ApiC.P1.Subjects[id].Relations.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Review => await ApiC.P1.Subjects[id].Reviews.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Person => await ApiC.P1.Subjects[id].Staffs.Persons.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Position => await ApiC.P1.Subjects[id].Staffs.Positions.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Topic => await ApiC.P1.Subjects[id].Topics.GetAsync(c => c.Paging(Limit, Offset), ct),
                    _ => throw new NotImplementedException(),
                },
                ItemType.Character => Type switch
                {
                    RelatedItemType.CharacterCast => await ApiC.P1.Characters[id].Casts.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Collector => await ApiC.P1.Characters[id].Collects.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Index => await ApiC.P1.Characters[id].Indexes.GetAsync(c => c.Paging(Limit, Offset), ct),
                    _ => throw new NotImplementedException(),
                },
                ItemType.Person => Type switch
                {
                    RelatedItemType.PersonCast => await ApiC.P1.Persons[id].Casts.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Collector => await ApiC.P1.Persons[id].Collects.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.Index => await ApiC.P1.Persons[id].Indexes.GetAsync(c => c.Paging(Limit, Offset), ct),
                    RelatedItemType.PersonWork => await ApiC.P1.Persons[id].Works.GetAsync(c => c.Paging(Limit, Offset), ct),
                    _ => throw new NotImplementedException(),
                },
                ItemType.Blog => Type switch
                {
                    RelatedItemType.Photo => await ApiC.P1.Blogs[id].Photos.GetAsync(c => c.Paging(Limit, Offset), ct),
                    _ => throw new NotImplementedException(),
                },
                _ => throw new NotImplementedException(),
            };
        }
        catch (Exception e) { Trace.TraceError(e.Message); return false; }
        Total = response?.Total;
        if (response?.Data == null) return false;
        Offset += response.Data.Count();
        SubjectViewModels ??= [];
        SubjectViewModels = Type switch
        {
            RelatedItemType.Episode => SubjectViewModels.Union(response.Data.Select(ConvertToChildVM))
                                           .OfType<EpisodeViewModel>().ToArray().LinkNeighbors()
                                           .OfType<ViewModelBase>().ToObservableCollection(),
            _ => SubjectViewModels.Union(response.Data.Select(ConvertToVM)).ToObservableCollection(),
        };
        return Offset >= Total;
    }

    public static ViewModelBase ConvertToVM(object obj) => obj switch
    {
        V0_subject_relation vsr => new SubjectViewModel(vsr),
        V0_RelatedSubject vrs => new SubjectViewModel(vrs),
        RelatedCharacter rc => new CharacterViewModel(rc),
        PersonCharacter pc => new CharacterViewModel(pc),
        RelatedPerson rp => new PersonViewModel(rp),
        CharacterPerson cp => new PersonViewModel(cp),
        Api.P1.Models.SubjectCharacter sCharacter => CharacterViewModel.Init(sCharacter),
        Api.P1.Models.SubjectStaff sPerson => PersonViewModel.Init(sPerson),
        Api.P1.Models.SubjectRelation sSubject => SubjectViewModel.Init(sSubject),
        Api.P1.Models.CharacterSubject cSubject => SubjectViewModel.Init(cSubject),
        Api.P1.Models.PersonCharacter pc => CharacterViewModel.Init(pc),
        Api.P1.Models.PersonWork pw => SubjectViewModel.Init(pw),
        Api.P1.Models.Episode ep => new EpisodeViewModel(ep),
        _ => new TextViewModel($"RelatedItemListViewModel.ConvertToVM: {obj}"),
    };

    public ViewModelBase ConvertToChildVM(object obj) => obj switch
    {
        Api.P1.Models.Episode ep => new EpisodeViewModel(ep) { Parent = this },
        _ => ConvertToVM(obj),
    };

    [Reactive] public partial RelatedItemType Type { get; set; }
    [Reactive] public partial ItemType ParentType { get; set; }
    [Reactive] public partial int? ParentId { get; set; }
    [Reactive] public partial int Offset { get; set; }
    [Reactive] public partial int? Total { get; set; }
    [Reactive] public partial bool IsFullyLoaded { get; set; }

    public ReactiveCommand<Unit, bool> LoadPageCommand { get; set; }

    public const int Limit = 20;
}
