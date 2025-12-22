using System.Net.Http.Json;
using System.Reflection;

namespace BangumiNet.Shared;

public static class Updater
{
    /// <returns>如果有更新，返回最新版本。如果没有，返回 null。</returns>
    public static async Task<Version?> CheckWith(HttpClient httpClient)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/ajtn123/BangumiNet/releases/latest");
        request.Headers.Accept.ParseAdd("application/vnd.github+json");
        request.Headers.TryAddWithoutValidation("X-GitHub-Api-Version", "2022-11-28");

        using var response = await httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var release = await response.Content.ReadFromJsonAsync<Release>();
        var latestVersion = new Version(release.tag_name.TrimStart('v'));

        var currentVersion = Assembly.GetEntryAssembly()!.GetName().Version;
        if (latestVersion > currentVersion)
            return latestVersion;
        else
            return null;
    }

    public record struct Release(string tag_name);
}
