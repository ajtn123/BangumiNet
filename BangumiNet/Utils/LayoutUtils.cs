using Avalonia;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;

namespace BangumiNet.Utils;

public class LayoutUtils
{
    // Source - https://stackoverflow.com/a/78945115
    // Posted by cboittin, modified by community. See post 'Timeline' for change history
    // Retrieved 2025-11-22, License - CC BY-SA 4.0
    public static Size CalculateTextSize(string? text, double fontSize, FontFamily? fontFamily = null)
    {
        if (string.IsNullOrEmpty(text)) text = " ";
        fontFamily ??= new("Microsoft YaHei UI");
        var ts = TextShaper.Current;
        var typeface = new Typeface(fontFamily);
        var shaped = ts.ShapeText(text, new TextShaperOptions(typeface.GlyphTypeface, fontSize));
        var run = new ShapedTextRun(shaped, new GenericTextRunProperties(typeface, fontSize));
        return run.Size;
    }
}
