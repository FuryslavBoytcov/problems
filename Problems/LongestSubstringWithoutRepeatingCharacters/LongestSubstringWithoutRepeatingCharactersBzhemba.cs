using Xunit;

namespace Problems.LongestSubstringWithoutRepeatingCharacters;

/*
 * 1. Copy this template
 * 2. Rename according to your name
 *      LongestSubstringWithoutRepeatingCharactersBzhemba -> LongestSubstringWithoutRepeatingCharactersYourName
 * 3. Implement your awesome algorithm
 */
public sealed class LongestSubstringWithoutRepeatingCharactersBzhemba
{
    public static int LengthOfLongestSubstring(string s)
    {
        if (s is null)
            return 0;

        var window = new HashSet<char>();
        var left = 0;
        var maxLength = 0;

        for (var right = 0; right < s.Length; right++)
        {
            while (window.Contains(s[right]))
            {
                window.Remove(s[left++]);
            }

            window.Add(s[right]);
            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
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