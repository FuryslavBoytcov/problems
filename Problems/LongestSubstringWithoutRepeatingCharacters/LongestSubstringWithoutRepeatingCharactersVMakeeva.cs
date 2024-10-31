using Xunit;

namespace Problems.LongestSubstringWithoutRepeatingCharacters;

public sealed class LongestSubstringWithoutRepeatingCharactersVMakeeva
{
    public static int LengthOfLongestSubstring(string s)
    {
        if (String.IsNullOrEmpty(s))
            return 0;

        var size = s.Length;
        var rightPtr = 0;
        var leftPtr = 0;
        var maxLength = 0;
        var sub = new List<char>();

        while (rightPtr < size)
        {
            if (!sub.Contains(s[rightPtr]))
            {
                sub.Add(s[rightPtr]);
                rightPtr++;
                maxLength = Math.Max(maxLength, rightPtr - leftPtr);
            }
            else
            {
                sub.Remove(s[leftPtr]);
                leftPtr++;
            }
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