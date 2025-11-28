using BangumiNet.Api.Interfaces;
using BangumiNet.Api.P1.Models;

namespace BangumiNet.Api.P1.P1.Subjects.Item
{
    namespace Characters
    {
        public partial class CharactersGetResponse : IPagedDataResponse<List<SubjectCharacter>>;
        public partial class CharactersRequestBuilder
        {
            public partial class CharactersRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Collects
    {
        public partial class CollectsGetResponse : IPagedDataResponse<List<SubjectCollect>>;
        public partial class CollectsRequestBuilder
        {
            public partial class CollectsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Comments
    {
        public partial class CommentsGetResponse : IPagedDataResponse<List<SubjectInterestComment>>;
        public partial class CommentsRequestBuilder
        {
            public partial class CommentsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Episodes
    {
        public partial class EpisodesGetResponse : IPagedDataResponse<List<Episode>>;
        public partial class EpisodesRequestBuilder
        {
            public partial class EpisodesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Indexes
    {
        public partial class IndexesGetResponse : IPagedDataResponse<List<SlimIndex>>;
        public partial class IndexesRequestBuilder
        {
            public partial class IndexesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Recs
    {
        public partial class RecsGetResponse : IPagedDataResponse<List<SubjectRec>>;
        public partial class RecsRequestBuilder
        {
            public partial class RecsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Relations
    {
        public partial class RelationsGetResponse : IPagedDataResponse<List<SubjectRelation>>;
        public partial class RelationsRequestBuilder
        {
            public partial class RelationsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Reviews
    {
        public partial class ReviewsGetResponse : IPagedDataResponse<List<SubjectReview>>;
        public partial class ReviewsRequestBuilder
        {
            public partial class ReviewsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Staffs.Persons
    {
        public partial class PersonsGetResponse : IPagedDataResponse<List<SubjectStaff>>;
        public partial class PersonsRequestBuilder
        {
            public partial class PersonsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Staffs.Positions
    {
        public partial class PositionsGetResponse : IPagedDataResponse<List<SubjectPosition>>;
        public partial class PositionsRequestBuilder
        {
            public partial class PositionsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Topics
    {
        public partial class TopicsGetResponse : IPagedDataResponse<List<Topic>>;
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
        public partial class CastsGetResponse : IPagedDataResponse<List<CharacterSubject>>;
        public partial class CastsRequestBuilder
        {
            public partial class CastsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Collects
    {
        public partial class CollectsGetResponse : IPagedDataResponse<List<PersonCollect>>;
        public partial class CollectsRequestBuilder
        {
            public partial class CollectsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Indexes
    {
        public partial class IndexesGetResponse : IPagedDataResponse<List<SlimIndex>>;
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
        public partial class CastsGetResponse : IPagedDataResponse<List<PersonCharacter>>;
        public partial class CastsRequestBuilder
        {
            public partial class CastsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Collects
    {
        public partial class CollectsGetResponse : IPagedDataResponse<List<PersonCollect>>;
        public partial class CollectsRequestBuilder
        {
            public partial class CollectsRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Indexes
    {
        public partial class IndexesGetResponse : IPagedDataResponse<List<SlimIndex>>;
        public partial class IndexesRequestBuilder
        {
            public partial class IndexesRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Works
    {
        public partial class WorksGetResponse : IPagedDataResponse<List<PersonWork>>;
        public partial class WorksRequestBuilder
        {
            public partial class WorksRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}

namespace BangumiNet.Api.P1.P1.Groups.Item
{
    namespace Members
    {
        public partial class MembersGetResponse : IPagedDataResponse<List<GroupMember>>;
        public partial class MembersRequestBuilder
        {
            public partial class MembersRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
    namespace Topics
    {
        public partial class TopicsGetResponse : IPagedDataResponse<List<Topic>>;
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
        public partial class PhotosGetResponse : IPagedDataResponse<List<BlogPhoto>>;
        public partial class PhotosRequestBuilder
        {
            public partial class PhotosRequestBuilderGetQueryParameters : IPagedRequest;
        }
    }
}
