using BangumiNet.Api.Interfaces;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using static BangumiNet.Api.BangumiAuthenticationProvider;

namespace BangumiNet.Api;

public class BangumiAuthenticationProvider(AuthenticationContext context) : IAuthenticationProvider, IHttpAuthenticationProvider
{
    public BangumiAuthenticationProvider(string? token) : this(AuthenticationContext.FromToken(token)) { }

    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        if (context.Bearer is { } bearer)
            request.Headers.Add("Authorization", $"Bearer {bearer}");

        if (context.Cookie is { } cookie)
            request.Headers.Add("Cookie", cookie);

        if (context.Referer is { } referer)
            request.Headers.Add("Referer", referer);

        return Task.CompletedTask;
    }

    public Task AuthenticateRequestAsync(HttpRequestMessage request)
    {
        if (context.Bearer is { } bearer)
            request.Headers.Add("Authorization", $"Bearer {bearer}");

        if (context.Cookie is { } cookie)
            request.Headers.Add("Cookie", cookie);

        if (context.Referer is { } referer)
            request.Headers.Add("Referer", referer);

        return Task.CompletedTask;
    }

    public class AuthenticationContext
    {
        public static AuthenticationContext FromToken(string? token) => new()
        {
            Bearer = token,
            Cookie = $"chiiNextSessionID={token}",
            Referer = P1.ApiClient.BaseUrlTrailing,
        };

        public string? Bearer { get; set; }
        public string? Cookie { get; set; }
        public string? Referer { get; set; }
    }
}
