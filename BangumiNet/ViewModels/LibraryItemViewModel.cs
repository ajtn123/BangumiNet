namespace BangumiNet.ViewModels;

public abstract partial class LibraryItemViewModel : ViewModelBase, IActivatableViewModel
{
    [Reactive] public partial string Name { get; set; }

    public ViewModelActivator Activator { get; } = new();
}
