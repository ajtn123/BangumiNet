using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using FluentAvalonia.UI.Controls;
using FluentIcons.Common;
using FluentIcons.Common.Internals;
using FluentIcons.Resources.Avalonia;

namespace FluentIcons.Avalonia.Fluent;

[TypeConverter(typeof(FluentIconConverter<FluentIcon>))]
public class FluentIcon : FAIconElement, IHasIcon
{
    public static TypeConverter Converter { get; } = new FluentIconConverter<FluentIcon>();

    static FluentIcon()
    {
        IconVariantProperty.Changed.AddClassHandler<FluentIcon>(OnCorePropertyChanged);
        FontSizeProperty.Changed.AddClassHandler<FluentIcon>(OnCorePropertyChanged);
        ForegroundProperty.Changed.AddClassHandler<FluentIcon>(OnCorePropertyChanged);
        FlowDirectionProperty.Changed.AddClassHandler<FluentIcon>(OnCorePropertyChanged);
        IconProperty.Changed.AddClassHandler<FluentIcon>(OnCorePropertyChanged);
        IconSizeProperty.Changed.AddClassHandler<FluentIcon>(OnCorePropertyChanged);
    }

    private readonly Panel _panel;
    private readonly Core _core;

    public FluentIcon()
    {
        _panel = new();
        _panel.Bind(WidthProperty, this.GetBindingObservable(WidthProperty));
        _panel.Bind(HeightProperty, this.GetBindingObservable(HeightProperty));
        _panel.Bind(HorizontalAlignmentProperty, this.GetBindingObservable(HorizontalAlignmentProperty));
        _panel.Bind(VerticalAlignmentProperty, this.GetBindingObservable(VerticalAlignmentProperty));

        (_panel as ISetLogicalParent).SetParent(this);
        VisualChildren.Add(_panel);
        LogicalChildren.Add(_panel);

        _core = new(FontSize);
        _panel.Children.Add(_core);
    }

    public IconVariant IconVariant
    {
        get => GetValue(IconVariantProperty);
        set => SetValue(IconVariantProperty, value);
    }
    public static readonly StyledProperty<IconVariant> IconVariantProperty
        = AvaloniaProperty.Register<FluentIcon, IconVariant>(nameof(IconVariant));

    public double FontSize
    {
        get => GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    public static readonly StyledProperty<double> FontSizeProperty
        = AvaloniaProperty.Register<FluentIcon, double>(nameof(FontSize), 20d, false);

    public Icon Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    public static readonly StyledProperty<Icon> IconProperty
        = AvaloniaProperty.Register<FluentIcon, Icon>(nameof(Icon), Icon.Home);

    public IconSize IconSize
    {
        get => GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
    public static readonly StyledProperty<IconSize> IconSizeProperty
        = AvaloniaProperty.Register<FluentIcon, IconSize>(nameof(IconSize), default);

    protected string IconText => Icon.ToString(IconVariant, FlowDirection == FlowDirection.RightToLeft);
    protected Typeface IconFont => TypefaceManager.GetFluent(IconSize, IconVariant);

    protected override void OnLoaded(RoutedEventArgs e)
    {
        InvalidateText();
        base.OnLoaded(e);
    }

    protected override void OnUnloaded(RoutedEventArgs e)
    {
        _core.Clear();
        base.OnUnloaded(e);
    }

    protected static void OnCorePropertyChanged(FluentIcon element, AvaloniaPropertyChangedEventArgs? _)
    {
        element.InvalidateText();
    }

    protected void InvalidateText()
        => _core.Update(IconText, IconFont, FontSize, Foreground);

    internal void AddHandIn(Control control)
        => _panel.Children.Add(control);

    internal bool RemoveHandIn(Control control)
        => _panel.Children.Remove(control);

    internal sealed class Core(double size) : Control
    {
        private bool _updating = false;

        private string? _text;
        private Typeface _typeface;
        private double _size = size;
        private IBrush? _foreground;

        private TextLayout? _textLayout;

        protected override Size MeasureOverride(Size availableSize)
            => new(Math.Min(_size, availableSize.Width),
                   Math.Min(_size, availableSize.Height));

        public void Update(string text, Typeface typeface, double fontSize, IBrush? foreground)
        {
            if (_size != fontSize) InvalidateMeasure();
            _text = text;
            _typeface = typeface;
            _size = fontSize;
            _foreground = foreground;

            _updating = true;
            InvalidateVisual();
        }

        public override void Render(DrawingContext context)
        {
            if (_updating || _textLayout is null)
            {
                _updating = false;
                _textLayout?.Dispose();
                _textLayout = new TextLayout(
                    _text,
                    _typeface,
                    _size,
                    _foreground,
                    TextAlignment.Center,
                    flowDirection: FlowDirection);
            }

            Rect bounds = Bounds;
            using (context.PushClip(new Rect(bounds.Size)))
            {
                IDisposable? flip = null;
                if (FlowDirection == FlowDirection.RightToLeft)
                    flip = context.PushTransform(new Matrix(-1, 0, 0, 1, bounds.Width, 0));
                var origin = new Point(
                    (bounds.Width - _size) / 2,
                    (bounds.Height - _size) / 2);
                _textLayout.Draw(context, origin);
                flip?.Dispose();
            }
        }

        public void Clear()
        {
            _textLayout?.Dispose();
            _textLayout = null;
        }
    }
}
