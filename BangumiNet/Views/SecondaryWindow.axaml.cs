using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.VisualTree;
using BangumiNet.Converters;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Windowing;
using System.Collections.Specialized;
using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class SecondaryWindow : FAAppWindow
{
    public SecondaryWindow()
    {
        DataContext = new SecondaryWindowViewModel();

        InitializeComponent();

        if (SettingProvider.Current.ShowSplashScreenOnWindowStartup)
            SplashScreen = new WindowSplashScreen(this);

        Bind(TitleProperty, this.WhenAnyValue(x => x.SecWindowTabView.SelectedItem)
            .OfType<FATabViewItem>()
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

    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);
        TitleBar?.ExtendsContentIntoTitleBar = true;
    }

    public ObservableCollection<ViewModelBase> Tabs => (DataContext as SecondaryWindowViewModel)!.Tabs;

    public static List<SecondaryWindow> Instances { get; } = [];
    public static SecondaryWindow Show(ViewModelBase? vm, SecondaryWindow? window = null)
    {
        vm ??= new();

        window ??= Instances.LastOrDefault(x => x.IsActive);
        window ??= Instances.LastOrDefault();
        window ??= new SecondaryWindow();

        if (!window.Tabs.Contains(vm))
            window.Tabs.Add(vm);

        window.SecWindowTabView.SelectedItem = vm;
        window.Show();
        window.Activate();
        return window;
    }

#pragma warning disable IDE0051
#pragma warning disable IDE0060
#pragma warning disable CA1822

    private void SelectionChanged(object? sender, SelectionChangedEventArgs args)
        => Title = args.AddedItems is { Count: > 0 } selection && selection[0] is ViewModelBase vm ? vm.Title : Constants.ApplicationName;

    private void AddTabButtonClick(FATabView sender, EventArgs args)
        => new NavigatorWindow().Show(this);

    private void TabCloseRequested(FATabView sender, FATabViewTabCloseRequestedEventArgs args)
        => Tabs.Remove((ViewModelBase)args.Item);

    private void TabItemsChanged(FATabView sender, NotifyCollectionChangedEventArgs args)
    {
        if (sender.TabItems.Count == 0) Close();
    }

    // DataTransfer is not properly implemented in FA 3.0.0
    private static (ObservableCollection<ViewModelBase> Source, ViewModelBase Item)? dragging;

    private void TabDragStarting(FATabView sender, FATabViewTabDragStartingEventArgs args)
    {
        dragging = null;
        if (sender.TabItemsSource is not ObservableCollection<ViewModelBase> source ||
            args.Item is not ViewModelBase item)
            return;
        dragging = (source, item);
        args.Data.RequestedOperation = DragDropEffects.Move;
    }

    private void TabStripDragOver(object? sender, DragEventArgs e)
    {
        if (dragging is null) return;
        e.DragEffects = DragDropEffects.Move;
    }

    private void TabStripDrop(object? sender, DragEventArgs e)
    {
        if (dragging is not { } src) return;
        dragging = null;
        if (sender is not FATabView des ||
            des.FindAncestorOfType<SecondaryWindow>() is not { } desWindow ||
            des.TabItemsSource is not ObservableCollection<ViewModelBase> desTabs ||
            src.Source == desTabs)
            return;

        int index = -1;
        for (int i = 0; i < desWindow.Tabs.Count; i++)
        {
            if (desWindow.SecWindowTabView.ContainerFromIndex(i) is not { } container)
                continue;
            if (e.GetPosition(container).X - container.Bounds.Width < 0)
            {
                index = i;
                break;
            }
        }

        src.Source.Remove(src.Item);
        if (index >= 0 && index < desWindow.Tabs.Count)
            desTabs.Insert(index, src.Item);
        else
            desWindow.Tabs.Add(src.Item);
        desWindow.SecWindowTabView.SelectedItem = src.Item;

        e.Handled = true;
    }

    private void TabDroppedOutside(FATabView sender, FATabViewTabDroppedOutsideEventArgs args)
    {
        dragging = null;
        if (sender.TabItemsSource is not ObservableCollection<ViewModelBase> source ||
            args.Item is not ViewModelBase item ||
            source.Count <= 1)
            return;

        source.Remove(item);
        Show(item, new());
    }

#pragma warning restore IDE0051
#pragma warning restore IDE0060
#pragma warning restore CA1822

}
