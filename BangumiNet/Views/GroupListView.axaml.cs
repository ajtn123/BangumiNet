using BangumiNet.Api.P1.Models;

namespace BangumiNet.Views;

public partial class GroupListView : ReactiveUserControl<GroupListViewModel>
{
    public GroupListView()
    {
        InitializeComponent();
        FilterComboBox.ItemsSource = Enum.GetValues<GroupFilterMode>();
        SortComboBox.ItemsSource = Enum.GetValues<GroupSort>();
    }
}