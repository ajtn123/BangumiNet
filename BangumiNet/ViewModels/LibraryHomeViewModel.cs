using BangumiNet.Library;

namespace BangumiNet.ViewModels;

public partial class LibraryHomeViewModel : ViewModelBase
{
    public LibraryHomeViewModel()
    {
        Libraries = Settings.LibraryDirectories?.Split('\r', '\n')
            .Where(str => !string.IsNullOrWhiteSpace(str))
            .Where(Directory.Exists)
            .Select(path => new LibraryViewModel(new SubjectLibrary { Directory = new(path) }))
            .ToObservableCollection() ?? [];
    }

    [Reactive] public partial ObservableCollection<LibraryViewModel> Libraries { get; set; }
}
