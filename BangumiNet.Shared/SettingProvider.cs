using System.Text.Json;

namespace BangumiNet.Shared;

public static class SettingProvider
{
    public static Settings CurrentSettings { get; set; } = LoadSettings().Result;

    public static ApiSettings ApiSetting => CurrentSettings.ApiSettings;
    public static LocaleSettings LocaleSetting => CurrentSettings.LocaleSettings;

    private static Settings GetNewSettings() => new() { ApiSettings = new(), LocaleSettings = new() };
    public static async Task<Settings> LoadSettings()
    {
        if (!File.Exists(Constants.SettingJsonPath)) return GetNewSettings();

        var json = await File.ReadAllTextAsync(Constants.SettingJsonPath);
        var settings = JsonSerializer.Deserialize<Settings>(json);
        return settings ?? GetNewSettings();
    }
    public static async Task<Settings> Save(this Settings settings)
    {
        var json = JsonSerializer.Serialize(settings);
        await File.WriteAllTextAsync(Constants.SettingJsonPath, json);
        return settings;
    }
}
