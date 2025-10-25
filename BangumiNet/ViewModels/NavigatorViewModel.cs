using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class NavigatorViewModel : ViewModelBase
{
    public NavigatorViewModel()
    {
        this.WhenAnyValue(x => x.Input).Subscribe(x =>
        {
            CanToId = int.TryParse(x, out int id) && id > 0;
            CanToUser = Common.IsAlphaNumeric(x?.Trim());
        });

        ToSubject = ReactiveCommand.CreateFromTask(async () =>
        {
            if (int.TryParse(Input, out var id))
            {
                var svm = await ApiC.GetViewModelAsync<SubjectViewModel>(id);
                if (svm != null) new SecondaryWindow() { Content = new SubjectView() { DataContext = svm } }.Show();
                else MessageWindow.ShowMessage($"未找到项目 {id}");
            }
        }, this.WhenAnyValue(x => x.CanToId));

        ToCharacter = ReactiveCommand.CreateFromTask(async () =>
        {
            if (int.TryParse(Input, out var id))
            {
                var cvm = await ApiC.GetViewModelAsync<CharacterViewModel>(id);
                if (cvm != null) new SecondaryWindow() { Content = new CharacterView() { DataContext = cvm } }.Show();
                else MessageWindow.ShowMessage($"未找到角色 {id}");
            }
        }, this.WhenAnyValue(x => x.CanToId));

        ToPerson = ReactiveCommand.CreateFromTask(async () =>
        {
            if (int.TryParse(Input, out var id))
            {
                var pvm = await ApiC.GetViewModelAsync<PersonViewModel>(id);
                if (pvm != null) new SecondaryWindow() { Content = new PersonView() { DataContext = pvm } }.Show();
                else MessageWindow.ShowMessage($"未找到人物 {id}");
            }
        }, this.WhenAnyValue(x => x.CanToId));

        ToEpisode = ReactiveCommand.CreateFromTask(async () =>
        {
            if (int.TryParse(Input, out var id))
            {
                var evm = await ApiC.GetViewModelAsync<EpisodeViewModel>(id);
                if (evm != null) new SecondaryWindow() { Content = new EpisodeFullView() { DataContext = evm } }.Show();
                else MessageWindow.ShowMessage($"未找到话 {id}");
            }
        }, this.WhenAnyValue(x => x.CanToId));

        ToUser = ReactiveCommand.CreateFromTask(async () =>
        {
            var username = Input?.Trim();
            if (Common.IsAlphaNumeric(username))
            {
                var uvm = await ApiC.GetViewModelAsync<UserViewModel>(username: username);
                if (uvm != null) new SecondaryWindow() { Content = new UserView() { DataContext = uvm } }.Show();
                else MessageWindow.ShowMessage($"未找到用户 {Input}");
            }
        }, this.WhenAnyValue(x => x.CanToUser));

        Items = [
            new() { Name="ToSubject", Command=ToSubject, TextTemplate="转到项目 {0} >" },
            new() { Name="ToCharacter", Command=ToCharacter, TextTemplate="转到角色 {0} >" },
            new() { Name="ToPerson", Command=ToPerson, TextTemplate="转到人物 {0} >" },
            new() { Name="ToEpisode", Command=ToEpisode, TextTemplate="转到话 {0} >" },
            new() { Name="User", Command=ToUser, TextTemplate="转到用户 {0} >" },
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
        return Items.Where(i => i.Command.CanExecute(null)).Select(x => { x.Prompt = Input; return x; });
    }
}

public partial class AutoCompleteBoxItemViewModel : ViewModelBase
{
    public AutoCompleteBoxItemViewModel()
    {
        this.WhenAnyValue(x => x.Prompt).Subscribe(x => Text = TextTemplate?.Replace("{0}", Prompt?.Trim()));
    }

    public required ICommand Command { get; init; }
    public required string Name { get; init; }
    public required string TextTemplate { get; init; }

    [Reactive] public partial string? Prompt { get; set; }
    [Reactive] public partial string? Text { get; private set; }

    public override string? ToString() => Prompt;
}
