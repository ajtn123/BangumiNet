using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class AiringView : UserControl
{
    public AiringView()
    {
        InitializeComponent();
        if (!Design.IsDesignMode) _ = Init();
    }
    public async Task Init() => DataContext = await ApiC.GetViewModelAsync<AiringViewModel>();
}
