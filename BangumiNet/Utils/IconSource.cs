using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;
using FluentIcons.Avalonia;
using FluentIcons.Common;

namespace BangumiNet.Utils;

public class IconSource(Icon icon) : MarkupExtension
{
    private readonly Icon icon = icon;
    public override ImageIconSource ProvideValue(IServiceProvider provider)
        => new() { Source = new FluentImage { Icon = icon } };
}
