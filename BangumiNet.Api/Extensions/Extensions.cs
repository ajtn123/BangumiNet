using BangumiNet.Api.Interfaces;
namespace BangumiNet.Api.P1
{
    public partial class ApiClient : IApiClient
    {
        /// <summary>server-private 仓库的最后 commit</summary>
        public const string Commit = "cd9f5cda466be9dcc569957253ad93e611418417";
        /// <summary>OpenAPI 定义文件的 URL</summary>
        public const string DefinitionUrl = "https://next.bgm.tv/p1/openapi.yaml";
        /// <summary>生成本 API 客户端的命令</summary>
        public const string KiotaCommand = $@"kiota generate -d {DefinitionUrl} -l csharp -n BangumiNet.Api.P1 -o .\P1 --co";
    }
}
namespace BangumiNet.Api.V0
{
    public partial class ApiClient : IApiClient
    {
        /// <summary>OpenAPI 定义文件的最后 commit</summary>
        public const string Commit = "117fc456a45c1e2a5a0647d3d3cf0e4e74a28344";
        /// <summary>OpenAPI 定义文件的 URL</summary>
        public const string DefinitionUrl = "https://github.com/trim21-bot/api/raw/117fc456a45c1e2a5a0647d3d3cf0e4e74a28344/open-api/v0.yaml";
        /// <summary>生成本 API 客户端的命令</summary>
        public const string KiotaCommand = $@"kiota generate -d {DefinitionUrl} -l csharp -n BangumiNet.Api.V0 -o .\V0 --co";
    }
}
namespace BangumiNet.Api.Legacy
{
    [Obsolete]
    public partial class ApiClient : IApiClient
    {
        /// <summary>OpenAPI 定义文件的最后 commit</summary>
        public const string Commit = "9b4e4267c008218b51c275d640fab292637ca7ae";
        /// <summary>OpenAPI 定义文件的 URL</summary>
        public const string DefinitionUrl = "https://github.com/bangumi/api/raw/9b4e4267c008218b51c275d640fab292637ca7ae/open-api/api.yml";
        /// <summary>生成本 API 客户端的命令</summary>
        public const string KiotaCommand = $@"kiota generate -d {DefinitionUrl} -l csharp -n BangumiNet.Api.Legacy -o .\Legacy --co";
    }
}
namespace BangumiNet.Api.P1.Models
{
    public partial class Avatar : IImages { }
    public partial class SubjectImages : IImagesGrid { }
    public partial class Calendar
    {
        public Dictionary<DayOfWeek, IEnumerable<CalendarItem>> Days { get; set; } = [];
    }
}
namespace BangumiNet.Api.V0.Models
{
    public partial class Subject_collection : ICollection { }
    public partial class Images : IImagesFull { }
    public partial class Subject_rating_count : IRatingCount { }
    public partial class Collections : ITag { }
    public partial class Person_images : IImagesGrid { }
    public partial class Character_images : IImagesGrid { }
    public partial class PersonDetail_images : IImagesGrid { }
    public partial class CharacterPerson_images : IImagesGrid { }
    public partial class RelatedPerson_images : IImagesGrid { }
    public partial class RelatedCharacter_images : IImagesGrid { }
    public partial class UserCharacterCollection_images : IImagesGrid { }
    public partial class UserPersonCollection_images : IImagesGrid { }
    public partial class PersonImages : IImagesGrid { }
    public partial class Avatar : IImages { }
    public partial class Paged_Character : IPagedResponse { }
    public partial class Paged_Episode : IPagedResponse { }
    public partial class Paged_Person : IPagedResponse { }
    public partial class Paged_Revision : IPagedResponse { }
    public partial class Paged_Subject : IPagedResponse { }
    public partial class Paged_UserCharacterCollection : IPagedResponse { }
    public partial class Paged_UserCollection : IPagedResponse { }
    public partial class Paged_UserPersonCollection : IPagedResponse { }
    public partial class SubjectRevision : IRevision { }
    public partial class CharacterRevision : IRevision { }
    public partial class PersonRevision : IRevision { }
    public partial class DetailedRevision : IRevision { }
    public partial class Revision : IRevision { }
}
namespace BangumiNet.Api.Legacy.Models
{
    public partial class Legacy_SubjectSmall_collection : ICollection { }
    public partial class Legacy_SubjectSmall_images : IImagesFull { }
    public partial class Legacy_SubjectSmall_rating_count : IRatingCount { }
}
namespace BangumiNet.Api.Legacy.Calendar
{
    public partial class Calendar_weekday : IWeekday { }
}
