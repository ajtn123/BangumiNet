using Avalonia.Data.Converters;
using Avalonia.Media;
using BangumiNet.Api.ExtraEnums;
using System.Globalization;

namespace BangumiNet.Converters;

public class EpBrushCvt : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Convert(value);
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotImplementedException();

    public static EpBrushCvt Instance { get; } = new();
    public static IBrush? Convert(object? obj)
        => (obj as EpisodeType?) switch
        {
            EpisodeType.Mainline => Brush.Parse(SettingProvider.CurrentSettings.EpMainBg),
            EpisodeType.Special => Brush.Parse(SettingProvider.CurrentSettings.EpSpBg),
            EpisodeType.Opening => Brush.Parse(SettingProvider.CurrentSettings.EpOpBg),
            EpisodeType.Ending => Brush.Parse(SettingProvider.CurrentSettings.EpEdBg),
            EpisodeType.Advertisement => Brush.Parse(SettingProvider.CurrentSettings.EpCmBg),
            EpisodeType.Mad => Brush.Parse(SettingProvider.CurrentSettings.EpMadBg),
            EpisodeType.Other => Brush.Parse(SettingProvider.CurrentSettings.EpOtherBg),
            null => Brush.Parse(SettingProvider.CurrentSettings.EpNullBg),
            _ => throw new NotImplementedException(),
        };
}
