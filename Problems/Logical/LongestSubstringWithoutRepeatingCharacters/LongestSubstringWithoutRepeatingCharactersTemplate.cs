using Xunit;

namespace Problems.Logical.LongestSubstringWithoutRepeatingCharacters;

/*
 * 1. Copy this template
 * 2. Rename according to your name
 *      LongestSubstringWithoutRepeatingCharactersTemplate -> LongestSubstringWithoutRepeatingCharactersYourName
 * 3. Implement your awesome algorithm
 */
public sealed class LongestSubstringWithoutRepeatingCharactersTemplate
{
    public static int LengthOfLongestSubstring(string source)
    {
        if (String.IsNullOrEmpty(source))
            return 0;

        Span<int> offsets = stackalloc int['z' + 1];
        var max = 0;
        var atIndex = 1;

        for (var index = 0; index < source.Length; index++)
        {
            var symbol = source[index];
            var previousIndex = offsets[symbol];
            var sequenceLength = index + 1 - atIndex;

            max = Math.Max(max, sequenceLength);
            atIndex = Math.Max(atIndex, previousIndex + 1);

            offsets[symbol] = index + 1;
        }

        return Math.Max(max, source.Length - atIndex + 1);
    }

    [Theory]
    [InlineData("abcdefgt", 8)]
    [InlineData("abcabcde", 5)]
    [InlineData("abcabatdefghb", 8)]
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