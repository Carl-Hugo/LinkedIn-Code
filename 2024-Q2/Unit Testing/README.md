# Integration Testing Minimal API

Getting started:

```bash
# Create a new solution
dotnet new sln -n MySolution

# Create a new ASP.NET Core project to test
dotnet new web -n MyProject

# Create a xUnit project
dotnet new xunit -n MyProject.Tests

# Add both projects to the solution
dotnet sln add MyProject/MyProject.csproj
dotnet sln add MyProject.Tests/MyProject.Tests.csproj

# Reference MyProject from MyProject.Tests
dotnet add MyProject.Tests/MyProject.Tests.csproj reference MyProject/MyProject.csproj

# Execute the tests
dotnet test
```
