using BangumiNet.Library;
using System.Reactive.Disposables;

namespace BangumiNet.ViewModels;

public partial class LibraryDirectoryViewModel : LibraryItemViewModel
{
    protected LibraryDirectory Directory { get; init; }

    public LibraryDirectoryViewModel(LibraryDirectory directory)
    {
        Directory = directory;
        Name = directory.Directory.Name;

        this.WhenActivated((CompositeDisposable disposables) =>
        {
            if (Directories == null)
            {
                Directory.LoadDirectories();
                Directory.LoadFiles();
                Directories = [.. Directory.Directories!.Select(dir => new LibraryDirectoryViewModel(dir))];
                Files = [.. Directory.Files!.Select(file => new LibraryFileViewModel(file))];
            }
        });
    }

    [Reactive] public partial ObservableCollection<LibraryDirectoryViewModel>? Directories { get; set; }
    [Reactive] public partial ObservableCollection<LibraryFileViewModel>? Files { get; set; }
}
