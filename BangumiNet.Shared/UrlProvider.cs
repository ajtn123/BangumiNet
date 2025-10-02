namespace BangumiNet.Shared;

public static class UrlProvider
{
    public static string BangumiTvUrlBase => SettingProvider.CurrentSettings.BangumiTvUrlBase;
    public static string BangumiTvSubjectUrlBase => $"{BangumiTvUrlBase}/subject/";
    public static string BangumiTvEpisodeUrlBase => $"{BangumiTvUrlBase}/ep/";
    public static string BangumiTvCharacterUrlBase => $"{BangumiTvUrlBase}/character/";
    public static string BangumiTvPersonUrlBase => $"{BangumiTvUrlBase}/person/";
    public static string BangumiTvUserUrlBase => $"{BangumiTvUrlBase}/user/";
    public static string DefaultUserAvatarUrl => "https://lain.bgm.tv/pic/user/l/icon.jpg";
}
