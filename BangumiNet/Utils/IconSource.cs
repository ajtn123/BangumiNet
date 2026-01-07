using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;
using FluentIcons.Common;

namespace BangumiNet.Utils;

public class IconSource(Icon icon) : MarkupExtension
{
    private readonly Icon icon = icon;
    public override ImageIconSource ProvideValue(IServiceProvider provider)
        => FromIcon(icon);
    public static ImageIconSource FromIcon(Icon icon)
        => new() { Source = IconHelper.GetFluentImage(icon) };
}
