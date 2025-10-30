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
    public TextViewModel(InlineCollection inlines)
    {
        Inlines = inlines;
    }
    [Reactive] public partial string? Text { get; set; }
    [Reactive] public partial InlineCollection? Inlines { get; set; }
}
