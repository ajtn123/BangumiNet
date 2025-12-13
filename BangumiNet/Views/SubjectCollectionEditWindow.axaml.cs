using Avalonia.Controls.Primitives;
using BangumiNet.Api.ExtraEnums;
using FluentIcons.Avalonia;
using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Views;

public partial class SubjectCollectionEditWindow : ReactiveWindow<SubjectCollectionViewModel>
{
    public SubjectCollectionEditWindow()
    {
        InitializeComponent();

        this.WhenActivated(d =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .Subscribe(vm => vm?.InitEditCommands())
                .DisposeWith(d);
            this.WhenAnyObservable(x => x.ViewModel!.SaveCommand)
                .Subscribe(async isSuccess =>
                {
                    SaveButton.IsEnabled = !isSuccess;
                    SaveIcon.Icon = isSuccess ? FluentIcons.Common.Icon.Checkmark : FluentIcons.Common.Icon.Dismiss;

                    await Task.Delay(5000);

                    SaveButton.IsEnabled = true;
                    SaveIcon.Icon = FluentIcons.Common.Icon.Save;
                }).DisposeWith(d);
        });

        StatusComboBox.ItemsSource = Enum.GetValues<CollectionType>();
        RatingSlider.WhenAnyValue(s => s.Value).Subscribe(v =>
        {
            if (v == 0)
            {
                RatingIcon.IconVariant = FluentIcons.Common.IconVariant.Regular;
                RatingIcon.Icon = FluentIcons.Common.Icon.StarDismiss;
            }
            else
            {
                RatingIcon.IconVariant = FluentIcons.Common.IconVariant.Color;
                RatingIcon.Icon = FluentIcons.Common.Icon.Star;
            }
        });

        TagInputBox.KeyDown += (s, e) =>
        {
            if (e.Key == Avalonia.Input.Key.Enter)
                ViewModel?.AddTagCommand?.Execute().Subscribe();
        };
    }

    private void ButtonExit(object? sender, Avalonia.Interactivity.RoutedEventArgs e) => Close();

    private void ButtonUndo(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (ViewModel?.Parent?.SubjectCollectionViewModel != null)
            DataContext = new SubjectCollectionViewModel(ViewModel.Parent.SubjectCollectionViewModel);
        else if (ViewModel?.Source != null)
            DataContext = new SubjectCollectionViewModel(ViewModel.Source);
        else if (ViewModel?.Parent != null)
            DataContext = new SubjectCollectionViewModel(ViewModel.Parent);
    }
    private void ChangeWrap(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (TagScoll.HorizontalScrollBarVisibility == ScrollBarVisibility.Auto)
        {
            TagScoll.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            WrapButton.Content = new FluentIcon() { Icon = FluentIcons.Common.Icon.ArrowWrapOff, FontSize = 16 };
        }
        else
        {
            TagScoll.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            WrapButton.Content = new FluentIcon() { Icon = FluentIcons.Common.Icon.ArrowWrap, FontSize = 16 };
        }
    }
}