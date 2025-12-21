using Avalonia;
using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class NavigatorWindow : ReactiveWindow<NavigatorViewModel>
{
    public NavigatorWindow()
    {
        InitializeComponent();

        ViewModel = new NavigatorViewModel();
        Navigator.AsyncPopulator = ViewModel.PopulateAsync;

        SizeChanged += (s, e) =>
        {
            var x = (e.PreviousSize.Width - e.NewSize.Width) / 2 * Scaling;
            var y = (e.PreviousSize.Height - e.NewSize.Height) / 2 * Scaling;
            Position = new PixelPoint(Position.X + (int)x, Position.Y + (int)y);
        };
        KeyDown += (s, e) => { if (e.Key == Avalonia.Input.Key.Escape) Close(); };
        Opened += (s, e) =>
        {
            var sender = (NavigatorWindow?)s;
            if (sender?.Owner is SecondaryWindow owner)
                sender.ViewModel?.TargetWindow = owner;
        };
        Deactivated += (s, e) => Close();
    }

    private double Scaling => Screens.ScreenFromWindow(this)!.Scaling;
}