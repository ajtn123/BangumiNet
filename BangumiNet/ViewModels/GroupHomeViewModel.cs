namespace BangumiNet.ViewModels;

public partial class GroupHomeViewModel : ViewModelBase
{
    public GroupHomeViewModel()
    {
        TopicBrief = new(null);
        Groups = new();
        JoinedGroups = new() { Filter = Api.P1.Models.GroupFilterMode.Joined };

        _ = TopicBrief.LoadPageCommand.Execute(1).Subscribe();
        _ = Groups.LoadPageCommand.Execute(1).Subscribe();
    }

    [Reactive] public partial GroupTopicListViewModel TopicBrief { get; set; }
    [Reactive] public partial GroupListViewModel Groups { get; set; }
    [Reactive] public partial GroupListViewModel JoinedGroups { get; set; }
}
