using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class UserView : ReactiveUserControl<UserViewModel>
{
    public UserView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => !vm.IsFull)
            .Subscribe(async vm =>
            {
                var fullItem = await ApiC.GetViewModelAsync<UserViewModel>(username: vm.Username);
                if (fullItem == null) return;
                ViewModel = fullItem;
            });
    }
}
