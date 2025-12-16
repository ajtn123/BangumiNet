using Avalonia;
using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class SubjectCollectionCardView : ReactiveUserControl<SubjectCollectionViewModel>
{
    public SubjectCollectionCardView()
    {
        InitializeComponent();

        var colInfo = new SubjectCollectionView();
        colInfo.Bind(DataContextProperty, this.GetObservable(DataContextProperty));
        SubjectCard.FindControl<StackPanel>("MainStackPanel")?.Children.Add(colInfo);

        //this.WhenActivated(disposables => { });
    }
}
