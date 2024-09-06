using Xunit;

namespace Problems.UniqueWords;

public sealed class UniqueWordsPresSimplestImprovedTimeAndSpace
{
    public static int CountUniqueWords(string source)
    {
        if (String.IsNullOrWhiteSpace(source))
            return 0;

        var passed = new HashSet<int>();
        var hashCode = new HashCode();

        foreach (var ch in source)
        {
            if (ch == ' ')
            {
                passed.Add(hashCode.ToHashCode());
                hashCode = new();
            }
            else
                hashCode.Add(ch);
        }
        
        return passed.Count;
    }

    [Theory]
    [InlineData(null, 0)]
    [InlineData("", 0)]
    [InlineData(" ", 0)]
    [InlineData("   ", 0)]
    [InlineData("abc someww1 123 abc someww1", 3)]
    [InlineData("ggggggggggggggggggggggggg SSSSSSSSSSSSSSSSSSSSSSSSS ttttttttttttttttttttttttt ", 3)]
    public void TestCases(string source, int count)
    {
        Assert.Equal(count, CountUniqueWords(source));
    }
}