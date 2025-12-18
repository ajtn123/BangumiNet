using FluentAvalonia.UI.Windowing;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class SecondaryWindow : AppWindow
{
    private readonly CompositeDisposable disposables = [];
    public SecondaryWindow()
    {
        InitializeComponent();

        if (SettingProvider.CurrentSettings.ShowSplashScreenOnWindowStartup)
            SplashScreen = new WindowSplashScreen(this);

        this.WhenAnyValue(x => x.Content)
            .WhereNotNull()
            .OfType<ViewModelBase>()
            .Select(content => content.WhenAnyValue(x => x.Title))
            .Switch()
            .Subscribe(title => Title = title)
            .DisposeWith(disposables);

        Closed += (s, e) => disposables.Dispose();
    }

    public static SecondaryWindow Show(ViewModelBase? data)
    {
        var window = new SecondaryWindow { Content = data };
        window.Show();
        return window;
    }
}
