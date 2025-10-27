using System.Security.Cryptography;
using System.Text;

namespace BangumiNet.Shared;

public static class Utils
{
    public static async Task<MemoryStream> Clone(this Stream stream, bool disposeOriginal = true, CancellationToken cancellationToken = default)
    {
        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream, cancellationToken);
        if (disposeOriginal)
            stream.Dispose();

        memoryStream.Position = 0;
        return memoryStream;
    }

    public static string GetHash(string input)
    {
        byte[] hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexStringLower(hashBytes);
    }
}
