using BenchmarkDotNet.Running;
using ZBase32.Performance;

/*
    BenchmarkDotNet v0.15.6, Windows 11 (10.0.26200.6899)
    AMD Ryzen 7 8845HS w/ Radeon 780M Graphics 3.80GHz, 1 CPU, 16 logical and 8 physical cores
    .NET SDK 10.0.100-rc.2.25502.107
      [Host]     : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v4
      DefaultJob : .NET 10.0.0 (10.0.0-rc.2.25502.107, 10.0.25.50307), X64 RyuJIT x86-64-v4
    
    Simple implementation performance:
    
    | Method           | Mean     | Error     | StdDev    | Gen0     | Allocated |
    |----------------- |---------:|----------:|----------:|---------:|----------:|
    | EncodeDecode1000 | 1.064 ms | 0.0039 ms | 0.0037 ms | 269.5313 |   2.15 MB |

    Optimized implementation performance:
    
    | Method           | Mean     | Error   | StdDev  | Gen0    | Allocated |
    |----------------- |---------:|--------:|--------:|--------:|----------:|
    | EncodeDecode1000 | 439.5 us | 2.83 us | 2.36 us | 80.0781 | 656.25 KB |

 */

internal static class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<Benchmarks>();
    }
}
