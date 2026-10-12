namespace Algorithms.Hashing;

/// <summary>
/// Longest Consecutive Sequence
/// Source: LeetCode, https://leetcode.com/problems/longest-consecutive-sequence/
///
/// You get an array of integers nums. The array is not sorted.
/// Find the longest run of consecutive numbers x, x + 1, x + 2, ... that are all in nums,
/// and return its length. The numbers of the run can be at any places in the array.
/// Your algorithm must work in O(n) time.
///
/// Example: nums = [100, 4, 200, 1, 3, 2]  ->  4   (the run 1, 2, 3, 4)
/// </summary>
public static class LongestConsecutiveSequence
{
    /// <summary>
    /// Hash set; walk up only from the start of a run. Time O(n), memory O(n).
    /// </summary>
    public static int Solve(int[] nums)
    {
        var set = new HashSet<int>(nums);
        int best = 0;
        foreach (int x in set)   // the set, not nums: no duplicates
        {
            if (set.Contains(x - 1))
                continue;               // x is not the start of a run

            int length = 1;
            while (set.Contains(x + length))
                length++;                    // walk up: x + 1, x + 2, ...
            best = Math.Max(best, length);
        }
        return best;
    }
}
