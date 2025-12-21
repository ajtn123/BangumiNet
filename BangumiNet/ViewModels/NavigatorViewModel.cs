using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class NavigatorViewModel : ViewModelBase
{
    public NavigatorViewModel()
    {
        var canToId = this.WhenAnyValue(x => x.Input).Select(i => int.TryParse(i, out int id) && id > 0);
        var canToUser = this.WhenAnyValue(x => x.Input).Select(i => CommonUtils.IsAlphaNumeric(i?.Trim()));

        var ToSubject = ReactiveCommand.CreateFromTask(async ct =>
        {
            if (int.TryParse(Input, out var id))
            {
                var svm = await ApiC.GetViewModelAsync<SubjectViewModel>(id, cancellationToken: ct);
                if (svm != null) SecondaryWindow.Show(svm, TargetWindow);
                else MessageWindow.Show($"未找到项目 {id}");
            }
        }, canToId);

        var ToCharacter = ReactiveCommand.CreateFromTask(async ct =>
        {
            if (int.TryParse(Input, out var id))
            {
                var cvm = await ApiC.GetViewModelAsync<CharacterViewModel>(id, cancellationToken: ct);
                if (cvm != null) SecondaryWindow.Show(cvm, TargetWindow);
                else MessageWindow.Show($"未找到角色 {id}");
            }
        }, canToId);

        var ToPerson = ReactiveCommand.CreateFromTask(async ct =>
        {
            if (int.TryParse(Input, out var id))
            {
                var pvm = await ApiC.GetViewModelAsync<PersonViewModel>(id, cancellationToken: ct);
                if (pvm != null) SecondaryWindow.Show(pvm, TargetWindow);
                else MessageWindow.Show($"未找到人物 {id}");
            }
        }, canToId);

        var ToEpisode = ReactiveCommand.CreateFromTask(async ct =>
        {
            if (int.TryParse(Input, out var id))
            {
                var evm = await ApiC.GetViewModelAsync<EpisodeViewModel>(id, cancellationToken: ct);
                if (evm != null) SecondaryWindow.Show(evm, TargetWindow);
                else MessageWindow.Show($"未找到话 {id}");
            }
        }, canToId);

        var ToTopic = ReactiveCommand.CreateFromTask(async ct =>
        {
            if (int.TryParse(Input, out var id))
            {
                var tvm = await ApiC.GetTopicViewModelAsync(ItemType.Subject, id, cancellationToken: ct);
                tvm ??= await ApiC.GetTopicViewModelAsync(ItemType.Group, id, cancellationToken: ct);
                if (tvm != null) SecondaryWindow.Show(tvm, TargetWindow);
                else MessageWindow.Show($"未找到话题 {id}");
            }
        }, canToId);

        var ToBlog = ReactiveCommand.CreateFromTask(async ct =>
        {
            if (int.TryParse(Input, out var id))
            {
                var bvm = await ApiC.GetViewModelAsync<BlogViewModel>(id, cancellationToken: ct);
                if (bvm != null) SecondaryWindow.Show(bvm, TargetWindow);
                else MessageWindow.Show($"未找到日志 {id}");
            }
        }, canToId);

        var ToIndex = ReactiveCommand.CreateFromTask(async ct =>
        {
            if (int.TryParse(Input, out var id))
            {
                var ivm = await ApiC.GetViewModelAsync<IndexViewModel>(id, cancellationToken: ct);
                if (ivm != null) SecondaryWindow.Show(ivm, TargetWindow);
                else MessageWindow.Show($"未找到目录 {id}");
            }
        }, canToId);

        var ToUser = ReactiveCommand.CreateFromTask(async ct =>
        {
            var username = Input?.Trim();
            if (CommonUtils.IsAlphaNumeric(username))
            {
                var uvm = await ApiC.GetViewModelAsync<UserViewModel>(username: username, cancellationToken: ct);
                if (uvm != null) SecondaryWindow.Show(uvm, TargetWindow);
                else MessageWindow.Show($"未找到用户 {Input}");
            }
        }, canToUser);

        var ToGroup = ReactiveCommand.CreateFromTask(async ct =>
        {
            var groupname = Input?.Trim();
            if (CommonUtils.IsAlphaNumeric(groupname))
            {
                var gvm = await ApiC.GetViewModelAsync<GroupViewModel>(username: groupname, cancellationToken: ct);
                if (gvm != null) SecondaryWindow.Show(gvm, TargetWindow);
                else MessageWindow.Show($"未找到小组 {Input}");
            }
        }, canToUser);

        Items = [
            new(ItemType.Subject) { Command=ToSubject },
            new(ItemType.Character) { Command=ToCharacter },
            new(ItemType.Person) { Command=ToPerson },
            new(ItemType.Episode) { Command=ToEpisode },
            new(ItemType.Topic) { Command=ToTopic },
            new(ItemType.Blog) { Command=ToBlog },
            new(ItemType.Index) { Command=ToIndex },
            new(ItemType.User) { Command=ToUser },
            new(ItemType.Group) { Command=ToGroup },
        ];
    }

    [Reactive] public partial string? Input { get; set; }

    public SecondaryWindow? TargetWindow { get; set; }
    public AutoCompleteBoxItemViewModel[] Items { get; init; }

    public async Task<IEnumerable<object>> PopulateAsync(string? searchText, CancellationToken cancellationToken)
    {
        Input = searchText;
        return Items.Where(i => i.Command.CanExecute(null)).Select(x => { x.Prompt = Input; return x; });
    }

    public void Navigate(string[] args)
    {
        if (!Enum.TryParse<ItemType>(args[0], true, out var type) || Items.FirstOrDefault(x => x.Type == type) is not { } nav) return;

        Input = args[1];
        if (nav.Command.CanExecute(null))
            nav.Command.Execute(null);
    }
}

public partial class AutoCompleteBoxItemViewModel : ViewModelBase
{
    public AutoCompleteBoxItemViewModel(ItemType type)
    {
        Type = type;
        TextTemplate = $"转到{type.GetNameCn()} {{0}} >";
        this.WhenAnyValue(x => x.Prompt).Subscribe(x => Text = TextTemplate?.Replace("{0}", Prompt?.Trim()));
    }

    public required ICommand Command { get; init; }
    public ItemType Type { get; init; }
    public string TextTemplate { get; init; }

    [Reactive] public partial string? Prompt { get; set; }
    [Reactive] public partial string? Text { get; private set; }

    public override string? ToString() => Prompt;
}
