namespace Algorithms.Hashing;

/// <summary>
/// Top K Frequent Elements
/// Source: LeetCode, https://leetcode.com/problems/top-k-frequent-elements/
///
/// You get an array of integers nums and an integer k.
/// Return the k numbers that appear in nums most often. You can return them in any order.
/// The answer is always unique.
///
/// Example: nums = [1, 1, 1, 2, 2, 3], k = 2  ->  [1, 2]
///          (1 appears 3 times, 2 appears 2 times, 3 appears 1 time)
/// </summary>
public static class TopKFrequentElements
{
    public static int[] Solve(int[] nums, int k)
    {
        // 1. Count: number -> how many times it appears.
        var counts = new Dictionary<int, int>();
        foreach (int num in nums)
            counts[num] = counts.GetValueOrDefault(num) + 1;

        // 2. buckets[c] = the numbers that appear exactly c times.
        //    A count is from 1 to n, so we need n + 1 buckets.
        var buckets = new List<int>[nums.Length + 1];
        foreach (var (num, count) in counts)
            (buckets[count] ??= new List<int>()).Add(num);

        // 3. Go from the biggest count down and take numbers until we have k.
        var result = new List<int>(k);
        for (int count = nums.Length; count >= 1; count--)
        {
            List<int>? bucket = buckets[count];
            if (bucket is null) continue;
            foreach (int num in bucket)
            {
                result.Add(num);
                if (result.Count == k)
                    return result.ToArray();
            }
        }
        return [.. result];                        // only if k is too big
    }
}
