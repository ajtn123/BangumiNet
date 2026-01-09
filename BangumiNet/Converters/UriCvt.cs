using Avalonia.Data.Converters;
using System.Globalization;

namespace BangumiNet.Converters;

public class UriCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is string str && !str.IsWhiteSpace() ? new Uri(str) : null;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => (value as Uri)?.ToString();
}
