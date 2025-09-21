using Avalonia.Controls;
using BangumiNet.ViewModels;

namespace BangumiNet.Views;

public partial class AiringView : UserControl
{
    public AiringView()
    {
        if (!Design.IsDesignMode) DataContext = new AiringViewModel();
        InitializeComponent();
    }
}