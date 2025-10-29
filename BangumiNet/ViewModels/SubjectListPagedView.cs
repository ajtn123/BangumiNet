namespace BangumiNet.ViewModels;

public partial class SubjectListPagedViewModel : SubjectListViewModel
{
    public SubjectListPagedViewModel()
    {
        PageNavigator = new();
    }

    [Reactive] public partial PageNavigatorViewModel PageNavigator { get; set; }
}
