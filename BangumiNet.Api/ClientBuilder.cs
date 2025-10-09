using BangumiNet.Api.Html;
using BangumiNet.Shared.Interfaces;
using Microsoft.Extensions.Caching.Abstractions;
using Microsoft.Extensions.Caching.InMemory;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;

namespace BangumiNet.Api;

public static class ClientBuilder
{
    public static P1.ApiClient GetP1Client(IApiSettings setting) => new(GetRequestAdapter(setting));
    public static V0.ApiClient GetV0Client(IApiSettings setting) => new(GetRequestAdapter(setting));
    public static Legacy.ApiClient GetLegacyClient(IApiSettings setting) => new(GetRequestAdapter(setting));

    private static HttpClientRequestAdapter GetRequestAdapter(IApiSettings setting)
    {
        var authProvider = new AnonymousAuthenticationProvider();
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", setting.UserAgent);
        if (!string.IsNullOrWhiteSpace(setting.AuthToken))
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", setting.AuthToken);

        return new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
    }

    public static Clients Build(IApiSettings setting)
    {
        var authProvider = new AnonymousAuthenticationProvider();
        var httpClientHandler = new HttpClientHandler();
        var cacheExpirationPerHttpResponseCode = CacheExpirationProvider.CreateSimple(TimeSpan.MaxValue, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
        var handler = new InMemoryCacheHandler(httpClientHandler, cacheExpirationPerHttpResponseCode);
        var httpClient = new HttpClient(handler);
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", setting.UserAgent);
        if (!string.IsNullOrWhiteSpace(setting.AuthToken))
            httpClient.DefaultRequestHeaders.Authorization = new("Bearer", setting.AuthToken);
        var requestAdapter = new HttpClientRequestAdapter(authProvider, httpClient: httpClient);
        var p1Client = new P1.ApiClient(requestAdapter);
        var v0Client = new V0.ApiClient(requestAdapter);
        var legacyClient = new Legacy.ApiClient(requestAdapter);
        var htmlClient = new HtmlClient(setting, httpClient);

        return new()
        {
            AuthenticationProvider = authProvider,
            HttpClient = httpClient,
            RequestAdapter = requestAdapter,
            P1Client = p1Client,
            V0Client = v0Client,
            LegacyClient = legacyClient,
            HtmlClient = htmlClient
        };
    }
}

public class Clients
{
    public required IAuthenticationProvider AuthenticationProvider { get; set; }
    public required HttpClient HttpClient { get; set; }
    public required IRequestAdapter RequestAdapter { get; set; }
    public required P1.ApiClient P1Client { get; set; }
    public required V0.ApiClient V0Client { get; set; }
    public required Legacy.ApiClient LegacyClient { get; set; }
    public required HtmlClient HtmlClient { get; set; }
}
