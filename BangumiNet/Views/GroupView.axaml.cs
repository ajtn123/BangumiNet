using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class GroupView : ReactiveUserControl<GroupViewModel>
{
    public GroupView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => !vm.IsFull)
            .Subscribe(async vm =>
            {
                var fullItem = await ApiC.GetViewModelAsync<GroupViewModel>(username: vm.Groupname);
                if (fullItem == null) return;
                ViewModel = fullItem;
            });
        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => vm.IsFull)
            .Subscribe(async vm =>
            {
                _ = vm.Members?.Load(1);
                _ = vm.Topics?.Load(1);
            });
    }
}