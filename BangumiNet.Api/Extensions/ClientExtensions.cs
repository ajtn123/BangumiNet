using BangumiNet.Api.Interfaces;

namespace BangumiNet.Api.P1
{
    public partial class ApiClient : IApiClient
    {
        /// <summary>bangumi private api 版本</summary>
        public const string Version = "2026-08-16-52a3a3c"; // P1
        /// <summary>OpenAPI 定义文件的 URL</summary>
        public const string DefinitionUrl = "https://next.bgm.tv/p1/openapi.json";
        /// <summary>生成本 API 客户端的命令</summary>
        public const string KiotaCommand = $@"kiota generate -d {DefinitionUrl} -l csharp -n BangumiNet.Api.P1 -o .\P1 --co --ebc";

        public const string BaseUrl = "https://next.bgm.tv";
        public const string BaseUrlDev = "https://next.bgm38.tv";
        public const string BaseUrlTrailing = BaseUrl + "/";
        public const string BaseUrlDevTrailing = BaseUrlDev + "/";
    }
}

namespace BangumiNet.Api.V0
{
    public partial class ApiClient : IApiClient
    {
        /// <summary>bangumi open api 版本</summary>
        public const string Version = "2026-05-02-b8c3ed"; // V0
        /// <summary>OpenAPI 定义文件的 URL</summary>
        public const string DefinitionUrl = "https://github.com/bangumi/api/raw/refs/heads/master/open-api/v0.yaml";
        /// <summary>生成本 API 客户端的命令</summary>
        public const string KiotaCommand = $@"kiota generate -d {DefinitionUrl} -l csharp -n BangumiNet.Api.V0 -o .\V0 --co --ebc";

        public const string BaseUrl = "https://api.bgm.tv";
        public const string BaseUrlDev = "https://api.bgm38.tv";
        public const string BaseUrlTrailing = BaseUrl + "/";
        public const string BaseUrlDevTrailing = BaseUrlDev + "/";
    }
}
