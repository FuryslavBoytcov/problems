using Xunit;

namespace Problems.LongestSubstringWithoutRepeatingCharacters;

/*
 * 1. Copy this template
 * 2. Rename according to your name
 *      LongestSubstringWithoutRepeatingCharactersTemplate -> LongestSubstringWithoutRepeatingCharactersYourName
 * 3. Implement your awesome algorithm
 */
public sealed class LongestSubstringWithoutRepeatingCharactersTemplate
{
    public static int LengthOfLongestSubstring(string s)
    {
        return 0;
    }

    [Theory]
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