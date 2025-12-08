using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class CharacterView : ReactiveUserControl<CharacterViewModel>
{
    public CharacterView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => !vm.IsFull)
            .Subscribe(async vm =>
            {
                var fullItem = await ApiC.GetViewModelAsync<CharacterViewModel>(vm.Id);
                if (fullItem == null) return;
                ViewModel = fullItem;
            });
        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => vm.IsFull)
            .Subscribe(async vm =>
            {
                vm.SubjectBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                vm.CommentListViewModel?.LoadPageCommand.Execute().Subscribe();
            });
    }
}
