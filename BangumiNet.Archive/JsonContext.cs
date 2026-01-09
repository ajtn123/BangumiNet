// If you need json source generation or AOT

/*

using BangumiNet.Archive.Converters;
using BangumiNet.Archive.Models;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BangumiNet.Archive;

[JsonSourceGenerationOptions(Converters = [
    typeof(DateOnlyConverter),
    typeof(NullableConverterFactory)])]
[JsonSerializable(typeof(Subject))]
[JsonSerializable(typeof(Person))]
[JsonSerializable(typeof(Character))]
[JsonSerializable(typeof(Episode))]
[JsonSerializable(typeof(SubjectRelation))]
[JsonSerializable(typeof(SubjectCharacterRelation))]
[JsonSerializable(typeof(SubjectPersonRelation))]
[JsonSerializable(typeof(PersonCharacterRelation))]
public partial class JsonContext : JsonSerializerContext;

public static class Loader
{
    public static async IAsyncEnumerable<T> Load<T>(Stream stream, [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : struct
    {
        using var reader = new StreamReader(stream, leaveOpen: true);

        string? line;
        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            if (string.IsNullOrEmpty(line))
                continue;

            yield return (T)JsonSerializer.Deserialize(line, JsonContext.Default.GetTypeInfo(typeof(T))!)!;
        }
    }
}

*/