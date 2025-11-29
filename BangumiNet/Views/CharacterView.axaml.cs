using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class CharacterView : ReactiveUserControl<CharacterViewModel>
{
    public CharacterView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .Where(x => x?.IsFull == false)
            .Subscribe(async x =>
            {
                if (ViewModel!.Id is not int id) return;
                var fullItem = await ApiC.GetViewModelAsync<CharacterViewModel>(id);
                if (fullItem == null) return;
                DataContext = fullItem;

                _ = ViewModel?.SubjectBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                //_ = ViewModel?.PersonBadgeListViewModel?.Load();
                _ = ViewModel?.CommentListViewModel?.LoadPageAsync(1);
            });
    }
}
