using BangumiNet.Api.P1.Models;

namespace BangumiNet.Views;

public partial class TimelineView : ReactiveUserControl<TimelineViewModel>
{
    public TimelineView()
    {
        InitializeComponent();
    }

    public static FilterMode[] Modes { get; } = Enum.GetValues<FilterMode>();
}