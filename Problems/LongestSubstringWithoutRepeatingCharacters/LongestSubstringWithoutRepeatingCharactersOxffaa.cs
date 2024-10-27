using Xunit;

namespace Problems.LongestSubstringWithoutRepeatingCharacters;

public sealed class LongestSubstringWithoutRepeatingCharactersOxffaa
{
    public static int LengthOfLongestSubstring(string s)
    {
        if (s == null || s.Length == 0) return 0;

        Span<int> dict = stackalloc int[128];

        var newLen = 0;
        var maxLen = 0;
        
        var start = 1;
        var current = 1;
        
        for(; current <= s.Length;  current++){
            var ch = s[current-1];
            
            if (dict[ch] != 0)
            {
                newLen = current - start;
                if (maxLen < newLen) maxLen = newLen;

                var newStart = dict[ch] + 1;
                if (newStart > start) start = newStart;
            }
            
            dict[ch] = current;
        }

        newLen = s.Length - (start - 1);
        return maxLen < newLen ? newLen : maxLen;
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