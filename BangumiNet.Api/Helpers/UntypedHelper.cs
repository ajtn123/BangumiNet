using Microsoft.Kiota.Abstractions.Serialization;

namespace BangumiNet.Api.Helpers;

public static class UntypedHelper
{
    extension(UntypedNode node)
    {
        public object? ToObject()
        {
            switch (node)
            {
                case UntypedInteger n:
                    return n.GetValue();
                case UntypedFloat f:
                    return f.GetValue();
                case UntypedDouble d:
                    return d.GetValue();
                case UntypedDecimal de:
                    return de.GetValue();
                case UntypedLong l:
                    return l.GetValue();
                case UntypedBoolean b:
                    return b.GetValue();
                case UntypedString s:
                    return s.GetValue();
                case UntypedNull:
                    return null;
                case UntypedArray arr:
                    {
                        var list = new List<object?>();
                        foreach (var item in arr.GetValue())
                            list.Add(item.ToObject());
                        return list;
                    }
                case UntypedObject obj:
                    {
                        var dict = new Dictionary<string, object?>();
                        if (obj.GetValue() is IDictionary<string, UntypedNode> inner)
                            foreach (var kvp in inner)
                                dict[kvp.Key] = kvp.Value.ToObject();
                        return dict;
                    }
                default:
                    return null;
            }
        }
    }
}
