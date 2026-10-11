namespace Algorithms.Common;

/// <summary>
/// A node of a binary tree.
/// </summary>
public sealed class TreeNode
{
    public int Value;
    public TreeNode? Left;
    public TreeNode? Right;

    public TreeNode(int value = 0, TreeNode? left = null, TreeNode? right = null)
    {
        Value = value;
        Left = left;
        Right = right;
    }

    /// <summary>
    /// Builds a tree from values in level order (BFS order), null means "no node".
    /// Example: FromLevelOrder(3, 9, 20, null, null, 15, 7)
    ///        3
    ///       / \
    ///      9   20
    ///         /  \
    ///        15   7
    /// </summary>
    public static TreeNode? FromLevelOrder(params int?[] values)
    {
        if (values.Length == 0 || values[0] is null)
            return null;

        var root = new TreeNode(values[0]!.Value);
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);
        int i = 1;

        while (queue.Count > 0 && i < values.Length)
        {
            var node = queue.Dequeue();
            if (i < values.Length && values[i] is int left)
            {
                node.Left = new TreeNode(left);
                queue.Enqueue(node.Left);
            }
            i++;
            if (i < values.Length && values[i] is int right)
            {
                node.Right = new TreeNode(right);
                queue.Enqueue(node.Right);
            }
            i++;
        }
        return root;
    }

    /// <summary>
    /// Writes the tree in level order: [3, 9, 20, null, null, 15, 7].
    /// </summary>
    public static List<int?> ToLevelOrder(TreeNode? root)
    {
        var result = new List<int?>();
        var queue = new Queue<TreeNode?>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            if (node is null)
            {
                result.Add(null);
                continue;
            }

            result.Add(node.Value);
            queue.Enqueue(node.Left);
            queue.Enqueue(node.Right);
        }

        // Remove "null" values at the end: they carry no information.
        while (result.Count > 0 && result[^1] is null)
            result.RemoveAt(result.Count - 1);
        return result;
    }

    /// <summary>
    /// Finds the first node with this value (DFS). Returns null if there is no such node.
    /// </summary>
    public static TreeNode? Find(TreeNode? root, int value)
    {
        if (root is null) return null;
        if (root.Value == value) return root;
        return Find(root.Left, value) ?? Find(root.Right, value);
    }

    public override string ToString() =>
        "[" + string.Join(", ", ToLevelOrder(this).Select(v => v?.ToString() ?? "null")) + "]";
}
