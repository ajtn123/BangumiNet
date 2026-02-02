using System.Text.RegularExpressions;

namespace BangumiNet.Common.BBCode;

public partial class BBCodeParser
{
    [GeneratedRegex(
        @"\[(?<closing>/)?(?<tag>\*|[a-zA-Z][a-zA-Z0-9]*)
            (?:=(?<attr>(?:[^\[\]]|\\\[|\\\])*))?\]
          | (?<text>[^\[]+|\[)",
        RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled)]
    private static partial Regex TokenRegex();

    public static List<BBNode> Parse(string input)
    {
        List<BBNode> root = [];
        Stack<BBTag> stack = [];

        foreach (Match m in TokenRegex().Matches(input))
        {
            // TEXT
            if (m.Groups["text"].Success)
            {
                if (!string.IsNullOrEmpty(m.Value))
                    CurrentChildren().Add(new BBText(m.Value));
                continue;
            }

            var tag = m.Groups["tag"].Value;
            var attr = m.Groups["attr"].Success ? m.Groups["attr"].Value : null;
            var closing = m.Groups["closing"].Success;

            // LIST ITEM
            if (tag == "*" && !closing)
            {
                CloseUntil("*");
                var li = new BBTag("*");
                CurrentChildren().Add(li);
                stack.Push(li);
                continue;
            }

            // CLOSING TAG
            if (closing)
            {
                CloseUntil(tag);
                continue;
            }

            // OPEN TAG
            var node = new BBTag(tag, attr);
            stack.Push(node);
        }

        // close everything
        while (stack.Count > 0)
            Attach(stack.Pop());

        return root;

        // ---------- helpers ----------

        List<BBNode> CurrentChildren() => stack.Count > 0 ? stack.Peek().Children : root;

        void Attach(BBTag node)
        {
            if (stack.Count > 0)
                stack.Peek().Children.Add(node);
            else
                root.Add(node);
        }

        void CloseUntil(string tagName)
        {
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                Attach(node);
                if (node.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase))
                    break;
            }
        }
    }
}
