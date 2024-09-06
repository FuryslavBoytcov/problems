using Xunit;

namespace Problems.UniqueWords;

public sealed class UniqueWordsValeriy
{
    public static int CountUniqueWords(string? source)
    {
        if (source is null) return 0;

        const char space = ' ';
        const int notSetIndex = -1;

        var words = new HashSet<string>();
        var wordStartIndex = notSetIndex;

        for (var i = 0; i < source.Length; i++)
        {
            if (wordStartIndex != notSetIndex)
            {
                if (source[i] != space)
                    continue;

                words.Add(source.Substring(wordStartIndex, i - wordStartIndex));
                wordStartIndex = notSetIndex;
            }
            else
            {
                if (source[i] != space)
                    wordStartIndex = i;
            }
        }

        if (wordStartIndex != notSetIndex)
            words.Add(source.Substring(wordStartIndex, source.Length - wordStartIndex));

        return words.Count;
    }
    
    [Theory]
    [InlineData(null, 0)]
    [InlineData("", 0)]
    [InlineData(" ", 0)]
    [InlineData("   ", 0)]
    [InlineData("abc someww1 123 abc someww1", 3)]
    [InlineData("ggggggggggggggggggggggggg SSSSSSSSSSSSSSSSSSSSSSSSS ttttttttttttttttttttttttt ", 3)]
    [InlineData("abc somEww1 123 abc someww1", 4)]
    public void TestCases(string source, int count)
    {
        Assert.Equal(count, CountUniqueWords(source));
    }
}