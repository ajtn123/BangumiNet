using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class TagListView : ReactiveUserControl<TagListViewModel>
{
    public TagListView()
    {
        InitializeComponent();
    }

    private void InfoBadge_Tapped(object? sender, Avalonia.Input.TappedEventArgs e)
        => ((TagViewModel)((Control)sender!).DataContext!).SearchTagCommand.Execute(null);
}