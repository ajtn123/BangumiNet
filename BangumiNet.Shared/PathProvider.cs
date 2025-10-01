namespace BangumiNet.Shared;

public static class PathProvider
{
    public static string GetAbsolutePathForLocalData(string relPath)
        => Path.Combine(SettingProvider.CurrentSettings.LocalDataDirectory, relPath);
    public static string TempFolderPath
        => Path.Combine(Path.GetTempPath(), Constants.ApplicationName);
}
