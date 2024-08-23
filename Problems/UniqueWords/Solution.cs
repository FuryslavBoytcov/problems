namespace Problems.UniqueWords;

public static class Solution
{ 
    public static int SolveProblem(string inputString) 
    {
        if (String.IsNullOrWhiteSpace(inputString))
            return 0;
        
        var input = inputString.AsSpan();
                
        var suffixTrie = new SuffixTrie();
        var uniqueWordCount = 0;
        
        const char space = ' ';
                
        var inWord = false;
        var wordStart = -1;
                
        for (var i = 0; i < input.Length; ++i)
        {
            if (!inWord && input[i] != space)
            {
                inWord = true;
                wordStart = i;
                continue;
            }
                    
            if (inWord && input[i] == space)
            {
                inWord = false;
        
                var word = input[wordStart..i];
                        
                if (!suffixTrie.Contains(word))
                {
                    suffixTrie.Add(word);
                    uniqueWordCount++;
                }
                        
                continue;
            }
        
            if (inWord && i == input.Length - 1)
            {
                var word = input[wordStart..];
                        
                if (!suffixTrie.Contains(word))
                {
                    suffixTrie.Add(word);
                    uniqueWordCount++;
                }
            }
        }
        
        return uniqueWordCount;
    }
}