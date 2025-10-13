using BangumiNet.Shared;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;

namespace BangumiNet.Api;

public class BangumiAuthenticationProvider : IAuthenticationProvider
{
    public Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
    {
        var token = SettingProvider.CurrentSettings.AuthToken;
        if (string.IsNullOrWhiteSpace(token)) return Task.CompletedTask;

        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Headers.Add("Cookie", $"chiiNextSessionID={token}");
        return Task.CompletedTask;
    }
}
