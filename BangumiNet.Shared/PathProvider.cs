using System;
using System.Collections.Generic;
using System.Text;

namespace BangumiNet.Shared;

public static class PathProvider
{
    public static string GetAbsolutePathForLocalData(string relPath)
        => Path.Combine(SettingProvider.CurrentSettings.LocalDataDirectory, relPath);
}
