using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Views;

public partial class EpisodeView : ReactiveUserControl<EpisodeViewModel>
{
    private readonly CompositeDisposable vmSwitch = [];
    public EpisodeView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(v => v.ViewModel)
                .WhereNotNull()
                .Subscribe(vm =>
                {
                    vmSwitch.Clear();
                    vm.ShowPrevCommand?.Subscribe(ev => DataContext = ev).DisposeWith(vmSwitch);
                    vm.ShowNextCommand?.Subscribe(ev => DataContext = ev).DisposeWith(vmSwitch);
                }).DisposeWith(disposables);
        });
    }
}