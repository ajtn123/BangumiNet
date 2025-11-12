using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class AiringView : UserControl
{
    public AiringView()
    {
        InitializeComponent();
        DataContextChanged += (s, e) =>
        {
            if (DataContext is not AiringViewModel vm) return;
            _ = vm.Highlight();
        };
    }
}
