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
            CanToUser = CommonUtils.IsAlphaNumeric(x?.Trim());
        });

        ToSubject = ReactiveCommand.CreateFromTask(async ct =>
        {
            if (int.TryParse(Input, out var id))
            {
                var svm = await ApiC.GetViewModelAsync<SubjectViewModel>(id, cancellationToken: ct);
                if (svm != null) SecondaryWindow.Show(svm);
                else MessageWindow.Show($"未找到项目 {id}");
            }
        }, this.WhenAnyValue(x => x.CanToId));

        ToCharacter = ReactiveCommand.CreateFromTask(async ct =>
        {
            if (int.TryParse(Input, out var id))
            {
                var cvm = await ApiC.GetViewModelAsync<CharacterViewModel>(id, cancellationToken: ct);
                if (cvm != null) SecondaryWindow.Show(cvm);
                else MessageWindow.Show($"未找到角色 {id}");
            }
        }, this.WhenAnyValue(x => x.CanToId));

        ToPerson = ReactiveCommand.CreateFromTask(async ct =>
        {
            if (int.TryParse(Input, out var id))
            {
                var pvm = await ApiC.GetViewModelAsync<PersonViewModel>(id, cancellationToken: ct);
                if (pvm != null) SecondaryWindow.Show(pvm);
                else MessageWindow.Show($"未找到人物 {id}");
            }
        }, this.WhenAnyValue(x => x.CanToId));

        ToEpisode = ReactiveCommand.CreateFromTask(async ct =>
        {
            if (int.TryParse(Input, out var id))
            {
                var evm = await ApiC.GetViewModelAsync<EpisodeViewModel>(id, cancellationToken: ct);
                if (evm != null) SecondaryWindow.Show(evm);
                else MessageWindow.Show($"未找到话 {id}");
            }
        }, this.WhenAnyValue(x => x.CanToId));

        ToTopic = ReactiveCommand.CreateFromTask(async ct =>
        {
            if (int.TryParse(Input, out var id))
            {
                var tvm = await ApiC.GetTopicViewModelAsync(ItemType.Subject, id, cancellationToken: ct);
                tvm ??= await ApiC.GetTopicViewModelAsync(ItemType.Group, id, cancellationToken: ct);
                if (tvm != null) SecondaryWindow.Show(tvm);
                else MessageWindow.Show($"未找到话题 {id}");
            }
        }, this.WhenAnyValue(x => x.CanToId));

        ToUser = ReactiveCommand.CreateFromTask(async ct =>
        {
            var username = Input?.Trim();
            if (CommonUtils.IsAlphaNumeric(username))
            {
                var uvm = await ApiC.GetViewModelAsync<UserViewModel>(username: username, cancellationToken: ct);
                if (uvm != null) SecondaryWindow.Show(uvm);
                else MessageWindow.Show($"未找到用户 {Input}");
            }
        }, this.WhenAnyValue(x => x.CanToUser));

        ToGroup = ReactiveCommand.CreateFromTask(async ct =>
        {
            var groupname = Input?.Trim();
            if (CommonUtils.IsAlphaNumeric(groupname))
            {
                var gvm = await ApiC.GetViewModelAsync<GroupViewModel>(username: groupname, cancellationToken: ct);
                if (gvm != null) SecondaryWindow.Show(gvm);
                else MessageWindow.Show($"未找到小组 {Input}");
            }
        }, this.WhenAnyValue(x => x.CanToUser));

        Items = [
            new() { Name="ToSubject", Command=ToSubject, TextTemplate="转到项目 {0} >" },
            new() { Name="ToCharacter", Command=ToCharacter, TextTemplate="转到角色 {0} >" },
            new() { Name="ToPerson", Command=ToPerson, TextTemplate="转到人物 {0} >" },
            new() { Name="ToEpisode", Command=ToEpisode, TextTemplate="转到话 {0} >" },
            new() { Name="ToTopic", Command=ToTopic, TextTemplate="转到话题 {0} >" },
            new() { Name="User", Command=ToUser, TextTemplate="转到用户 {0} >" },
            new() { Name="Group", Command=ToGroup, TextTemplate="转到小组 {0} >" },
        ];
    }

    [Reactive] public partial string? Input { get; set; }

    [Reactive] public partial bool CanToId { get; set; }
    [Reactive] public partial bool CanToUser { get; set; }

    public ICommand ToSubject { get; set; }
    public ICommand ToCharacter { get; set; }
    public ICommand ToPerson { get; set; }
    public ICommand ToEpisode { get; set; }
    public ICommand ToTopic { get; set; }
    public ICommand ToUser { get; set; }
    public ICommand ToGroup { get; set; }

    public readonly AutoCompleteBoxItemViewModel[] Items;

#pragma warning disable CS1998 // 异步方法缺少 "await" 运算符，将以同步方式运行
#pragma warning disable IDE0060 // 删除未使用的参数
    public async Task<IEnumerable<object>> PopulateAsync(string? searchText, CancellationToken cancellationToken)
#pragma warning restore IDE0060 // 删除未使用的参数
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
