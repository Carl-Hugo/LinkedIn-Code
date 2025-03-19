using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace StringManipulationBenchmarks;
public class SpeedupMeanColumn : IColumn
{
    public string Id => nameof(SpeedupMeanColumn);
    public string ColumnName => "Speed";

    public bool AlwaysShow => true;
    public bool IsNumeric => true;
    public UnitType UnitType => UnitType.Dimensionless;
    public string Legend => "How many times faster than the baseline";

    public ColumnCategory Category { get; } = ColumnCategory.Statistics;
    public int PriorityInCategory { get; } = 1;

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase)
    {
        var baseline = summary
            .Reports
            .LastOrDefault(r => r.BenchmarkCase.Descriptor.Baseline)?
            .ResultStatistics?.Mean;

        var current = summary[benchmarkCase]?.ResultStatistics?.Mean;

        if (baseline.HasValue && current.HasValue)
        {
            return (baseline.Value / current.Value).ToString("0.00x");
        }

        return "N/A";
    }

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
        => GetValue(summary, benchmarkCase);

    public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
    public bool IsAvailable(Summary summary) => true;
}