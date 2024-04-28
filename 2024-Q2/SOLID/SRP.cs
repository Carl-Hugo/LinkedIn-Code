#define USERMANAGER1

#if USERMANAGER1
public class UserManager
{
    public void RegisterUser(string userName) { /* Register user logic */ }
    public void SendEmail(string message) { /* Email sending logic */ }
}
#else
public class UserManager
{
    public void Register(string userName) { /* Register user logic */ }
}
public class EmailService
{
    public void Send(string message) { /* Email sending logic */ }
}
#endif