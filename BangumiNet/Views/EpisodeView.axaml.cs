using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Views;

public partial class EpisodeView : ReactiveUserControl<EpisodeViewModel>
{
    private readonly CompositeDisposable disposables = [];
    public EpisodeView()
    {
        InitializeComponent();

        this.WhenAnyValue(v => v.ViewModel)
            .WhereNotNull()
            .Subscribe(vm =>
            {
                disposables.Clear();
                vm.ShowPrevCommand?.Subscribe(ev => DataContext = ev).DisposeWith(disposables);
                vm.ShowNextCommand?.Subscribe(ev => DataContext = ev).DisposeWith(disposables);
            });
    }
}