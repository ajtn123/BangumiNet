namespace BangumiNet.Shared;

public class CacheProvider(string name, long maxSize)
{
    private readonly DirectoryInfo dir = new(PathProvider.GetAbsolutePathForLocalData(Constants.DiskCacheDirectory, name));

    private string GetAbsolutePath(string id) => Path.Combine(dir.FullName, Utils.Hash(id));

    public long Size() => dir.Exists ? dir.EnumerateFiles().Sum(f => f.Length) : 0;

    private long size = -1;

    public async Task Write(string id, Stream content)
    {
        dir.Create();
        var path = GetAbsolutePath(id);

        var length = content.Length;
        if (length > maxSize) return;
        if (size == -1) size = Size();
        if (size + length > maxSize) Clean();
        size += length;

        await using var file = File.OpenWrite(path);
        await content.CopyToAsync(file, CancellationToken.None);
        content.Position = 0;
    }

    public string? Get(string id)
    {
        var path = GetAbsolutePath(id);

        if (File.Exists(path))
            return path;
        else
            return null;
    }

    public FileStream? Read(string id)
    {
        var path = GetAbsolutePath(id);

        if (File.Exists(path))
            return File.OpenRead(path);
        else
            return null;
    }

    public void Delete(string id)
    {
        var path = GetAbsolutePath(id);

        if (File.Exists(path))
            File.Delete(path);
    }

    public void Clean()
    {
        var files = dir.GetFiles();
        var dels = Math.Max(files.Length / 4, 1);
        foreach (var file in files.OrderBy(f => f.LastAccessTimeUtc).Take(dels))
            file.Delete();

        if ((size = Size()) > maxSize)
            Clean();
    }

    public void Clear() => dir.Delete(true);
}
