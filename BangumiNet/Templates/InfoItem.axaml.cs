using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using FluentIcons.Common;

namespace BangumiNet.Templates;

public class InfoItem : TemplatedControl
{
    public InfoItem()
    {
        if (Design.IsDesignMode) Text = "DInfo";
        this.WhenAnyValue(x => x.Text).Subscribe(t => { if (!Design.IsDesignMode) IsVisible = !string.IsNullOrWhiteSpace(t); });
    }

    public static readonly StyledProperty<Icon?> IconProperty =
        AvaloniaProperty.Register<InfoItem, Icon?>(nameof(Icon));
    public Icon? Icon
    {
        get => GetValue(IconProperty);
        set { IconR = value ?? FluentIcons.Common.Icon.Dismiss; SetValue(IconProperty, value); }
    }

    public static readonly StyledProperty<Icon> IconRProperty =
        AvaloniaProperty.Register<InfoItem, Icon>(nameof(IconR));
    public Icon IconR
    {
        get => GetValue(IconRProperty);
        set => SetValue(IconRProperty, value);
    }

    public static readonly StyledProperty<IconVariant> IconVariantProperty =
        AvaloniaProperty.Register<InfoItem, IconVariant>(nameof(IconVariant));
    public IconVariant IconVariant
    {
        get => GetValue(IconVariantProperty);
        set => SetValue(IconVariantProperty, value);
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