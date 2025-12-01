using Avalonia.Controls;
using BangumiNet.Common;

namespace BangumiNet.Views;

public partial class TrendingView : UserControl
{
    public TrendingView()
    {
        InitializeComponent();
        ItemTypeComboBox.ItemsSource = new[] { ItemType.Subject, ItemType.Topic };
        SubjectTypeComboBox.ItemsSource = Enum.GetValues<SubjectType>();
    }
}