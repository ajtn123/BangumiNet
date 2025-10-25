using BangumiNet.Api.Interfaces;
using LiveChartsCore;
using LiveChartsCore.Painting;
using LiveChartsCore.SkiaSharpView;

namespace BangumiNet.ViewModels;

public partial class SubjectRatingViewModel : ViewModelBase
{
    private static readonly int[] rs = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    public static List<string> Ratings { get; } = [.. rs.Select(r => r.ToString()).Reverse()];

    public SubjectRatingViewModel(IRatingCount ratings)
    {
        Source = ratings;
        int[] counts = [
            ratings.One ?? 0,
            ratings.Two ?? 0,
            ratings.Three ?? 0,
            ratings.Four ?? 0,
            ratings.Five ?? 0,
            ratings.Six ?? 0,
            ratings.Seven ?? 0,
            ratings.Eight ?? 0,
            ratings.Nine ?? 0,
            ratings.OneZero ?? 0
        ];

        Total = counts.Sum();
        Average = counts.Select((c, r) => c * (r + 1)).Sum() / Total;
        StandardDeviation = Math.Sqrt(counts.Select((c, r) => (c, r)).Sum(x => x.c * Math.Pow(x.r + 1 - Average, 2)) / Total);

        Series = [
            new ColumnSeries<int>
            {
                Values = [.. counts.Reverse()],
                Fill = Paint.Default,
                DataPadding = new(0,0),
                DataLabelsPadding=new(0),
                XToolTipLabelFormatter = point => $"{Ratings[point.Index]} 分",
                YToolTipLabelFormatter = point => $"{point.Model} 票  {point.Model/Total:P2}"
            }
        ];
    }

    [Reactive] public partial IRatingCount Source { get; set; }
    [Reactive] public partial double Total { get; set; }
    [Reactive] public partial double Average { get; set; }
    [Reactive] public partial double StandardDeviation { get; set; }
    [Reactive] public partial ISeries[] Series { get; set; }
}
