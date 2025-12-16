using System.Reactive.Disposables.Fluent;

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
                .Subscribe(async vm =>
                {
                    vm.CommentListViewModel?.LoadPageCommand.Execute().Subscribe();
                }).DisposeWith(disposables);
        });
    }
}