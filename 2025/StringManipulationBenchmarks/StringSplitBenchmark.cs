using BenchmarkDotNet.Attributes;
using static System.MemoryExtensions;

public class StringSplitBenchmark
{
    [Params(
        TextConstants.x5Words,
        TextConstants.x50Words,
        TextConstants.x500Words,
        TextConstants.x5000Words
    )]
    public string Input { get; set; } = "";

    [Benchmark(Baseline = true)]
    public bool ClassicSplit()
    {
        var result = Input.Split(' '); // Allocates new strings for each split
        var pearFound = false;
        foreach (var item in result)
        {
            if (item == "condimentum")
            {
                pearFound = true;
            }
        }
        return pearFound;
    }

    [Benchmark]
    public bool SpanSplit()
    {
        ReadOnlySpan<char> inputSpan = Input.AsSpan();
        SpanSplitEnumerator<char> enumerator = inputSpan.Split(' ');
        var pearFound = false;
        foreach (Range range in enumerator)
        {
            ReadOnlySpan<char> item = inputSpan[range];
            if (item.SequenceEqual("condimentum"))
            {
                pearFound = true;
            }
        }
        return pearFound;
    }
}

public class StringSplitBenchmark2
{
    [Params(
        TextConstants.x5Words,
        TextConstants.x50Words,
        TextConstants.x500Words,
        TextConstants.x5000Words
    )]
    public string Input { get; set; } = "";

    [Benchmark(Baseline = true)]
    public string[] ClassicSplit()
        => Input.Split(' ');

    [Benchmark]
    public SpanSplitEnumerator<char> SpanSplit()
        => Input.AsSpan().Split(' ');
}




public class StringSplitBenchmarkCodeSnap
{
    [Params(
        // 5 words
        "Lorem ipsum dolor sit amet",
        // 50 words
        "Lorem ipsum dolor sit amet, ...",
        // 500 words
        "Lorem ipsum dolor sit amet, ...",
        // 5000 words
        "Lorem ipsum dolor sit amet, ..."
    )]
    public string Input { get; set; } = "";

    [Benchmark(Baseline = true)]
    public string[] ClassicSplit()
        => Input.Split(' ');

    [Benchmark]
    public SpanSplitEnumerator<char> SpanSplit()
        => Input.AsSpan().Split(' ');
}


public class StringSplitBenchmarkCodeSnap2
{
    [Params(
        // 5 words
        "Lorem ipsum dolor sit amet",
        // 50 words
        "Lorem ipsum dolor sit amet, ...",
        // 500 words
        @"Lorem ipsum dolor sit amet, ...",
        // 5000 words
        @"Lorem ipsum dolor sit amet, ..."
    )]
    public string Input { get; set; } = "";

    [Benchmark(Baseline = true)]
    public bool ClassicSplit()
    {
        var result = Input.Split(' '); // Allocates new strings for each split
        var condimentumFound = false;
        foreach (var item in result)
        {
            if (item == "condimentum")
            {
                condimentumFound = true;
            }
        }
        return condimentumFound;
    }

    [Benchmark]
    public bool SpanSplit()
    {
        ReadOnlySpan<char> inputSpan = Input.AsSpan();
        SpanSplitEnumerator<char> enumerator = inputSpan.Split(' ');
        var condimentumFound = false;
        foreach (Range range in enumerator)
        {
            ReadOnlySpan<char> item = inputSpan[range];
            if (item.SequenceEqual("condimentum"))
            {
                condimentumFound = true;
            }
        }
        return condimentumFound;
    }
}
