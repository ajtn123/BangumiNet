using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;

namespace BangumiNet.Views;

public partial class SubjectCollectionCardView : ReactiveUserControl<SubjectCollectionView>
{
    public SubjectCollectionCardView()
    {
        InitializeComponent();

        var colInfo = new SubjectCollectionView();
        colInfo.Bind(DataContextProperty, this.GetObservable(DataContextProperty));
        SubjectCard.FindControl<StackPanel>("MainStackPanel")?.Children.Add(colInfo);
    }
}
