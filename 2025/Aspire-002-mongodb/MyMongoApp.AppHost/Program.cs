var builder = DistributedApplication.CreateBuilder(args);

// var mongo = builder
//     .AddMongoDB("mongo")
//     .WithDataBindMount("C:\\MongoDB\\Data")
//     // Add Mongo Express
//     .WithMongoExpress()
// ;

var mongo = builder
    .AddMongoDB("mongo")
    .WithDataBindMount("C:\\MongoDB\\Data")
;
var openFoodFactsDatabase = mongo.AddDatabase("openfoodfacts");
// Use this name in the API


var apiService = builder
    .AddProject<Projects.MyMongoApp_ApiService>("apiservice")
    .WithReference(openFoodFactsDatabase)
    .WaitFor(openFoodFactsDatabase)
;

builder.AddProject<Projects.MyMongoApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
