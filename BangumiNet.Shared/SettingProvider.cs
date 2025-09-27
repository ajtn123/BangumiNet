using System.Text.Json;

namespace BangumiNet.Shared;

public static class SettingProvider
{
    public static Settings CurrentSettings { get; private set; } = LoadSettings();

    public static void UpdateSettings(Settings settings)
    {
        CurrentSettings = settings;
        CurrentSettings.Save();
    }

    private static readonly JsonSerializerOptions options = new()
    {
        IgnoreReadOnlyFields = true,
        IgnoreReadOnlyProperties = true,
    };
    private static Settings LoadSettings()
    {
        Settings defaults = new();

        if (!File.Exists(Constants.SettingJsonPath)) return defaults;

        var json = File.ReadAllText(Constants.SettingJsonPath);
        var overrides = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json, options);

        if (overrides != null)
            foreach (var uc in overrides)
            {
                var prop = typeof(Settings).GetProperty(uc.Key);
                if (prop != null)
                {
                    var value = uc.Value.Deserialize(prop.PropertyType);
                    prop.SetValue(defaults, value);
                }
            }

        return defaults;
    }

    private static void Save(this Settings current)
    {
        Settings defaults = new();
        Dictionary<string, object?> overrides = [];

        foreach (var prop in typeof(Settings).GetProperties())
        {
            var currentValue = prop.GetValue(current);
            var defaultValue = prop.GetValue(defaults);

            if (!Equals(currentValue, defaultValue))
                overrides[prop.Name] = currentValue;
        }

        File.WriteAllText(Constants.SettingJsonPath, JsonSerializer.Serialize(overrides, options));
    }
}
