namespace Problems.UniqueWords;

public static class UniqueWordsValeriy
{
    public static int CountUniqueWords(string? source)
    {
        if (source is null) return 0;

        const char space = ' ';
        const int notSetIndex = -1;

        var words = new HashSet<string>();
        var wordStartIndex = notSetIndex;

        for (var i = 0; i < source.Length; i++)
        {
            if (wordStartIndex != notSetIndex)
            {
                if (source[i] != space)
                    continue;

                words.Add(source.Substring(wordStartIndex, i - wordStartIndex));
                wordStartIndex = notSetIndex;
            }
            else
            {
                if (source[i] != space)
                    wordStartIndex = i;
            }
        }

        if (wordStartIndex != notSetIndex)
            words.Add(source.Substring(wordStartIndex, source.Length - wordStartIndex));

        return words.Count;
    }

    public static int CountUniqueWordsAlternative(string? source)
    {
        if (source is null) return 0;

        var words = new HashSet<string>();

        foreach (var word in source.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)))
        {
            words.Add(word);
        }

        return words.Count;
    }

    public static int CountUniqueWordsFun(string? source) => source is null
        ? 0
        : new HashSet<string>(source.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x))).Count;
}