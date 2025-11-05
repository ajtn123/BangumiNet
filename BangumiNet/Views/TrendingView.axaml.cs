using Avalonia.Controls;
using BangumiNet.Api.ExtraEnums;

namespace BangumiNet.Views;

public partial class TrendingView : UserControl
{
    public TrendingView()
    {
        InitializeComponent();
        ItemTypeComboBox.ItemsSource = new[] { ItemType.Subject };
        SubjectTypeComboBox.ItemsSource = Enum.GetValues<SubjectType>();
    }
}