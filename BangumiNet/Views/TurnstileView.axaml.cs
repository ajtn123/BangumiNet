using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class TurnstileView : UserControl
{
    private readonly TaskCompletionSource<string?> tcs = new();
    private readonly CancellationTokenSource cts = new();
    private bool returned;

    public TurnstileView()
    {
        InitializeComponent();
        WebViewControl.NavigationStarted += OnNavigationStarted;
        WebViewControl.WebMessageReceived += OnWebMessageReceived;
        WebViewControl.Source = TurnstileUri;
        TimeoutAsync(cts.Token);
    }

    private static Uri TurnstileUri => new("https://next.bgm.tv/p1/turnstile?theme=auto&redirect_uri=bangumi%3A%2F%2F");
    private const string QueryLeading = "?token=";

    private async void OnNavigationStarted(object? sender, WebViewNavigationStartingEventArgs e)
    {
        if (returned)
        {
            e.Cancel = true;
            return;
        }
        if (e.Request is not { } uri)
        {
            return;
        }

        if (uri.Scheme is "bangumi" && uri.Query.StartsWith(QueryLeading))
        {
            tcs.TrySetResult(uri.Query[QueryLeading.Length..]);
            returned = true;
        }
    }

    private void OnWebMessageReceived(object? sender, WebMessageReceivedEventArgs e)
    {
        if (returned) return;
        returned = true;

        cts.Cancel();
        tcs.TrySetResult(e.Body);
    }

    private async void TimeoutAsync(CancellationToken ct)
    {
        try
        {
            await Task.Delay(2 * 60 * 1000, ct);
        }
        catch (OperationCanceledException)
        {
            return;
        }

        if (!returned)
        {
            Trace.TraceError("TurnstileView Timeout");
            returned = true;
            tcs.TrySetResult(null);
        }
    }

    public Task<string?> GetToken() => tcs.Task;
}
