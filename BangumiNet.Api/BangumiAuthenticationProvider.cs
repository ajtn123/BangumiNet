using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using static BangumiNet.Api.BangumiAuthenticationProvider;

namespace BangumiNet.Api;

public class BangumiAuthenticationProvider(string? token, AuthenticationMethod methods = AuthenticationMethod.All) : IAuthenticationProvider
{
    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        if (methods.HasFlag(AuthenticationMethod.Referer))
            request.Headers.Add("Referer", P1.ApiClient.BaseUrl);

        if (!string.IsNullOrWhiteSpace(token))
        {
            if (methods.HasFlag(AuthenticationMethod.Bearer))
                request.Headers.Add("Authorization", $"Bearer {token}");

            if (methods.HasFlag(AuthenticationMethod.Cookie))
                request.Headers.Add("Cookie", $"chiiNextSessionID={token}");
        }

        return Task.CompletedTask;
    }

    [Flags]
    public enum AuthenticationMethod
    {
        None = 0,

        Bearer = 1 << 0,
        Cookie = 1 << 1,

        Referer = 1 << 2,

        All = Bearer | Cookie | Referer,
    }
}
