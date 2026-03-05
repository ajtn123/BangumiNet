using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace BangumiNet.Api;

public static class ClientBuilder
{
    /// <summary>
    /// 从自定义的 <see cref="IApiSettings"/> 构建 v0 和 p1 客户端。
    /// </summary>
    /// <param name="setting">自定义的 <see cref="IApiSettings"/>。</param>
    public static Clients Build(IApiSettings setting)
    {
        var authProvider = new BangumiAuthenticationProvider(setting.AuthToken);
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", setting.UserAgent);

        return Build(authProvider, httpClient, setting.DevEnvironment);
    }

    /// <summary>
    /// 构建 v0 和 p1 客户端。
    /// </summary>
    /// <param name="authProvider">可以使用 <see cref="BangumiAuthenticationProvider"/>，也可以自己实现。</param>
    /// <param name="httpClient">想要复用的 <see cref="HttpClient"/>。</param>
    /// <param name="devEnvironment">是否使用开发环境。</param>
    /// <returns></returns>
    public static Clients Build(IAuthenticationProvider authProvider, HttpClient httpClient, bool devEnvironment = false)
    {
        var requestAdapter0 = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
        var requestAdapter1 = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);

        if (devEnvironment)
        {
            requestAdapter0.BaseUrl = V0.ApiClient.BaseUrlDev;
            requestAdapter1.BaseUrl = P1.ApiClient.BaseUrlDev;
        }

        var v0Client = new V0.ApiClient(requestAdapter0);
        var p1Client = new P1.ApiClient(requestAdapter1);

        return new()
        {
            HttpClient = httpClient,
            RequestAdapter0 = requestAdapter0,
            RequestAdapter1 = requestAdapter1,
            V0Client = v0Client,
            P1Client = p1Client,
        };
    }
}

public class Clients
{
    public required HttpClient HttpClient { get; set; }
    public required IRequestAdapter RequestAdapter0 { get; set; }
    public required IRequestAdapter RequestAdapter1 { get; set; }
    public required V0.ApiClient V0Client { get; set; }
    public required P1.ApiClient P1Client { get; set; }
}
