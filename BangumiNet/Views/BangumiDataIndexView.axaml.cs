using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
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
            new DataGridTemplateColumn { Header = "译名", CellTemplate = new TranslationsTemplate() },
            new DataGridTextColumn { Header = "类型", Binding = new Binding(nameof(Item.ItemType)) },
            new DataGridTextColumn { Header = "语言", Binding = new Binding(nameof(Item.Language)) },
            new DataGridTemplateColumn { Header = "放送开始", CellTemplate = new TimeTemplate(item => item.Begin), SortMemberPath = nameof(Item.Begin)  },
            new DataGridTemplateColumn { Header = "放送结束", CellTemplate = new TimeTemplate(item => item.End), SortMemberPath = nameof(Item.End)  },
            new DataGridTemplateColumn { Header = "放送周期开始", CellTemplate = new TimeTemplate(item => item.Broadcast?.Start), SortMemberPath = "Broadcast.Start"  },
            new DataGridTemplateColumn { Header = "放送周期", CellTemplate = new BroadcastIntervalTemplate(), SortMemberPath = "Broadcast.Duration" },
            new DataGridTemplateColumn { Header = "官网", CellTemplate = new OfficialSiteTemplate() },
            new DataGridTemplateColumn { Header = "网站", CellTemplate = new SitesTemplate() },
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


    public abstract class CellTemplate<T> : IDataTemplate
    {
        protected abstract T? Select(Item item);
        protected abstract Control? Build(T data);

        public Control? Build(object? param) => Build(Select((Item)param!)!);
        public bool Match(object? data) => data is Item item && Select(item) is not null;
    }

    public abstract class TextTemplate<T> : CellTemplate<T>
    {
        protected abstract string? BuildString(T data);
        protected override Control? Build(T data) => BuildString(data) is { } text ? new TextBlock
        {
            Text = text,
            Margin = new(3),
            VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
        } : null;
    }

    public class OfficialSiteTemplate : CellTemplate<string>
    {
        protected override string? Select(Item item) => item.OfficialSite;
        protected override Control? Build(string url)
        {
            if (string.IsNullOrWhiteSpace(url) || url.StartsWith("http://%")) return null;
            var uri = new Uri(url);
            var btn = new HyperlinkButton
            {
                Content = uri.Host,
                NavigateUri = uri,
                Padding = new(2, 0),
            };
            ToolTip.SetTip(btn, url);
            return btn;
        }
    }

    public class TranslationsTemplate : TextTemplate<Dictionary<Language, string[]>>
    {
        protected override Dictionary<Language, string[]>? Select(Item item) => item.TitleTranslate;
        protected override string? BuildString(Dictionary<Language, string[]> translations)
            => string.Join(Environment.NewLine, translations.SelectMany(x => x.Value));
    }

    public class SitesTemplate : CellTemplate<Site[]>
    {
        protected override Site[]? Select(Item item) => item.Sites;
        protected override Control? Build(Site[] sites)
        {
            StackPanel panel = new() { Orientation = Avalonia.Layout.Orientation.Horizontal, Spacing = 2 };
            if (sites.FirstOrDefault(s => s.Name == "bangumi") is { Id: string } bgm)
                panel.Children.Add(new Button
                {
                    Padding = new(0),
                    Content = new FluentIcons.Avalonia.FluentIcon { Icon = FluentIcons.Common.Icon.Open, FontSize = 20 },
                    Command = ReactiveCommand.CreateFromTask(async () => SecondaryWindow.Show(await ApiC.GetViewModelAsync<SubjectViewModel>(int.Parse(bgm.Id)))),
                });
            foreach (var site in sites)
                panel.Children.Add(new HyperlinkButton
                {
                    Content = Meta[site.Name].Title,
                    NavigateUri = new Uri(site.GetUrl(Meta)!),
                });
            return panel;
        }

        private static Dictionary<string, SiteMeta> Meta => BangumiDataProvider.BangumiDataObject?.SiteMeta!;
    }

    public class TimeTemplate(Func<Item, DateTimeOffset?> selector) : TextTemplate<DateTimeOffset?>
    {
        private readonly Func<Item, DateTimeOffset?> selector = selector;

        protected override DateTimeOffset? Select(Item item) => selector(item);
        protected override string? BuildString(DateTimeOffset? data)
            => data?.ToString("F");
    }

    public class BroadcastIntervalTemplate : TextTemplate<TimeSpan?>
    {
        protected override TimeSpan? Select(Item item) => item.Broadcast?.Duration;
        protected override string? BuildString(TimeSpan? data)
            => data?.ToString("g");
    }
}