using System.Text.Json;

namespace BangumiNet.Shared;

public static class SettingProvider
{
    static SettingProvider()
    {
        Current = Load();
    }

    public static Settings Current { get; private set; }

    public static void Update(Settings settings)
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
    private static readonly FileInfo local = new(Constants.SettingJsonPath);
    private static Settings Load()
    {
        Settings defaults = new();

        if (!local.Exists) return defaults;

        using var json = local.OpenRead();
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
        => Utils.WriteAppData(local, JsonSerializer.SerializeToUtf8Bytes(current.GetOverrides(), options));
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
