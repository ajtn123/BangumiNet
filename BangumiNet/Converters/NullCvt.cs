using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace BangumiNet.Converters;

public class NullCvt : IValueConverter
{
    public static NullCvt Instance { get; } = new();
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value != null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
public class InNullCvt : IValueConverter
{
    public static InNullCvt Instance { get; } = new();
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value == null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
