using Avalonia;
using Avalonia.Controls.Primitives;

namespace BangumiNet.Templates;

public class InfoBadge : TemplatedControl
{
    public InfoBadge()
    {
        this.WhenAnyValue(x => x.Text).Subscribe(t => IsVisible = !string.IsNullOrWhiteSpace(t));
    }

    public static readonly StyledProperty<string?> LabelProperty
        = AvaloniaProperty.Register<InfoBadge, string?>(nameof(Label));
    public string? Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public static readonly StyledProperty<string?> TextProperty
        = AvaloniaProperty.Register<InfoBadge, string?>(nameof(Text));
    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly StyledProperty<bool> IsHighlightedProperty
        = AvaloniaProperty.Register<InfoBadge, bool>(nameof(IsHighlighted));
    public bool IsHighlighted
    {
        get => GetValue(IsHighlightedProperty);
        set => SetValue(IsHighlightedProperty, value);
    }
}