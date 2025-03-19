using BenchmarkDotNet.Attributes;

public class SubstringBenchmark
{
    public string Input { get; } = new string('a', 1000); // Simulated large string

    [Benchmark(Baseline = true)]
    public int ClassicSubstring()
    {
        int sum = 0;
        for (int i = 0; i < Input.Length - 10; i++)
        {
            string sub = Input.Substring(i, 10); // Allocates new strings
            sum += sub.Length;
        }
        return sum;
    }

    [Benchmark]
    public int SpanProcessing()
    {
        ReadOnlySpan<char> span = Input.AsSpan();
        int sum = 0;
        for (int i = 0; i < span.Length - 10; i++)
        {
            ReadOnlySpan<char> sub = span.Slice(i, 10); // No heap allocations
            sum += sub.Length;
        }
        return sum;
    }
}

[MemoryDiagnoser]
public class SubstringBenchmark2
{
    public string Input { get; } = new string('a', 1000);

    [Benchmark(Baseline = true)]
    public string ClassicSubstring() => Input.Substring(0, 10); // Allocates new string

    [Benchmark]
    public ReadOnlySpan<char> SpanProcessing() => Input.AsSpan().Slice(0, 10); // No allocations
}
