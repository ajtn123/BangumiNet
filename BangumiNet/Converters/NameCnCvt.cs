using Avalonia.Data.Converters;
using System.Globalization;
using System.Reflection;

namespace BangumiNet.Converters;

public class NameCnCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Convert(value);
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();

    public static string? Convert(object? obj)
    {
        if (obj is not { } subject) return null;

        var type = subject.GetType();
        var nameProp = type.GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
        var nameCnProp = type.GetProperty("NameCn", BindingFlags.Public | BindingFlags.Instance);
        var name = nameProp?.GetValue(subject)?.ToString();
        var nameCn = nameCnProp?.GetValue(subject)?.ToString();

        return Convert(name, nameCn);
    }
    public static string? Convert(string? name, string? nameCn)
        => string.IsNullOrWhiteSpace(nameCn) ? name : SettingProvider.CurrentSettings.PreferChineseNames ? nameCn : name;
}
public class NameAltCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Convert(value);
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();

    public static string? Convert(object? obj)
    {
        if (obj is not { } subject) return null;

        var type = subject.GetType();
        var nameProp = type.GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
        var nameCnProp = type.GetProperty("NameCn", BindingFlags.Public | BindingFlags.Instance);
        var name = nameProp?.GetValue(subject)?.ToString();
        var nameCn = nameCnProp?.GetValue(subject)?.ToString();

        return Convert(name, nameCn);
    }
    public static string? Convert(string? name, string? nameCn)
        => string.IsNullOrWhiteSpace(nameCn) ? null : SettingProvider.CurrentSettings.PreferChineseNames ? name : nameCn;
}
