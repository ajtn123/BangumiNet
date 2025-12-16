using BangumiNet.Api.P1.Models;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class MeViewModel : UserViewModel
{
    public MeViewModel(Profile user) : base(user)
    {
        ShowNotificationWindow = ReactiveCommand.Create(() =>
        {
            NotificationListViewModel ??= new();
            _ = NotificationListViewModel.LoadNotifications();
            SecondaryWindow.Show(NotificationListViewModel);
        });
        ShowFriendListWindow = ReactiveCommand.Create(() =>
        {
            FriendListViewModel ??= new(UserRelationType.Friend);
            _ = FriendListViewModel.LoadPageCommand.Execute(1).Subscribe();
            SecondaryWindow.Show(FriendListViewModel);
        });
        ShowFollowerListWindow = ReactiveCommand.Create(() =>
        {
            FollowerListViewModel ??= new(UserRelationType.Follower);
            _ = FollowerListViewModel.LoadPageCommand.Execute(1).Subscribe();
            SecondaryWindow.Show(FollowerListViewModel);
        });
        ShowBlockListWindow = ReactiveCommand.Create(() =>
        {
            BlockListViewModel ??= new(UserRelationType.Blocked);
            _ = BlockListViewModel.LoadPageCommand.Execute(1).Subscribe();
            SecondaryWindow.Show(BlockListViewModel);
        });
    }

    [Reactive] public partial NotificationListViewModel? NotificationListViewModel { get; set; }
    [Reactive] public partial UserListViewModel? FriendListViewModel { get; set; }
    [Reactive] public partial UserListViewModel? FollowerListViewModel { get; set; }
    [Reactive] public partial UserListViewModel? BlockListViewModel { get; set; }

    public ICommand? ShowNotificationWindow { get; set; }
    public ICommand? ShowFriendListWindow { get; set; }
    public ICommand? ShowFollowerListWindow { get; set; }
    public ICommand? ShowBlockListWindow { get; set; }
}
