#define USECASE1
#define USECASE2
public interface INotificationService
{
    void Send(string message);
}

public class EmailNotificationService : INotificationService
{
    public void Send(string message) { /* Send email logic*/ }
}

public class SMSNotificationService : INotificationService
{
    public void Send(string message) { /* Send SMS logic */ }
}

#if USECASE1
public class UseCase
{
    // Leverage the Strategy pattern and Dependency Injection to enable the OCP
    private INotificationService _notificationService;
    public UseCase(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public void Execute()
    {
        // Other business logic...
        _notificationService.Send("message");
        // Other business logic...
    }
}
#elif USECASE2
public class UseCase
{
    public void Execute()
    {
        // Create a direct dependency on a specific notification service
        INotificationService notificationService = new EmailNotificationService();

        // Other business logic...
        notificationService.Send("message");
        // Other business logic...
    }
}
#else
public class UseCase
{
    public void Execute()
    {
        // Other business logic...
        Send("message");
        // Other business logic...
    }

    private void Send(string message) { /* Send notification */ }
}
#endif