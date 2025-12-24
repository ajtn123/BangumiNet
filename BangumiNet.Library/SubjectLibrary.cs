namespace BangumiNet.Library;

public class SubjectLibrary
{
    public SubjectLibrary(string path)
    {
        Directory = new(path);
        if (!Directory.Exists) return;

        var topLevelItems = Directory.EnumerateDirectories();
        List<LibraryItem> items = [];
        foreach (var topLevelItem in topLevelItems)
        {
            items.Add(new LibrarySubject { Directory = topLevelItem });
        }

        Items = items;
    }

    public DirectoryInfo Directory { get; set; }
    public List<LibraryItem>? Items { get; set; }
}
