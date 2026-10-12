using Algorithms.TwoPointers;

namespace Algorithms.Tests.TwoPointers;

public class TwoSumSortedTests
{
    public static TheoryData<int[], int[], int> Examples => new()
    {
        { [1, 2], [2, 7, 11, 15], 9 },
        { [1, 3], [2, 3, 4], 6 },
        { [3, 4], [1, 3, 4, 6, 8, 11], 10 },
    };

    [Theory, MemberData(nameof(Examples))]
    public void Solve_Example(int[] expected, int[] numbers, int target) =>
        Assert.Equal(expected, TwoSumSorted.Solve(numbers, target));

    [Fact]
    public void NegativeNumbers() =>
        Assert.Equal([1, 2], TwoSumSorted.Solve([-1, 0], -1));
}
