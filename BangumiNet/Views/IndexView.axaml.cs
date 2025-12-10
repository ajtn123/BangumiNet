using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class IndexView : ReactiveUserControl<IndexViewModel>
{
    public IndexView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => !vm.IsFull)
            .Subscribe(async vm =>
            {
                var fullItem = await ApiC.GetViewModelAsync<IndexViewModel>(vm.Id);
                if (fullItem == null) return;
                ViewModel = fullItem;
            });
        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => vm.IsFull)
            .Subscribe(async vm =>
            {
                vm.Comments?.LoadPageCommand.Execute().Subscribe();
                vm.RelatedItems?.LoadPageCommand.Execute().Subscribe();
            });
    }
}