using BangumiNet.Api.Helpers;
using BangumiNet.Api.V0.Models;
using Microsoft.Kiota.Abstractions.Serialization;

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
        if (ib.AdditionalData.TryGetValue("key", out var key))
            Key = key.ToString();
        if (ib.AdditionalData.TryGetValue("value", out var value))
            if (value is string vs)
                Value = vs;
            else if (value is UntypedArray vua)
                SubValues = ((List<object?>?)vua.ToObject())?.Select(p =>
                {
                    if (p is not Dictionary<string, object?> dict) return null;
                    dict.TryGetValue("k", out var k);
                    dict.TryGetValue("v", out var v);
                    return new InfoboxItemViewModel(k?.ToString(), v?.ToString());
                }).Where(x => x is not null).ToObservableCollection()!;
            else Value = value.ToString();
    }
    public InfoboxItemViewModel(PersonDetail_infobox ib)
    {
        if (ib.AdditionalData.TryGetValue("key", out var key))
            Key = key.ToString();
        if (ib.AdditionalData.TryGetValue("value", out var value))
            if (value is string vs)
                Value = vs;
            else if (value is UntypedArray vua)
                SubValues = ((List<object?>?)vua.ToObject())?.Select(p =>
                {
                    if (p is not Dictionary<string, object?> dict) return null;
                    dict.TryGetValue("k", out var k);
                    dict.TryGetValue("v", out var v);
                    return new InfoboxItemViewModel(k?.ToString(), v?.ToString());
                }).Where(x => x is not null).ToObservableCollection()!;
            else Value = value.ToString();
    }
    public InfoboxItemViewModel(Dictionary<string, object?> ib)
    {
        if (ib.TryGetValue("key", out var key))
            Key = key?.ToString();
        if (ib.TryGetValue("value", out var value))
            if (value is string vs)
                Value = vs;
            else if (value is List<object?> vl)
                SubValues = vl.Select(p =>
                {
                    if (p is not Dictionary<string, object?> dict) return null;
                    dict.TryGetValue("k", out var k);
                    dict.TryGetValue("v", out var v);
                    return new InfoboxItemViewModel(k?.ToString(), v?.ToString());
                }).Where(x => x is not null).ToObservableCollection()!;
            else Value = value?.ToString();
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
