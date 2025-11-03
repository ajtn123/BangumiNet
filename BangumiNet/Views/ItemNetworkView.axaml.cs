namespace BangumiNet.Views;

public partial class ItemNetworkView : ReactiveUserControl<ItemNetworkViewModel>
{
    public ItemNetworkView()
    {
        InitializeComponent();
        XAxis.Labels = [];
        YAxis.Labels = [];
    }
}
