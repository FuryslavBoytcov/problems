using Xunit;

namespace Problems.Logical.FindMissingNumber;

public sealed class FindMissingNumberTemplate
{
    public static int Find(IReadOnlyCollection<int> source)
    {
        return 0;
    }

    [Fact]
    public void Test()
    {
        Assert.Equal(4, Find(new[] {0, 1, 2, 3, 5, 6}));
        Assert.Equal(3, Find(new[] {0, 1, 2, 4, 5}));
        Assert.Equal(6, Find(new[] {0, 1, 2, 3, 4, 5, 7}));
    }
}