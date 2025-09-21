using BangumiNet.Views;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class NavigatorViewModel : ViewModelBase
{
    public NavigatorViewModel()
    {
        ToSubject = ReactiveCommand.Create(() =>
        {
            if (int.TryParse(Input, out var id))
                new SecondaryWindow() { Content = new SubjectView() { DataContext = new SubjectViewModel(id) } }.Show();
        }, this.WhenAnyValue(x => x.Input).Select(i => int.TryParse(i, out _)));
        ToSubject.CanExecuteChanged += (s, e) => CanToSubject = ToSubject.CanExecute(null);
    }

    [Reactive] public partial string? Input { get; set; }


    [Reactive] public partial bool CanToSubject { get; set; }
    public ICommand ToSubject { get; set; }
}
