namespace BangumiNet.Library;

public class LibraryDirectory : LibraryItem
{
    public required DirectoryInfo Directory { get; init; }

    public List<LibraryDirectory>? Directories { get; private set; }
    public List<LibraryFile>? Files { get; private set; }

    private static readonly string pattern = "*";
    private static readonly EnumerationOptions options = new() { AttributesToSkip = FileAttributes.Hidden | FileAttributes.System };

    public IEnumerable<LibraryDirectory> EnumerateDirectories() => Directory.EnumerateDirectories(pattern, options).Select(dir => new LibraryDirectory { Directory = dir });
    public IEnumerable<LibraryFile> EnumerateFiles() => Directory.EnumerateFiles(pattern, options).Select(file => new LibraryFile { File = file });

    public void LoadDirectories(bool refresh = false)
    {
        if (refresh)
            Directories = [.. EnumerateDirectories()];
        else
            Directories ??= [.. EnumerateDirectories()];
    }

    public void LoadFiles(bool refresh = false)
    {
        if (refresh)
            Files = [.. EnumerateFiles()];
        else
            Files ??= [.. EnumerateFiles()];
    }
}
