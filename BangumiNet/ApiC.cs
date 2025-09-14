using BangumiNet.Api;
using BangumiNet.Shared;
using System.Net.Http;

namespace BangumiNet;

public class ApiC
{
    public static Clients Clients { get; } = ClientBuilder.Build(SettingProvider.CurrentSettings);
    public static Api.V0.V0.V0RequestBuilder V0 => Clients.V0Client.V0;
    public static HttpClient HttpClient => Clients.HttpClient;
}
