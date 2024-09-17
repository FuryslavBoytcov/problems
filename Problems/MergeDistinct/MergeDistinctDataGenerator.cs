namespace Problems.MergeDistinct;

public sealed class MergeDistinctDataGenerator
{
    private readonly Random _random;

    public MergeDistinctDataGenerator(Random random)
    {
        _random = random;
    }

    public MergeDistinctDataModel[] CreateInitialData(int capacity)
    {
        var buffer = new Stack<MergeDistinctDataModel>();
        var result = Enumerable.Range(0, capacity)
            .Select(
                (t, i) =>
                {
                    if (i % 8 == 0)
                    {
                        while (buffer.Count > 0)
                        {
                            var previous = buffer.Pop();
                            return new MergeDistinctDataModel(
                                Id: _random.NextInt64(),
                                Title: Guid.NewGuid().ToString(),
                                Price: (decimal) _random.NextDouble(),
                                UniqueExternalId1: previous.UniqueExternalId2.ToString(),
                                UniqueExternalId2: previous.UniqueExternalId3,
                                UniqueExternalId3: previous.UniqueExternalId4,
                                UniqueExternalId4: _random.Next());
                        }
                    }
                    else if (i % 7 == 0)
                    {
                        var id4 = _random.Next();
                        return new MergeDistinctDataModel(
                            Id: _random.NextInt64(),
                            Title: Guid.NewGuid().ToString(),
                            Price: (decimal) _random.NextDouble(),
                            UniqueExternalId1: id4.ToString(),
                            UniqueExternalId2: id4,
                            UniqueExternalId3: id4,
                            UniqueExternalId4: id4);
                    }

                    var result = new MergeDistinctDataModel(
                        Id: _random.NextInt64(),
                        Title: Guid.NewGuid().ToString(),
                        Price: (decimal) _random.NextDouble(),
                        UniqueExternalId1: Guid.NewGuid().ToString(),
                        UniqueExternalId2: _random.NextInt64(),
                        UniqueExternalId3: _random.Next(),
                        UniqueExternalId4: _random.Next());

                    if (i % 9 == 0)
                        buffer.Push(result);

                    return result;
                })
            .ToArray();

        buffer.Clear();
        return result;
    }

    public Func<MergeDistinctDataModel, MergeDistinctDataModel> CreateChange(
        Func<MergeDistinctDataModel, object>[] uniqueFieldsTest)
    {
        var luckyNumber = 1;

        return t =>
        {
            var result = t;
            if (luckyNumber++ % _random.Next(2, 9) == 0)
            {
                foreach (var uniqueField in uniqueFieldsTest)
                {
                    var value = uniqueField(t);
                    if (t.Id == value as long?)
                        result = result with {Id = _random.NextInt64()};
                    if (t.UniqueExternalId1 == value as string)
                        result = result with {UniqueExternalId1 = Guid.NewGuid().ToString()};
                    if (t.UniqueExternalId2 == value as long?)
                        result = result with {UniqueExternalId2 = _random.NextInt64()};
                    if (t.UniqueExternalId3 == value as int?)
                        result = result with {UniqueExternalId3 = _random.Next()};
                    if (t.UniqueExternalId4 == value as int?)
                        result = result with {UniqueExternalId4 = _random.Next()};
                }
            }

            return result;
        };
    }

    public static Func<MergeDistinctDataModel, object>[][] CreateUniqueFieldsTestCases()
        => new Func<MergeDistinctDataModel, object>[][]
        {
            new Func<MergeDistinctDataModel, object>[]
            {
                fid => fid.Id
            },
            new Func<MergeDistinctDataModel, object>[]
            {
                fid => fid.Id,
                fid1 => fid1.UniqueExternalId1
            },
            new Func<MergeDistinctDataModel, object>[]
            {
                fid => fid.Id,
                fid1 => fid1.UniqueExternalId1,
                fid2 => fid2.UniqueExternalId2
            },
            new Func<MergeDistinctDataModel, object>[]
            {
                fid => fid.Id,
                fid1 => fid1.UniqueExternalId1,
                fid2 => fid2.UniqueExternalId2,
                fid3 => fid3.UniqueExternalId3
            },
            new Func<MergeDistinctDataModel, object>[]
            {
                fid => fid.Id,
                fid1 => fid1.UniqueExternalId1,
                fid2 => fid2.UniqueExternalId2,
                fid3 => fid3.UniqueExternalId3,
                fid4 => fid4.UniqueExternalId4
            }
        };

