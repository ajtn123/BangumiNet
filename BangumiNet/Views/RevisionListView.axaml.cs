using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class RevisionListView : ReactiveUserControl<RevisionListViewModel>
{
    public RevisionListView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => vm.RevisionList.SubjectViewModels == null)
            .Subscribe(async vm =>
            {
                vm.LoadPageCommand.Execute(1).Subscribe();
            });
    }
}