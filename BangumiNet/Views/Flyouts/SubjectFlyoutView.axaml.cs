using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Views;

public partial class SubjectFlyoutView : ReactiveUserControl<SubjectViewModel>
{
    public SubjectFlyoutView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Subscribe(async vm =>
                {
                    if (vm.Id is int id && id != initializedItem)
                    {
                        initializedItem = id;
                        OpenInBrowserSplitButton.Flyout = await SubjectView.GetOpenInBrowserFlyout(id);
                    }
                }).DisposeWith(disposables);
        });
    }

    private int? initializedItem;
}
