using Avalonia;
using Avalonia.Controls;

namespace BangumiNet.Utils;

public class EventHandlers
{
    public static void AddHandlers()
    {
        //TextBlock.TextProperty.Changed.AddClassHandler<TextBlock>((tb, e) => tb.Text = System.Net.WebUtility.HtmlDecode(tb.Text));
        HyperlinkButton.NavigateUriProperty.Changed.AddClassHandler<HyperlinkButton>(HandleUri);
    }

    public static void HandleUri(HyperlinkButton link, AvaloniaPropertyChangedEventArgs args)
    {
        var url = link.NavigateUri;
        if (!(url?.Host is "bgm.tv" or "bangumi.tv" or "chii.in")) return;

        var path = url.AbsolutePath.Trim('/');
        Func<Task<ViewModelBase?>>? vm = null;
        if (path.StartsWith("subject/topic/") && int.TryParse(path.Replace("subject/topic/", ""), out int id))
            vm = async () => await ApiC.GetTopicViewModelAsync(ItemType.Subject, id);
        else if (path.StartsWith("group/topic/") && int.TryParse(path.Replace("group/topic/", ""), out id))
            vm = async () => await ApiC.GetTopicViewModelAsync(ItemType.Group, id);
        else if (path.StartsWith("character/") && int.TryParse(path.Replace("character/", ""), out id))
            vm = async () => await ApiC.GetViewModelAsync<CharacterViewModel>(id);
        else if (path.StartsWith("subject/") && int.TryParse(path.Replace("subject/", ""), out id))
            vm = async () => await ApiC.GetViewModelAsync<SubjectViewModel>(id);
        else if (path.StartsWith("person/") && int.TryParse(path.Replace("person/", ""), out id))
            vm = async () => await ApiC.GetViewModelAsync<PersonViewModel>(id);
        else if (path.StartsWith("blog/") && int.TryParse(path.Replace("blog/", ""), out id))
            vm = async () => await ApiC.GetViewModelAsync<BlogViewModel>(id);
        else if (path.StartsWith("index/") && int.TryParse(path.Replace("index/", ""), out id))
            vm = async () => await ApiC.GetViewModelAsync<IndexViewModel>(id);
        else if (path.StartsWith("ep/") && int.TryParse(path.Replace("ep/", ""), out id))
            vm = async () => await ApiC.GetViewModelAsync<EpisodeViewModel>(id);
        else if (path.StartsWith("user/") && path.Replace("user/", "") is string username && CommonUtils.IsAlphaNumeric(username))
            vm = async () => await ApiC.GetViewModelAsync<UserViewModel>(username: username);
        else if (path.StartsWith("group/") && path.Replace("group/", "") is string groupname && CommonUtils.IsAlphaNumeric(groupname))
            vm = async () => await ApiC.GetViewModelAsync<GroupViewModel>(username: groupname);

        if (vm is not null)
        {
            link.NavigateUri = null;
            link.Command = ReactiveCommand.CreateFromTask(async () => SecondaryWindow.Show(await vm()));
        }
    }
}
