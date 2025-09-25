using Avalonia;
using Avalonia.Data.Converters;
using System.Globalization;

namespace BangumiNet.Converters;

public class BoundCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not Rect rect) return null;
        return new Rect(0, 0, rect.Width, rect.Height);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
