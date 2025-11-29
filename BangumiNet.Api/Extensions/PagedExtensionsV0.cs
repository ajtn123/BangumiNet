using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;

namespace BangumiNet.Api.V0.Models
{
    public partial class Page : IPagedResponseFull;
    public partial class Paged_Character : IPagedResponseFull<List<Character>>;
    public partial class Paged_Episode : IPagedResponseFull<List<Episode>>;
    public partial class Paged_Person : IPagedResponseFull<List<Person>>;
    public partial class Paged_Revision : IPagedResponseFull<List<Revision>>;
    public partial class Paged_Subject : IPagedResponseFull<List<Subject>>;
    public partial class Paged_UserCharacterCollection : IPagedResponseFull<List<UserCharacterCollection>>;
    public partial class Paged_UserCollection : IPagedResponseFull<List<UserSubjectCollection>>;
    public partial class Paged_UserPersonCollection : IPagedResponseFull<List<UserPersonCollection>>;
}

namespace BangumiNet.Api.V0.V0
{
    namespace Search.Subjects
    {
        public partial class SubjectsRequestBuilder
        {
            public partial class SubjectsRequestBuilderPostQueryParameters : IPagedRequest;
        }
    }
    namespace Search.Characters
    {
        public partial class CharactersRequestBuilder
        {
            public partial class CharactersRequestBuilderPostQueryParameters : IPagedRequest;
        }
    }
    namespace Search.Persons
    {
        public partial class PersonsRequestBuilder
        {
            public partial class PersonsRequestBuilderPostQueryParameters : IPagedRequest;
        }
    }
    namespace Subjects
    {
        public partial class SubjectsRequestBuilder
        {
            public partial class SubjectsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Episodes
    {
        public partial class EpisodesRequestBuilder
        {
            public partial class EpisodesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Users.Item.Collections
    {
        public partial class CollectionsRequestBuilder
        {
            public partial class CollectionsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Users.Collections.Item.Episodes
    {
        public partial class EpisodesGetResponse : IPagedResponseFull<List<UserEpisodeCollection>>;
        public partial class EpisodesRequestBuilder
        {
            public partial class EpisodesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Revisions.Subjects
    {
        public partial class SubjectsRequestBuilder
        {
            public partial class SubjectsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Revisions.Characters
    {
        public partial class CharactersRequestBuilder
        {
            public partial class CharactersRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Revisions.Persons
    {
        public partial class PersonsRequestBuilder
        {
            public partial class PersonsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Revisions.Episodes
    {
        public partial class EpisodesRequestBuilder
        {
            public partial class EpisodesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Indices.Item.Subjects
    {
        public partial class SubjectsRequestBuilder
        {
            public partial class SubjectsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}
