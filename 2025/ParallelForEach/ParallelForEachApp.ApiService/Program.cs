#define SMALL_LIST
using Refit;
using System.Collections.Concurrent;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services
    .AddRefitClient<IRemoteDependency>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://remotedependency"))
;

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
#if SMALL_LIST
string[] keys = [
    "Growth.Mindset",
    "Consistency.Beats.Motivation",
    "Value.Over.Hype",
    "Build.Ship.Learn.Repeat",
    "Network.Impact.Scale",
    "Authenticity.Wins"
];
#else
string[] keys = [
    "Growth.Mindset",
    "Consistency.Beats.Motivation",
    "Value.Over.Hype",
    "Build.Ship.Learn.Repeat",
    "Network.Impact.Scale",
    "Authenticity.Wins",
    "Execute.Learn.Improve",
    "Small.Steps.Big.Results",
    "Iterate.Adapt.Thrive",
    "Resilience.Beats.Talent",
    "Speed.Over.Perfection",
    "Bias.Towards.Action",
    "Solve.Real.Problems",
    "Systems.Over.Goals",
    "Discipline.Creates.Freedom",
    "Failure.Is.Feedback",
    "Energy.Drives.Execution",
    "Hard.Choices.Easy.Life",
    "Clarity.Beats.Complexity",
    "Optimize.For.Learning",
    "Time.In.Market",
    "Work.Smart.Consistently",
    "Momentum.Beats.Potential",
    "Effort.Compounds.Over.Time",
    "Accountability.Drives.Growth",
    "Leverage.Creates.Impact",
    "Value.Delivery.Wins",
    "Input.Controls.Output",
    "Work.With.Focus",
    "Feedback.Loops.Matter",
    "Execution.Creates.Opportunities",
    "Action.Beats.Intention",
    "Decisions.Compound.Results",
    "Simplicity.Scales.Better",
    "Mission.Over.Ego",
    "Strong.Foundations.Last",
    "Reps.Build.Mastery",
    "Ship.Early.Improve.Often",
    "Learn.Apply.Repeat",
    "Clear.Goals.Drive.Execution",
    "Iterate.Or.Stagnate",
    "Ownership.Drives.Results",
    "Strategy.Guides.Tactics",
    "Optimize.For.Speed",
    "Velocity.Matters",
    "Solve.High.Leverage.Problems",
    "Adaptability.Wins",
    "Innovation.Requires.Action",
    "Consistency.Over.Intensity",
    "Growth.Requires.Discomfort",
    "Great.Teams.Execute",
    "Smart.Risk.Taking",
    "Solve.For.X",
    "Quality.Compounds.Over.Time",
    "Stay.Relentless",
    "Deliver.Value.Daily",
    "Clarity.Drives.Efficiency",
    "Your.Future.Is.Created.Today",
    "Persistence.Breaks.Barriers",
    "Optimize.Your.Environment",
    "Master.The.Basics",
    "Execution.Drives.Growth",
    "Be.Relentlessly.Resourceful",
    "Play.Long.Term.Games",
    "Success.Leaves.Clues",
    "Make.It.Happen",
    "10X.Your.Thinking",
    "Courage.Over.Comfort",
    "Decisive.Action.Wins",
    "Embrace.Constraints",
    "Build.Strong.Habits",
    "Learn.From.Failure",
    "Focus.Creates.Flow",
    "Stay.In.The.Game",
    "Take.Calculated.Risks",
    "The.Process.Is.The.Product",
    "Iterate.To.Excellence",
    "Master.The.Grind",
    "Keep.Moving.Forward",
    "No.Excuses.Just.Results",
    "Create.Real.Impact",
    "Compounding.Momentum",
    "Dare.To.Be.Great",
    "Execution.Beats.Ideation",
    "Progress.Not.Perfection",
    "Stay.Hungry.Stay.Humble",
    "Growth.Requires.Commitment",
    "Seek.Discomfort.Embrace.Growth",
    "Keep.Breaking.Limits",
    "Small.Gains.Big.Wins",
    "Track.Progress.Iterate",
    "Push.Limits.Daily",
    "Extreme.Ownership.Wins",
    "Make.Things.Happen",
    "Solve.First.Scale.Later",
    "Find.The.Edge",
    "Consistency.Wins.Every.Time",
    "Do.The.Hard.Things",
    "Win.The.Day",
    "Take.Action.Now",
    "Small.Steps.Lead.To.Greatness",
    "Drive.Relentless.Progress",
    "Optimize.Relentlessly",
    "Do.Deep.Work"
];
#endif

app.MapGet("/sequential", async (IRemoteDependency remoteDependency) =>
{
    var results = new ConcurrentBag<RemoteDependencyDto>();
    var stopWatch = Stopwatch.StartNew();
    foreach (var key in keys.Take(100))
    {
        var result = await remoteDependency.Get(key);
        results.Add(result);
    }
    stopWatch.Stop();
    var childElapsedSum = TimeSpan.FromMicroseconds(results.Sum(x => x.Elapsed.TotalMicroseconds));
    return Results.Ok(new
    {
        stopWatch.Elapsed,
        childElapsedSum,
        results
    });
})
.WithName("Fetch RemoteDependency Sequentially");

app.MapGet("/parallel", async (IRemoteDependency remoteDependency) =>
{
    var results = new ConcurrentBag<RemoteDependencyDto>();
    var stopWatch = Stopwatch.StartNew();
    await Parallel.ForEachAsync(keys.Take(100), async (key, ct) =>
    {
        var result = await remoteDependency.Get(key);
        results.Add(result);
    });
    stopWatch.Stop();
    var childElapsedSum = TimeSpan.FromMicroseconds(results.Sum(x => x.Elapsed.TotalMicroseconds));
    return Results.Ok(new
    {
        stopWatch.Elapsed,
        childElapsedSum,
        results
    });
})
.WithName("Fetch RemoteDependency in Parallel");


app.MapDefaultEndpoints();

app.Run();

public record class RemoteDependencyDto(string Key, int Delay, TimeSpan Elapsed);

public interface IRemoteDependency
{
    [Get("/{key}")]
    Task<RemoteDependencyDto> Get(string key);
}