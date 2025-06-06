using Xunit;

namespace Problems.Logical.FindMissingNumber;

public sealed class FindMissingNumberDyatlov
{
    /// <summary>
    /// Искать в заранее отсортированном массиве всегда будет эффективнее бинарным поиском. Sum -> O(n), binary search -> O(log n).
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int Find(IReadOnlyCollection<int> source)
    {
        var expected = source.Count * (source.Count + 1) / 2;
        var actual = source.Sum();
        var missingNumber = expected - actual;
        return missingNumber;
    }

    [Fact]
    public void Test()
    {
        Assert.Equal(4, Find(new[] {0, 1, 2, 3, 5, 6}));
        Assert.Equal(3, Find(new[] {0, 1, 2, 4, 5}));
        Assert.Equal(6, Find(new[] {0, 1, 2, 3, 4, 5, 7}));
    }
}