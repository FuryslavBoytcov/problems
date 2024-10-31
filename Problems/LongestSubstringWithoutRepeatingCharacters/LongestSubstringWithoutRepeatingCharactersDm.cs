using Xunit;

namespace Problems.LongestSubstringWithoutRepeatingCharacters;

public sealed class LongestSubstringWithoutRepeatingCharactersDm
{
    public static int LengthOfLongestSubstring(string s)
    {
        if (string.IsNullOrEmpty(s))
            return 0;

        var maxLength = 0;
        var symbolIndexes = new int[128];
        Array.Fill(symbolIndexes, -1);

        var start = 0;
        for (var end = 0; end < s.Length; end++)
        {
            var symbol = s[end];
            start = Math.Max(start, symbolIndexes[symbol] + 1);

            var length = end - start + 1;
            if (length > maxLength)
                maxLength = length;

            symbolIndexes[symbol] = end;
        }

        return maxLength;
    }

    [Theory]
    [InlineData("abcdefgt", 8)]
    [InlineData("abcabcde", 5)]
    [InlineData("abcabatdefgh", 8)]
    [InlineData("abcabacdefgh", 8)]
    [InlineData("abba", 2)]
    [InlineData("abcabcbb", 3)]
    [InlineData("", 0)]
    [InlineData(" ", 1)]
    [InlineData("bbbbb", 1)]
    [InlineData("pwwkew", 3)]
    [InlineData("au", 2)]
    [InlineData(null, 0)]
    public void TestCases(string s, int len)
    {
        var result = LengthOfLongestSubstring(s);

        Assert.Equal(len, result);
    }
}