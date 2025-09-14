namespace BangumiNet.Shared;

public static class Constants
{
    public const string ApplicationName = "BangumiNet";
    public const string SourceRepository = "https://github.com/ajtn123/BangumiNet";
    public const string SettingJsonName = "BNSettings.json";

    public static string AppData { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ApplicationName);
    public static string SettingJsonPath { get; } = Path.Combine(AppData, SettingJsonName);
}
