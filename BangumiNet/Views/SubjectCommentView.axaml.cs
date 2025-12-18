using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Views;

public partial class SubjectCommentView : ReactiveUserControl<SubjectCollectionViewModel>
{
    public SubjectCommentView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Subscribe(vm => vm.User?.Activator.Activate().DisposeWith(disposables))
                .DisposeWith(disposables);
        });
    }
}