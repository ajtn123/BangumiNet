using BangumiNet.Api.Interfaces;
namespace BangumiNet.Api.V0
{
    public partial class ApiClient : IApiClient
    {
        /// <summary>OpenAPI 定义文件的版本</summary>
        public const string Commit = "4433d6a0265e23a12324180569ac4abc964e682b";
        /// <summary>OpenAPI 定义文件的 URL</summary>
        public const string DefinitionUrl = "https://raw.githubusercontent.com/bangumi/api/refs/heads/master/open-api/v0.yaml";
        /// <summary>生成本 API 客户端的命令</summary>
        public const string KiotaCommand = $@"kiota generate -d {DefinitionUrl} -l csharp -n BangumiNet.Api.V0 -o .\V0 -co";
    }
}
namespace BangumiNet.Api.Legacy
{
    public partial class ApiClient : IApiClient
    {
        /// <summary>OpenAPI 定义文件的版本</summary>
        public const string Commit = "9b4e4267c008218b51c275d640fab292637ca7ae";
        /// <summary>OpenAPI 定义文件的 URL</summary>
        public const string DefinitionUrl = "https://raw.githubusercontent.com/bangumi/api/refs/heads/master/open-api/api.yml";
        /// <summary>生成本 API 客户端的命令</summary>
        public const string KiotaCommand = $@"kiota generate -d {DefinitionUrl} -l csharp -n BangumiNet.Api.Legacy -o .\Legacy -co";
    }
}
namespace BangumiNet.Api.V0.Models
{
    public partial class Subject_collection : ICollection { }
    public partial class Images : IImagesCommon { }
    public partial class Subject_rating_count : IRatingCount { }
    public partial class Collections : ITag { }
    public partial class Person_images : IImages { }
    public partial class Character_images : IImages { }
    public partial class PersonDetail_images : IImages { }
}
namespace BangumiNet.Api.Legacy.Models
{
    public partial class Legacy_SubjectSmall_collection : ICollection { }
    public partial class Legacy_SubjectSmall_images : IImagesCommon { }
    public partial class Legacy_SubjectSmall_rating_count : IRatingCount { }
}
namespace BangumiNet.Api.Legacy.Calendar
{
    public partial class Calendar_weekday : IWeekday { }
}
