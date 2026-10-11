using Algorithms.Common;
using System.Collections;
using System.Globalization;
using System.Xml.Linq;

namespace Algorithms.Tests.Helpers;

/// <summary>
/// Turns values into short readable text for test messages.
/// </summary>
public static class Show
{
    private const int MaxItems = 20;

    public static string Value(object? value) => value switch
    {
        null => "null",
        string s => "\"" + s + "\"",
        char c => "'" + c + "'",
        bool b => b ? "true" : "false",
        ListNode node => "[" + string.Join(" -> ", ListNode.ToArray(node).Take(MaxItems)) + "]",
        TreeNode tree => tree.ToString(),
        IEnumerable items => Sequence(items),
        IFormattable f => f.ToString(null, CultureInfo.InvariantCulture),
        _ => value.ToString() ?? "",
    };

    private static string Sequence(IEnumerable items)
    {
        var parts = new List<string>();
        int count = 0;
        foreach (object? item in items)
        {
            count++;
            if (count <= MaxItems)
                parts.Add(Value(item));
        }

        if (count > MaxItems)
            parts.Add($"... ({count} items)");
        return "[" + string.Join(", ", parts) + "]";
    }
}
