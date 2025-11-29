using BangumiNet.Api.Interfaces;
using BangumiNet.Api.P1.Models;

namespace BangumiNet.Api.P1.P1.Followers
{
    public partial class FollowersGetResponse : IPagedResponse<List<Friend>>;
    public partial class FollowersRequestBuilder
    {
        public partial class FollowersRequestBuilderGetQueryParameters : IPagedRequest;
    }
}

namespace BangumiNet.Api.P1.P1.Friends
{
    public partial class FriendsGetResponse : IPagedResponse<List<Friend>>;
    public partial class FriendsRequestBuilder
    {
        public partial class FriendsRequestBuilderGetQueryParameters : IPagedRequest;
    }
}

namespace BangumiNet.Api.P1.P1.Subjects.Item
{
    namespace Characters
    {
        public partial class CharactersGetResponse : IPagedResponse<List<SubjectCharacter>>;
        public partial class CharactersRequestBuilder
        {
            public partial class CharactersRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Collects
    {
        public partial class CollectsGetResponse : IPagedResponse<List<SubjectCollect>>;
        public partial class CollectsRequestBuilder
        {
            public partial class CollectsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Comments
    {
        public partial class CommentsGetResponse : IPagedResponse<List<SubjectInterestComment>>;
        public partial class CommentsRequestBuilder
        {
            public partial class CommentsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Episodes
    {
        public partial class EpisodesGetResponse : IPagedResponse<List<Episode>>;
        public partial class EpisodesRequestBuilder
        {
            public partial class EpisodesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Indexes
    {
        public partial class IndexesGetResponse : IPagedResponse<List<SlimIndex>>;
        public partial class IndexesRequestBuilder
        {
            public partial class IndexesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Recs
    {
        public partial class RecsGetResponse : IPagedResponse<List<SubjectRec>>;
        public partial class RecsRequestBuilder
        {
            public partial class RecsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Relations
    {
        public partial class RelationsGetResponse : IPagedResponse<List<SubjectRelation>>;
        public partial class RelationsRequestBuilder
        {
            public partial class RelationsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Reviews
    {
        public partial class ReviewsGetResponse : IPagedResponse<List<SubjectReview>>;
        public partial class ReviewsRequestBuilder
        {
            public partial class ReviewsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Staffs.Persons
    {
        public partial class PersonsGetResponse : IPagedResponse<List<SubjectStaff>>;
        public partial class PersonsRequestBuilder
        {
            public partial class PersonsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Staffs.Positions
    {
        public partial class PositionsGetResponse : IPagedResponse<List<SubjectPosition>>;
        public partial class PositionsRequestBuilder
        {
            public partial class PositionsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Topics
    {
        public partial class TopicsGetResponse : IPagedResponse<List<Topic>>;
        public partial class TopicsRequestBuilder
        {
            public partial class TopicsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Characters.Item
{
    namespace Casts
    {
        public partial class CastsGetResponse : IPagedResponse<List<CharacterSubject>>;
        public partial class CastsRequestBuilder
        {
            public partial class CastsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Collects
    {
        public partial class CollectsGetResponse : IPagedResponse<List<PersonCollect>>;
        public partial class CollectsRequestBuilder
        {
            public partial class CollectsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Indexes
    {
        public partial class IndexesGetResponse : IPagedResponse<List<SlimIndex>>;
        public partial class IndexesRequestBuilder
        {
            public partial class IndexesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Persons.Item
{
    namespace Casts
    {
        public partial class CastsGetResponse : IPagedResponse<List<PersonCharacter>>;
        public partial class CastsRequestBuilder
        {
            public partial class CastsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Collects
    {
        public partial class CollectsGetResponse : IPagedResponse<List<PersonCollect>>;
        public partial class CollectsRequestBuilder
        {
            public partial class CollectsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Indexes
    {
        public partial class IndexesGetResponse : IPagedResponse<List<SlimIndex>>;
        public partial class IndexesRequestBuilder
        {
            public partial class IndexesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Works
    {
        public partial class WorksGetResponse : IPagedResponse<List<PersonWork>>;
        public partial class WorksRequestBuilder
        {
            public partial class WorksRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Indexes.Item
{
    namespace Related
    {
        public partial class RelatedGetResponse : IPagedResponse<List<IndexRelated>>;
        public partial class RelatedRequestBuilder
        {
            public partial class RelatedRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Users.Item
{
    namespace Blogs
    {
        public partial class BlogsGetResponse : IPagedResponse<List<SlimBlogEntry>>;
        public partial class BlogsRequestBuilder
        {
            public partial class BlogsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Collections.Subjects
    {
        public partial class SubjectsGetResponse : IPagedResponse<List<SlimSubject>>;
        public partial class SubjectsRequestBuilder
        {
            public partial class SubjectsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Collections.Characters
    {
        public partial class CharactersGetResponse : IPagedResponse<List<SlimCharacter>>;
        public partial class CharactersRequestBuilder
        {
            public partial class CharactersRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Collections.Persons
    {
        public partial class PersonsGetResponse : IPagedResponse<List<SlimPerson>>;
        public partial class PersonsRequestBuilder
        {
            public partial class PersonsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Collections.Indexes
    {
        public partial class IndexesGetResponse : IPagedResponse<List<SlimIndex>>;
        public partial class IndexesRequestBuilder
        {
            public partial class IndexesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Followers
    {
        public partial class FollowersGetResponse : IPagedResponse<List<SlimUser>>;
        public partial class FollowersRequestBuilder
        {
            public partial class FollowersRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Friends
    {
        public partial class FriendsGetResponse : IPagedResponse<List<SlimUser>>;
        public partial class FriendsRequestBuilder
        {
            public partial class FriendsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Groups
    {
        public partial class GroupsGetResponse : IPagedResponse<List<SlimGroup>>;
        public partial class GroupsRequestBuilder
        {
            public partial class GroupsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Indexes
    {
        public partial class IndexesGetResponse : IPagedResponse<List<SlimIndex>>;
        public partial class IndexesRequestBuilder
        {
            public partial class IndexesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Groups.Item
{
    namespace Members
    {
        public partial class MembersGetResponse : IPagedResponse<List<GroupMember>>;
        public partial class MembersRequestBuilder
        {
            public partial class MembersRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Topics
    {
        public partial class TopicsGetResponse : IPagedResponse<List<Topic>>;
        public partial class TopicsRequestBuilder
        {
            public partial class TopicsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Blogs.Item
{
    namespace Photos
    {
        public partial class PhotosGetResponse : IPagedResponse<List<BlogPhoto>>;
        public partial class PhotosRequestBuilder
        {
            public partial class PhotosRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Subjects
{
    public partial class SubjectsGetResponse : IPagedResponse<List<SlimSubject>>;

