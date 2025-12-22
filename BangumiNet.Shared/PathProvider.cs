namespace BangumiNet.Shared;

public static class PathProvider
{
    public static string GetAbsolutePathForLocalData(string relPath)
        => Path.Combine(SettingProvider.Current.LocalDataDirectory, relPath);
    public static string TempFolderPath
        => Path.Combine(Path.GetTempPath(), Constants.ApplicationName);
    public static string LogFilePath
        => Path.Combine(SettingProvider.Current.LocalDataDirectory, Constants.LogFileName);
}
