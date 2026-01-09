using System.Diagnostics;

namespace BangumiNet.Shared;

public class DisposableStopwatch(string name) : IDisposable
{
    private readonly string name = name;
    private readonly Stopwatch stopwatch = Stopwatch.StartNew();

    public void Dispose()
    {
        stopwatch.Stop();
        Trace.TraceInformation($"[Stopwatch] {name}: {stopwatch.ElapsedMilliseconds}ms");
    }
}
