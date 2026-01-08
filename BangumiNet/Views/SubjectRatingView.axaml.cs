using Avalonia;
using LiveChartsCore.Painting;
using LiveChartsCore.SkiaSharpView;

namespace BangumiNet.Views;

public partial class SubjectRatingView : ReactiveUserControl<SubjectRatingViewModel>
{
    public SubjectRatingView()
    {
        InitializeComponent();
    }

    private static readonly int[] ratingScores = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    public static readonly List<string> Labels = [.. ratingScores.Select(r => r.ToString()).Reverse()];

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        Chart.Series = ViewModel is { } vm ? [
            new ColumnSeries<int>
            {
                Values = [.. vm.Ratings.Reverse<int>()],
                Fill = Paint.Default,
                DataPadding = new(0,0),
                DataLabelsPadding = new(0),
                XToolTipLabelFormatter = point => $"{Labels[point.Index]} 分",
                YToolTipLabelFormatter = point => $"{point.Model} 票  {point.Model/vm.Total:P2}"
            }
        ] : null;
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);

        Chart.Series = null;
    }
}