using Algorithms.Hashing;

namespace Algorithms.Tests.Hashing;

public class ContainsDuplicateTests
{
    public static TheoryData<bool, int[]> Examples => new()
    {
        { true, [1, 2, 3, 1] },
        { false, [1, 2, 3, 4] },
        { true, [1, 1, 1, 3, 3, 4, 3, 2, 4, 2] },
    };

    [Theory, MemberData(nameof(Examples))]
    public void Solve_Example(bool expected, int[] nums) => Assert.Equal(expected, ContainsDuplicate.Solve(nums));

    [Fact]
    public void OneNumber() => Assert.False(ContainsDuplicate.Solve([7]));

    [Fact]
    public void NegativeAndLimitValues()
    {
        Assert.False(ContainsDuplicate.Solve([int.MinValue, -1, 0, 1, int.MaxValue]));
        Assert.True(ContainsDuplicate.Solve([int.MinValue, 5, int.MinValue]));
    }
}
