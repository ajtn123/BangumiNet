using Avalonia.Controls;
using BangumiNet.Api.P1.Models;

namespace BangumiNet.Views;

public partial class GroupTopicBriefView : UserControl
{
    public GroupTopicBriefView()
    {
        InitializeComponent();
        FilterComboBox.ItemsSource = Enum.GetValues<GroupTopicFilterMode>();
    }
}