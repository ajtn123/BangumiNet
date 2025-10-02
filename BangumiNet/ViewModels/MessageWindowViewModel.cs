using FluentIcons.Common;

namespace BangumiNet.ViewModels;

public partial class MessageWindowViewModel : ViewModelBase
{
    public MessageWindowViewModel()
    {
        Title = "信息";
        Icon = Icon.Info;
    }

    [Reactive] public partial string? Message { get; set; }
    [Reactive] public partial Icon Icon { get; set; }
}
