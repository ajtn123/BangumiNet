namespace BangumiNet.Views;

public partial class NotificationListView : ReactiveUserControl<NotificationListViewModel>
{
    public NotificationListView()
    {
        InitializeComponent();
        DataContextChanged += (s, e) =>
        {
            _ = ViewModel?.LoadNotifications();
        };
    }
}