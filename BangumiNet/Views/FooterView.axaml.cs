using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Reflection;

namespace BangumiNet.Views;

public partial class FooterView : UserControl
{
    public FooterView()
    {
        InitializeComponent();
        VersionText.Text = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
    }
    private void OpenGitHub(object? sender, RoutedEventArgs e)
        => CommonUtils.OpenUri(Constants.SourceRepository);
    private void OpenBangumi(object? sender, RoutedEventArgs e)
        => CommonUtils.OpenUri(UrlProvider.BangumiUrl);
    private void OpenBgm(object? sender, RoutedEventArgs e)
        => CommonUtils.OpenUri(UrlProvider.BgmUrl);
    private void OpenChii(object? sender, RoutedEventArgs e)
        => CommonUtils.OpenUri(UrlProvider.ChiiUrl);
}