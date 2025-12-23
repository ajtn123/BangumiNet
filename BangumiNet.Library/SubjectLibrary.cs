namespace BangumiNet.Library;

public class SubjectLibrary
{
    public SubjectLibrary(string path)
    {
        Directory = new(path);
        if (!Directory.Exists) return;

        var topLevelItems = Directory.EnumerateDirectories();
        List<DirectoryInfo> potentialItems = [.. topLevelItems];
        Items = [.. potentialItems.Select(item => new LibraryItem { Directory = item })];
    }

    public DirectoryInfo Directory { get; set; }
    public List<LibraryItem>? Items { get; set; }
}
