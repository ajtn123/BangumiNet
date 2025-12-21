using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class GroupView : ReactiveUserControl<GroupViewModel>
{
    public GroupView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Where(vm => !vm.IsFull)
                .Subscribe(async vm =>
                {
                    var fullItem = await ApiC.GetViewModelAsync<GroupViewModel>(username: vm.Groupname);
                    if (fullItem == null) return;
                    ViewModel = fullItem;
                }).DisposeWith(disposables);
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Where(vm => vm.IsFull && !vm.IsLoaded)
                .Subscribe(async vm =>
                {
                    vm.IsLoaded = true;
                    _ = vm.Members?.LoadPageCommand.Execute(1).Subscribe();
                    _ = vm.Topics?.LoadPageCommand.Execute(1).Subscribe();
                }).DisposeWith(disposables);
        });
    }
}