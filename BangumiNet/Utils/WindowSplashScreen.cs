using Avalonia.Media;
using FluentAvalonia.UI.Windowing;

namespace BangumiNet.Utils;

public class WindowSplashScreen(AppWindow owner) : IApplicationSplashScreen
{
    public string AppName => Constants.ApplicationName;
    public IImage? AppIcon => null;
    public object? SplashScreenContent => null;
    public int MinimumShowTime => 1000;

    public Task RunTasks(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private readonly AppWindow owner = owner;
}