    public static TSource[] CreateExpectedMerged<TSource, TKey>(
        IEnumerable<TSource> first,
        IEnumerable<TSource> second,
        Func<TSource, TKey> keySelector,
        Func<TSource, object>[] uniqueFields) where TKey : notnull
    {
        var concatSequences = first
            .Select(t => new {Source = t, Collection = 1})
            .Concat(second.Select(t => new {Source = t, Collection = 2}))
            .ToArray();

        var addedSources = new Dictionary<TKey, int>();
        var excludedSources = new HashSet<TKey>();
        var mergeDistinctDataModels = new Dictionary<TKey, TSource>();
        foreach (var item in concatSequences)
        {
            var key = keySelector(item.Source);
            var add = !excludedSources.Contains(key);
            if (!add)
                continue;

            foreach (var uniqueField in uniqueFields)
            {
                var itemValue = uniqueField(item.Source);
                var buffer = new HashSet<int>() {1, 2};
                add = concatSequences
                    .Where(t => Equals(itemValue, uniqueField(t.Source)))
                    .Select(t => t.Collection)
                    .All(t => buffer.Remove(t));

                if (!add)
                    break;
            }

            if (add)
            {
                if (addedSources.TryGetValue(key, out var added))
                {
                    if (added < item.Collection)
                        mergeDistinctDataModels[key] = item.Source;
                }
                else
                {
                    addedSources.Add(key, item.Collection);
                    mergeDistinctDataModels.Add(key, item.Source);
                }
            }
            else
            {
                excludedSources.Add(key);
                mergeDistinctDataModels.Remove(key);
            }
        }

        return mergeDistinctDataModels.Values.ToArray();
    }

    public static ExcludedMergeDistinctDataModel CreateSelfCheckMergedResult(
        MergeDistinctDataModel[] first,
        MergeDistinctDataModel[] second,
        Func<MergeDistinctDataModel, object>[] uniqueFields)
    {
        var testSelfAssertSource = first.Concat(second).GroupBy(t => t.Id).ToDictionary(t => t.Key);
        var excludes = new List<(MergeDistinctDataModel Model, string Reason)>();

        foreach (var uniqueField in uniqueFields)
        {
            var duplicates =
                first.GroupBy(uniqueField)
                    .Where(t => t.Count() > 1)
                    .SelectMany(t => t.Select(x => x.Id))
                    .Union(
                        second.GroupBy(uniqueField).Where(t => t.Count() > 1).SelectMany(t => t.Select(x => x.Id)));

            foreach (var duplicate in duplicates)
            {
                if (testSelfAssertSource.TryGetValue(duplicate, out var removingGroup))
                {
                    foreach (var removingModel in removingGroup)
                        excludes.Add(
                            (removingModel, $"{uniqueField.Method.GetParameters().First().Name}: {removingGroup.Key}"));
                }

                testSelfAssertSource.Remove(duplicate);
            }
        }

        return new(testSelfAssertSource, excludes.ToArray());
    }

    public static IEnumerable<MergeDistinctDataModel> CreateSource(
        IEnumerable<MergeDistinctDataModel> source,
        int length,
        Func<MergeDistinctDataModel, MergeDistinctDataModel>? change = null)
    {
        change ??= f => f;
        while (length > 0)
        {
            foreach (var item in source)
            {
                if (length <= 0)
                    yield break;

                yield return change(item);

                length--;
            }
        }
    }

    public sealed record ExcludedMergeDistinctDataModel(
        Dictionary<long, IGrouping<long, MergeDistinctDataModel>> Result,
        (MergeDistinctDataModel Model, string Reason)[] Excludes);
}