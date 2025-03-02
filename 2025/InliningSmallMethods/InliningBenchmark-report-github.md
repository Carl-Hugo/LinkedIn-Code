```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4391/23H2/2023Update/SunValley3)
AMD Ryzen 9 3900X, 1 CPU, 24 logical and 12 physical cores
.NET SDK 9.0.200-preview.0.24575.35
  [Host]     : .NET 9.0.2 (9.0.225.6610), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.2 (9.0.225.6610), X64 RyuJIT AVX2


```

| Method                   | A   | B   |      Mean |     Error |    StdDev |    StdErr |    Median |       Min |        Q1 |        Q3 |       Max |              Op/s |
| ------------------------ | --- | --- | --------: | --------: | --------: | --------: | --------: | --------: | --------: | --------: | --------: | ----------------: |
| NoInliningMethod         | 10  | 20  | 0.2581 ns | 0.0381 ns | 0.0423 ns | 0.0097 ns | 0.2558 ns | 0.1878 ns | 0.2305 ns | 0.2786 ns | 0.3342 ns |   3,874,281,645.3 |
| AggressiveInliningMethod | 10  | 20  | 0.0381 ns | 0.0204 ns | 0.0191 ns | 0.0049 ns | 0.0382 ns | 0.0000 ns | 0.0304 ns | 0.0517 ns | 0.0693 ns |  26,227,761,473.5 |
| DirectInlineLogic        | 10  | 20  | 0.0094 ns | 0.0185 ns | 0.0174 ns | 0.0045 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0118 ns | 0.0680 ns | 106,757,975,366.2 |


// * Legends *
  A           : Value of the 'A' parameter
  B           : Value of the 'B' parameter
  Mean        : Arithmetic mean of all measurements
  Error       : Half of 99.9% confidence interval
  StdDev      : Standard deviation of all measurements
  StdErr      : Standard error of all measurements
  Median      : Value separating the higher half of all measurements (50th percentile)
  Min         : Minimum
  Q1          : Quartile 1 (25th percentile)
([Current]/[Baseline])
  1 ns        : 1 Nanosecond (0.000000001 sec)