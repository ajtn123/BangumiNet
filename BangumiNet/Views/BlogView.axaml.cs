using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class BlogView : ReactiveUserControl<BlogViewModel>
{
    public BlogView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Where(vm => !vm.IsFull)
                .Subscribe(async vm =>
                {
                    var fullItem = await ApiC.GetViewModelAsync<BlogViewModel>(vm.Id);
                    if (fullItem == null) return;
                    ViewModel = fullItem;
                }).DisposeWith(disposables);
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Where(vm => vm.IsFull && !vm.IsLoaded)
                .Subscribe(async vm =>
                {
                    vm.IsLoaded = true;
                    _ = vm.RelatedSubjects?.LoadAsync();
                    vm.Photos?.ProceedPageCommand.Execute().Subscribe();
                    vm.Comments?.LoadPageCommand.Execute().Subscribe();
                }).DisposeWith(disposables);
        });
    }
}