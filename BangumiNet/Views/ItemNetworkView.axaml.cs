namespace BangumiNet.Views;

public partial class ItemNetworkView : ReactiveUserControl<ItemNetworkViewModel>
{
    public ItemNetworkView()
    {
        InitializeComponent();
        XAxis.Labels = [];
        YAxis.Labels = [];
    }

    private void StackPanel_ActualThemeVariantChanged(object? sender, System.EventArgs e)
    {
    }
}