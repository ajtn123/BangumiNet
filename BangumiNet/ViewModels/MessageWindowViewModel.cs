using FluentIcons.Common;

namespace BangumiNet.ViewModels;

public partial class MessageWindowViewModel : ViewModelBase
{
    public MessageWindowViewModel()
    {
        Title = $"信息 - {Constants.ApplicationName}";
        Icon = Icon.Info;
    }

    [Reactive] public required partial TextViewModel Message { get; set; }
    [Reactive] public required partial Icon Icon { get; set; }
}
