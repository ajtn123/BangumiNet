using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class BlogCardView : ReactiveUserControl<BlogViewModel>
{
    public BlogCardView()
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