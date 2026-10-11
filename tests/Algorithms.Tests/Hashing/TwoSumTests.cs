using Algorithms.Hashing;
using Algorithms.Tests.Helpers;

namespace Algorithms.Tests.Hashing;

public class TwoSumTests
{
    public static TheoryData<int[], int[], int> Examples => new()
    {
        { [0, 1], [2, 7, 11, 15], 9 },
        { [3, 5], [1, 5, 8, 3, 9, 4], 7 },
    };

    [Theory, MemberData(nameof(Examples))]
    public void Solve_Example(int[] expected, int[] nums, int target) =>
        Assert.Equal(expected, TwoSum.Solve(nums, target));

    [Theory, MemberData(nameof(Examples))]
    public void SolveBruteForce_Example(int[] expected, int[] nums, int target) =>
        Assert.Equal(expected, TwoSum.SolveBruteForce(nums, target));

    [Fact]
    public void AnswerNotAtTheStart() =>
        Assert.Equal([1, 2], TwoSum.Solve([3, 2, 4], 6));

    [Fact]
    public void SameValueTwice() =>
        Assert.Equal([0, 1], TwoSum.Solve([3, 3], 6));

    [Fact]
    public void NegativeNumbers() =>
        Assert.Equal([0, 2], TwoSum.Solve([-3, 4, 3, 90], 0));

    [Fact]
    public void TwoElements() =>
        Assert.Equal([0, 1], TwoSum.Solve([5, -5], 0));

    [Fact]
    public void DuplicateThatIsNotTheAnswer()
    {
        // the second 3 is not a part of the answer (Dictionary.Add would throw on it)
        Assert.Equal([2, 3], TwoSum.Solve([3, 3, 1, 6], 7));
        Assert.Equal([2, 3], TwoSum.SolveBruteForce([3, 3, 1, 6], 7));
    }

    [Fact]
    public void LimitValuesDoNotOverflow()
    {
        // target - nums[i] can be -2 * 10^9 or 2 * 10^9: it still fits into int
        int big = 1_000_000_000;
        Assert.Equal([1, 2], TwoSum.Solve([big, -big, 0], -big));
        Assert.Equal([2, 3], TwoSum.Solve([-big, 7, big, 0], big));
        Assert.Equal([1, 2], TwoSum.SolveBruteForce([big, -big, 0], -big));
    }

    [Fact]
    public void NoAnswerGivesEmptyArray()
    {
        Assert.Equal([], TwoSum.Solve([1, 2, 3], 100));
        Assert.Equal([], TwoSum.SolveBruteForce([1, 2, 3], 100));
        AssertExtensions.Equal([], TwoSum.Solve([3], 6), "one element");
    }

    [Fact]
    public void BruteForceGivesTheSameAnswers()
    {
        Assert.Equal([0, 1], TwoSum.SolveBruteForce([2, 7, 11, 15], 9));
        Assert.Equal([1, 2], TwoSum.SolveBruteForce([3, 2, 4], 6));
        Assert.Equal([0, 1], TwoSum.SolveBruteForce([3, 3], 6));
        Assert.Equal([0, 2], TwoSum.SolveBruteForce([-3, 4, 3, 90], 0));
    }

    [Fact]
    public void BruteForceIsStillOkAtTheLimit() => Limits.Run(() =>
    {
        // n = 10^4 and the pair is at the very end: the brute force checks about 5 * 10^7 pairs.
        int n = 10_000;
        int[] nums = [.. Enumerable.Range(1, n)];
        int target = (n - 1) + n;
        Assert.Equal([n - 2, n - 1], TwoSum.SolveBruteForce(nums, target));
    });

    [Fact]
    public void BigInputIsFast() => Limits.Run(() =>
    {
        // 200 000 numbers: an O(n^2) solution is too slow for this test.
        int n = 200_000;
        int[] nums = [.. Enumerable.Range(1, n)];   // 1, 2, ..., n
        int target = (n - 1) + n;                   // only the last two numbers give this sum
        Assert.Equal([n - 2, n - 1], TwoSum.Solve(nums, target));
    });
}
