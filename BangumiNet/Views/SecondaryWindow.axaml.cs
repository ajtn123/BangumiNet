using Avalonia.Controls;
using FluentAvalonia.UI.Windowing;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Views;

public partial class SecondaryWindow : AppWindow
{
    private readonly CompositeDisposable disposables = [];
    private readonly CompositeDisposable disposablesForContent = [];
    public SecondaryWindow()
    {
        InitializeComponent();

        if (SettingProvider.CurrentSettings.ShowSplashScreenOnWindowStartup)
            SplashScreen = new WindowSplashScreen(this);

        this.WhenAnyValue(x => x.Content).Subscribe(c =>
        {
            disposablesForContent.Clear();
            ViewModelBase? viewModel = null;
            if (c is ViewModelBase vm) viewModel = vm;
            else if (c is Control control && control.DataContext is ViewModelBase vm2) viewModel = vm2;
            if (viewModel == null) return;

            viewModel.WhenAnyValue(x => x.Title).Subscribe(title =>
            {
                Title = title;
            }).DisposeWith(disposablesForContent);
        }).DisposeWith(disposables);

        Closing += (s, e) =>
        {
            disposables.Clear();
            disposablesForContent.Clear();
        };
    }

    public static SecondaryWindow Show(ViewModelBase? data)
    {
        ArgumentNullException.ThrowIfNull(data);
        var window = new SecondaryWindow { Content = data };
        window.Show();
        return window;
    }
}