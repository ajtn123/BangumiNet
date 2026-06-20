using System.ComponentModel;
using Avalonia;
using Avalonia.Media;
using FluentAvalonia.UI.Controls;
using FluentIcons.Common;
using FluentIcons.Avalonia.Internals;
using FluentIcons.Resources.Avalonia;
using FluentIcons.Common.Internals;

namespace FluentIcons.Avalonia.Fluent;

[TypeConverter(typeof(FluentIconConverter<FluentIconSource>))]
public class FluentIconSource : FAFontIconSource, IHasIcon
{
    public static TypeConverter Converter { get; } = new FluentIconConverter<FluentIconSource>();

    static FluentIconSource()
    {

        IconVariantProperty.Changed.AddClassHandler<FluentIconSource>(OnCorePropertyChanged);
        FontSizeProperty.Changed.AddClassHandler<FluentIconSource>(OnCorePropertyChanged);
        FlowDirectionProperty.Changed.AddClassHandler<FluentIconSource>(OnCorePropertyChanged);
        IconProperty.Changed.AddClassHandler<FluentIconSource>(OnCorePropertyChanged);
        IconSizeProperty.Changed.AddClassHandler<FluentIconSource>(OnCorePropertyChanged);

        GlyphProperty.OverrideMetadata<FluentIconSource>(
            new(coerce: static (o, v) => (o as FluentIconSource)?.IconText ?? v));
        FontSizeProperty.OverrideMetadata<FluentIconSource>(
            new(coerce: static (o, v) => (o as FluentIconSource)?.FontSize ?? v,
                defaultValue: FontSizeProperty.GetDefaultValue(typeof(FluentIconSource))));
        FontFamilyProperty.OverrideMetadata<FluentIconSource>(
            new(coerce: static (o, v) => (o as FluentIconSource)?.IconFont.FontFamily ?? v));
        FontStyleProperty.OverrideMetadata<FluentIconSource>(
            new(coerce: static (o, v) => FontStyle.Normal));
        FontWeightProperty.OverrideMetadata<FluentIconSource>(
            new(coerce: static (o, v) => FontWeight.Regular));
    }

    public FluentIconSource()
    {
        base.FontSize = FontSize;
        FontStyle = FontStyle.Normal;
        FontWeight = FontWeight.Regular;
        InvalidateText();
    }

    public IconVariant IconVariant
    {
        get => GetValue(IconVariantProperty);
        set => SetValue(IconVariantProperty, value);
    }
    public static readonly StyledProperty<IconVariant> IconVariantProperty
        = GenericIcon.IconVariantProperty.AddOwner<FluentIconSource>();

    public FlowDirection FlowDirection
    {
        get => GetValue(FlowDirectionProperty);
        set => SetValue(FlowDirectionProperty, value);
    }
    public static readonly StyledProperty<FlowDirection> FlowDirectionProperty
        = Visual.FlowDirectionProperty.AddOwner<FluentIconSource>();

    public new double FontSize
    {
        get => GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    public static new readonly StyledProperty<double> FontSizeProperty
        = GenericIcon.FontSizeProperty.AddOwner<FluentIconSource>();

    public Icon Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    public static readonly StyledProperty<Icon> IconProperty
        = FluentIcon.IconProperty.AddOwner<FluentIconSource>();

    public IconSize IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
    public static readonly StyledProperty<IconSize> IconSizeProperty
        = FluentIcon.IconSizeProperty.AddOwner<FluentIconSource>();

    protected string IconText => Icon.ToString(IconVariant, FlowDirection == FlowDirection.RightToLeft);
    protected Typeface IconFont => TypefaceManager.GetFluent(IconSize, IconVariant);

    protected static void OnCorePropertyChanged(FluentIconSource element, AvaloniaPropertyChangedEventArgs? _)
    {
        element.InvalidateText();
    }

    protected void InvalidateText()
    {
        Glyph = IconText;
        FontFamily = IconFont.FontFamily;
    }
}
