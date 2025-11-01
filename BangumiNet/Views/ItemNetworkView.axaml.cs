using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class ItemNetworkView : UserControl
{
    public ItemNetworkView()
    {
        InitializeComponent();
        XAxis.Labels = [];
        YAxis.Labels = [];
    }
}