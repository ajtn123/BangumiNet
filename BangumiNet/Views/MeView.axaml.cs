using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class MeView : UserControl
{
    public MeView()
    {
        if (!Design.IsDesignMode)
            DataContext = new MeViewModel();
        InitializeComponent();
    }
}