#define V1
#define V2
#define V3
#define V4

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();

#if V4
public interface IFileParser
{
    bool TryParse(IFormFile formFile, out IEnumerable<Flight> flights);
}
public abstract class BaseParser : IFileParser
{
    protected abstract string FileExtension { get; }
    public bool CanParse(string fileExtension)
        => FileExtension == fileExtension;

    public bool TryParse(IFormFile formFile, out IEnumerable<Flight> flights)
    {
        if (!CanParse(Path.GetExtension(formFile.FileName)))
        {
            flights = [];
            return false;
        }
        flights = Parse(formFile);
        return true;
    }

    protected abstract IEnumerable<Flight> Parse(IFormFile formFile);
}

public class JsonParser : BaseParser
{
    protected override string FileExtension => ".json";
    protected override List<Flight> Parse(IFormFile formFile)
    {
        // JSON parser code goes here
        throw new NotImplementedException();
    }
}

public class CsvParser : BaseParser
{
    protected override string FileExtension => ".csv";
    protected override List<Flight> Parse(IFormFile formFile)
    {
        // CSV parser code goes here
        throw new NotImplementedException();
    }
}
#elif V3
public interface IFileParser
{
    List<Flight> Parse(string fileExtension, Stream fileStream);
}

// Template Method + Chain of Responsibility
public abstract class BaseParser(IFileParser? next) : IFileParser
{
    private readonly IFileParser? _next = next;
    protected abstract string FileExtension { get; }
    public bool CanParse(string fileExtension)
        => FileExtension == fileExtension;

    public virtual List<Flight> Parse(string fileExtension, Stream fileStream)
    {
        if (CanParse(fileExtension))
        {
            return Parse(fileStream);
        }
        if (_next is null)
        {
            throw new NoParserFoundException(fileExtension);
        }
        return _next.Parse(fileExtension, fileStream);
    }

    protected abstract List<Flight> Parse(Stream fileStream);
}

public class NoParserFoundException(string? fileExtension) : Exception($"No parser found for {fileExtension}.");

public class JsonParser(IFileParser next) : BaseParser(next)
{
    protected override string FileExtension => ".json";
    protected override List<Flight> Parse(Stream fileStream)
    {
        // JSON parser code goes here
        throw new NotImplementedException();
    }
}

public class CsvParser(IFileParser next) : BaseParser(next)
{
    protected override string FileExtension => ".csv";
    protected override List<Flight> Parse(Stream fileStream)
    {
        // CSV parser code goes here
        throw new NotImplementedException();
    }
}
#elif V2
// Template Method
public interface IFileParser
{
    List<Flight> Parse(string fileExtension, Stream fileStream);
    bool CanParse(string fileExtension);
}

public abstract class BaseParser : IFileParser
{
    protected abstract string FileExtension { get; }
    public virtual bool CanParse(string fileExtension)
        => FileExtension == fileExtension;

    public virtual List<Flight> Parse(string fileExtension, Stream fileStream)
    {
        if (!CanParse(fileExtension))
        {
            throw new CanNotParseException(fileExtension: fileExtension);
        }
        return Parse(fileStream);
    }

    protected abstract List<Flight> Parse(Stream fileStream);
}

public class JsonParser : BaseParser
{
    protected override string FileExtension => ".json";
    protected override List<Flight> Parse(Stream fileStream)
    {
        // JSON parser code goes here
        throw new NotImplementedException();
    }
}

public class CsvParser : BaseParser
{
    protected override string FileExtension => ".csv";
    protected override List<Flight> Parse(Stream fileStream)
    {
        // CSV parser code goes here
        throw new NotImplementedException();
    }
}

public class CanNotParseException(string? fileExtension) : Exception($"Can't parse {fileExtension}.");

#elif V1
// Template Method
public interface IFileParser
{
    bool TryParse(string fileExtension, Stream fileStream, out IEnumerable<Flight> flights);
}


public abstract class BaseParser : IFileParser
{
    protected abstract string FileExtension { get; }
    protected virtual bool CanParse(string fileExtension)
        => FileExtension == fileExtension;

    public bool TryParse(string fileExtension, Stream fileStream, out IEnumerable<Flight> flights)
    {
        if (CanParse(fileExtension))
        {
            flights = Parse(fileStream);
            return true;
        }
        flights = [];
        return false;
    }

    protected abstract List<Flight> Parse(Stream fileStream);
}

public class JsonParser : BaseParser
{
    protected override string FileExtension => ".json";
    protected override List<Flight> Parse(Stream fileStream)
    {
        // JSON parser code goes here
        throw new NotImplementedException();
    }
}

public class CsvParser : BaseParser
{
    protected override string FileExtension => ".csv";
    protected override List<Flight> Parse(Stream fileStream)
    {
        // CSV parser code goes here
        throw new NotImplementedException();
    }
}
#endif

public record class Flight();