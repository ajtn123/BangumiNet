using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace BangumiNet.Api;

public static class ClientBuilder
{
    /// <summary>
    /// 从 <see cref="IApiSettings"/> 构建 v0 和 p1 客户端。
    /// </summary>
    /// <param name="setting">自定义的 <see cref="IApiSettings"/>。</param>
    public static Clients Build(IApiSettings setting)
    {
        var authProvider = new BangumiAuthenticationProvider(setting.AuthToken);
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", setting.UserAgent);

        return Build(httpClient, authProvider, setting.DevEnvironment);
    }

    /// <summary>
    /// 构建 v0 和 p1 客户端。
    /// </summary>
    /// <param name="httpClient">使用的 <see cref="HttpClient"/>。</param>
    /// <param name="authProvider">可以使用 <see cref="BangumiAuthenticationProvider"/>。</param>
    /// <param name="devEnvironment">是否使用开发环境。</param>
    /// <returns></returns>
    public static Clients Build(HttpClient httpClient, IAuthenticationProvider authProvider, bool devEnvironment = false)
    {
        var v0RequestAdapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
        var p1RequestAdapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);

        if (devEnvironment)
        {
            v0RequestAdapter.BaseUrl = V0.ApiClient.BaseUrlDev;
            p1RequestAdapter.BaseUrl = P1.ApiClient.BaseUrlDev;
        }

        var v0Client = new V0.ApiClient(v0RequestAdapter);
        var p1Client = new P1.ApiClient(p1RequestAdapter);

        return new()
        {
            HttpClient = httpClient,
            AuthProvider = authProvider,
            V0Client = v0Client,
            P1Client = p1Client,
        };
    }
}

public class Clients
{
    public required HttpClient HttpClient { get; set; }
    public required IAuthenticationProvider AuthProvider { get; set; }
    public required V0.ApiClient V0Client { get; set; }
    public required P1.ApiClient P1Client { get; set; }
}
