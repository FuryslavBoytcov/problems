using Xunit;

namespace Problems.Temp.MergeDistinct;

public sealed class MergeDistinctTemplates
{
    /// <summary>
    /// Represents the concatenation of two <see cref="IEnumerable{TSource}"/>.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of the input sequence.</typeparam>
    /// <typeparam name="TKey">The type of key to identify elements by.</typeparam>
    /// <param name="first">An <see cref="IEnumerable{TSource}" /> whose keys that are also in <paramref name="second"/> will be returned from <paramref name="second"/>, whose keys or <paramref name="uniqueFields"/> that are not unique to the sequence, will cause these elements to be removed from the returned sequence.</param>
    /// <param name="second">An <see cref="IEnumerable{TSource}" /> whose keys or <paramref name="uniqueFields"/> that are not unique to the sequence, will cause these elements to be removed from the returned sequence.</param>
    /// <param name="keySelector">A function to extract the key for each element.</param>
    /// <param name="uniqueFields">A function to extract the unique field for each element.</param>
    /// <returns>A sequence consisting of the result of the combination of two sequences. Duplicate elements will be removed from the result. Uniqueness is determined by combining sequences <paramref name="first"/> and <paramref name="second"/></returns>
    public static IEnumerable<TSource> MergeDistinct<TSource, TKey>(
        IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        Func<TSource, TKey> keySelector,
        params Func<TSource, object>[] uniqueFields) where TKey : notnull
    {
        return MergeDistinctDataGenerator.CreateExpectedMerged(first, second, keySelector, uniqueFields);
    }

    [Theory]
    [InlineData(1, 1, 1)]
    [InlineData(1, 1, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(8, 6, 8)]
    [InlineData(100, 500, 0)]
    [InlineData(100, 0, 500)]
    [InlineData(100, 90, 135)]
    [InlineData(1_000, 200, 13_000)]
    [InlineData(1_000, 900, 33_000)]
    public void TestCases(int size, int firstSize, int secondSize)
    {
        var random = new Random();
        var generator = new MergeDistinctDataGenerator(random);
        var initialData = generator.CreateInitialData(size);
        var uniqueFieldsTests = MergeDistinctDataGenerator.CreateUniqueFieldsTestCases();

        foreach (var uniqueFields in uniqueFieldsTests)
        {
            var createChange = generator.CreateChange(uniqueFields);
            var first = MergeDistinctDataGenerator.CreateSource(initialData, firstSize).ToArray();
            var second = MergeDistinctDataGenerator.CreateSource(initialData, secondSize, createChange).ToArray();

            var expectedMerged
                = MergeDistinctDataGenerator.CreateExpectedMerged(first, second, f => f.Id, uniqueFields);

            var actualMerged = MergeDistinct(
                    first: first,
                    second: second,
                    keySelector: f => f.Id,
                    uniqueFields: uniqueFields)
                .ToArray();

            SelfCheck(first, second, uniqueFields, expectedMerged);

            Assert.Equal(expectedMerged.Length, actualMerged.Length);
            Assert.Equal(expectedMerged, actualMerged);
        }

        return;

        void SelfCheck(
            MergeDistinctDataModel[] first,
            MergeDistinctDataModel[] second,
            Func<MergeDistinctDataModel, object>[] uniqueFields,
            MergeDistinctDataModel[] expectedMerged)
        {
            var testSelfAssertSource
                = MergeDistinctDataGenerator.CreateSelfCheckMergedResult(first, second, uniqueFields);

            Assert.DoesNotContain(testSelfAssertSource.Result.Values, t => t.Count() > 2);
            Assert.Equal(testSelfAssertSource.Result.Values.Count, expectedMerged.Length);
        }
    }
}