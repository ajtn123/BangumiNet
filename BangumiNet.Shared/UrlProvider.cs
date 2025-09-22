namespace BangumiNet.Shared;

public static class UrlProvider
{
    public static string BangumiTvUrlBase => SettingProvider.CurrentSettings.BangumiTvUrlBase;
    public static string BangumiTvSubjectUrlBase => $"{BangumiTvUrlBase}/subject/";
    public static string BangumiTvEpisodeUrlBase => $"{BangumiTvUrlBase}/ep/";
    public static string BangumiTvCharacterUrlBase => $"{BangumiTvUrlBase}/character/";

    public static string GoogleQueryBase => SettingProvider.CurrentSettings.GoogleQueryUrlBase;
}
