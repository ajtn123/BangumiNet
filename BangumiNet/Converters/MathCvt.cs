using Avalonia.Data.Converters;
using System.Globalization;

namespace BangumiNet.Converters;

public class AddCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((double?)value is not double x) return null;
        if ((double?)parameter is not double y) return x;
        return x + y;
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((double?)value is not double x) return null;
        if ((double?)parameter is not double y) return x;
        return x - y;
    }
}
public class SubCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((double?)value is not double x) return null;
        if ((double?)parameter is not double y) return x;
        return x - y;
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((double?)value is not double x) return null;
        if ((double?)parameter is not double y) return x;
        return x + y;
    }
}
public class MulCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((double?)value is not double x) return null;
        if ((double?)parameter is not double y) return x;
        return x * y;
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((double?)value is not double x) return null;
        if ((double?)parameter is not double y) return x;
        return x / y;
    }
}
public class DivCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((double?)value is not double x) return null;
        if ((double?)parameter is not double y) return x;
        return x / y;
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if ((double?)value is not double x) return null;
        if ((double?)parameter is not double y) return x;
        return x * y;
    }
}
