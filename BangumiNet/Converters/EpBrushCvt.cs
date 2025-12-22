using Avalonia.Collections;
using Avalonia.Data.Converters;
using Avalonia.Media;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Common.Extras;
using System.Globalization;

namespace BangumiNet.Converters;

public class EpBrushCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Convert(value);
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();

    public static IBrush? Convert(object? obj)
        => (obj as EpisodeType?) switch
        {
            EpisodeType.Mainline => Brush.Parse(SettingProvider.Current.ColorEpMainBg),
            EpisodeType.Special => Brush.Parse(SettingProvider.Current.ColorEpSpBg),
            EpisodeType.Opening => Brush.Parse(SettingProvider.Current.ColorEpOpBg),
            EpisodeType.Ending => Brush.Parse(SettingProvider.Current.ColorEpEdBg),
            EpisodeType.Advertisement => Brush.Parse(SettingProvider.Current.ColorEpCmBg),
            EpisodeType.Mad => Brush.Parse(SettingProvider.Current.ColorEpMadBg),
            EpisodeType.Other => Brush.Parse(SettingProvider.Current.ColorEpOtherBg),
            null => Brush.Parse(SettingProvider.Current.ColorErrorBg),
            _ => throw new NotImplementedException(),
        };
}
public class EpDashCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Convert(value);
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();

    public static AvaloniaList<double>? Convert(object? obj) => (EpisodeCollectionType?)obj switch
    {
        EpisodeCollectionType.Uncollected => [0.000001, 2],
        EpisodeCollectionType.Wish => [5, 2],
        EpisodeCollectionType.Done => null,
        EpisodeCollectionType.Dropped => [2, 5],
        null => [0.000001, 2],
        _ => throw new NotImplementedException(),
    };
}
public class EpUnairedCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Convert(value);
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();

    public static bool? Convert(object? obj)
    {
        if (obj is not DateOnly date) return false;
        if (date.ToDateTime(TimeOnly.MaxValue) > DateTime.UtcNow) return true;
        else return false;
    }
}
