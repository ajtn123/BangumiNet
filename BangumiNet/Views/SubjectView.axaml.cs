using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class SubjectView : ReactiveUserControl<SubjectViewModel>
{
    public SubjectView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .Where(x => x?.IsFull == false)
            .Subscribe(async x =>
            {
                if (ViewModel!.Id is not int id) return;
                var fullItem = await ApiC.GetViewModelAsync<SubjectViewModel>(id);
                if (fullItem == null) return;
                DataContext = fullItem;

                _ = ViewModel?.EpisodeListViewModel?.LoadPageCommand.Execute().Subscribe();
                _ = ViewModel?.PersonBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                _ = ViewModel?.CharacterBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                _ = ViewModel?.SubjectBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                _ = ViewModel?.CommentListViewModel?.LoadPageAsync(1);
                ViewModel?.SubjectCollectionViewModel = await ApiC.GetViewModelAsync<SubjectCollectionViewModel>(ViewModel.Id);
                ViewModel?.SubjectCollectionViewModel?.Parent = ViewModel;
            });
    }
}
