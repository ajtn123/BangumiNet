using FFMpegCore;

namespace BangumiNet.Library;

public class LibraryFile : LibraryItem
{
    public IMediaAnalysis? Analysis { get; set; }
    public required FileInfo File { get; init; }
    public List<LibraryFile>? Attachments { get; set; }

    public async Task<IMediaAnalysis> AnalyseAsync()
        => Analysis = await FFProbe.AnalyseAsync(File.FullName);
}
