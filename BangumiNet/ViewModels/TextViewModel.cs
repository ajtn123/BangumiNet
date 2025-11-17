using Avalonia.Controls;
using Avalonia.Controls.Documents;
using BangumiNet.Templates;

namespace BangumiNet.ViewModels;

/// <summary>
/// For <see cref="SubjectListViewModel.SubjectViewModels"/>
/// </summary>
public partial class TextViewModel : ViewModelBase
{
    public TextViewModel(string? text)
    {
        Init();
        Text = text;
    }
    public TextViewModel(params object[] inlines)
    {
        Init();
        Inlines = [.. inlines.Select(x => x switch {
            string str => new Run(str),
            Inline inline => inline,
            Control control => new InlineUIContainer(control),
            _ => new Run(x.ToString())
        })];
    }

    private void Init()
    {
        this.WhenAnyValue(x => x.Text).Subscribe(t =>
        {
            if (string.IsNullOrWhiteSpace(t))
            {
                IsVisible = false;
                Content = null;
            }
            else if (Content is not BBCode)
            {
                IsVisible = true;
                var bbcode = new BBCode();
                bbcode.Bind(BBCode.TextProperty, this.WhenAnyValue(x => x.Text));
                Content = bbcode;
            }
        });
        this.WhenAnyValue(x => x.Inlines).Subscribe(i =>
        {
            if (i == null)
            {
                IsVisible = false;
                Content = null;
            }
            else if (Content is not SelectableTextBlock)
            {
                IsVisible = true;
                var tb = new SelectableTextBlock { TextWrapping = Avalonia.Media.TextWrapping.Wrap };
                tb.Bind(TextBlock.InlinesProperty, this.WhenAnyValue(x => x.Inlines));
                Content = tb;
            }
        });
    }

    [Reactive] public partial string? Text { get; set; }
    [Reactive] public partial InlineCollection? Inlines { get; set; }
    [Reactive] public partial Control? Content { get; set; }
}
