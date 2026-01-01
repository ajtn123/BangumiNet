using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Notify;

namespace BangumiNet.ViewModels;

public partial class NotificationListViewModel : ViewModelBase
{
    public NotificationListViewModel()
    {
        Title = $"通知 - {Title}";
    }

    public async Task LoadNotifications()
    {
        NotifyGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Notify.GetAsync(config =>
            {
                config.QueryParameters.Limit = Limit;
                config.QueryParameters.Unread = Unread;
            });
        }
        catch (Exception e) { Trace.TraceError(e.ToString()); }
        if (response == null) return;
        Source = response;
        Total = response.Total;
        Notifications ??= new();
        Notifications.SubjectViewModels = response.Data?.Select<Notice, ViewModelBase>(x => new NotificationViewModel(x)).ToObservableCollection();
    }

    [Reactive] public partial NotifyGetResponse? Source { get; set; }
    [Reactive] public partial SubjectListViewModel? Notifications { get; set; }
    [Reactive] public partial bool? Unread { get; set; }
    [Reactive] public partial int? Total { get; set; }

    public static int Limit => 40;
}
