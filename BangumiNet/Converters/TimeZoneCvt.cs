using Avalonia.Data.Converters;
using System.Globalization;

namespace BangumiNet.Converters;

public class TimeZoneCvt : IValueConverter
{
    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int timeOffset)
            return $"UTC{timeOffset:+0;-#}";
        else return null;
    }

    object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
