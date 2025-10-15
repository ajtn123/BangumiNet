using Avalonia.Media;
using FluentAvalonia.UI.Windowing;

namespace BangumiNet.Utils;

public class WindowSplashScreen(AppWindow owner) : IApplicationSplashScreen
{
    public string AppName => Shared.Constants.ApplicationName;
    public IImage? AppIcon => null;
    public object? SplashScreenContent => null;
    public int MinimumShowTime => 1000;
    public Action? InitApp => null;

    public Task RunTasks(CancellationToken cancellationToken)
    {
        if (InitApp == null) return Task.CompletedTask;
        else return Task.Run(InitApp, cancellationToken);
    }

    private readonly AppWindow owner = owner;
}
