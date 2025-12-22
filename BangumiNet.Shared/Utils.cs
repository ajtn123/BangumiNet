using BangumiNet.Common.Attributes;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace BangumiNet.Shared;

public static class Utils
{
    public static async Task<MemoryStream> Clone(this Stream stream, CancellationToken cancellationToken = default)
    {
        var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream, cancellationToken);
        memoryStream.Position = 0;
        return memoryStream;
    }

    public static string GetHash(string input)
    {
        byte[] hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexStringLower(hashBytes);
    }

    public static string GetNameCn(this ItemType value) => AttributeHelpers.GetNameCn(value)!;
    public static string GetNameCn(this RelatedItemType value) => AttributeHelpers.GetNameCn(value)!;
    public static string GetNameCn(this ApplicationTheme value) => AttributeHelpers.GetNameCn(value)!;

    private static readonly TextWriterTraceListener listener = new(PathProvider.LogFilePath);
    public static void SetupLogging(Settings settings)
    {
        var file = new FileInfo(PathProvider.LogFilePath);
        if (file.Exists && file.Length >= 1 << 20)
            file.MoveTo(file.FullName + ".old", true);

        if (settings.SaveLogFile && !Trace.Listeners.Contains(listener))
        {
            Trace.Listeners.Add(listener);
            Trace.AutoFlush = true;
        }
        else if (!settings.SaveLogFile && Trace.Listeners.Contains(listener))
        {
            Trace.Listeners.Add(listener);
            Trace.AutoFlush = false;
        }
    }
}
