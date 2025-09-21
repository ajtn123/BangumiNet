using Avalonia.Controls;
using BangumiNet.ViewModels;

namespace BangumiNet.Views;

public partial class HomeView : UserControl
{
    public HomeView()
    {
        if (!Design.IsDesignMode) DataContext = new HomeViewModel();
        InitializeComponent();
    }
}