var builder = DistributedApplication.CreateBuilder(args);

var remoteDependency = builder.AddProject<Projects.ParallelForEachApp_RemoteDependency>("remotedependency");

var apiService = builder.AddProject<Projects.ParallelForEachApp_ApiService>("apiservice")
    .WithReference(remoteDependency)
    .WaitFor(remoteDependency)
;

builder.AddProject<Projects.ParallelForEachApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService)
;

builder.Build().Run();
