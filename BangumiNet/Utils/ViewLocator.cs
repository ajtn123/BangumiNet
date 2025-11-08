using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace BangumiNet.Utils;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
        => new TextBlock() { Text = "DataTemplate Not Found: " + param?.GetType().FullName ?? "NULL" };
    public bool Match(object? data)
        => data is ViewModelBase;
}
