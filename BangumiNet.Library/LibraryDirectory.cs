using BangumiNet.Api.Helpers;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.P1.Models;
using BangumiNet.Common;
using System.Diagnostics;

namespace BangumiNet.Library;

public class LibraryDirectory : LibraryItem
{
    public LibraryDirectory(DirectoryInfo directory, LibraryDirectory? parent = null)
    {
        Directory = directory;
        Parent = parent;
        Ancestor = parent?.Ancestor;

        var dirName = Directory.Name;
        if (Parent == null) Type = DirectoryType.Library;
        else if (Patterns.ExtraDirectory().IsMatch(dirName)) Type = DirectoryType.Extra;
        else if (Parent.Type is DirectoryType.Extra or DirectoryType.Other)
            if (Patterns.CDDirectoryLoose().IsMatch(dirName)) Type = DirectoryType.CD;
            else if (Patterns.ScanDirectoryLoose().IsMatch(dirName)) Type = DirectoryType.Scan;
            else if (Patterns.SPDirectoryLoose().IsMatch(dirName)) Type = DirectoryType.SP;
            else if (Patterns.SubtitlesDirectoryLoose().IsMatch(dirName)) Type = DirectoryType.Subtitles;
            else Type = DirectoryType.Other;
        else
            if (Patterns.CDDirectory().IsMatch(dirName)) Type = DirectoryType.CD;
            else if (Patterns.ScanDirectory().IsMatch(dirName)) Type = DirectoryType.Scan;
            else if (Patterns.SPDirectory().IsMatch(dirName)) Type = DirectoryType.SP;
            else if (Patterns.SubtitlesDirectory().IsMatch(dirName)) Type = DirectoryType.Subtitles;
            else if (Parent.Type is DirectoryType.Subject or DirectoryType.Library &&
                     Patterns.SubjectDirectory().IsMatch(dirName)) Type = DirectoryType.Subject;
            else Type = DirectoryType.Other;

        if (Type == DirectoryType.Subject)
        {
            var match = Patterns.SubjectDirectory().Match(dirName);
            match.Groups.TryGetValue("Uploader", out var uploader);
            match.Groups.TryGetValue("Title", out var title);
            match.Groups.TryGetValue("Attribute", out var attribute);
            var attributes = attribute?.Captures.Select(x => x.Value).ToArray();
            SubjectInfo = new(uploader?.Value, title?.Value, attributes);
        }
    }

    public DirectoryInfo Directory { get; private set; }
    public LibraryDirectory? Parent { get; private set; }
    public SubjectLibrary? Ancestor { get; protected set; }

    public List<LibraryDirectory>? Directories { get; private set; }
    public List<LibraryFile>? Files { get; private set; }

    private const string pattern = "*";
    private static readonly EnumerationOptions options = new() { AttributesToSkip = FileAttributes.Hidden | FileAttributes.System };

    public IEnumerable<LibraryDirectory> EnumerateDirectories() => Directory.EnumerateDirectories(pattern, options).Select(dir => new LibraryDirectory(dir, this));
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

    public DirectoryType Type { get; private set; }
    public SubjectDirectoryInfo? SubjectInfo { get; private set; }

    public record class SubjectDirectoryInfo(string? Uploader, string? Title, string[]? Attributes);

    public async Task<LibrarySubjectProvider.SubjectEntry?> SearchBangumi(Api.P1.ApiClient client, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(SubjectInfo?.Title)) return null;
        var keyword = SubjectInfo.Title;
        if (LibrarySubjectProvider.Subjects.TryGetValue(keyword, out var result)) return result;
        try
        {
            var response = await client.P1.Search.Subjects.PostAsync(new()
            {
                Keyword = SubjectInfo.Title,
                Sort = SubjectSearchSort.Match,
                Filter = new() { Type = Ancestor?.SubjectTypes.Select(x => (int?)x).ToList() },
            }, config => config.Paging(1, 0), cancellationToken);

            LibrarySubjectProvider.Set(keyword, response?.Data?.FirstOrDefault() is { } r ?
                new(r.Id, r.Name, r.NameCN, (SubjectType?)r.Type, r.Images is { } images ? new ImageSet
                {
                    Grid = images.Grid,
                    Small = images.Small,
                    Medium = images.Medium,
                    Large = images.Large,
                } : null) : null);
            return LibrarySubjectProvider.Get(keyword);
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Message);
            return null;
        }
    }
    public async Task<LibrarySubjectProvider.SubjectEntry?> SearchBangumi(int? id, Api.P1.ApiClient client, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(SubjectInfo?.Title)) return null;
        var keyword = SubjectInfo.Title;

        Subject? response = null;
        if (id is int subjectId and > 0)
            try
            {
                response = await client.P1.Subjects[subjectId].GetAsync(cancellationToken: cancellationToken);
            }
            catch (Exception e) { Trace.TraceError(e.Message); }

        LibrarySubjectProvider.Set(keyword, response is { } r ?
            new(r.Id, r.Name, r.NameCN, (SubjectType?)r.Type, r.Images is { } images ? new ImageSet
            {
                Grid = images.Grid,
                Small = images.Small,
                Medium = images.Medium,
                Large = images.Large,
            } : null) : null);
        return LibrarySubjectProvider.Get(keyword);
    }
}
