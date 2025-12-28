using BangumiNet.Library;

namespace BangumiNet.ViewModels;

public partial class LibraryFileViewModel : LibraryItemViewModel
{
    protected LibraryFile File { get; init; }

    public LibraryFileViewModel(LibraryFile file)
    {
        File = file;
        FileInfo = file.File;
        Name = file.File.Name;
    }

    public async Task LoadItems()
    {
        Items ??= File.Attachments?.Select(a => new TextViewModel(a.File.Name)).ToObservableCollection();
    }

    [Reactive] public partial FileInfo? FileInfo { get; set; }
    [Reactive] public partial ObservableCollection<TextViewModel>? Items { get; set; }
}