    namespace Topics
    {
        public partial class TopicsGetResponse : IPagedResponse<List<SubjectTopic>>;
        public partial class TopicsRequestBuilder
        {
            public partial class TopicsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Groups
{
    public partial class GroupsGetResponse : IPagedResponse<List<SlimGroup>>;
    public partial class GroupsRequestBuilder
    {
        public partial class GroupsRequestBuilderGetQueryParameters : IPagedRequest;
    }

    namespace Topics
    {
        public partial class TopicsGetResponse : IPagedResponse<List<GroupTopic>>;
        public partial class TopicsRequestBuilder
        {
            public partial class TopicsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Collections
{
    namespace Subjects
    {
        public partial class SubjectsGetResponse : IPagedResponse<List<Subject>>;
        public partial class SubjectsRequestBuilder
        {
            public partial class SubjectsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Characters
    {
        public partial class CharactersGetResponse : IPagedResponse<List<Character>>;
        public partial class CharactersRequestBuilder
        {
            public partial class CharactersRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Persons
    {
        public partial class PersonsGetResponse : IPagedResponse<List<Person>>;
        public partial class PersonsRequestBuilder
        {
            public partial class PersonsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Indexes
    {
        public partial class IndexesGetResponse : IPagedResponse<List<IndexObject>>;
        public partial class IndexesRequestBuilder
        {
            public partial class IndexesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Search
{
    namespace Subjects
    {
        public partial class SubjectsPostResponse : IPagedResponse<List<SlimSubject>>;
        public partial class SubjectsRequestBuilder
        {
            public partial class SubjectsRequestBuilderPostQueryParameters : IPagedRequest;
        }
    }
    namespace Characters
    {
        public partial class CharactersPostResponse : IPagedResponse<List<SlimCharacter>>;
        public partial class CharactersRequestBuilder
        {
            public partial class CharactersRequestBuilderPostQueryParameters : IPagedRequest;
        }
    }
    namespace Persons
    {
        public partial class PersonsPostResponse : IPagedResponse<List<SlimPerson>>;
        public partial class PersonsRequestBuilder
        {
            public partial class PersonsRequestBuilderPostQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Trending
{
    namespace Subjects
    {
        public partial class SubjectsGetResponse : IPagedResponse<List<TrendingSubject>>;
        public partial class SubjectsRequestBuilder
        {
            public partial class SubjectsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Subjects.Topics
    {
        public partial class TopicsGetResponse : IPagedResponse<List<SubjectTopic>>;
        public partial class TopicsRequestBuilder
        {
            public partial class TopicsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Wiki
{
    namespace Characters.Item.HistorySummary
    {
        public partial class HistorySummaryGetResponse : IPagedResponse<List<RevisionHistory>>;
        public partial class HistorySummaryRequestBuilder
        {
            public partial class HistorySummaryRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Users.Item.Contributions.Characters
    {
        public partial class CharactersGetResponse : IPagedResponse<List<UserCharacterContribution>>;
        public partial class CharactersRequestBuilder
        {
            public partial class CharactersRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}
