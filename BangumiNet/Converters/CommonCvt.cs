using Avalonia.Data.Converters;
using System.Globalization;

namespace BangumiNet.Converters;

public class CommonCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Convert(value);

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();

    public static string? Convert(object? value)
    {
        if (value == null) return null;
        else if (value is string str) return str;
        // 时间
        else if (value is DateOnly date)
            if (date.Year == 1) return $"{date:M}";
            else return $"{date:D}";
        else if (value is TimeOnly time) return time.ToString();
        else if (value is DateTime dateTime) return dateTime.ToString();
        else if (value is DateTimeOffset dateTimeOffset)
        {
            dateTimeOffset = dateTimeOffset.ToLocalTime();
            if (dateTimeOffset.Date == DateTime.Today)
                return $"今日 {dateTimeOffset:T}";
            else
                return $"{dateTimeOffset:F}";
        }
        // 默认
        else return value.ToString();
    }
}
