using System.Reactive;

namespace BangumiNet.Views;

public partial class CommentView : ReactiveUserControl<CommentViewModel>
{
    public CommentView()
    {
        InitializeComponent();
    }

    private void ReplyButtonClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (RightStackPanel.Children.Any(x => x is ReplyView)) return;
        var rvm = new ReplyViewModel(ViewModel);
        rvm.DismissCommand.Subscribe(DismissReplyView);
        RightStackPanel.Children.Insert(1, new ReplyView() { DataContext = rvm });
    }

    private void DismissReplyView(Unit _)
    {
        var cs = RightStackPanel.Children.Where(c => c is ReplyView);
        RightStackPanel.Children.RemoveAll(cs);
    }
}