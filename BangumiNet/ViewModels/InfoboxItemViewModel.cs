using BangumiNet.Api.V0.Models;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;

namespace BangumiNet.ViewModels;

public partial class InfoboxItemViewModel : ViewModelBase
{
    public InfoboxItemViewModel(Subjects sub)
    {
        Key = sub.Key;
        Value = sub.Value?.String;
        if (sub.Value?.SubjectsValueMember1 != null)
        {
            SubValues = [];
            foreach (var subject in sub.Value.SubjectsValueMember1)
            {
                subject.AdditionalData.TryGetValue("k", out var k);
                subject.AdditionalData.TryGetValue("v", out var v);
                SubValues.Add(new(k?.ToString(), v?.ToString()));
            }
        }
    }
    public InfoboxItemViewModel(Character_infobox ib)
    {
        Key = ib.AdditionalData["key"].ToString();
        if (ib.AdditionalData["value"] is string vStr) Value = vStr;
    }
    public InfoboxItemViewModel(string? key, string? value)
    {
        Key = key;
        Value = value;
    }

    [Reactive] public partial string? Key { get; set; }
    [Reactive] public partial string? Value { get; set; }
    [Reactive] public partial ObservableCollection<InfoboxItemViewModel>? SubValues { get; set; }
}
