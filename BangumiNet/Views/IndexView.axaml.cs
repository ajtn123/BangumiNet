using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class IndexView : ReactiveUserControl<IndexViewModel>
{
    public IndexView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Where(vm => !vm.IsFull)
                .Subscribe(async vm =>
                {
                    var fullItem = await ApiC.GetViewModelAsync<IndexViewModel>(vm.Id);
                    if (fullItem == null) return;
                    ViewModel = fullItem;
                }).DisposeWith(disposables);
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Where(vm => vm.IsFull && !vm.IsLoaded)
                .Subscribe(async vm =>
                {
                    vm.IsLoaded = true;
                    vm.Comments?.LoadPageCommand.Execute().Subscribe();
                    vm.RelatedItems?.ProceedPageCommand.Execute().Subscribe();
                }).DisposeWith(disposables);
        });
    }
}