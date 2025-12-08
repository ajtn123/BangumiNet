using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class RevisionView : ReactiveUserControl<RevisionViewModel>
{
    public RevisionView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => !vm.IsFull)
            .Subscribe(async vm =>
            {
                var fullItem = await ApiC.GetViewModelAsync<RevisionViewModel>(vm);
                if (fullItem == null) return;
                ViewModel = fullItem;
            });
        //this.WhenAnyValue(x => x.ViewModel)
        //    .WhereNotNull()
        //    .Where(vm => vm.IsFull)
        //    .Subscribe(async vm => { });
    }
}