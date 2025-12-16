using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class RevisionListView : ReactiveUserControl<RevisionListViewModel>
{
    public RevisionListView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Where(vm => vm.SubjectViewModels == null)
                .Subscribe(async vm =>
                {
                    vm.LoadPageCommand.Execute(1).Subscribe();
                }).DisposeWith(disposables);
        });
    }
}