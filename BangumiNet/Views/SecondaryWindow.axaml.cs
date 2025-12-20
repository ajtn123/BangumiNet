using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Windowing;
using FluentIcons.Avalonia;
using System.Collections;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class SecondaryWindow : AppWindow
{
    public SecondaryWindow()
    {
        InitializeComponent();

        if (SettingProvider.CurrentSettings.ShowSplashScreenOnWindowStartup)
            SplashScreen = new WindowSplashScreen(this);

        Bind(TitleProperty, this.WhenAnyValue(x => x.SecWindowTabView.SelectedItem)
            .OfType<TabViewItem>()
            .Select(x => x.Content)
            .WhereNotNull()
            .OfType<ContentControl>()
            .Select(x => x.Content)
            .WhereNotNull()
            .OfType<ViewModelBase>()
            .Select(x => x.WhenAnyValue(x => x.Title))
            .Switch());

        Instances.Add(this);
        Closed += (s, e) => Instances.Remove(this);
    }

    // https://github.com/amwx/FluentAvalonia/blob/master/samples/FAControlsGallery/Pages/FAControlsPages/TabViewWindowingSample.axaml.cs
    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);

        if (TitleBar != null)
        {
            TitleBar.ExtendsContentIntoTitleBar = true;
            TitleBar.TitleBarHitTestType = TitleBarHitTestType.Complex;

            var dragRegion = this.FindControl<Panel>("CustomDragRegion");
            dragRegion?.MinWidth = FlowDirection == Avalonia.Media.FlowDirection.LeftToRight ?
                TitleBar.RightInset : TitleBar.LeftInset;
        }
    }

    public ObservableCollection<TabViewItem> Tabs { get; } = [];
    private static TabViewItem CreateTab(ViewModelBase vm) => new()
    {
        Content = new ContentControl { Content = vm },
        Header = vm.Title,
        IconSource = new ImageIconSource { Source = new FluentImage { Icon = FluentIcons.Common.Icon.Document } }
    };
    private static ViewModelBase? GetVm(TabViewItem tab)
        => (tab.Content as ContentControl)?.Content as ViewModelBase;


    public static List<SecondaryWindow> Instances { get; } = [];
    public static SecondaryWindow Show(ViewModelBase? vm, bool inNewWindow = false)
    {
        ArgumentNullException.ThrowIfNull(vm);

        var window = inNewWindow ? null : Instances.LastOrDefault();
        window ??= new SecondaryWindow();

        if (vm is not ItemViewModelBase item ||
            !(window.Tabs.FirstOrDefault(t => GetVm(t) is ItemViewModelBase i && i.ItemType == item.ItemType && i.Id == item.Id) is { } tab))
        {
            tab = CreateTab(vm);
            window.Tabs.Add(tab);
        }

        window.SecWindowTabView.SelectedItem = tab;
        window.Show();
        window.Activate();
        return window;
    }

    private void TabCloseRequested(TabView sender, TabViewTabCloseRequestedEventArgs args)
        => Tabs.Remove(args.Tab);

    private void AddTabButtonClick(TabView sender, EventArgs args)
        => new NavigatorWindow().Show(this);

    private void TabItemsChanged(TabView sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs args)
    {
        if (sender.TabItems.Count() == 0) Close();
    }

    private void TabDragStarting(TabView sender, TabViewTabDragStartingEventArgs args)
    {
        args.Data.SetData(DataIdentifier, args.Tab);
        args.Data.RequestedOperation = DragDropEffects.Move;
    }

#pragma warning disable CS0618 // 类型或成员已过时
    private void TabStripDragOver(object? sender, DragEventArgs e)
    {
        if (!e.Data.Contains(DataIdentifier)) return;
        e.DragEffects = DragDropEffects.Move;
    }

    private void TabStripDrop(object? sender, DragEventArgs e)
    {
        if (!e.Data.Contains(DataIdentifier) || e.Data.Get(DataIdentifier) is not TabViewItem tvi) return;
        var desTabView = (TabView)sender!;
        var srcTabView = tvi.FindAncestorOfType<TabView>()!;

        int index = -1;
        for (int i = 0; i < desTabView.TabItems.Count(); i++)
        {
            var item = (TabViewItem)desTabView.ContainerFromIndex(i);
            if (e.GetPosition(item).X - item.Bounds.Width < 0)
            {
                index = i;
                break;
            }
        }

        ((IList)srcTabView.TabItems).Remove(tvi);
        if (index < 0 || index >= desTabView.TabItems.Count())
            ((IList)desTabView.TabItems).Add(tvi);
        else if (index < desTabView.TabItems.Count())
            ((IList)desTabView.TabItems).Insert(index, tvi);
        desTabView.SelectedItem = tvi;

        e.Handled = true;

        // TabItemsChanged won't fire during DragDrop so we need to check
        if (srcTabView.TabItems.Count() == 0)
            srcTabView.FindAncestorOfType<SecondaryWindow>()?.Close();
    }
#pragma warning restore CS0618 // 类型或成员已过时

    private void TabDroppedOutside(TabView sender, TabViewTabDroppedOutsideEventArgs args)
    {
        var srcTabs = ((IList)sender.TabItems);
        if (srcTabs.Count <= 1) return;
        srcTabs.Remove(args.Tab);
        var window = new SecondaryWindow();
        window.Tabs.Add(args.Tab);
        window.Show();
    }

    public const string DataIdentifier = "SecWindowTabItem";
}
