using FFMpegCore;
using System.Diagnostics;

namespace BangumiNet.Library;

public class LibraryFile : LibraryItem
{
    public IMediaAnalysis? Analysis { get; set; }
    public required FileInfo File { get; init; }
    public List<LibraryFile>? Attachments { get; set; }

    public async Task<IMediaAnalysis?> AnalyseAsync()
    {
        try
        {
            return Analysis = await FFProbe.AnalyseAsync(File.FullName);
        }
        catch (Exception e)
        {
            Trace.TraceInformation(e.Message);
            Trace.TraceInformation($"{File.FullName} is not a media file.");
            return null;
        }
    }
}
