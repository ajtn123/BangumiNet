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

    private static readonly string[] attachmentExtensions = [".ass", ".ssa", ".srt"];
    public void LoadFiles(bool refresh = false)
    {
        List<LibraryFile> GetFiles()
        {
            var files = EnumerateFiles().ToList();
            var attachments = files.Where(f => attachmentExtensions.Contains(f.File.Extension.ToLowerInvariant()));
            var owners = files.Except(attachments).ToList();
            foreach (var attachment in attachments)
            {
                if (owners.FirstOrDefault(f => attachment.File.Name.StartsWith(Path.GetFileNameWithoutExtension(f.File.Name))) is { } owner)
                {
                    owner.Attachments ??= [];
                    owner.Attachments.Add(attachment);
                }
                else
                {
                    owners.Add(attachment);
                }
            }
            return owners;
        }

        if (refresh)
            Files = GetFiles();
        else
            Files ??= GetFiles();
    }
}
