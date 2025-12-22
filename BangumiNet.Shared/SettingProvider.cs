using System.Text.Json;

namespace BangumiNet.Shared;

public static class SettingProvider
{
    public static Settings Current { get; private set; } = LoadSettings();

    public static void UpdateSettings(Settings settings)
    {
        Current = settings;
        Current.Save();
    }

    private static readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
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
        => File.WriteAllText(Constants.SettingJsonPath, JsonSerializer.Serialize(current.GetOverrides(), options));
    public static Dictionary<string, object?> GetOverrides(this Settings current)
    {
        Settings defaults = new();
        Dictionary<string, object?> overrides = [];

        foreach (var prop in typeof(Settings).GetProperties())
        {
            var currentValue = prop.GetValue(current);
            var defaultValue = prop.GetValue(defaults);

            // https://stackoverflow.com/a/3804852
            if (currentValue is Dictionary<string, string> dic1 && defaultValue is Dictionary<string, string> dic2 && dic1.Count == dic2.Count && !dic1.Except(dic2).Any())
                continue;
            if (!Equals(currentValue, defaultValue))
                overrides[prop.Name] = currentValue;
        }

        return overrides;
    }
}
