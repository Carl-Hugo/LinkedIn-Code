namespace Utilities;

file class GreeterHelper
{
    public static string FormatGreeting(string name) => $"Hello, {name}!";
}

public class Greeter
{
    public void Greet(string name)
    {
        var message = GreeterHelper.FormatGreeting(name);
        Console.WriteLine(message);
    }
}