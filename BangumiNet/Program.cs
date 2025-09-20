global using BangumiNet.Shared;
global using BangumiNet.Utils;
global using System;
global using System.Collections.Generic;
global using System.Linq;
using Avalonia;
using Avalonia.ReactiveUI;

namespace BangumiNet;

internal sealed class Program
{
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}
