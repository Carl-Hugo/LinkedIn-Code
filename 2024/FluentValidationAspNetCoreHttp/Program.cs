using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Ensure the dictionaries are also serialized to CamelCase (a.k.a. the "errors")
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});

// Register the validator
builder.Services.AddScoped<IValidator<JobApplication>, JobApplicationValidator>();

// Add FluentValidation.AspNetCore.Http (endpoint filter)
builder.AddFluentValidationEndpointFilter();

var app = builder.Build();

// Define a endpoint to validate
app.MapPost("/submit-application", (JobApplication application) =>
{
    // Process the valid job application
    return Results.Ok("Application submitted successfully.");
})
.AddFluentValidationFilter(); // Apply the validation filter

app.Run();

public record class JobApplication(string ApplicantName, string Email, string Position, int YearsOfExperience);

public class JobApplicationValidator : AbstractValidator<JobApplication>
{
    public JobApplicationValidator()
    {
        RuleFor(x => x.ApplicantName)
            .NotEmpty().WithMessage("Applicant name is required.")
            .MaximumLength(100).WithMessage("Applicant name must be 100 characters or fewer.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");

        RuleFor(x => x.Position)
            .NotEmpty().WithMessage("Position applied for is required.")
            .MaximumLength(50).WithMessage("Position must be 50 characters or fewer.");

        RuleFor(x => x.YearsOfExperience)
            .InclusiveBetween(0, 40).WithMessage("Years of experience must be between 0 and 40.");
    }
}
