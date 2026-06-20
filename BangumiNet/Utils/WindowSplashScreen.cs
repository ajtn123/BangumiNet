using Avalonia.Media;
using FluentAvalonia.UI.Windowing;

namespace BangumiNet.Utils;

public class WindowSplashScreen(FAAppWindow owner) : IFAApplicationSplashScreen
{
    public string AppName => Constants.ApplicationName;
    public IImage? AppIcon => null;
    public object? SplashScreenContent => null;
    public int MinimumShowTime => 1000;

    public Task RunTasks(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private readonly FAAppWindow owner = owner;
}
