using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Problems.MergeDistinct;

namespace Problems.Benchmarks;

[SimpleJob(RuntimeMoniker.Net70)]
[JsonExporter]
[MemoryDiagnoser]
public class MergeDistinctBenchmark
{
    public static (int Size, int FirstSize, int SecondSize)[] _params { get; } = new[]
    {
        (1_000, 1_000, 1_000),
        (10_000, 200, 15_000),
        (100_000, 70_000, 50_000),
        (100_000, 10_000, 500_000),
    };

    [ParamsSource(nameof(_params))]
    public (int Size, int FirstSize, int SecondSize) SourceLength { get; set; }

    private MergeDistinctDataModel[] _first;
    private MergeDistinctDataModel[] _second;
    private int _expectedResultCount;
    private Func<MergeDistinctDataModel, object>[] _uniqueFields;

    [GlobalSetup]
    public void Setup()
    {
        var generator = new MergeDistinctDataGenerator(new());
        var initialData = generator.CreateInitialData(SourceLength.Size);
        _first = MergeDistinctDataGenerator.CreateSource(initialData, SourceLength.FirstSize).ToArray();
        _second = MergeDistinctDataGenerator.CreateSource(initialData, SourceLength.SecondSize).ToArray();
        _uniqueFields = MergeDistinctDataGenerator.CreateUniqueFieldsTestCases().Last();

        _expectedResultCount = MergeDistinctDataGenerator.CreateExpectedMerged(
                _first,
                _second,
                f => f.Id,
                _uniqueFields)
            .Length;

        Console.WriteLine($"Generated merge count: {_expectedResultCount}.");
    }

    [Benchmark]
    public void Template_ToArray()
    {
        var actual = MergeDistinctTemplates.MergeDistinctTemplate(_first, _second, f => f.Id, _uniqueFields).ToArray();
        if (actual.Length != _expectedResultCount)
            Console.WriteLine(
                $"Merge distinct failed: {nameof(Template_ToArray)}. Expected: {_expectedResultCount}, Actual: {
                    actual.Length}.");
    }

    [Benchmark]
    public void Template_Yield()
    {
        var actual = MergeDistinctTemplates.MergeDistinctTemplate(_first, _second, f => f.Id, _uniqueFields);
        var count = 0;

        foreach (var item in actual)
        {
            if (item is not null)
                count++;
        }

        if (count != _expectedResultCount)
            Console.WriteLine(
                $"Merge distinct failed: {nameof(Template_Yield)}. Expected: {_expectedResultCount}, Actual: {count}.");
    }
}

// dotnet run --project src/Problems.Benchmarks/Problems.Benchmarks.csproj -c Release --filter '*MergeDistinctBenchmark*' 