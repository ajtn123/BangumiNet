using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class EpisodeFullView : ReactiveUserControl<EpisodeViewModel>
{
    public EpisodeFullView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Where(vm => !vm.IsLoaded)
                .Subscribe(async vm =>
                {
                    vm.IsLoaded = true;
                    vm.CommentListViewModel?.LoadPageCommand.Execute().Subscribe();
                }).DisposeWith(disposables);
        });
    }
}