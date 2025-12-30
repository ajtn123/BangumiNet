namespace BangumiNet.Library;

public class LibraryDirectory : LibraryItem
{
    public required DirectoryInfo Directory { get; init; }
    public LibraryDirectory? Parent { get; init; }

    public List<LibraryDirectory>? Directories { get; private set; }
    public List<LibraryFile>? Files { get; private set; }

    private static readonly string pattern = "*";
    private static readonly EnumerationOptions options = new() { AttributesToSkip = FileAttributes.Hidden | FileAttributes.System };

    public IEnumerable<LibraryDirectory> EnumerateDirectories() => Directory.EnumerateDirectories(pattern, options).Select(dir => new LibraryDirectory { Directory = dir, Parent = this });
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

    public DirectoryType Type
    {
        get
        {
            if (field != (DirectoryType)(-1)) return field;
            var name = Directory.Name;
            if (Patterns.ExtraDirectory().IsMatch(name)) return field = DirectoryType.Extra;
            else if (Parent?.Type == DirectoryType.Extra)
                if (Patterns.CDDirectoryLoose().IsMatch(name)) return field = DirectoryType.CD;
                else if (Patterns.ScanDirectoryLoose().IsMatch(name)) return field = DirectoryType.Scan;
                else if (Patterns.SPDirectoryLoose().IsMatch(name)) return field = DirectoryType.SP;
                else if (Patterns.SubtitlesDirectoryLoose().IsMatch(name)) return field = DirectoryType.Subtitles;
                else return field = DirectoryType.Subject;
            else
                if (Patterns.CDDirectory().IsMatch(name)) return field = DirectoryType.CD;
                else if (Patterns.ScanDirectory().IsMatch(name)) return field = DirectoryType.Scan;
                else if (Patterns.SPDirectory().IsMatch(name)) return field = DirectoryType.SP;
                else if (Patterns.SubtitlesDirectory().IsMatch(name)) return field = DirectoryType.Subtitles;
                else return field = DirectoryType.Subject;
        }
        private set => field = value;
    } = (DirectoryType)(-1);
}
