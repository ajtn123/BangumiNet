using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using BangumiNet.Converters;
using BangumiNet.ViewModels;
using BangumiNet.Views;
using System.Net;

namespace BangumiNet;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel() };

        AddConverter(NameCnCvt.Instance);
        AddConverter(NullCvt.Instance);
        AddConverter(InNullCvt.Instance);
        AddConverter(EpBrushCvt.Instance);

        CacheProvider.CalculateCacheSize();

        TextBlock.TextProperty.Changed.AddClassHandler<TextBlock>((tb, e) => tb.Text = WebUtility.HtmlDecode(tb.Text));

        base.OnFrameworkInitializationCompleted();
    }

    private void AddConverter(IValueConverter res)
        => Resources[res.GetType().Name] = res;
}
