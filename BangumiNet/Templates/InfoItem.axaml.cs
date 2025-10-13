using Avalonia;
using Avalonia.Controls.Primitives;
using FluentIcons.Common;

namespace BangumiNet.Templates;

public class InfoItem : TemplatedControl
{
    public InfoItem()
    {
        this.WhenAnyValue(x => x.Text).Subscribe(t => IsVisible = !string.IsNullOrWhiteSpace(t));
    }

    public static readonly StyledProperty<Icon?> IconProperty =
        AvaloniaProperty.Register<InfoItem, Icon?>(nameof(Icon));
    public Icon? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<InfoItem, string?>(nameof(Text));
    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly StyledProperty<string?> TipProperty =
        AvaloniaProperty.Register<InfoItem, string?>(nameof(Tip));
    public string? Tip
    {
        get => GetValue(TipProperty);
        set => SetValue(TipProperty, value);
    }
}