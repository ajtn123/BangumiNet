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
    public static bool? TryGetRecord(ItemType type, int id) => type switch
    {
        ItemType.Subject => SubjectCollectionStates.TryGetValue(id, out var record) ? record : null,
        ItemType.Character => CharacterCollectionStates.TryGetValue(id, out var record) ? record : null,
        ItemType.Person => PersonCollectionStates.TryGetValue(id, out var record) ? record : null,
        _ => throw new NotImplementedException(),
    };
    public static bool TryGetRecord(ItemType type, int id, out bool record) => type switch
    {
        ItemType.Subject => SubjectCollectionStates.TryGetValue(id, out record),
        ItemType.Character => CharacterCollectionStates.TryGetValue(id, out record),
        ItemType.Person => PersonCollectionStates.TryGetValue(id, out record),
        _ => throw new NotImplementedException(),
    };
    public static void UpdateRecord(ItemType type, int id, bool record)
    {
        switch (type)
        {
            case ItemType.Subject:
                SubjectCollectionStates[id] = record;
                break;
            case ItemType.Character:
                CharacterCollectionStates[id] = record;
                break;
            case ItemType.Person:
                PersonCollectionStates[id] = record;
                break;
            default:
                throw new NotImplementedException();
        }
    }
}
