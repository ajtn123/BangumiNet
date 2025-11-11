using Avalonia.Controls;
using BangumiNet.Api.P1.Models;

namespace BangumiNet.Views;

public partial class GroupListView : UserControl
{
    public GroupListView()
    {
        InitializeComponent();
        FilterComboBox.ItemsSource = Enum.GetValues<GroupFilterMode>();
        SortComboBox.ItemsSource = Enum.GetValues<GroupSort>();
    }
}