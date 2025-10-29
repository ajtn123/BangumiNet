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
            _ = NotificationListViewModel.LoadNotifications();
            new SecondaryWindow() { Content = NotificationListViewModel }.Show();
        });
        ShowFriendListWindow = ReactiveCommand.Create(() =>
        {
            FriendListViewModel ??= new(UserRelationType.Friend);
            _ = FriendListViewModel.LoadPage(1);
            new SecondaryWindow() { Content = FriendListViewModel }.Show();
        });
        ShowFollowerListWindow = ReactiveCommand.Create(() =>
        {
            FollowerListViewModel ??= new(UserRelationType.Follower);
            _ = FollowerListViewModel.LoadPage(1);
            new SecondaryWindow() { Content = FollowerListViewModel }.Show();
        });
        ShowBlockListWindow = ReactiveCommand.Create(() =>
        {
            BlockListViewModel ??= new(UserRelationType.Blocked);
            _ = BlockListViewModel.LoadPage(1);
            new SecondaryWindow() { Content = BlockListViewModel }.Show();
        });
        User = await ApiC.GetViewModelAsync<UserViewModel>();
    }

    [Reactive] public partial UserViewModel? User { get; set; }
    [Reactive] public partial NotificationListViewModel? NotificationListViewModel { get; set; }
    [Reactive] public partial UserListViewModel? FriendListViewModel { get; set; }
    [Reactive] public partial UserListViewModel? FollowerListViewModel { get; set; }
    [Reactive] public partial UserListViewModel? BlockListViewModel { get; set; }

    public ICommand? ShowNotificationWindow { get; set; }
    public ICommand? ShowFriendListWindow { get; set; }
    public ICommand? ShowFollowerListWindow { get; set; }
    public ICommand? ShowBlockListWindow { get; set; }
}
