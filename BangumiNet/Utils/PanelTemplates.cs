using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;

namespace BangumiNet.Utils;

public class SpacedStackPanel : MarkupExtension
{
    public double Spacing { get; set; } = 5;
    public override ITemplate<Panel?> ProvideValue(IServiceProvider provider)
        => new FuncTemplate<Panel?>(() => new StackPanel { Spacing = Spacing });
}
