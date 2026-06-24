using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using static BangumiNet.Api.BangumiAuthenticationProvider;

namespace BangumiNet.Api;

public class BangumiAuthenticationProvider(AuthenticationContext context) : IAuthenticationProvider
{
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

    public record AuthenticationContext(string? Bearer, string? Cookie, string? Referer)
    {
        public static AuthenticationContext FromToken(string? token) => new(
            Bearer: token,
            Cookie: $"chiiNextSessionID={token}",
            Referer: P1.ApiClient.BaseUrlTrailing);
    }
}
