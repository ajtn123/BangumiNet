
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class MeViewModel : ViewModelBase
{
    public MeViewModel() => _ = Load();
    public async Task Load()
    {
        ShowNotificationWindow = ReactiveCommand.Create(() =>
        {
            NotificationListViewModel ??= new();
            new SecondaryWindow() { Content = NotificationListViewModel }.Show();
        });
        User = await ApiC.GetViewModelAsync<UserViewModel>();
    }

    [Reactive] public partial UserViewModel? User { get; set; }
    [Reactive] public partial NotificationListViewModel? NotificationListViewModel { get; set; }

    public ICommand? ShowNotificationWindow { get; set; }
}
