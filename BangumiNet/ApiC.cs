using Avalonia.Media.Imaging;
using BangumiNet.Api;
using BangumiNet.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace BangumiNet;

public class ApiC
{
    public static Clients Clients { get; } = ClientBuilder.Build(SettingProvider.CurrentSettings);
    public static Api.V0.V0.V0RequestBuilder V0 => Clients.V0Client.V0;
    public static HttpClient HttpClient => Clients.HttpClient;

    public static async Task<Bitmap?> GetImageAsync(string? url)
    {
        if (string.IsNullOrWhiteSpace(url)) return null;

        using var cacheStream = CacheProvider.ReadCache(url);
        if (cacheStream is not null)
            return new Bitmap(cacheStream);

        var stream = (await HttpClient.GetStreamAsync(url)).Clone();
        CacheProvider.WriteCache(url, stream);
        return new Bitmap(stream);
    }
}
