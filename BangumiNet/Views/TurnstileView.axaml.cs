using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class TurnstileView : UserControl
{
    public TurnstileView() => InitializeComponent();

    //public TurnstileView()
    //{
    //    InitializeComponent();
    //    WebViewControl.Url = Uri;
    //    WebViewControl.NavigationStarting += WebViewControl_NavigationStarting;
    //    Task.Run(async () =>
    //    {
    //        await Task.Delay(2 * 60 * 1000);
    //        WebViewControl.NavigationStarting -= WebViewControl_NavigationStarting;
    //        if (!returned)
    //        {
    //            tcs.TrySetResult(null);
    //            returned = true;
    //        }
    //    });
    //}

    //private void WebViewControl_NavigationStarting(object? sender, WebViewCore.Events.WebViewUrlLoadingEventArg e)
    //{
    //    var url = e.Url?.OriginalString ?? string.Empty;
    //    if (url.StartsWith("bangumi:") && url.Contains("?token=") && !returned)
    //    {
    //        tcs.TrySetResult(url.Split("?token=").Last());
    //        returned = true;
    //    }
    //}

    //public Task<string?> GetToken() => tcs.Task;

    //private static Uri Uri => new("https://next.bgm.tv/p1/turnstile?theme=auto&redirect_uri=bangumi%3A%2F%2F");

    //private readonly TaskCompletionSource<string?> tcs = new();
    //private bool returned = false;
}
