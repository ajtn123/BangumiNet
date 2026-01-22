using BangumiNet.Api.Interfaces;

namespace BangumiNet.Api.P1
{
    public partial class ApiClient : IApiClient
    {
        /// <summary>server-private 仓库的最后 commit</summary>
        public const string Commit = "572b1f0b579f2a7f3279bfdff528d63358614e18";
        /// <summary>OpenAPI 定义文件的 URL</summary>
        public const string DefinitionUrl = "https://next.bgm.tv/p1/openapi.yaml";
        /// <summary>生成本 API 客户端的命令</summary>
        public const string KiotaCommand = $@"kiota generate -d {DefinitionUrl} -l csharp -n BangumiNet.Api.P1 -o .\P1 --co --ebc";
    }
}
namespace BangumiNet.Api.V0
{
    public partial class ApiClient : IApiClient
    {
        /// <summary>OpenAPI 定义文件的最后 commit</summary>
        public const string Commit = "88bd1376af22b416eaa9d1af854560316d924b9a";
        /// <summary>OpenAPI 定义文件的 URL</summary>
        public const string DefinitionUrl = "https://github.com/bangumi/api/raw/refs/heads/master/open-api/v0.yaml";
        /// <summary>生成本 API 客户端的命令</summary>
        public const string KiotaCommand = $@"kiota generate -d {DefinitionUrl} -l csharp -n BangumiNet.Api.V0 -o .\V0 --co --ebc";
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
        public const string KiotaCommand = $@"kiota generate -d {DefinitionUrl} -l csharp -n BangumiNet.Api.Legacy -o .\Legacy --co --ebc";
    }
}
