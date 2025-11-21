using Avalonia.Controls;
using Avalonia.Controls.Documents;

namespace BangumiNet.ViewModels;

/// <summary>
/// For <see cref="SubjectListViewModel.SubjectViewModels"/>
/// </summary>
public partial class TextViewModel : ViewModelBase
{
    public TextViewModel(params object[] inlines)
        => SetInlines(inlines);

    public void SetInlines(params object[] inlines)
        => Inlines = [.. inlines.Select(x => x switch {
            string str => new Run(str),
            Inline inline => inline,
            Control control => new InlineUIContainer(control),
            _ => new Run(x.ToString())
        })];

    [Reactive] public partial InlineCollection? Inlines { get; private set; }
}
