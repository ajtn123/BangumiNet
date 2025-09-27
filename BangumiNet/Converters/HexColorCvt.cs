using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;
using System.Globalization;

namespace BangumiNet.Converters;

public class HexColorCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string hex && Color.TryParse(hex, out var color))
            return color;
        return BindingOperations.DoNothing;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Color color)
            return color.ToString();
        return BindingOperations.DoNothing;
    }
}
