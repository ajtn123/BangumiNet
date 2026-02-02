using System.Text;

namespace BangumiNet.Common.BBCode;

public abstract class BBNode
{
    public abstract string ToPlainText();
}

public class BBText(string text) : BBNode
{
    public string Text { get; set; } = text;

    public override string ToPlainText() => Text;
}

public class BBTag(string name, string? attribute = null) : BBNode
{
    public string Name { get; set; } = name.ToLower();
    public string? Attribute { get; set; } = attribute;
    public List<BBNode> Children { get; set; } = [];

    public override string ToPlainText()
    {
        var content = new StringBuilder();
        foreach (var child in Children)
        {
            content.Append(child.ToPlainText());
        }
        return content.ToString();
    }
}