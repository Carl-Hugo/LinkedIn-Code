using BenchmarkDotNet.Attributes;

public class IndexOfBenchmark
{
    [Params(
        TextConstants.x5Words,
        TextConstants.x50Words,
        TextConstants.x500Words,
        TextConstants.x5000Words
    )]
    public string Input { get; set; } = "";

    [Benchmark]
    public int StringIndexOf()
        => Input.IndexOf("pretium");

    [Benchmark]
    public int SpanIndexOf()
        => Input.AsSpan().IndexOf("pretium".AsSpan());

    [Benchmark]
    public int StringLastIndexOf()
        => Input.LastIndexOf("pretium");

    [Benchmark]
    public int SpanLastIndexOf()
        => Input.AsSpan().LastIndexOf("pretium".AsSpan());
}

public class IndexOfBenchmarkOrdinalIgnoreCase
{
    [Params(
        TextConstants.x5Words,
        TextConstants.x50Words,
        TextConstants.x500Words,
        TextConstants.x5000Words
    )]
    public string Input { get; set; } = "";

    [Benchmark]
    public int StringIndexOf()
        => Input.IndexOf("PrEtIuM", StringComparison.OrdinalIgnoreCase);

    [Benchmark]
    public int SpanIndexOf()
        => Input.AsSpan().IndexOf("PrEtIuM".AsSpan(), StringComparison.OrdinalIgnoreCase);

    [Benchmark]
    public int StringLastIndexOf()
        => Input.LastIndexOf("PrEtIuM", StringComparison.OrdinalIgnoreCase);

    [Benchmark]
    public int SpanLastIndexOf()
        => Input.AsSpan().LastIndexOf("PrEtIuM".AsSpan(), StringComparison.OrdinalIgnoreCase);
}
