using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class BlogView : ReactiveUserControl<BlogViewModel>
{
    public BlogView()
    {
        InitializeComponent();
        this.WhenAnyValue(x => x.ViewModel)
            .Where(x => x?.IsFull == false)
            .Subscribe(async x =>
            {
                if (ViewModel!.Id is not int id) return;
                var fullItem = await ApiC.GetViewModelAsync<BlogViewModel>(id);
                if (fullItem == null) return;
                DataContext = fullItem;

                _ = ViewModel.RelatedSubjects?.Load();
                ViewModel.Photos?.LoadPageCommand.Execute().Subscribe();
                ViewModel.Comments?.LoadPageCommand.Execute().Subscribe();
            });
    }
}