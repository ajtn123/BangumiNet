using Avalonia.Controls;
using Avalonia.Controls.Documents;

namespace BangumiNet.ViewModels;

/// <summary>
/// For <see cref="SubjectListViewModel.SubjectViewModels"/>
/// </summary>
public class TextViewModel : ViewModelBase
{
    private Func<object[]> Builder { get; set; }
    public TextViewModel(Func<object[]> builder)
        => Builder = builder;
    public TextViewModel(string str)
        => Builder = () => [str];
    public InlineCollection? Inlines
        => [..Builder().Select(x => x switch {
            string str => new Run(str),
            Inline inline => inline,
            Control control => new InlineUIContainer(control),
            _ => new Run(x.ToString())
        })];
}
