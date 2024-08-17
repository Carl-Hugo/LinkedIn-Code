var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var IncrementBy = (int source, int increment = 1) => source + increment;
app.MapGet("/increment/{source}", (int source, int? increment) => IncrementBy(source, increment ?? 1));

app.Run();