namespace BangumiNet.ViewModels;

public partial class SubjectRatingViewModel : ViewModelBase
{
    public SubjectRatingViewModel(List<int> ratings)
    {
        Ratings = ratings;
        Total = ratings.Sum();
        Average = ratings.Select((c, r) => c * (r + 1)).Sum() / Total;
        StandardDeviation = Math.Sqrt(ratings.Select((c, r) => (c, r)).Sum(x => x.c * Math.Pow(x.r + 1 - Average, 2)) / Total);
    }

    [Reactive] public partial List<int> Ratings { get; set; }
    [Reactive] public partial double Total { get; set; }
    [Reactive] public partial double Average { get; set; }
    [Reactive] public partial double StandardDeviation { get; set; }
}
