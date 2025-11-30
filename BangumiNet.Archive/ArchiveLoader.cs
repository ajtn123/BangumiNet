using BangumiNet.Archive.Converters;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace BangumiNet.Archive;

public static class ArchiveLoader
{
    private static readonly JsonSerializerOptions options = new()
    {
        Converters = {
            new DateOnlyConverter(),
            new NullableConverterFactory(),
        }
    };

    public static async IAsyncEnumerable<T> Load<T>(Stream stream, [EnumeratorCancellation] CancellationToken cancellationToken = default) where T : struct
    {
        using var reader = new StreamReader(stream, leaveOpen: true);

        string? line;
        while ((line = await reader.ReadLineAsync(cancellationToken)) != null)
        {
            if (string.IsNullOrEmpty(line))
                continue;

            yield return JsonSerializer.Deserialize<T>(line, options);
        }
    }
}
