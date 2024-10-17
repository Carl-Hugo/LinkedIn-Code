using System.Runtime.CompilerServices;
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
        WriteProperty(stringBuilder, FirstName);
        WriteProperty(stringBuilder, LastName);

        var fullName = FirstName + " " + LastName;
        WriteProperty(stringBuilder, fullName);

        return true;
    }

    protected static void WriteProperty(StringBuilder stringBuilder, string value, [CallerArgumentExpression(nameof(value))] string? attributeName = null)
    {
        stringBuilder.Append(", \"");
        stringBuilder.Append(attributeName);
        stringBuilder.Append("\": \"");
        stringBuilder.Append(value);
        stringBuilder.Append('"');
    }
    // See RecordTypesCanSealToString (2024-Q3) for the original version that do not use CallerArgumentExpression
}

public record class Student(string FirstName, string LastName, string School) : Person(FirstName, LastName)
{
    protected override bool PrintMembers(StringBuilder stringBuilder)
    {
        base.PrintMembers(stringBuilder);
        WriteProperty(stringBuilder, School);
        return true;
    }
}



// public record class Person(string FirstName, string LastName)
// {
//     public sealed override string ToString()
//     {
//         var sb = new StringBuilder("{ \"@type\": \"");
//         sb.Append(GetType().Name);
//         sb.Append('"');
//         PrintMembers(sb);
//         sb.Append(" }");
//         return sb.ToString();
//     }

//     protected virtual bool PrintMembers(StringBuilder stringBuilder)
//     {
//         WriteProperty(stringBuilder, FirstName);
//         WriteProperty(stringBuilder, LastName);

//         var fullName = FirstName + " " + LastName;
//         WriteProperty(stringBuilder, fullName);

//         return true;
//     }

//     // See RecordTypesCanSealToString (2024-Q3) for the original version that do not use CallerArgumentExpression
//     protected static void WriteProperty(StringBuilder stringBuilder, string value, [CallerArgumentExpression(nameof(value))] string? attributeName = null)
//     {
//         stringBuilder.Append(", \"");
//         stringBuilder.Append(toCamelCase(attributeName));
//         stringBuilder.Append("\": \"");
//         stringBuilder.Append(value);
//         stringBuilder.Append('"');

//         string toCamelCase(string? name)
//         {
//             if (name is null) { return string.Empty; }

//             var end = name.Substring(1);
//             var firstLetter = name.Substring(0, 1);
//             return firstLetter.ToLowerInvariant() + end;
//         }
//     }
// }
