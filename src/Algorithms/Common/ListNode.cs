namespace Algorithms.Common;

/// <summary>
/// A node of a singly linked list.
/// Example: 1 -> 2 -> 3 -> null
/// </summary>
public sealed class ListNode
{
    public int Value;
    public ListNode? Next;

    public ListNode(int value = 0, ListNode? next = null)
    {
        Value = value;
        Next = next;
    }

    /// <summary>
    /// Builds a list from values: From(1, 2, 3) gives 1 -> 2 -> 3.
    /// </summary>
    public static ListNode? From(params int[] values)
    {
        var dummy = new ListNode();
        var tail = dummy;
        foreach (int value in values)
        {
            tail.Next = new ListNode(value);
            tail = tail.Next;
        }
        return dummy.Next;
    }

    /// <summary>
    /// Returns the node with this position (0, 1, 2, ...).
    /// </summary>
    public static ListNode NoadeAt(ListNode head, int position)
    {
        var node = head;
        for (int i = 0; i < position; i++)
            node = node.Next ?? throw new ArgumentOutOfRangeException(nameof(position));
        return node;
    }

    /// <summary>
    /// Copies all values to an array. Stops with an error on very long lists (a cycle?).
    /// </summary>
    public static int[] ToArray(ListNode? head)
    {
        var values = new List<int>();
        for (var node = head; node != null; node = node.Next)
        {
            if (values.Count > 1_000_000)
                throw new InvalidOperationException("The list is too long. Maybe it has a cycle?");
            values.Add(node.Value);
        }
        return [.. values];
    }

    public override string ToString() => string.Join(" -> ", ToArray(this));
}
