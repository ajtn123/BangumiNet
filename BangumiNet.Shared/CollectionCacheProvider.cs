using System.Text.Json;

namespace BangumiNet.Shared;

public static class CollectionCacheProvider
{
    public static Dictionary<int, bool> SubjectCollectionStates { get; private set; } = [];
    public static Dictionary<int, bool> CharacterCollectionStates { get; private set; } = [];
    public static Dictionary<int, bool> PersonCollectionStates { get; private set; } = [];

    private static string SubjectCachePath => PathProvider.GetAbsolutePathForLocalData(Constants.SubjectCollectionCacheFileName);
    private static string CharacterCachePath => PathProvider.GetAbsolutePathForLocalData(Constants.CharacterCollectionCacheFileName);
    private static string PersonCachePath => PathProvider.GetAbsolutePathForLocalData(Constants.PersonCollectionCacheFileName);
    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        WriteIndented = false,
    };
    public static void Save()
    {
        var subject = JsonSerializer.Serialize(SubjectCollectionStates, jsonSerializerOptions);
        var character = JsonSerializer.Serialize(CharacterCollectionStates, jsonSerializerOptions);
        var person = JsonSerializer.Serialize(PersonCollectionStates, jsonSerializerOptions);
        File.WriteAllText(SubjectCachePath, subject);
        File.WriteAllText(CharacterCachePath, character);
        File.WriteAllText(PersonCachePath, person);
    }
    public static async Task Load()
    {
        if (File.Exists(SubjectCachePath))
        {
            var json = await File.ReadAllTextAsync(SubjectCachePath);
            SubjectCollectionStates = JsonSerializer.Deserialize<Dictionary<int, bool>>(json) ?? [];
        }
        if (File.Exists(CharacterCachePath))
        {
            var json = await File.ReadAllTextAsync(CharacterCachePath);
            CharacterCollectionStates = JsonSerializer.Deserialize<Dictionary<int, bool>>(json) ?? [];
        }
        if (File.Exists(PersonCachePath))
        {
            var json = await File.ReadAllTextAsync(PersonCachePath);
            PersonCollectionStates = JsonSerializer.Deserialize<Dictionary<int, bool>>(json) ?? [];
        }
    }
}

public static class CollectionCacheProviderExtension
{
    public static bool? TryGetRecord(this Dictionary<int, bool> stats, int key)
    {
        if (stats.TryGetValue(key, out var record)) return record;
        else return null;
    }
}
