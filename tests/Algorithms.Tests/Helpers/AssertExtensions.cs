using Xunit.Sdk;

namespace Algorithms.Tests.Helpers;

/// <summary>
/// Checks that xUnit's Assert does not have.
///
/// 1. Comparisons where the order does not matter: SameItems, SameLists, SameGroups.
/// 2. The same checks as Assert.Equal / Assert.Throws, but with a "message" text in the failure message.
///    Use them inside a loop over many inputs (random tests): the message then says which input failed,
///    for example "nums = [3, 1, 2]: Assert.Equal() Failure ... Expected: 6, Actual: 5".
///
/// Every method throws an XunitException (the same exception as Assert.*) when the check does not pass.
/// </summary>
public static class AssertExtensions
{
    /// <summary>
    /// Assert.Equal with a "message" text in front of the failure message.
    /// </summary>
    public static void Equal<T>(T expected, T actual, string message)
    {
        try
        {
            Assert.Equal(expected, actual);
        }
        catch (XunitException ex)
        {
            throw new XunitException(message + ": " + ex.Message, ex);
        }
    }

    /// <summary>
    /// Assert.Equal for sequences (same items in the same order) with a "message" text.
    /// </summary>
    public static void Equal<T>(IEnumerable<T>? expected, IEnumerable<T>? actual, string message)
    {
        try
        {
            Assert.Equal(expected, actual);
        }
        catch (XunitException ex)
        {
            throw new XunitException(message + ": " + ex.Message, ex);
        }
    }

    /// <summary>
    /// Same items, but the order does not matter: [1, 2, 2] and [2, 1, 2] are the same.
    /// </summary>
    public static void SameItems<T>(IEnumerable<T> expected, IEnumerable<T> actual, string? message = null)
    {
        if (actual is null || !Sorted(expected).SequenceEqual(Sorted(actual)))
            Fail(message, expected, actual);
    }

    /// <summary>
    /// Lists of lists. The order of the lists does not matter, but the order inside each list matters.
    /// Good for permutations: [[1,2],[2,1]] and [[2,1],[1,2]] are the same.
    /// </summary>
    public static void SameLists<T>(IEnumerable<IEnumerable<T>> expected, IEnumerable<IEnumerable<T>>? actual,
        string? message = null)
    {
        if (actual is null || !Keys(expected, sortInside: false).SequenceEqual(Keys(actual, sortInside: false)))
            Fail(message, expected, actual);
    }

    /// <summary>
    /// Lists of lists. No order matters: not the order of the lists, not the order inside a list.
    /// Good for subsets and groups: [[1,2],[3]] and [[3],[2,1]] are the same.
    /// </summary>
    public static void SameGroups<T>(IEnumerable<IEnumerable<T>> expected, IEnumerable<IEnumerable<T>>? actual,
        string? message = null)
    {
        if (actual is null || !Keys(expected, sortInside: true).SequenceEqual(Keys(actual, sortInside: true)))
            Fail(message, expected, actual);
    }

    /// <summary>
    /// The action must throw TException (or an exception derived from it).
    /// </summary>
    public static void Throws<TException>(Action action, string message) where TException : Exception
    {
        try
        {
            action();
        }
        catch (TException)
        {
            return;
        }
        catch (Exception ex)
        {
            throw new XunitException($"{message}: expected exception {typeof(TException).Name}, but got {ex.GetType().Name}");
        }

        throw new XunitException($"{message}: expected exception {typeof(TException).Name}, but there was no exception");
    }

    private static IEnumerable<T> Sorted<T>(IEnumerable<T> items) => items.OrderBy(x => x, Comparer<T>.Default);

    private static List<string> Keys<T>(IEnumerable<IEnumerable<T>> lists, bool sortInside)
    {
        var keys = new List<string>();
        foreach (var list in lists)
        {
            var items = sortInside ? Sorted(list) : list;
            keys.Add(Show.Value(items.ToList()));
        }
        keys.Sort(StringComparer.Ordinal);
        return keys;
    }

    private static void Fail(string? message, object? expected, object? actual)
    {
        string prefix = message is null ? "" : message + ": ";
        throw new XunitException(prefix + $"expected {Show.Value(expected)}, but got {Show.Value(actual)}");
    }
}
