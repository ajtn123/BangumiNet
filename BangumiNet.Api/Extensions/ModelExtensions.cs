using BangumiNet.Api.Interfaces;

namespace BangumiNet.Api.P1.Models
{
    public partial class SubjectStaffPositionType : IMultiLang;
    public partial class SubjectRelationType : IMultiLang;
    public partial class Avatar : IImages;
    public partial class SubjectImages : IImagesGrid;
    public partial class PersonImages : IImagesGrid;
    public partial class SubjectTag : ITag;
    public partial class Subjects : IInfoboxItem<List<Subjects_values>>;
    public partial class Subjects_values : IInfoboxKeyValuePair;
    public partial class Persons : IInfoboxItem<List<Persons_values>>;
    public partial class Persons_values : IInfoboxKeyValuePair;
    public partial class Characters : IInfoboxItem<List<Characters_values>>;
    public partial class Characters_values : IInfoboxKeyValuePair;
    public partial class Calendar
    {
        public Dictionary<DayOfWeek, IEnumerable<CalendarItem>> Days { get; set; } = [];
    }
    public partial class SubjectCollection : ICollection
    {
        public int? Collect { get; set; }
        public int? Doing { get; set; }
        public int? Dropped { get; set; }
        public int? OnHold { get; set; }
        public int? Wish { get; set; }
    }
}
namespace BangumiNet.Api.V0.Models
{
    public partial class Subject_collection : ICollection;
    public partial class Images : IImagesFull;
    public partial class Subject_rating_count : IRatingCount;
    public partial class Collections : ITag;
    public partial class Person_images : IImagesGrid;
    public partial class Character_images : IImagesGrid;
    public partial class PersonDetail_images : IImagesGrid;
    public partial class CharacterPerson_images : IImagesGrid;
    public partial class RelatedPerson_images : IImagesGrid;
    public partial class RelatedCharacter_images : IImagesGrid;
    public partial class UserCharacterCollection_images : IImagesGrid;
    public partial class UserPersonCollection_images : IImagesGrid;
    public partial class PersonImages : IImagesGrid;
    public partial class Avatar : IImages;
    public partial class Paged_Character : IPagedResponse;
    public partial class Paged_Episode : IPagedResponse;
    public partial class Paged_Person : IPagedResponse;
    public partial class Paged_Revision : IPagedResponse;
    public partial class Paged_Subject : IPagedResponse;
    public partial class Paged_UserCharacterCollection : IPagedResponse;
    public partial class Paged_UserCollection : IPagedResponse;
    public partial class Paged_UserPersonCollection : IPagedResponse;
    public partial class SubjectRevision : IRevision;
    public partial class CharacterRevision : IRevision;
    public partial class PersonRevision : IRevision;
    public partial class DetailedRevision : IRevision;
    public partial class Revision : IRevision;
}
namespace BangumiNet.Api.Legacy.Models
{
    public partial class Legacy_SubjectSmall_collection : ICollection;
    public partial class Legacy_SubjectSmall_images : IImagesFull;
    public partial class Legacy_SubjectSmall_rating_count : IRatingCount;
}
namespace BangumiNet.Api.Legacy.Calendar
{
    public partial class Calendar_weekday : IWeekday;
}
