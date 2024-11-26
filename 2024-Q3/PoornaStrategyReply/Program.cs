#define V1
#undef V2
#undef V3
#undef V4

using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddExample();

var app = builder.Build();
app.MapGet("/", Ok<List<Flight>> (string fileExtension, IFileService fileService) => TypedResults.Ok(fileService.Import(fileExtension, Stream.Null)));
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

public static class StartupExtensions
{
    public static IServiceCollection AddExample(this IServiceCollection services)
    {
        return services;
    }
}
#elif V3
public interface IFileParser
{
    List<Flight> Parse(string fileExtension, Stream fileStream);
}

// Template Method + Chain of Responsibility
public abstract class BaseParser(IFileParser? next = default) : IFileParser
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

public class JsonParser(IFileParser? next = default) : BaseParser(next)
{
    protected override string FileExtension => ".json";
    protected override List<Flight> Parse(Stream fileStream)
    {
        // JSON parser code goes here
        //throw new NotImplementedException();
        Console.WriteLine("JsonParser");
        return [];
    }
}

public class CsvParser(IFileParser? next = default) : BaseParser(next)
{
    protected override string FileExtension => ".csv";
    protected override List<Flight> Parse(Stream fileStream)
    {
        // CSV parser code goes here
        //throw new NotImplementedException();
        Console.WriteLine("CsvParser");
        return [];
    }
}

public class FileService(IFileParser fileParser) : IFileService
{
    private readonly IFileParser _fileParser = fileParser;
    public List<Flight> Import(string fileExtension, Stream fileStream)
    {
        return _fileParser.Parse(fileExtension, fileStream);
    }
}

public static class StartupExtensions
{
    public static IServiceCollection AddExample(this IServiceCollection services)
    {
        services
            .AddSingleton<IFileParser>(new JsonParser(new CsvParser()))
            .AddSingleton<IFileService, FileService>()
        ;
        return services;
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
        //throw new NotImplementedException();
        Console.WriteLine("JsonParser");
        return [];
    }
}

public class CsvParser : BaseParser
{
    protected override string FileExtension => ".csv";
    protected override List<Flight> Parse(Stream fileStream)
    {
        // CSV parser code goes here
        //throw new NotImplementedException();
        Console.WriteLine("CsvParser");
        return [];
    }
}

public class FileService(IEnumerable<IFileParser> fileParsers) : IFileService
{
    private readonly IEnumerable<IFileParser> _fileParsers = fileParsers;

    public List<Flight> Import(string fileExtension, Stream fileStream)
    {
        foreach (var parser in _fileParsers)
        {
            if (parser.CanParse(fileExtension))
            {
                return parser.Parse(fileExtension, fileStream);
            }
        }
        throw new CanNotParseException(fileExtension);
    }
}

public static class StartupExtensions
{
    public static IServiceCollection AddExample(this IServiceCollection services)
    {
        services
            .AddSingleton<IFileParser, JsonParser>()
            .AddSingleton<IFileParser, CsvParser>()
            .AddSingleton<IFileService, FileService>()
        ;
        return services;
    }
}

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
        //throw new NotImplementedException();
        Console.WriteLine("JsonParser");
        return [];
    }
}

public class CsvParser : BaseParser
{
    protected override string FileExtension => ".csv";
    protected override List<Flight> Parse(Stream fileStream)
    {
        // CSV parser code goes here
        //throw new NotImplementedException();
        Console.WriteLine("CsvParser");
        return [];
    }
}

public class FileService(IEnumerable<IFileParser> fileParsers) : IFileService
{
    private readonly IEnumerable<IFileParser> _fileParsers = fileParsers;

    public List<Flight> Import(string fileExtension, Stream fileStream)
    {
        foreach (var parser in _fileParsers)
        {
            if (parser.TryParse(fileExtension, fileStream, out var flights))
            {
                return new(flights);
            }
        }
        throw new CanNotParseException(fileExtension);
    }
}

public static class StartupExtensions
{
    public static IServiceCollection AddExample(this IServiceCollection services)
    {
        services
            .AddSingleton<IFileParser, JsonParser>()
            .AddSingleton<IFileParser, CsvParser>()
            .AddSingleton<IFileService, FileService>()
        ;
        return services;
    }
}
#endif

public interface IFileService
{
    List<Flight> Import(string fileExtension, Stream fileStream);
}

public record class Flight();
public class CanNotParseException(string? fileExtension) : Exception($"Can't parse {fileExtension}.");
