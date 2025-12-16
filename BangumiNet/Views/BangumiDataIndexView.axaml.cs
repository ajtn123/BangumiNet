using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Media;
using BangumiNet.BangumiData.Models;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class BangumiDataIndexView : ReactiveUserControl<BangumiDataIndexViewModel>
{
    public BangumiDataIndexView()
    {
        InitializeComponent();

        DataGridColumn[] columns = [
            new DataGridTextColumn { Header = "标题", Binding = new Binding(nameof(Item.Title)) },
            new DataGridTemplateColumn { Header = "译名", CellTemplate = new CellTemplate<TranslationList>(nameof(Item.TitleTranslate)) },
            new DataGridTextColumn { Header = "类型", Binding = new Binding(nameof(Item.ItemType)) },
            new DataGridTextColumn { Header = "语言", Binding = new Binding(nameof(Item.Language)) },
            new DataGridTextColumn { Header = "开始放送日期", Binding = new Binding(nameof(Item.Begin)) },
            new DataGridTemplateColumn { Header = "官网", CellTemplate = new CellTemplate<OfficialSiteButton>(nameof(Item.OfficialSite)) },
            new DataGridTemplateColumn { Header = "网站", CellTemplate = new CellTemplate<SiteList>(nameof(Item.Sites)) },
        ];

        foreach (var column in columns)
            IndexGrid.Columns.Add(column);

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Select(vm => vm.WhenAnyValue(x => x.Items).WhereNotNull())
                .Switch()
                .Subscribe(items => IndexGrid.ItemsSource = new DataGridCollectionView(items.Reverse())
                {
                    GroupDescriptions = { new DataGridPathGroupDescription("Begin.Year") }
                }).DisposeWith(disposables);
        });
    }

    public class CellTemplate<TControl>(string bindingPath) : IDataTemplate where TControl : ContentControl, ICellTemplate, new()
    {
        private readonly string bindingPath = bindingPath;
        public Control? Build(object? data)
        {
            var control = new TControl();
            control.Bind(DataContextProperty, new Binding(bindingPath));
            control.WhenAnyValue(x => x.DataContext).Skip(1).Take(1).Subscribe(x => control.Init());
            return control;
        }
        public bool Match(object? data) => true;
    }
    public interface ICellTemplate { void Init(); }
    public class OfficialSiteButton : ContentControl, ICellTemplate
    {
        public void Init()
        {
            if (DataContext is not string url || string.IsNullOrWhiteSpace(url)) return;
            if (url.StartsWith("http://%")) return;
            var uri = new Uri(url);
            Content = new HyperlinkButton
            {
                Content = uri.Host,
                NavigateUri = uri,
                Padding = new(2, 0)
            };
            ToolTip.SetTip(this, url);
        }
    }
    public class TranslationList : ContentControl, ICellTemplate
    {
        public void Init()
        {
            if (DataContext is not Dictionary<Language, string[]> trans) return;
            Content = new TextBlock
            {
                Text = string.Join(Environment.NewLine, trans.SelectMany(x => x.Value)),
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            };
        }
    }
    public class SiteList : ContentControl, ICellTemplate
    {
        public void Init()
        {
            if (DataContext is not Site[] sites) return;
            StackPanel sp = new() { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 2 };
            if (sites.FirstOrDefault(s => s.Name == "bangumi") is { Id: string } bgm)
                sp.Children.Add(new Button
                {
                    Padding = new(0),
                    Content = new FluentIcons.Avalonia.FluentIcon { Icon = FluentIcons.Common.Icon.Open, FontSize = 20 },
                    Command = ReactiveCommand.CreateFromTask(async () => SecondaryWindow.Show(await ApiC.GetViewModelAsync<SubjectViewModel>(int.Parse(bgm.Id)))),
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new(1),
                    Background = Brushes.White,
                });
            foreach (var site in sites)
                sp.Children.Add(new HyperlinkButton
                {
                    Content = Meta[site.Name].Title,
                    NavigateUri = new Uri(site.GetUrl(Meta)!),
                    BorderBrush = Brushes.LightGray,
                    BorderThickness = new(1),
                    Background = Brushes.White,
                    Padding = new(2, 0)
                });
            Content = sp;
        }

        private static Dictionary<string, SiteMeta> Meta => BangumiDataProvider.BangumiDataObject?.SiteMeta!;
    }
}