using BangumiNet.Library;

namespace BangumiNet.ViewModels;

public partial class LibraryHomeViewModel : ViewModelBase
{
    public LibraryHomeViewModel()
    {
        Libraries = Settings.LibraryDirectories?.Split('\r', '\n')
            .Where(str => !string.IsNullOrWhiteSpace(str))
            .Select(path => new LibraryViewModel(new SubjectLibrary(path)))
            .ToObservableCollection() ?? [];
    }

    [Reactive] public partial ObservableCollection<LibraryViewModel> Libraries { get; set; }
}
