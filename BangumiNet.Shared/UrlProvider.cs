namespace BangumiNet.Shared;

public static class UrlProvider
{
    public static string BangumiTvUrlBase => SettingProvider.CurrentSettings.BangumiTvUrlBase;
    public static string BangumiTvSubjectUrlBase => $"{BangumiTvUrlBase}/subject/";
    public static string BangumiTvEpisodeUrlBase => $"{BangumiTvUrlBase}/ep/";
    public static string BangumiTvCharacterUrlBase => $"{BangumiTvUrlBase}/character/";
    public static string BangumiTvPersonUrlBase => $"{BangumiTvUrlBase}/person/";
    public static string BangumiTvUserUrlBase => $"{BangumiTvUrlBase}/user/";
    public static string BangumiTvGroupUrlBase => $"{BangumiTvUrlBase}/group/";
    public static string BangumiTvSubjectTopicUrlBase => $"{BangumiTvUrlBase}/subject/topic/";
    public static string BangumiTvGroupTopicUrlBase => $"{BangumiTvUrlBase}/group/topic/";

    public const string DefaultUserAvatarUrl = "https://lain.bgm.tv/pic/user/l/icon.jpg";
    public const string BangumiUrl = "https://bangumi.tv/";
    public const string BgmUrl = "https://bgm.tv/";
    public const string ChiiUrl = "https://chii.in/";
}
