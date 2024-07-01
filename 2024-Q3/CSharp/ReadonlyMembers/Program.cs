// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

public class ResetPasswordHandler(IEmailService emailService, ITokenService tokenService)
{
    private readonly IEmailService _emailService = emailService;
    private readonly ITokenService _tokenService = tokenService;

    public async Task<ResetPasswordResult> HandleAsync(ResetPasswordCommand command,
        CancellationToken cancellationToken)
    {
        // ...
        var resetToken = _tokenService.GenerateResetTokenFor(command.UserEmail);
        // ...
        await _emailService.SendAsync(
            recipient: command.UserEmail,
            subject: "Password Reset Request",
            body: $"Use the following token to reset your password: {resetToken}",
            cancellationToken
        );
        // ...
        return ResetPasswordResult.Success();
    }
}

public interface IEmailService
{
    Task SendAsync(string recipient, string subject, string body, CancellationToken cancellationToken);
}

public interface ITokenService
{
    string GenerateResetTokenFor(string email);
}

public record class ResetPasswordCommand(string UserEmail);
public record class ResetPasswordResult()
{
    public static ResetPasswordResult Success() => new();
}