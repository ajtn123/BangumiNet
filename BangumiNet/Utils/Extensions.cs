using System;
using System.IO;

namespace BangumiNet.Utils;

public static class Extensions
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

    public static int ToInt(this DayOfWeek dayOfWeek, DayOfWeek startingDay = DayOfWeek.Monday, int startingIndex = 0)
    {
        int i = dayOfWeek - startingDay;
        if (i < 0) i += 7;
        return i += startingIndex;
    }
}
