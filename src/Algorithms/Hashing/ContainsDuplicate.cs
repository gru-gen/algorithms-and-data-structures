namespace Algorithms.Hashing;

/// <summary>
/// Contains Duplicate
/// Source: LeetCode, https://leetcode.com/problems/contains-duplicate/
///
/// You get an array of integers nums.
/// Return true if some value appears in the array two or more times.
/// Return false if all values are different.
///
/// Example: nums = [1, 2, 3, 1]  ->  true
///          nums = [1, 2, 3, 4]  ->  false
/// </summary>
public static class ContainsDuplicate
{
    /// <summary>
    /// A set of the numbers we have already seen. Time O(n), memory O(n).
    /// </summary>
    public static bool Solve(int[] nums)
    {
        var seen = new HashSet<int>();
        foreach (int num in nums)
        {
            if (!seen.Add(num))     // Add returns false: num is already in the set
                return true;
        }
        return false;
    }
}
