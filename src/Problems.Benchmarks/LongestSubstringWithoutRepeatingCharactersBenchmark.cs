using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Problems.Logical.LongestSubstringWithoutRepeatingCharacters;

namespace Problems.Benchmarks;

[SimpleJob(RuntimeMoniker.Net70)]
[JsonExporter]
[MemoryDiagnoser]
public class LongestSubstringWithoutRepeatingCharactersBenchmark
{
    private string _input;

    [GlobalSetup]
    public void Setup()
    {
        const int length = 50000;
        var abc = new[]
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
            'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'F', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', '.', ',', '-', '/', ':', ';', '<', '>', '=',
            '?', '@', '[', ']', '\\', '^', '`'
        };

        var buffer = new StringBuilder(length);
        var random = new Random();

        for (var i = 0; i < length; i++)
            buffer.Append(abc[random.Next(abc.Length)]);

        _input = buffer.ToString();
    }

    [Benchmark]
    public void Bzhemba() => LongestSubstringWithoutRepeatingCharactersBzhemba.LengthOfLongestSubstring(_input);

    [Benchmark]
    public void Dm() => LongestSubstringWithoutRepeatingCharactersDm.LengthOfLongestSubstring(_input);

    [Benchmark]
    public void Oxffaa() => LongestSubstringWithoutRepeatingCharactersOxffaa.LengthOfLongestSubstring(_input);

    [Benchmark]
    public void VMakeeva() => LongestSubstringWithoutRepeatingCharactersVMakeeva.LengthOfLongestSubstring(_input);

    [Benchmark]
    public void Solution() => LongestSubstringWithoutRepeatingCharactersTemplate.LengthOfLongestSubstring(_input);
}

// dotnet run --project src/Problems.Benchmarks/Problems.Benchmarks.csproj -c Release --filter '*LongestSubstringWithoutRepeatingCharactersBenchmark*' 