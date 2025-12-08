namespace BangumiNet.Views;

public partial class AiringView : ReactiveUserControl<AiringViewModel>
{
    public AiringView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Subscribe(async vm =>
            {
                _ = vm.Highlight();
            });
    }
}
