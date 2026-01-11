using Avalonia.Controls;
using Avalonia.Controls.Documents;

namespace BangumiNet.ViewModels.Generic;

public partial class TextViewModel : ViewModelBase
{
    public TextViewModel(Func<object[]> builder)
        => Builder = builder;
    public TextViewModel(string text)
        => Builder = () => [text];

    private Func<object[]> Builder { get; set; }
    public InlineCollection? Inlines
        => [.. Builder().Select(x => x switch {
            string str => new Run(str),
            Inline inline => inline,
            Control control => new InlineUIContainer(control),
            _ => new Run(x.ToString()) })];

    [Reactive] public partial bool IsFocusable { get; set; }
}
