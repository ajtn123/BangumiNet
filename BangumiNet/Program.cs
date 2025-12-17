global using BangumiNet.Shared;
global using BangumiNet.Utils;
global using BangumiNet.ViewModels;
global using BangumiNet.Views;
global using ReactiveUI;
global using ReactiveUI.Avalonia;
global using ReactiveUI.SourceGenerators;
global using System.Collections.ObjectModel;
global using System.Diagnostics;
using Avalonia;

namespace BangumiNet;

internal sealed class Program
{
    [STAThread]
    public static void Main(string[] args)
        => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
    //      .UseDesktopWebView();
}
