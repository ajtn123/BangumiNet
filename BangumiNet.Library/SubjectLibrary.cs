using BangumiNet.Common;

namespace BangumiNet.Library;

public class SubjectLibrary : LibraryDirectory
{
    public SubjectLibrary(DirectoryInfo directory) : base(directory)
    {
        Ancestor = this;
    }

    public SubjectType[] SubjectTypes { get; set; } = [SubjectType.Anime];
}
