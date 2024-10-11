using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Problems.Benchmarks;

[SimpleJob(RuntimeMoniker.Net70)]
[JsonExporter]
[MemoryDiagnoser]
public class LongestSubstringWithoutRepeatingCharactersBenchmark
{
    private string input;
    
    [GlobalSetup]
    public void Setup()
    {
        const int length = 50000;
        var abc = new []
        {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            'A', 'B', 'C', 'D', 'E', 'F', 'F', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            ' ', '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', '.', ',', '-', '/', ':', ';', '<', '>', '=', '?', '@', '[', ']', '\\', '^', '`'
        };

        var sb = new StringBuilder(length);
        var rnd = new Random();

        for (int i = 0; i < length; i++)
        {
            sb.Append(abc[rnd.Next(abc.Length)]);
        }
        
        input = sb.ToString();    
    }
    
    //[Benchmark]
    //public void Name() => LongestSubstringWithoutRepeatingCharactersName.LengthOfLongestSubstring(input);
}