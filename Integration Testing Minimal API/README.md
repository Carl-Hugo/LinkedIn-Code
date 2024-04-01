# Integration Testing Minimal API

Getting started:

```bash
# Create a new solution
dotnet new sln

# Create a new ASP.NET Core project to test
dotnet new web -n MyApi

# Create a xUnit project
dotnet new xunit -n MyApi.IntegrationTests

# Add both projects to the solution
dotnet sln add MyApi/MyApi.csproj
dotnet sln add MyApi.IntegrationTests/MyApi.IntegrationTests.csproj

# Reference MyApi from MyApi.IntegrationTests
dotnet add MyApi.IntegrationTests/MyApi.IntegrationTests.csproj reference MyApi/MyApi.csproj

# The following package contains the WebApplicationFactory class
dotnet add MyApi.IntegrationTests/MyApi.IntegrationTests.csproj package Microsoft.AspNetCore.Mvc.Testing

# Execute the tests
dotnet test
```
