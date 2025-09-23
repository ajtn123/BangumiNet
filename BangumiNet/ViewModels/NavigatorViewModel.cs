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
        this.WhenAnyValue(x => x.Input).Subscribe(x => CanToId = int.TryParse(x, out _));

        ToSubject = ReactiveCommand.CreateFromTask(async () =>
        {
            if (int.TryParse(Input, out var id))
            {
                var sm = await ApiC.V0.Subjects[id].GetAsync();
                if (sm != null) new SecondaryWindow() { Content = new SubjectView() { DataContext = new SubjectViewModel(sm) } }.Show();
            }
        }, this.WhenAnyValue(x => x.CanToId));

        ToCharacter = ReactiveCommand.CreateFromTask(async () =>
        {
            if (int.TryParse(Input, out var id))
            {
                var cm = await ApiC.V0.Characters[id].GetAsync();
                if (cm != null) new SecondaryWindow() { Content = new CharacterView() { DataContext = new CharacterViewModel(cm) } }.Show();
            }
        }, this.WhenAnyValue(x => x.CanToId));

        ToPerson = ReactiveCommand.CreateFromTask(async () =>
        {
            if (int.TryParse(Input, out var id))
            {
                var pm = await ApiC.V0.Persons[id].GetAsync();
                if (pm != null) new SecondaryWindow() { Content = new PersonView() { DataContext = new PersonViewModel(pm) } }.Show();
            }
        }, this.WhenAnyValue(x => x.CanToId));

        ToEpisode = ReactiveCommand.CreateFromTask(async () =>
        {
            if (int.TryParse(Input, out var id))
            {
                var em = await ApiC.V0.Episodes[id].GetAsync();
                if (em != null) new SecondaryWindow() { Content = new EpisodeView() { DataContext = new EpisodeViewModel(em) } }.Show();
            }
        }, this.WhenAnyValue(x => x.CanToId));
    }

    [Reactive] public partial string? Input { get; set; }

    [Reactive] public partial bool CanToId { get; set; }

    public ICommand ToSubject { get; set; }
    public ICommand ToCharacter { get; set; }
    public ICommand ToPerson { get; set; }
    public ICommand ToEpisode { get; set; }
}
