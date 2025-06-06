using Xunit;

namespace Problems.Logical.UniqueWords;

public sealed class UniqueWordsDyatlovA
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
        if (String.IsNullOrWhiteSpace(source))
            return 0;

        const int startFlag = -1;
        const char space = ' ';

        var set = new HashSet<int>();
        var letters = source.AsSpan();
        var start = startFlag;

        for (var index = 0; index < letters.Length; index++)
        {
            var letter = letters[index];

            if (letter != space && start == startFlag)
            {
                start = index;
            }
            else if (letter == space && start != startFlag)
            {
                set.Add(string.GetHashCode(letters.Slice(start, index - start)));
                start = startFlag;
            }
        }

        return set.Count;
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
}