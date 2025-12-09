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
            .WhereNotNull()
            .Where(vm => !vm.IsFull)
            .Subscribe(async vm =>
            {
                var fullItem = await ApiC.GetViewModelAsync<SubjectViewModel>(vm.Id);
                if (fullItem == null) return;
                ViewModel = fullItem;
            });
        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Where(vm => vm.IsFull)
            .Subscribe(async vm =>
            {
                vm.EpisodeListViewModel?.LoadPageCommand.Execute().Subscribe();
                vm.PersonBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                vm.CharacterBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                vm.SubjectBadgeListViewModel?.LoadPageCommand.Execute().Subscribe();
                vm.BlogCardListViewModel?.LoadPageCommand.Execute().Subscribe();
                vm.TopicCardListViewModel?.LoadPageCommand.Execute().Subscribe();
                vm.IndexCardListViewModel?.LoadPageCommand.Execute().Subscribe();
                vm.CommentListViewModel?.LoadPageCommand.Execute(1).Subscribe();
                vm.SubjectCollectionViewModel = await ApiC.GetViewModelAsync<SubjectCollectionViewModel>(vm.Id);
                vm.SubjectCollectionViewModel?.Parent = ViewModel;
                OpenInBrowserSplitButton.Flyout = GetOpenInBrowserFlyout((int)vm.Id!);
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
