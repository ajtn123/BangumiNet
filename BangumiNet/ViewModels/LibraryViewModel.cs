using BangumiNet.Library;

namespace BangumiNet.ViewModels;

public partial class LibraryViewModel : ViewModelBase
{
    public LibraryViewModel(SubjectLibrary library)
    {
        Library = library;
    }

    [Reactive] public partial SubjectLibrary Library { get; set; }
}
