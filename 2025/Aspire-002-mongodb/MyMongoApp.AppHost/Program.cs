var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.MyMongoApp_ApiService>("apiservice");

builder.AddProject<Projects.MyMongoApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
