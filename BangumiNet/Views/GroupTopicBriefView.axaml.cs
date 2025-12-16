using BangumiNet.Api.P1.Models;

namespace BangumiNet.Views;

public partial class GroupTopicBriefView : ReactiveUserControl<GroupTopicListViewModel>
{
    public GroupTopicBriefView()
    {
        InitializeComponent();
        FilterComboBox.ItemsSource = Enum.GetValues<GroupTopicFilterMode>();
    }
}