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

    public static string Hash(string input)
    {
        var data = Encoding.UTF8.GetBytes(input);
        var hash = SHA256.HashData(data);
        return Convert.ToHexStringLower(hash)[0..32];
    }

    public static string GetNameCn(this ItemType value) => AttributeHelpers.GetNameCn(value)!;
    public static string GetNameCn(this RelatedItemType value) => AttributeHelpers.GetNameCn(value)!;
    public static string GetNameCn(this ApplicationTheme value) => AttributeHelpers.GetNameCn(value)!;

    private static readonly TextWriterTraceListener listener = new(PathProvider.LogFilePath);
    public static void SetupLogger(Settings settings)
    {
        var file = new FileInfo(PathProvider.LogFilePath);
        if (file.Exists && file.Length >= 1 << 20)
            file.MoveTo(file.FullName + ".old", true);

        Trace.AutoFlush = settings.SaveLogFile;
        if (settings.SaveLogFile && !Trace.Listeners.Contains(listener))
            Trace.Listeners.Add(listener);
        else if (!settings.SaveLogFile && Trace.Listeners.Contains(listener))
            Trace.Listeners.Remove(listener);
    }

    public static void WriteAppData(FileInfo location, byte[] data)
    {
        location.Directory?.Create();
        var tmp = location.FullName + ".tmp";
        File.WriteAllBytes(tmp, data);
        File.Move(tmp, location.FullName, true);
    }

    public static async Task WriteAppData(FileInfo location, Stream data)
    {
        location.Directory?.Create();
        var tmp = location.FullName + ".tmp";
        await using (var tmpFS = File.Create(tmp))
            await data.CopyToAsync(tmpFS);
        File.Move(tmp, location.FullName, true);
    }
}
