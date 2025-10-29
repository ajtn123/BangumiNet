using LiveChartsCore;
using LiveChartsCore.Painting;
using LiveChartsCore.SkiaSharpView;

namespace BangumiNet.ViewModels;

public partial class SubjectRatingViewModel : ViewModelBase
{
    private static readonly int[] rs = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    public static List<string> Ratings { get; } = [.. rs.Select(r => r.ToString()).Reverse()];

    public SubjectRatingViewModel(List<int> ratings)
    {
        Source = ratings;

        Total = ratings.Sum();
        Average = ratings.Select((c, r) => c * (r + 1)).Sum() / Total;
        StandardDeviation = Math.Sqrt(ratings.Select((c, r) => (c, r)).Sum(x => x.c * Math.Pow(x.r + 1 - Average, 2)) / Total);

        Series = [
            new ColumnSeries<int>
            {
                Values = [.. ratings.Reverse<int>()],
                Fill = Paint.Default,
                DataPadding = new(0,0),
                DataLabelsPadding=new(0),
                XToolTipLabelFormatter = point => $"{Ratings[point.Index]} 分",
                YToolTipLabelFormatter = point => $"{point.Model} 票  {point.Model/Total:P2}"
            }
        ];
    }

    [Reactive] public partial List<int> Source { get; set; }
    [Reactive] public partial double Total { get; set; }
    [Reactive] public partial double Average { get; set; }
    [Reactive] public partial double StandardDeviation { get; set; }
    [Reactive] public partial ISeries[] Series { get; set; }
}
