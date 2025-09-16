using Avalonia.Data.Converters;
using BangumiNet.Shared;
using System;
using System.Globalization;
using System.Reflection;

namespace BangumiNet.Converters;

public class ScoreConverter : IValueConverter
{
    public static ScoreConverter Instance { get; } = new();
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not double score) return null;
        return $"⭐{score:n2}";
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
