// Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IService, Service>();
var app = builder.Build();

app.MapGet("/", (IService service) =>
{
    return service.DoWork();
});

app.Run();

public interface IService
{
    string DoWork();
}

public class Service : IService
{
    public string DoWork() => "DI: Work Done!";
}
