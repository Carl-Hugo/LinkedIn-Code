using GlobalHttpHandler.Features.Dogs;
using GlobalHttpHandler.Features.GlobalHttpMessageHandler;
using GlobalHttpHandler.Features.Users;

var builder = WebApplication.CreateBuilder(args);
builder
    .AddDogsServices()
    .AddUsersServices()
    .AddGlobalHttpMessageHandling()
;
builder
    .Build()
    .MapDogsEndpoints()
    .MapUsersEndpoints()
    .Run()
;