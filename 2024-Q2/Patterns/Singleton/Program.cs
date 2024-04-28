// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

public class MySingleton
{
    private static MySingleton? _instance;
    private MySingleton() { }
    public static MySingleton Create()
    {
        if (_instance == null)
        {
            _instance = new MySingleton();
        }
        return _instance;
    }
}

public class MySingletonWithLock
{
    private static MySingletonWithLock? _instance;
    private static readonly object _myLock = new();
    private MySingletonWithLock() { }
    public static MySingletonWithLock Create()
    {
        lock (_myLock)
        {
            if (_instance == null)
            {
                _instance = new MySingletonWithLock();
            }
        }
        return _instance;
    }
}

public class MySimpleSingleton
{
    public static MySimpleSingleton Instance { get; } = new MySimpleSingleton();
    private MySimpleSingleton() { }
}
