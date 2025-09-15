using System.Security.Cryptography;
using System.Text;

namespace BangumiNet.Shared;

public static class Utils
{
    public static MemoryStream Clone(this Stream stream, bool disposeOriginal = true)
    {
        var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
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
