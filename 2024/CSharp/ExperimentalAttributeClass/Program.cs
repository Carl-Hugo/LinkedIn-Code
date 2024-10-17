using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/", static () =>
{
    var service = new ExperimentalService();
    return Results.Ok(service.GetData());
});

app.Run();


[Experimental("MyExperimentalFeature")]
public class ExperimentalService
{
    public string GetData()
        => "Experimental Data";
}