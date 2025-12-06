namespace BangumiNet.Api.Misc;

//https://github.com/bangumi/server-private/blob/master/docs/socket.io.md
public class SocketIoNotificationClient
{
    public record NotifyEvent(int Count);

    public const string Host = "https://next.bgm.tv/";
    public const string Path = "/p1/socket-io/";
    public const string NotifyEventName = "notify";
    public const string ErrorEventName = "connect_error";

    public SocketIoNotificationClient(IApiSettings settings, Action<NotifyEvent> callback)
    {
        Client = GetClient(settings);
        Client.On(NotifyEventName, r => callback(r.GetValue<NotifyEvent>()));
    }
    public SocketIoNotificationClient(IApiSettings settings, Func<NotifyEvent, Task> callback)
    {
        Client = GetClient(settings);
        Client.On(NotifyEventName, r => callback(r.GetValue<NotifyEvent>()));
    }

    private static SocketIOClient.SocketIO GetClient(IApiSettings settings)
    {
        var client = new SocketIOClient.SocketIO(Host, new()
        {
            Path = Path,
            Reconnection = true,
            ReconnectionDelay = 5000,
            ReconnectionDelayMax = 10000,
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket,
        });
        client.Options.ExtraHeaders = new()
        {
            ["Cookie"] = $"chiiNextSessionID={settings.AuthToken}",
            ["User-Agent"] = settings.UserAgent,
        };
        client.On(ErrorEventName, response =>
        {
            client.Options.AutoUpgrade = true;
            client.Options.Transport = SocketIOClient.Transport.TransportProtocol.Polling;
        });
        return client;
    }

    public SocketIOClient.SocketIO Client { get; }
    public Task ConnectAsync() => Client.ConnectAsync();
    public Task DisconnectAsync() => Client.DisconnectAsync();
}
