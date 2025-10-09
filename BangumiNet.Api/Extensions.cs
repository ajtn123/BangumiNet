﻿using BangumiNet.Api.Interfaces;
namespace BangumiNet.Api.P1
{
    public partial class ApiClient : IApiClient
    {
        /// <summary>server-private 仓库的最后 commit</summary>
        public const string Commit = "0e7da899e84dc4c19df61966ab1e9cd708493350";
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
        public const string Commit = "4433d6a0265e23a12324180569ac4abc964e682b";
        /// <summary>OpenAPI 定义文件的 URL</summary>
        public const string DefinitionUrl = "https://raw.githubusercontent.com/bangumi/api/refs/heads/master/open-api/v0.yaml";
        /// <summary>生成本 API 客户端的命令</summary>
        public const string KiotaCommand = $@"kiota generate -d {DefinitionUrl} -l csharp -n BangumiNet.Api.V0 -o .\V0 --co";
    }
}
namespace BangumiNet.Api.Legacy
{
    public partial class ApiClient : IApiClient
    {
        /// <summary>OpenAPI 定义文件的最后 commit</summary>
        public const string Commit = "9b4e4267c008218b51c275d640fab292637ca7ae";
        /// <summary>OpenAPI 定义文件的 URL</summary>
        public const string DefinitionUrl = "https://raw.githubusercontent.com/bangumi/api/refs/heads/master/open-api/api.yml";
        /// <summary>生成本 API 客户端的命令</summary>
        public const string KiotaCommand = $@"kiota generate -d {DefinitionUrl} -l csharp -n BangumiNet.Api.Legacy -o .\Legacy --co";
    }
}
namespace BangumiNet.Api.P1.Models
{
    public partial class Avatar : IImages { }
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
