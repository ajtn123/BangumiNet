using Avalonia.Data.Converters;
using FFMpegCore;
using System.Globalization;

namespace BangumiNet.Converters;

public class ResolutionCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not VideoStream vs) return null;
        return $"{vs.Width}*{vs.Height}";
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
