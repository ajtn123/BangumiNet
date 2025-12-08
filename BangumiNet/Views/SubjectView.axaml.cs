using Avalonia.Controls;
using BangumiNet.BangumiData.Models;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class SubjectView : ReactiveUserControl<SubjectViewModel>
{
    public SubjectView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel)
            .Where(x => x?.IsFull == false)
            .Subscribe(async x =>
            {
                if (ViewModel!.Id is not int id) return;
                var fullItem = await ApiC.GetViewModelAsync<SubjectViewModel>(id);
                if (fullItem == null) return;
                DataContext = fullItem;

                ViewModel?.EpisodeListViewModel?.LoadPageCommand.Execute().Subscribe();
                ViewModel?.PersonBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                ViewModel?.CharacterBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                ViewModel?.SubjectBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                ViewModel?.BlogCardListViewModel?.LoadPageCommand.Execute().Subscribe();
                ViewModel?.TopicCardListViewModel?.LoadPageCommand.Execute().Subscribe();
                ViewModel?.CommentListViewModel?.LoadPageCommand.Execute(1).Subscribe();
                ViewModel?.SubjectCollectionViewModel = await ApiC.GetViewModelAsync<SubjectCollectionViewModel>(ViewModel.Id);
                ViewModel?.SubjectCollectionViewModel?.Parent = ViewModel;
                OpenInBrowserSplitButton.Flyout = GetOpenInBrowserFlyout(id);
            });
    }

    public static MenuFlyout? GetOpenInBrowserFlyout(int id)
    {
        if (BangumiDataProvider.BangumiDataObject?.Items.LastOrDefault(item =>
        {
            if (item.Sites.Length == 0) return false;
            var bgm = item.Sites.FirstOrDefault(x => x.Name == "bangumi");
            return bgm.Id != null && int.Parse(bgm.Id) == id;
        }) is { Sites: Site[] sites })
        {
            var menu = new MenuFlyout();
            foreach (var site in sites)
            {
                SiteMeta meta = BangumiDataProvider.BangumiDataObject.Value.SiteMeta[site.Name];
                menu.Items.Add(new MenuItem
                {
                    Header = meta.Title,
                    Command = ReactiveCommand.Create(() => CommonUtils.OpenUrlInBrowser(site.GetUrl(meta)!))
                });
            }
            return menu;
        }
        else return null;
    }
}
