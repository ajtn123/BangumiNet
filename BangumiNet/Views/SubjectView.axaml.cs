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
            var fullSubject = await ApiC.GetViewModelAsync<SubjectViewModel>(id);
            if (fullSubject == null) return;
            DataContext = fullSubject;

            _ = ViewModel?.EpisodeListViewModel?.LoadEpisodes();
            _ = ViewModel?.PersonBadgeListViewModel?.LoadPage();
            _ = ViewModel?.CharacterBadgeListViewModel?.LoadPage();
            _ = ViewModel?.SubjectBadgeListViewModel?.LoadPage();
            _ = ViewModel?.CommentListViewModel?.LoadPageAsync(1);
            ViewModel?.SubjectCollectionViewModel = await ApiC.GetViewModelAsync<SubjectCollectionViewModel>(ViewModel.Id);
            ViewModel?.SubjectCollectionViewModel?.Parent = ViewModel;
        });
    }
}
