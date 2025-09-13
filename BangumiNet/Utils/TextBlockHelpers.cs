using Avalonia;
using Avalonia.Controls;
using System;
using System.Net;

namespace BangumiNet.Utils;
public static class TextBlockHelpers
{
    public static readonly AttachedProperty<bool> DecodeHtmlProperty =
        AvaloniaProperty.RegisterAttached<TextBlock, bool>("DecodeHtml", typeof(TextBlockHelpers));

    static TextBlockHelpers()
        => DecodeHtmlProperty.Changed.AddClassHandler<TextBlock>((tb, e) =>
        {
            if (e.NewValue is bool enabled && enabled)
            {
                tb.Text = WebUtility.HtmlDecode(tb.Text);
                tb.GetPropertyChangedObservable(TextBlock.TextProperty).Subscribe(change =>
                {
                    if (change.NewValue is string raw) tb.Text = WebUtility.HtmlDecode(raw);
                });
            }
        });

    public static void SetDecodeHtml(TextBlock element, bool value)
        => element.SetValue(DecodeHtmlProperty, value);
    public static bool GetDecodeHtml(TextBlock element)
        => element.GetValue(DecodeHtmlProperty);
}
