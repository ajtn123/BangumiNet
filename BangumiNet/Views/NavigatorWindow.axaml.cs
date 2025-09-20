using Avalonia;
using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class NavigatorWindow : Window
{
    public NavigatorWindow()
    {
        InitializeComponent();
        SizeChanged += (s, e) =>
        {
            var x = (e.PreviousSize.Width - e.NewSize.Width) / 2 * Scaling;
            var y = (e.PreviousSize.Height - e.NewSize.Height) / 2 * Scaling;
            Position = new PixelPoint(Position.X + (int)x, Position.Y + (int)y);
        };
        Deactivated += (s, e) => Close();
    }

    private double Scaling => Screens.ScreenFromWindow(this)!.Scaling;
}