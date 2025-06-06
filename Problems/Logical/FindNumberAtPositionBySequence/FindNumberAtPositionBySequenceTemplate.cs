using System.Collections.Immutable;
using Xunit;

namespace Problems.Logical.FindNumberAtPositionBySequence;

public class FindNumberAtPositionBySequenceTemplate
{
    public int FindNumber(ImmutableArray<int> digits, int position)
    {
        return 0;
    }

    [Theory]
    [InlineData(99, 15, 87)]
    [InlineData(99, 28, 75)]
    [InlineData(99, 99, 10)]
    [InlineData(89, 17, 75)]
    [InlineData(79, 35, 48)]
    public void Test(int max, int position, int expected)
    {
        var sequence = Enumerable.Range(1, max).ToImmutableArray();

        Assert.Equal(expected, FindNumber(sequence, position));
    }
}