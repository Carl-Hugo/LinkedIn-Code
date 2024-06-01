using Global_Using_Directives.MyNamespace;

namespace Global_Using_Directives;

public class Service2
{
    public void Operation()
    {
        var myClass = new MyClass();
        Console.WriteLine("Service2");
        myClass.Operation();
    }
}
