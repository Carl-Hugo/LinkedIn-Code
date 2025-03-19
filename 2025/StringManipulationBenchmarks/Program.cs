using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Diagnostics.Windows;
using BenchmarkDotNet.Running;
using StringManipulationBenchmarks;

var summary = BenchmarkRunner.Run<IndexOfBenchmarkOrdinalIgnoreCase>(ManualConfig
    .Create(DefaultConfig.Instance)
    //.AddColumn(new SpeedupMeanColumn())
    .AddColumn(StatisticColumn.AllStatistics)
    .AddDiagnoser(MemoryDiagnoser.Default)
    .AddDiagnoser(new InliningDiagnoser())
);

