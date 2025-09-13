using Avalonia.Data.Converters;
using BangumiNet.Shared;
using System;
using System.Globalization;
using System.Reflection;

namespace BangumiNet.Converters;

public class NameCnConverter : IValueConverter
{
    public static NameCnConverter Instance { get; } = new();
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not { } subject) return "Null";

        var type = value.GetType();
        var nameProp = type.GetProperty("Name", BindingFlags.Public | BindingFlags.Instance);
        var nameCnProp = type.GetProperty("NameCn", BindingFlags.Public | BindingFlags.Instance);
        var nameCn = nameCnProp?.GetValue(subject)?.ToString();

        return !string.IsNullOrWhiteSpace(nameCn) && SettingProvider.LocaleSetting.PreferChineseNames ? nameCn : nameProp?.GetValue(subject);
    }
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
