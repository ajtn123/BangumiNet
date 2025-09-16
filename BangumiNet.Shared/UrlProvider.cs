namespace BangumiNet.Shared;

public static class UrlProvider
{
    public static string BangumiTvUrlBase => SettingProvider.CurrentSettings.BangumiTvUrlBase;
    public static string BangumiTvSubjectUrlBase => $"{BangumiTvUrlBase}/subject/";

    public static string GoogleQueryBase => SettingProvider.CurrentSettings.GoogleQueryUrlBase;
}
