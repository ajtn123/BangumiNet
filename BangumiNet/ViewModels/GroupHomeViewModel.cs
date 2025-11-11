namespace BangumiNet.ViewModels;

public partial class GroupHomeViewModel : ViewModelBase
{
    public GroupHomeViewModel()
    {
        TopicBrief = new(null);
        Groups = new();
        JoinedGroups = new() { Filter = Api.P1.Models.GroupFilterMode.Joined };

        _ = TopicBrief.Load(1);
        _ = Groups.Load(1);
    }

    [Reactive] public partial GroupTopicListViewModel TopicBrief { get; set; }
    [Reactive] public partial GroupListViewModel Groups { get; set; }
    [Reactive] public partial GroupListViewModel JoinedGroups { get; set; }
}
