using Xunit;

namespace Problems.UniqueWords;

public sealed class UniqueWordsPresAdvanced
{
    public static int CountUniqueWords(string source)
    {
        if (String.IsNullOrWhiteSpace(source))
            return 0;
        
        var symbols = source.AsSpan();
        var passed = new HashSet<int>();
        
        for (int index = 0, length = 0; index < symbols.Length; index++)
        {
            var symbol = source[index];

            if (symbol == ' ' && length > 0)
            {
                var word = symbols.Slice(index - length, length);
                
                passed.Add(String.GetHashCode(word));
                length = 0;
            }
            else
                length++;
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