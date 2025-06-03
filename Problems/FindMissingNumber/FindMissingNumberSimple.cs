namespace Problems.FindMissingNumber;

public sealed class FindMissingNumberSimple
{
    /// <summary>
    /// Искать в заранее отсортированном массиве всегда будет эффективнее бинарным поиском. Sum -> O(n), binary search -> O(log n).
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static int LengthOfLongestSubstring(IReadOnlyCollection<int> source)
    {
        var expected = source.Count * (source.Count + 1) / 2;
        var actual = source.Sum();
        var missingNumber = expected - actual;
        return missingNumber;
    }
}