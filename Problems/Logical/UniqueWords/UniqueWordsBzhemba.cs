using Xunit;

namespace Problems.Logical.UniqueWords;

public sealed class UniqueWordsBzhemba
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

    public static int CountUniqueWords(string input)
    {
        if (String.IsNullOrWhiteSpace(input))
            return 0;

        var uniqueWords = new HashSet<string>();
        var span = input.AsSpan();
        var start = 0;

        for (var i = 0; i < span.Length; i++)
        {
            if (span[i] != ' ')
                continue;

            if (i > start)
                uniqueWords.Add(span.Slice(start, i - start).ToString());
            start = i + 1;
        }

        if (start < span.Length)
            uniqueWords.Add(span.Slice(start).ToString());

        return uniqueWords.Count;
    }

    [Theory]
    [InlineData(null, 0)]
    [InlineData("", 0)]
    [InlineData(" ", 0)]
    [InlineData("   ", 0)]
    [InlineData("abc someww1 123 abc someww1", 3)]
    [InlineData("ggggggggggggggggggggggggg SSSSSSSSSSSSSSSSSSSSSSSSS ttttttttttttttttttttttttt ", 3)]
    [InlineData("abc somEww1 123 abc someww1", 4)]
    public void TestCases(string source, int count)
    {
        Assert.Equal(count, CountUniqueWords(source));
    }
}