using Avalonia.Controls;
using BangumiNet.Library;
using BangumiNet.Templates;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Disposables.Fluent;

namespace BangumiNet.ViewModels;

public partial class LibraryFileViewModel : LibraryItemViewModel
{
    protected LibraryFile File { get; init; }

    public LibraryFileViewModel(LibraryFile file)
    {
        File = file;
        FileInfo = file.File;
        Name = file.File.Name;

        this.WhenActivated(disposables =>
        {
            OpenFileCommand = CommonUtils.GetOpenUriCommand(FileInfo.FullName).DisposeWith(disposables);
        });
    }

    public async Task LoadItems()
    {
        if (Items != null) return;
        List<object> items = [];

        try
        {
            var analysis = await File.AnalyseAsync();
            items.Add(analysis);
            items.AddRange(analysis.VideoStreams);
            items.AddRange(analysis.AudioStreams);
            items.AddRange(analysis.SubtitleStreams);
        }
        catch (Win32Exception e)
        {
            Trace.TraceError(e.ToString());
            MainWindow.ShowInfo(FluentAvalonia.UI.Controls.InfoBarSeverity.Error, "未找到 FFProbe", e.Message, new HyperlinkButton { NavigateUri = new(UrlProvider.FFMpegDownloadUrl), Content = "前往下载" });
        }
        catch (Exception e)
        {
            Trace.TraceInformation(e.ToString());
            Trace.TraceInformation($"{File.File.FullName} is not a media file.");
        }

        if (File.Attachments is { } ats)
            items.AddRange(ats.Select(a => new TextViewModel(() => [new InfoBadge { Text = "附件", Margin = new(0, 0, 5, 0) }, new HyperlinkButton { Content = a.File.Name, Command = CommonUtils.GetOpenUriCommand(a.File.FullName) }])));

        Items = [.. items];
    }

    [Reactive] public partial FileInfo? FileInfo { get; private set; }
    [Reactive] public partial ObservableCollection<object>? Items { get; private set; }

    [Reactive] public partial ReactiveCommand<Unit, Unit>? OpenFileCommand { get; private set; }
}
