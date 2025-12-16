using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class PersonView : ReactiveUserControl<PersonViewModel>
{
    public PersonView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Where(vm => !vm.IsFull)
                .Subscribe(async vm =>
                {
                    var fullItem = await ApiC.GetViewModelAsync<PersonViewModel>(vm.Id);
                    if (fullItem == null) return;
                    ViewModel = fullItem;
                }).DisposeWith(disposables);
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Where(vm => vm.IsFull)
                .Subscribe(async vm =>
                {
                    vm.SubjectBadgeListViewModel?.ProceedPageCommand.Execute().Subscribe();
                    vm.CharacterBadgeListViewModel?.ProceedPageCommand.Execute().Subscribe();
                    vm.CommentListViewModel?.LoadPageCommand.Execute().Subscribe();
                    vm.IndexCardListViewModel?.ProceedPageCommand.Execute().Subscribe();
                }).DisposeWith(disposables);
        });
    }
}
