namespace BangumiNet.Shared;

public class SettingProvider
{
    public static ApiSetting ApiSetting { get; set; } = new();
    public static LocaleSetting LocaleSetting { get; set; } = new();
}
