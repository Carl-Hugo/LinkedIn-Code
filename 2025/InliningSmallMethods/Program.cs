using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<InliningBenchmark>(ManualConfig
    .Create(DefaultConfig.Instance)
    .AddColumn(StatisticColumn.AllStatistics)
    .AddDiagnoser(MemoryDiagnoser.Default)
    .AddDiagnoser(new InliningDiagnoser())
);

public class InliningBenchmark
{
    [Params(10)]
    public int A { get; set; }

    [Params(20)]
    public int B { get; set; }


    [Benchmark]
    public int NoInliningMethod() => Add(A, B);

    [Benchmark]
    public int AggressiveInliningMethod() => AggressiveAdd(A, B);

    [Benchmark(Baseline = true)]
    public int DirectInlineLogic() => A + B;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private int Add(int x, int y) => x + y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int AggressiveAdd(int x, int y) => x + y;
}