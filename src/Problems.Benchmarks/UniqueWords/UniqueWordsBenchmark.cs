using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Problems.UniqueWords;

namespace Problems.Benchmarks.UniqueWords;

[SimpleJob(RuntimeMoniker.Net70)]
[JsonExporter]
[MemoryDiagnoser]
public class UniqueWordsBenchmark
{
    [Params(1024, 1024 * 1024, 1024 * 1024 * 5)]
    public int SourceLength { get; set; }

    private string _source = default!;

    [GlobalSetup]
    public void Setup()
    {
        var bufffer = new StringBuilder(SourceLength);
        var wordBuffer = new StringBuilder();
        var random = new Random();
        var repeatWords = new Stack<string>();

        var remainderLength = SourceLength;
        while (remainderLength > 0)
        {
            if (repeatWords.Count > 10)
            {
                while (repeatWords.Count > 0 && remainderLength > 0)
                {
                    var word = repeatWords.Pop();
                    if (remainderLength - word.Length - 1 <= 0)
                        continue;

                    bufffer.Append(word);
                    bufffer.Append(' ');

                    remainderLength -= word.Length + 1;
                }

                continue;
            }

            const int min = 1;
            var max = remainderLength > 100 ? 100 : remainderLength;
            var wordLength = random.Next(min, max);
            remainderLength -= wordLength + 1;

            for (var index = 0; index < wordLength; index++)
            {
                var (minChar, maxChar) = index % 2 == 0 ? ('a', 'z') : ((int) 'A', (int) 'Z');
                wordBuffer.Append((char) random.Next(minChar, maxChar));
            }

            bufffer.Append(wordBuffer);
            bufffer.Append(' ');

            repeatWords.Push(wordBuffer.ToString());
            wordBuffer.Clear();
        }

        _source = bufffer.ToString();

        Console.WriteLine(_source[..1024]);
        Console.WriteLine($"Source length: {_source.Length}");
    }

    [Benchmark]
    public void Test_Template() => UniqueWordsDyatlovA.CountUniqueWords(_source);
}

// dotnet run --project src/Problems.Benchmarks/Problems.Benchmarks.csproj -c Release --filter '*UniqueWordsBenchmark*' 