using Avalonia.Media.Imaging;
using BangumiNet.Api;
using System.Net.Http;
using System.Threading.Tasks;

namespace BangumiNet.Utils;

public class ApiC
{
    public static Clients Clients { get; } = ClientBuilder.Build(SettingProvider.CurrentSettings);
    public static Api.V0.V0.V0RequestBuilder V0 => Clients.V0Client.V0;
    public static HttpClient HttpClient => Clients.HttpClient;

    public static async Task<Bitmap?> GetImageAsync(string? url, bool useCache = true)
    {
        if (string.IsNullOrWhiteSpace(url)) return null;
        useCache = useCache && SettingProvider.CurrentSettings.IsDiskCacheEnabled;
        Bitmap? result = null;

        if (useCache)
            try
            {
                using var cacheStream = CacheProvider.ReadCache(url);
                if (cacheStream is not null)
                    result = new Bitmap(cacheStream);
            }
            catch (Exception e) { Trace.TraceError(e.Message); CacheProvider.DeleteCache(url); }
        if (result != null) return result;

        var stream = (await HttpClient.GetStreamAsync(url)).Clone();
        if (useCache) CacheProvider.WriteCache(url, stream);
        result = new Bitmap(stream);

        return result;
    }
}
