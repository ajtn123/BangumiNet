using Avalonia.Data.Converters;
using System.Globalization;

namespace BangumiNet.Converters;

public class DateCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not DateOnly date) return null;
        if (date.Year == 1) return date.ToString("MM/dd");
        else return date.ToString();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
