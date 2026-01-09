using Avalonia.Controls;
using Avalonia.Styling;

namespace BangumiNet.ViewModels.Generic;

public class TemplateViewModel(Func<Control> template) : ViewModelBase, ITemplate<Control>
{
    public Func<Control> Template { get; } = template;

    public Control Build() => Template();
    object? ITemplate.Build() => Build();
}
