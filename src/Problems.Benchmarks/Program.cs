using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

//1 Microsecond (0.000001 sec)


public class Setup
{
    static void Main(string[] args)
        => BenchmarkSwitcher.FromAssembly(typeof(Setup).Assembly).Run(args);
}





