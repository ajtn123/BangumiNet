using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Templates;

public class BBCode : TemplatedControl
{
    private ContentPresenter? contentPresenter;
    private readonly CompositeDisposable disposables = [];

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        contentPresenter = e.NameScope.Find<ContentPresenter>("PART_Text");
        disposables.Clear();

        this.WhenAnyValue(x => x.Text).Subscribe(text =>
        {
            contentPresenter!.Content = BBCodeRenderer.Render(text);
        }).DisposeWith(disposables);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        disposables.Clear();
    }

    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<BBCode, string?>(nameof(Text));
    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}
