using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class ItemViewModelBase : ViewModelBase
{
    public ItemViewModelBase()
    {
        ShowRevisionsCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new RevisionListView() { DataContext = RevisionListViewModel } }.Show());
    }

    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial RevisionListViewModel? RevisionListViewModel { get; set; }

    public ICommand? OpenInNewWindowCommand { get; set; }
    public ICommand? SearchWebCommand { get; set; }
    public ICommand? OpenInBrowserCommand { get; set; }
    public ICommand? ShowRevisionsCommand { get; set; }
}
