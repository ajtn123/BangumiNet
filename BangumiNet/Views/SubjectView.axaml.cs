using Avalonia.Controls;
using BangumiNet.BangumiData.Models;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class SubjectView : ReactiveUserControl<SubjectViewModel>
{
    public SubjectView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Where(vm => !vm.IsFull)
                .Subscribe(async vm =>
                {
                    var fullItem = await ApiC.GetViewModelAsync<SubjectViewModel>(vm.Id);
                    if (fullItem == null) return;
                    ViewModel = fullItem;
                }).DisposeWith(disposables);
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Where(vm => vm.IsFull && !vm.IsLoaded)
                .Subscribe(async vm =>
                {
                    vm.IsLoaded = true;
                    vm.EpisodeListViewModel?.ProceedPageCommand.Execute().Subscribe();
                    vm.PersonBadgeListViewModel?.ProceedPageCommand.Execute().Subscribe();
                    vm.CharacterBadgeListViewModel?.ProceedPageCommand.Execute().Subscribe();
                    vm.SubjectBadgeListViewModel?.ProceedPageCommand.Execute().Subscribe();
                    vm.BlogCardListViewModel?.ProceedPageCommand.Execute().Subscribe();
                    vm.TopicCardListViewModel?.ProceedPageCommand.Execute().Subscribe();
                    vm.IndexCardListViewModel?.ProceedPageCommand.Execute().Subscribe();
                    vm.Recommendations?.ProceedPageCommand?.Execute().Subscribe();
                    vm.CommentListViewModel?.LoadPageCommand.Execute(1).Subscribe();
                    vm.SubjectCollectionViewModel = await ApiC.GetViewModelAsync<SubjectCollectionViewModel>(vm.Id);
                    vm.SubjectCollectionViewModel?.Parent = ViewModel;
                    OpenInBrowserSplitButton.Flyout = GetOpenInBrowserFlyout((int)vm.Id!);
                }).DisposeWith(disposables);
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
                SiteMeta meta = BangumiDataProvider.BangumiDataObject.SiteMeta[site.Name];
                menu.Items.Add(new MenuItem
                {
                    Header = meta.Title,
                    Command = ReactiveCommand.Create(() => CommonUtils.OpenUri(site.GetUrl(meta)!))
                });
            }
            return menu;
        }
        else return null;
    }
}
