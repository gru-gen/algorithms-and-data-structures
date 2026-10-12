using Algorithms.Hashing;
using Algorithms.Tests.Helpers;

namespace Algorithms.Tests.Hashing;

public class TopKFrequentElementsTests
{
    public static TheoryData<int[], int[], int> Examples => new()
    {
        { [1, 2], [1, 1, 1, 2, 2, 3], 2 },
        { [1], [1], 1 },
        { [1, 2], [1, 2, 1, 2, 1, 2, 3, 1, 3, 2], 2 },
        { [7, 3, 5], [7, 3, 7, 5, 3, 7, 1, 5], 3 },
    };

    [Theory, MemberData(nameof(Examples))]
    public void Solve_Example(int[] expected, int[] nums, int k) =>
        AssertExtensions.SameItems(expected, TopKFrequentElements.Solve(nums, k));
}
