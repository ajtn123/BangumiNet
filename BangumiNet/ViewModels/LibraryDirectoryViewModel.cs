using Avalonia.Media;
using BangumiNet.Library;
using FluentIcons.Common;
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

    [Reactive] public partial ObservableCollection<LibraryDirectoryViewModel>? Directories { get; private set; }
    [Reactive] public partial ObservableCollection<LibraryFileViewModel>? Files { get; private set; }

    public IBrush HeaderBackgroundBrush => Brush.Parse(Directory.Type switch
    {
        DirectoryType.Subject => Settings.ColorLibraryDirectorySubjectBg,
        DirectoryType.Extra => Settings.ColorLibraryDirectoryExtraBg,
        DirectoryType.CD => Settings.ColorLibraryDirectoryCDBg,
        DirectoryType.Scan => Settings.ColorLibraryDirectoryScanBg,
        DirectoryType.SP => Settings.ColorLibraryDirectorySPBg,
        _ => Settings.ColorErrorBg,
    });
    public Icon HeaderIcon => Directory.Type switch
    {
        DirectoryType.Extra => Icon.ReadingList,
        DirectoryType.CD => Icon.Cd,
        DirectoryType.Scan => Icon.Image,
        DirectoryType.SP => Icon.MoviesAndTv,
        _ => Icon.Icons,
    };
}
