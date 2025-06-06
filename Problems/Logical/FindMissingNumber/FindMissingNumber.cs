using Xunit;

namespace Problems.Logical.FindMissingNumber;

public sealed class FindMissingNumber
{
    public static int Find(IReadOnlyCollection<int> source)
    {
        if (source.First() != 0)
            return 0;

        var expectedSum = source.Count * ((1 + source.Count) / 2f);
        var actualSum = source.Sum();

        return (int) expectedSum - actualSum;
    }

    [Fact]
    public void Test()
    {
        Assert.Equal(4, Find(new[] {0, 1, 2, 3, 5, 6}));
        Assert.Equal(3, Find(new[] {0, 1, 2, 4, 5}));
        Assert.Equal(6, Find(new[] {0, 1, 2, 3, 4, 5, 7}));
    }
}