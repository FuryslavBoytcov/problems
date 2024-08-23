namespace Problems.UniqueWords;

public sealed class SuffixTrie
{
    private SuffixTrieNode _root = new();

    public void Add(ReadOnlySpan<char> word)
    {
        var current = _root;
            
        for (int i = 0; i < word.Length; ++i)
        {
            var letter = word[i];
                
            if (!current.Children.ContainsKey(letter))
                current!.Children[letter] = new();
                
            current = current.Children[letter];
        }

        current.IsWordEnding = true;
    }

    public bool Contains(ReadOnlySpan<char> word)
    {
        var current = _root;
            
        for (int i = 0; i < word.Length; ++i)
        {
            var letter = word[i];
                
            if (current.Children.TryGetValue(letter, out var suffixTrieNode))
                current = suffixTrieNode;
            else
                return false;
        }

        return current.IsWordEnding;
    }
    
    private sealed class SuffixTrieNode
    {
        public bool IsWordEnding = false;
        public readonly Dictionary<char, SuffixTrieNode> Children = new();
    }
}