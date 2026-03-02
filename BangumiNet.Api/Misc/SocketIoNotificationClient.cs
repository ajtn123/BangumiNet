namespace BangumiNet.Api.Misc;

//https://github.com/bangumi/server-private/blob/master/docs/socket.io.md
public class SocketIoNotificationClient : IDisposable
{
    public record NotifyEvent(int Count);

    public static readonly Uri Host = new("https://next.bgm.tv/");
    public const string Path = "/p1/socket-io/";
    public const string NotifyEventName = "notify";
    public const string ErrorEventName = "connect_error";

    public SocketIoNotificationClient(IApiSettings settings, Func<NotifyEvent?, Task> callback)
    {
        Client = GetClient(settings);
        Client.On(NotifyEventName, response => callback(response.GetValue<NotifyEvent>(0)));
    }

    private static SocketIOClient.SocketIO GetClient(IApiSettings settings)
    {
        var client = new SocketIOClient.SocketIO(Host, new SocketIOClient.SocketIOOptions()
        {
            Path = Path,
            Reconnection = true,
            ReconnectionDelayMax = 10000,
            Transport = SocketIOClient.Common.TransportProtocol.WebSocket,
        });
        client.Options.ExtraHeaders = new Dictionary<string, string>()
        {
            ["Cookie"] = $"chiiNextSessionID={settings.AuthToken}",
            ["User-Agent"] = settings.UserAgent,
        };
        client.On(ErrorEventName, response =>
        {
            client.Options.AutoUpgrade = true;
            client.Options.Transport = SocketIOClient.Common.TransportProtocol.Polling;
            System.Diagnostics.Trace.TraceWarning($"Bangumi socket.io notification emitted an error, downgrading transport protocol.");
            return Task.CompletedTask;
        });
        return client;
    }

    public SocketIOClient.SocketIO Client { get; }
    public Task ConnectAsync() => Client.ConnectAsync();
    public Task DisconnectAsync() => Client.DisconnectAsync();

    public void Dispose() => Client.Dispose();
}
