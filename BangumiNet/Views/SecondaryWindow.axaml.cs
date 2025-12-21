using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;
using BangumiNet.Converters;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Windowing;
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

        SecWindowTabView.Bind(TabView.TabItemsProperty, this.WhenAnyValue(x => x.Tabs));

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
        Header = NameCnCvt.Convert(vm) ?? vm.Title,
        IconSource = vm is ItemViewModelBase ivm ? IconHelper.GetIconSource(ivm.ItemType)
                                                 : Utils.IconSource.FromIcon(FluentIcons.Common.Icon.Document),
    };
    private static ViewModelBase? GetVm(TabViewItem tab)
        => (tab.Content as ContentControl)?.Content as ViewModelBase;

    public static List<SecondaryWindow> Instances { get; } = [];
    public static SecondaryWindow Show(ViewModelBase? vm, SecondaryWindow? window = null)
    {
        ArgumentNullException.ThrowIfNull(vm);

        window ??= Instances.LastOrDefault(x => x.IsActive);
        window ??= Instances.LastOrDefault();
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

#pragma warning disable IDE0051
#pragma warning disable CS0618
#pragma warning disable IDE0060
#pragma warning disable CA1822

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

    private void TabStripDragOver(object? sender, DragEventArgs e)
    {
        if (!e.Data.Contains(DataIdentifier)) return;
        e.DragEffects = DragDropEffects.Move;
    }

    private void TabStripDrop(object? sender, DragEventArgs e)
    {
        if (!e.Data.Contains(DataIdentifier) || e.Data.Get(DataIdentifier) is not TabViewItem srcTab) return;
        if ((srcTab.Content as ContentControl)?.GetLogicalChildren().OfType<UserControl>().FirstOrDefault()?.DataContext is not ViewModelBase vm) return;
        var desWindow = ((TabView)sender!).FindAncestorOfType<SecondaryWindow>()!;
        var srcWindow = srcTab.FindAncestorOfType<SecondaryWindow>()!;

        int index = -1;
        for (int i = 0; i < desWindow.Tabs.Count; i++)
        {
            var item = desWindow.SecWindowTabView.ContainerFromIndex(i);
            if (e.GetPosition(item).X - item.Bounds.Width < 0)
            {
                index = i;
                break;
            }
        }

        var desTab = CreateTab(vm);
        if (index >= 0 && index < desWindow.Tabs.Count)
            desWindow.Tabs.Insert(index, desTab);
        else
            desWindow.Tabs.Add(desTab);
        desWindow.SecWindowTabView.SelectedItem = desTab;

        e.Handled = true;

        Avalonia.Threading.Dispatcher.UIThread.Post(() => srcWindow.Tabs.Remove(srcTab), Avalonia.Threading.DispatcherPriority.Background);
    }

    private void TabDroppedOutside(TabView sender, TabViewTabDroppedOutsideEventArgs args)
    {
        var srcTab = args.Tab;
        if ((srcTab.Content as ContentControl)?.GetLogicalChildren().OfType<UserControl>().FirstOrDefault()?.DataContext is not ViewModelBase vm) return;

        Show(vm, new());

        Avalonia.Threading.Dispatcher.UIThread.Post(() => ((IList)sender.TabItems).Remove(srcTab), Avalonia.Threading.DispatcherPriority.Background);
    }

#pragma warning restore IDE0051
#pragma warning restore CS0618
#pragma warning restore IDE0060
#pragma warning restore CA1822

    public const string DataIdentifier = "SecWindowTabItem";
}
