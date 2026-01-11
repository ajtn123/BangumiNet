using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Views;

public partial class RevisionCardView : ReactiveUserControl<RevisionViewModel>
{
    public RevisionCardView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Subscribe(vm => vm.Creator?.Activator.Activate().DisposeWith(disposables))
                .DisposeWith(disposables);
        });
    }
}