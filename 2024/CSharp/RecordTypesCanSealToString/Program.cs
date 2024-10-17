using System.Text;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
var root = app.MapGroup("/")
    .AddEndpointFilter((ctx, d) =>
    {
        ctx.HttpContext.Response.ContentType = "application/json";
        return d.Invoke(ctx);
    })
    .AddEndpointFilter(async (ctx, d) =>
    {
        var result = await d.Invoke(ctx);
        var loggerFactory = ctx.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("RecordTypesCanSealToString");
        logger.LogInformation("{0}", result);
        return result;
    })
;
root.MapGet("/person", () => new Person("John", "Doe").ToString());
root.MapGet("/student", () => new Student("Jane", "Doe", "AroundTheCorner High").ToString());

app.Run();

public record class Person(string FirstName, string LastName)
{
    public sealed override string ToString()
    {
        var sb = new StringBuilder("{ \"@type\": \"");
        sb.Append(GetType().Name);
        sb.Append('"');
        PrintMembers(sb);
        sb.Append(" }");
        return sb.ToString();
    }

    protected virtual bool PrintMembers(StringBuilder stringBuilder)
    {
        WriteProperty(stringBuilder, "firstName", FirstName);
        WriteProperty(stringBuilder, "lastName", LastName);
        return true;
    }

    protected static void WriteProperty(StringBuilder stringBuilder, string attributeName, string value)
    {
        stringBuilder.Append(", \"");
        stringBuilder.Append(attributeName);
        stringBuilder.Append("\": \"");
        stringBuilder.Append(value);
        stringBuilder.Append('"');
    }
}

public record class Student(string FirstName, string LastName, string School) : Person(FirstName, LastName)
{
    protected override bool PrintMembers(StringBuilder stringBuilder)
    {
        base.PrintMembers(stringBuilder);
        WriteProperty(stringBuilder, "school", School);
        return true;
    }
}