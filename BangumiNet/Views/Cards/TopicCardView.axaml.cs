using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Views;

public partial class TopicCardView : ReactiveUserControl<TopicViewModel>
{
    public TopicCardView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Subscribe(vm => vm.User?.Activator.Activate().DisposeWith(disposables))
                .DisposeWith(disposables);
        });
    }
}