using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class NavigatorViewModel : ViewModelBase
{
    public NavigatorViewModel()
    {
        this.WhenAnyValue(x => x.Input).Subscribe(x =>
        {
            CanToId = int.TryParse(x, out _);
            CanToUser = Common.IsAlphaNumeric(x);
        });

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

        ToUser = ReactiveCommand.CreateFromTask(async () =>
        {
            if (Common.IsAlphaNumeric(Input))
            {
                var um = await ApiC.V0.Users[Input].GetAsync();
                if (um != null) new SecondaryWindow() { Content = new UserView() { DataContext = new UserViewModel(um) } }.Show();
            }
        }, this.WhenAnyValue(x => x.CanToUser));

        Items = [
            new() { Name="ToSubject", Command=ToSubject,TextTemplate="转到项目 {0} >" },
            new() { Name="ToCharacter", Command=ToCharacter,TextTemplate="转到角色 {0} >" },
            new() { Name="ToPerson", Command=ToPerson,TextTemplate="转到人物 {0} >" },
            new() { Name="ToEpisode", Command=ToEpisode,TextTemplate="转到话 {0} >" },
            new() { Name="User", Command=ToUser,TextTemplate="转到用户 {0} >" },
        ];
    }

    [Reactive] public partial string? Input { get; set; }

    [Reactive] public partial bool CanToId { get; set; }
    [Reactive] public partial bool CanToUser { get; set; }

    public ICommand ToSubject { get; set; }
    public ICommand ToCharacter { get; set; }
    public ICommand ToPerson { get; set; }
    public ICommand ToEpisode { get; set; }
    public ICommand ToUser { get; set; }

    public readonly AutoCompleteBoxItemViewModel[] Items;

#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
    public async Task<IEnumerable<object>> PopulateAsync(string? searchText, CancellationToken cancellationToken)
#pragma warning restore CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
    {
        Input = searchText;
        // await Task.Delay(TimeSpan.FromSeconds(0.1), cancellationToken);
        return Items.Where(i => i.Command.CanExecute(null)).Select(x => { x.Text = x.TextTemplate.Replace("{0}", Input); return x; }).ToList();
    }
}

public partial class AutoCompleteBoxItemViewModel : ViewModelBase
{
    public required string Name { get; set; }
    public required string TextTemplate { get; set; }
    [Reactive] public partial string Text { get; set; }
    public required ICommand Command { get; set; }
}
