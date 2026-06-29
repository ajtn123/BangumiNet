using System.Globalization;
using Avalonia.Data.Converters;
using FluentIcons.Avalonia.Fluent;
using FluentIcons.Common;

namespace BangumiNet.Converters;

public class IconSourceConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Icon icon ? new FluentIconSource { Icon = icon } : null;
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
