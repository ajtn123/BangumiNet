using Avalonia.ReactiveUI;

namespace BangumiNet.Views;

public partial class EpisodeView : ReactiveUserControl<EpisodeViewModel>
{
    public EpisodeView()
    {
        InitializeComponent();

        this.WhenAnyValue(v => v.ViewModel).Subscribe(vm =>
        {
            vm?.ShowPrevCommand?.Subscribe(ev => DataContext = ev);
            vm?.ShowNextCommand?.Subscribe(ev => DataContext = ev);
        });
    }
}