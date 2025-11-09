using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class ItemViewModelBase : ViewModelBase
{
    public ItemViewModelBase()
    {
        ShowRevisionsCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = RevisionListViewModel }.Show());
        OpenInNewWindowCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = this }.Show());
        ShowNetworkCommand = ReactiveCommand.Create(() => new SecondaryWindow { Content = new ItemNetworkViewModel(this) }.Show());
    }

    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial string? NameCn { get; set; }
    [Reactive] public partial RevisionListViewModel? RevisionListViewModel { get; set; }

    public ICommand? OpenInNewWindowCommand { get; set; }
    public ICommand? SearchWebCommand { get; set; }
    public ICommand? OpenInBrowserCommand { get; set; }
    public ICommand? ShowRevisionsCommand { get; set; }
    public ICommand? ShowNetworkCommand { get; set; }

    public ItemType ItemTypeEnum { get; protected set; }
}
