using Avalonia.Controls;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class TurnstileView : UserControl
{
    public TurnstileView()
    {
        InitializeComponent();
        WebViewControl.Url = uri;
        WebViewControl.NavigationStarting += (s, e) =>
        {
            var url = e.Url?.AbsolutePath ?? string.Empty;
            if (url.StartsWith("chii:") && url.Contains("?token="))
                Token = url.Split("?token=").Last();
        };
    }

    private static readonly Uri uri = new("https://next.bgm.tv/p1/turnstile?theme=auto&redirect_uri=chii%3A%2F%2F");

    public string? Token { get; private set; }

    public Task<string> GetToken()
    {
        var tcs = new TaskCompletionSource<string>();

        if (!string.IsNullOrWhiteSpace(Token))
            tcs.SetResult(Token);
        else
            this.WhenAnyValue(x => x.Token)
                .Where(token => !string.IsNullOrWhiteSpace(token))
                .Take(1)
                .Subscribe(tcs.SetResult!);

        return tcs.Task;
    }
}
