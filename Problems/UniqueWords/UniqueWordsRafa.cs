using Xunit;

namespace Problems.UniqueWords;

public sealed class UniqueWordsRafa
{
    /* Реализовать метод который посчитает кол-во уникальных слов в передаваемой ему строке. 
        Определение что такое слово:  словом считается любое сочетание цифр и/или любых символов разделенных пробелом. 
        Например: “abc someww1 123 abc someww1” => [abc, someww1, 123] - ответ 3
       
        Объем данных: Длина строки может быть [0; 500MB]
       
        Требования:
            - сигнатура метода `public static int CountUniqueWords(string source)`
            - метод должен корректно обрабатывать NullOrWhiteSpace строку
            - использовать static поля/свойства запрещено
            - реализовать максимально производительный метод в отдельном классе. Класс наименовать `UniqueWordsYourName`  например `UniqueWordsFuryslav`
            - Нельзя использовать параллелизм
    */

    public static int CountUniqueWords(string source)
    {
        return Solution.SolveProblem(source);
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
}