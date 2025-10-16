using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;

namespace BangumiNet.ViewModels;

public partial class NotificationViewModel : ViewModelBase
{
    public NotificationViewModel(Notice notice)
    {
        Id = notice.Id;
        MainId = notice.MainID;
        NoticeTitle = notice.Title;
        RelatedId = notice.RelatedID;
        Unread = notice.Unread;
        Type = (NotificationType?)notice.Type;
        Title = $"通知 - {NoticeTitle} - {Title}";
        if (notice.Sender != null)
            User = new(notice.Sender);
        if (notice.CreatedAt is int ct)
            CreationTime = DateTimeOffset.FromUnixTimeSeconds(ct).ToLocalTime();
    }

    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
    [Reactive] public partial string? NoticeTitle { get; set; }
    [Reactive] public partial int? MainId { get; set; }
    [Reactive] public partial int? RelatedId { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial bool? Unread { get; set; }
    [Reactive] public partial UserViewModel? User { get; set; }
    [Reactive] public partial NotificationType? Type { get; set; }
}
