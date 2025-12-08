using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class BlogView : ReactiveUserControl<BlogViewModel>
{
    public BlogView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => !vm.IsFull)
            .Subscribe(async vm =>
            {
                var fullItem = await ApiC.GetViewModelAsync<BlogViewModel>(vm.Id);
                if (fullItem == null) return;
                ViewModel = fullItem;

            });
        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => vm.IsFull)
            .Subscribe(async vm =>
            {
                _ = vm.RelatedSubjects?.Load();
                vm.Photos?.LoadPageCommand.Execute().Subscribe();
                vm.Comments?.LoadPageCommand.Execute().Subscribe();
            });
    }
}