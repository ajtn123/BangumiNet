using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class PersonView : ReactiveUserControl<PersonViewModel>
{
    public PersonView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .Where(x => x?.IsFull == false)
            .Subscribe(async x =>
            {
                if (ViewModel!.Id is not int id) return;
                var fullItem = await ApiC.GetViewModelAsync<PersonViewModel>(id);
                if (fullItem == null) return;
                DataContext = fullItem;

                _ = ViewModel?.SubjectBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                _ = ViewModel?.CharacterBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                _ = ViewModel?.CommentListViewModel?.LoadPageAsync(1);
            });
    }
}
