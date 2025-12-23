using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Views;

public partial class ReactionFlyoutView : ReactiveUserControl<ReactionViewModel>
{
    public ReactionFlyoutView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Subscribe(vm => { foreach (var user in vm.Users) user.Activator.Activate().DisposeWith(disposables); })
                .DisposeWith(disposables);
        });
    }
}