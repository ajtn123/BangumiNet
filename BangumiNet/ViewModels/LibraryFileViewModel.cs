using BangumiNet.Library;

namespace BangumiNet.ViewModels;

public partial class LibraryFileViewModel : LibraryItemViewModel
{
    protected LibraryFile File { get; init; }

    public LibraryFileViewModel(LibraryFile file)
    {
        File = file;
        Name = file.File.Name;
    }
}
