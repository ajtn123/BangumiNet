using Avalonia.Controls;
using Avalonia.Controls.Documents;

namespace BangumiNet.ViewModels;

/// <summary>
/// For <see cref="SubjectListViewModel.SubjectViewModels"/>
/// </summary>
public partial class TextViewModel : ViewModelBase
{
    public TextViewModel(string? text)
    {
        Text = text;
        this.WhenAnyValue(x => x.Text).Subscribe(t => IsVisible = !string.IsNullOrWhiteSpace(t));
    }
    public TextViewModel(params object[] inlines)
    {
        Inlines = [.. inlines.Select(x => x switch {
            string str => new Run(str),
            Inline inline => inline,
            Control control => new InlineUIContainer(control),
            _ => new Run(x.ToString())
        })];
        this.WhenAnyValue(x => x.Inlines).Subscribe(i => IsVisible = i != null && i.Count != 0);
    }

    [Reactive] public partial string? Text { get; set; }
    [Reactive] public partial InlineCollection? Inlines { get; set; }
}
