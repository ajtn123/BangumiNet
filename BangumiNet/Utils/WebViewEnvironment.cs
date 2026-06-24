using Avalonia.Controls;
using Avalonia.Platform;

namespace BangumiNet.Utils;

public class WebViewEnivironment
{
    public static void Handler(object? sender, WebViewEnvironmentRequestedEventArgs args)
    {
        args.EnableDevTools = false;

        switch (args)
        {
            case WindowsWebView2EnvironmentRequestedEventArgs win:
                win.UserDataFolder = SettingProvider.Current.LocalDataDirectory;
                break;
        }

        // case AppleWKWebViewEnvironmentRequestedEventArgs osx:
        //     osx.NonPersistentDataStore = true;
        //     break;
        // case LinuxWpeWebViewEnvironmentRequestedEventArgs linux:
        //     linux.DataDirectory = PathProvider.GetAbsolutePathForLocalData("wpe-data");
        //     linux.CacheDirectory = PathProvider.GetAbsolutePathForLocalData("wpe-cache");
        //     break;
    }
}
