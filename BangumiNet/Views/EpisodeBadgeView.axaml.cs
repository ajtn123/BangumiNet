namespace BangumiNet.Views;

public partial class EpisodeBadgeView : ReactiveUserControl<EpisodeViewModel>
{
    public EpisodeBadgeView()
    {
        InitializeComponent();
        EpBadgeGrid.PointerReleased += (s, e) =>
        {
            if (EpBadgeGrid.ContextFlyout?.IsOpen ?? true) return;
            e.GetPosition(EpBadgeGrid).Deconstruct(out var x, out var y);
            if (x < 0 || y < 0 || x > EpBadgeGrid.Bounds.Width || y > EpBadgeGrid.Bounds.Height) return;
            EpBadgeGrid.ContextFlyout.ShowAt(EpBadgeGrid);
            e.Handled = true;
        };
    }
}