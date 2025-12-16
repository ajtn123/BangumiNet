using Avalonia;
using Avalonia.Controls;
using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Views;

public partial class SubjectCollectionCardView : ReactiveUserControl<SubjectCollectionViewModel>
{
    public SubjectCollectionCardView()
    {
        InitializeComponent();

        var colInfo = new SubjectCollectionView();
        SubjectCard.FindControl<StackPanel>("MainStackPanel")?.Children.Add(colInfo);

        this.WhenActivated(disposables =>
        {
            colInfo.Bind(DataContextProperty, this.GetObservable(DataContextProperty)).DisposeWith(disposables);
        });
    }
}
