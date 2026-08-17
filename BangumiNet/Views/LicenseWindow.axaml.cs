using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Input.Platform;
using BangumiNet.Models;
using FluentAvalonia.UI.Windowing;

namespace BangumiNet.Views;

public partial class LicenseWindow : FAAppWindow
{
    public LicenseWindow()
    {
        InitializeComponent();

        CopyButton.Click += async (s, e) =>
        {
            if (DataContext is LicenseWindowViewModel { Item.LicenseText: { } text } && Clipboard is { } cb)
                await cb.SetTextAsync(text);
        };
        OpenUrlButton.Click += (s, e) =>
        {
            if (DataContext is LicenseWindowViewModel { Item.LicenseUrl: { } url })
                CommonUtils.OpenUri(url);
        };
        OpenRepoButton.Click += (s, e) =>
        {
            if (DataContext is LicenseWindowViewModel { Item.RepositoryUrl: { } url })
                CommonUtils.OpenUri(url);
        };
    }

    public static void Show(LicenseItem item) => new LicenseWindow { DataContext = new LicenseWindowViewModel(item) }.Show();
}
