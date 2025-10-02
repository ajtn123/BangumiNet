using FluentAvalonia.UI.Windowing;

namespace BangumiNet.Views;

public partial class SecondaryWindow : AppWindow
{
    public SecondaryWindow()
    {
        InitializeComponent();

        if (SettingProvider.CurrentSettings.ShowSplashScreenOnWindowStartup)
            SplashScreen = new WindowSplashScreen(this);
    }
}