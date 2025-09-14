namespace BangumiNet.Shared;

public static class Constants
{
    public const string ApplicationName = "BangumiNet";
    public const string SourceRepository = "https://github.com/ajtn123/BangumiNet";
    public const string SettingJsonName = "BNSettings.json";

    public static string AppData { get; } = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}{DirSep}{ApplicationName}{DirSep}";
    public static string SettingJsonPath { get; } = $@"{AppData}{SettingJsonName}";

    private static readonly char DirSep = Path.DirectorySeparatorChar;
}
