using Algorithms.Hashing;

namespace Algorithms.Tests.Hashing;

public class LongestConsecutiveSequenceTests
{
    public static TheoryData<int, int[]> Examples => new()
    {
        { 4, [100, 4, 200, 1, 3, 2] },
        { 9, [0, 3, 7, 2, 5, 8, 4, 6, 0, 1] },
        { 3, [1, 0, 1, 2] },
        { 4, [4, 10, 2, 3, 11, 1] },
    };

    [Theory, MemberData(nameof(Examples))]
    public void Solve_Example(int expected, int[] nums) =>
        Assert.Equal(expected, LongestConsecutiveSequence.Solve(nums));

}
