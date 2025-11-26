using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class ItemViewModelBase : ViewModelBase
{
    public ItemViewModelBase()
    {
        ShowRevisionsCommand = ReactiveCommand.Create(() => SecondaryWindow.Show(RevisionListViewModel));
        OpenInNewWindowCommand = ReactiveCommand.Create(() => SecondaryWindow.Show(this));
        ShowNetworkCommand = ReactiveCommand.Create(() => SecondaryWindow.Show(new ItemNetworkViewModel(this)));
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial string? NameCn { get; set; }
    [Reactive] public partial RevisionListViewModel? RevisionListViewModel { get; set; }

    public ICommand? OpenInNewWindowCommand { get; set; }
    public ICommand? SearchWebCommand { get; set; }
    public ICommand? OpenInBrowserCommand { get; set; }
    public ICommand? ShowRevisionsCommand { get; set; }
    public ICommand? ShowNetworkCommand { get; set; }

    public ItemType ItemType { get; protected set; }
}
