namespace BangumiNet.Views;

public partial class EpisodeFullView : ReactiveUserControl<EpisodeViewModel>
{
    public EpisodeFullView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Subscribe(async vm =>
            {
                vm.CommentListViewModel?.LoadPageCommand.Execute().Subscribe();
            });
    }
}