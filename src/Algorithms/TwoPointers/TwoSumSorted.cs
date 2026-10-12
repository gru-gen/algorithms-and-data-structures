namespace Algorithms.TwoPointers;

/// <summary>
/// Two Sum II - Input Array Is Sorted
/// Source: LeetCode, https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/
///
/// The array numbers is sorted (from small to big numbers).
/// Find two numbers with the sum equal to target.
/// Return their positions as [index1, index2], index1 is smaller than index2.
/// Note: here positions start from 1, not from 0!
/// There is always exactly one answer. Use only O(1) extra memory.
///
/// Example: numbers = [2, 7, 11, 15], target = 9  ->  [1, 2]
/// </summary>
public static class TwoSumSorted
{
    public static int[] Solve(int[] numbers, int target)
    {
        int left = 0, right = numbers.Length - 1;
        while (left < right)
        {
            int sum = numbers[left] + numbers[right];
            if (sum == target)
                return [left + 1, right + 1];   // positions start from 1

            if (sum < target)
                left++;             // numbers[left] is too small for ANY pair: drop it
            else
                right--;            // numbers[right] is too big for ANY pair: drop it
        }
        return [];                  // no answer
    }
}
