using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using BangumiNet.Converters;
using FluentIcons.Common;

namespace BangumiNet.Templates;

public class InfoItem : TemplatedControl
{
    public InfoItem()
    {
        this.WhenAnyValue(x => x.CommonText).Subscribe(obj => Text = CommonConverter.Convert(obj));
        this.WhenAnyValue(x => x.EnumText).Subscribe(obj => Text = CommonEnumConverter.Convert(obj));
        this.WhenAnyValue(x => x.Text).Subscribe(t => IsVisible = !string.IsNullOrWhiteSpace(t));
        if (Design.IsDesignMode) Text ??= "DInfo";
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

    public static readonly StyledProperty<object?> CommonTextProperty =
        AvaloniaProperty.Register<InfoItem, object?>(nameof(CommonText));
    /// <summary>
    /// Set <see cref="Text"/> with <see cref="CommonConverter.Convert(object?)"/>.
    /// </summary>
    public object? CommonText
    {
        get => GetValue(CommonTextProperty);
        set => SetValue(CommonTextProperty, value);
    }

    public static readonly StyledProperty<Enum?> EnumTextProperty =
        AvaloniaProperty.Register<InfoItem, Enum?>(nameof(EnumText));
    /// <summary>
    /// Set <see cref="Text"/> with <see cref="CommonEnumConverter.Convert(object?)"/>.
    /// </summary>
    public Enum? EnumText
    {
        get => GetValue(EnumTextProperty);
        set => SetValue(EnumTextProperty, value);
    }
}