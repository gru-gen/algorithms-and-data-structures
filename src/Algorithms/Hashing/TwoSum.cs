namespace Algorithms.Hashing;

/// <summary>
/// Two Sum
/// Source: LeetCode, https://leetcode.com/problems/two-sum/
///
/// You get an array of integers nums and an integer target.
/// Find two different positions i and j, so that nums[i] + nums[j] == target.
/// Return them as [i, j]. There is always exactly one answer.
///
/// Example: nums = [2, 7, 11, 15], target = 9  ->  [0, 1]   (2 + 7 = 9)
/// </summary>
public static class TwoSum
{
    /// <summary>
    /// Check every pair. Time O(n^2), memory O(1).
    /// </summary>
    public static int[] SolveBruteForce(int[] nums, int target)
    {
        for (int i = 0; i < nums.Length; i++)
            for (int j = i + 1; j < nums.Length; j++)
                if (nums[i] + nums[j] == target)
                    return new[] { i, j };
        return [];  // no pair found
    }

    /// <summary>
    /// One pass with a dictionary. Time O(n), memory O(n).
    /// </summary>
    public static int[] Solve(int[] nums, int target)
    {
        // value -> index where we saw this value
        var seen = new Dictionary<int, int>();

        for (int i = 0; i < nums.Length; i++)
        {
            int need = target - nums[i];
            if (seen.TryGetValue(need, out int j))
                return new[] { j, i };  // j < i, so the order is right              

            seen[nums[i]] = i;  // add AFTER the check: never use one element twice
        }
        return [];  // no pair found
    }
}
