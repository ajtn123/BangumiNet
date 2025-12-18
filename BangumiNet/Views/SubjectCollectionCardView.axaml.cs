using Avalonia;
using Avalonia.Controls;
using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Views;

public partial class SubjectCollectionCardView : ReactiveUserControl<SubjectCollectionViewModel>
{
    public SubjectCollectionCardView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Subscribe(vm => vm.User?.Activator.Activate().DisposeWith(disposables))
                .DisposeWith(disposables);
        });

        var colInfo = new SubjectCollectionView();
        colInfo.Bind(DataContextProperty, this.GetObservable(DataContextProperty));
        SubjectCard.FindControl<StackPanel>("MainStackPanel")?.Children.Add(colInfo);

        //this.WhenActivated(disposables => { });
    }
}
