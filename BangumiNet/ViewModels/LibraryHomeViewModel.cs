using BangumiNet.Library;

namespace BangumiNet.ViewModels;

public partial class LibraryHomeViewModel : ViewModelBase
{
    public LibraryHomeViewModel()
    {
        Library = new SubjectLibrary(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos));
    }

    [Reactive] public partial SubjectLibrary Library { get; set; }
}
